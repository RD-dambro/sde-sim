using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyControls;
using Statistics2020Library;
using Calculator;

namespace SDE_Sim
{
    public partial class SDESim : Form
    {
        public SDESim()
        {
            InitializeComponent(800, 600);

            InitializeGraphics();

            InitializeCombo();

            InitializeListView(Statistics);
        }
        
        
        //---------------------------------------------------------------------------------------------------//
        private Rectangle Viewport;
        private Bitmap b;
        private Graphics g;
        private Font SmallFont = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Pixel);
        private string[] sims = new string[9]
        { 
            "Law Of Large Numbers", 
            "Random Walk", 
            "Poisson Process", 
            "Brownian Diffusion", 
            "Geometric Brownian Diffusion", 
            "Vasicek Process", 
            //"Cox, Ingersoll, Ross", 
            "Poisson Compound Process",
            "Basic Affine Jump Diffusion",
            "Merton Jump Diffusion"
        };

        private Random rand = new Random();
        //private double[][] raw_values;
        private List<AlphanumericDataset> raw_paths;
        private GenericDataset myds;
        int n = 1;
        int m = 1;
        int check = 1;
        double distN = 1;
        double p = 0;
        double eps = 0;
        double lambda = 1;
        double sigma = 1;
        double A = 1;
        double B = 1;
        double L = 1;
        private static readonly ICalculator<double> Calc = Calculators.GetInstance<double>();
        double min;
        double max;
        double range;
        double mean;
        double variance;

        bool available = true;
        bool drawing = false;

        List<string> Statistics = new List<string>();

        List<Pen> MyPen;    
        List<int> prev_X;
        List<int> prev_Y;

        private string[] LVColumns = new string[2]{"Statistics", "Value"};
        private string[] LVStatistics = new string[5]{"Minimum", "Maximum", "Range", "Expected", "Variance"};
        //---------------------------------------------------------------------------------------------------//
        //
        //
        //
        private void enableSharedControls()
        {
            NumericStart.Enabled = true;
            NumericM.Enabled = true;
            NumericN.Enabled = true;
            NumericCheck.Enabled = true;
            NumericDistN.Enabled = true;
        }

        //---------------------------------------------------------------------------------------------------//
        //
        //
        //
        private void disableAllControls()
        {
            NumericStart.Enabled = false;
            NumericM.Enabled = false;
            NumericN.Enabled = false;
            NumericCheck.Enabled = false;
            NumericDistN.Enabled = false;
            NumericP.Enabled = false;
            // NumericEpsilon.Enabled = false;
            NumericLambda.Enabled = false;
            NumericSigma.Enabled = false;
            NumericA.Enabled = false;
            NumericB.Enabled = false;
            NumericL.Enabled = false;
        }

        private Rectangle distToRect(KeyValuePair<Interval, FrequenciesForValues> kvp, int X)
        {
                double scl = Viewport.Height/range;
                double height = scl * (kvp.Key.getMax() - kvp.Key.getMin());
                double width = ((Viewport.Width) * kvp.Value.RelativeFrequency);
                if(width < 2) width = 2;

                double Y = Calc.Viewport(kvp.Key.getMax(), min, range,  Viewport.Bottom, -Viewport.Height);
                
                if(X > (Viewport.Left + Viewport.Width*0.35))
                    X -= (int) width;
                if(Y + height > Viewport.Bottom) height = Viewport.Bottom - Y;
                return new Rectangle(X, (int)Y, (int)width, (int)height);
        }
        
        //---------------------------------------------------------------------------------------------------//
        //
        //
        //
        private void draw()
        {
            InitializeGraphics();
            
            
            int j = 0;
            
            var D1 = new Dictionary<Interval, FrequenciesForValues>();
            var D2 = new Dictionary<Interval, FrequenciesForValues>();
            double size = range / distN;
            double start = max;
            List<Interval> ListOfIntervals = new List<Interval>();
            var ListOfRects = new List<Rectangle>();

            //Interval Interval_0 = new Interval(start, size);
            // ListOfIntervals.Add(Interval_0);
            // D.Add(Interval_0, new FrequenciesForValues());

            var bv = new BivariateDataset();

            double t = 0;
            foreach (var kvp in myds.UnivariateDatasets)
            {
                var path = kvp.Value;
                double index = 0;
                foreach (var item in path.ANO)
                {
                    double val = double.Parse(path.getObservationAt((int)index));

                    int X_device = Calc.Viewport(index, 0, n-1,  Viewport.Left, Viewport.Width);
                    int Y_device = Calc.Viewport(val, min, range,  Viewport.Bottom, -Viewport.Height);
                    
                    if(X_device > 0)
                        g.DrawLine(MyPen[j], prev_X[j],  prev_Y[j], X_device, Y_device);

                    if(index == check)
                    {
                        bv.Add(D1, ListOfIntervals, start, size, (index/(double)n), val, m);                       
                    } 
                    else if(index == n-1)
                    {
                        bv.Add(D2, ListOfIntervals, start, size, (index/(double)n), val, m);                       
                    }

                    index += 1;
                    prev_X[j] = X_device;
                    prev_Y[j] = Y_device;
                }
                j++;
            }
            //

            foreach(var kvp in D1)
            {
                var rect = distToRect(kvp, Viewport.Left + (int)(Viewport.Width*check/(double)n));
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), rect );
                g.DrawRectangle(new Pen(Color.FromArgb( 255, 255, 255)), rect );
            }

            double dist_index = 1;
            foreach(var kvp in D2)
            {
                var x_i = (kvp.Key.getMin() + (kvp.Key.getMax() - kvp.Key.getMin())*0.5);
                var val = kvp.Value.RelativeFrequency* x_i;
                variance += val*x_i;
                mean += val;
                //mean += (val - mean)/(double)dist_index;    
                var rect = distToRect(kvp, Viewport.Right);
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), rect );
                g.DrawRectangle(new Pen(Color.FromArgb( 255, 255, 255)), rect );

                dist_index++;
            }

            variance = variance - (mean*mean);
            // g.DrawLine(Pens.Red, Viewport.Left, Calc.Viewport(mean, min, range,  Viewport.Bottom, -Viewport.Height)  , Viewport.Right, Calc.Viewport(mean, min, range,  Viewport.Bottom, -Viewport.Height));
        
            Statistics = new List<string>();
            Statistics.Add(min.ToString());
            Statistics.Add(max.ToString());
            Statistics.Add(range.ToString());
            Statistics.Add(mean.ToString());
            Statistics.Add(variance.ToString());

            InitializeListView(Statistics);

            Chart.Image = b;
        }

        private void InitializeListView(List<string> values)
        {
            this.DataTable.Clear();
            this.DataTable.BeginUpdate();

            
            foreach(var col in LVColumns)
            {
                ColumnHeader header = new ColumnHeader();
                header.Text = col;
                header.TextAlign = HorizontalAlignment.Left;
                header.Width = (this.DataTable.Width) / LVColumns.Length - 2;

                this.DataTable.Columns.Add(header);
            }

            int i = 0;
            foreach(var stat in LVStatistics)
            {
                ListViewItem item = new ListViewItem(stat);
                
                if(values.Count > 0)
                {
                    item.SubItems.Add(values[i]);
                    i++;
                }

                this.DataTable.Items.Add(item);
            }

            this.DataTable.EndUpdate();
        }

        private void updateParameters()
        {
            if(NumericN.Value > 0) n = (int) NumericN.Value;

            if(NumericM.Value > 0) m = (int) NumericM.Value;
            
            check = n/2;
            if(NumericCheck.Value < n) check = (int) NumericCheck.Value;
            else check = (int) NumericN.Value;
            
            distN = (double) NumericDistN.Value;

            if(NumericP.Value < 100) p = (double)NumericP.Value/100.00;
            else p = 0.99;
            
            // if(NumericEpsilon.Value < 100) eps = (double)NumericEpsilon.Value/200.00;

            if(NumericLambda.Value > 0) lambda = (double) NumericLambda.Value;
            if(NumericLambda.Value > NumericN.Value) lambda = (double) NumericN.Value;

            sigma = 0.01 * (double) NumericSigma.Value;
            //if(NumericSigma.Value > NumericN.Value) sigma = NumericN.Value;

            A = 0.01* (double)NumericA.Value;
            B = 0.01* (double) NumericB.Value;
            L = 0.001* (double) NumericL.Value;
            //raw_values = new double[n][m]{};
            raw_paths = new List<AlphanumericDataset>(); //[k].ListOfObservations new (List).ToString()<AlphanumericDataset>();
            myds = new GenericDataset();
            min = double.MaxValue;
            max = double.MinValue;
            mean = 0;
            variance = 0;
            MyPen = new List<Pen>();    
            prev_X = new List<int>();
            prev_Y = new List<int>();
        }
        //---------------------------------------------------------------------------------------------------//
        //
        //
        //
        private void GenerateDataPoints()
        {

            updateParameters();

            // var AN_jumps = new AlphanumericDataset();
            // AN_jumps.Name = "Distance Of Individual Jumps";
            // AN_jumps.ListOfObservations = new List<string>();

            // var AN_cons = new AlphanumericDataset();
            // AN_cons.Name = "Distance Of Consecutive Jumps";
            // AN_cons.ListOfObservations = new List<string>();
            // var prev_val = new double[m];

            // var AN_Walkers = new AlphanumericDataset();
            // AN_Walkers.Name = "Poisson Walkers";
            // AN_Walkers.ListOfObservations = new List<string>();
            

            var f = new double[m];
            for(int k = 0; k < m; k++)
            {
                f[k] =  (NumericStart.Value > 0? (double) NumericStart.Value * 0.01 : getInitialValue());
                //f[k] = (double) (NumericStart.Value);
                raw_paths.Add(new AlphanumericDataset());
                raw_paths[k].Name = (k).ToString();
                raw_paths[k].ListOfObservations = new List<string>();

                MyPen.Add(new Pen(Color.FromArgb(150,rand.Next(256), rand.Next(256), rand.Next(256)), 1));
                prev_X.Add(Viewport.Left);
                prev_Y.Add( Viewport.Bottom  - Viewport.Height/2 );
            } 

            AlphanumericDataset DistSamples;

            for(int i = 0; i < n ; i++)
            {
                double index = (double)(i+1);

                // if(i == n-1)
                // {
                //     DistSamples = new AlphanumericDataset();
                //     DistSamples.ListOfObservations = new List<string>();
                //     DistSamples.Name = i.ToString();
                // }
                for(int k = 0; k< m; k++)
                {
                    double val =  updateVal(n, p, lambda, sigma, A, B, L, f[k], index); 
                    // Console.WriteLine("{0} updated", k);
                    f[k] = f[k] + val;
                    
                    raw_paths[k].ListOfObservations.Add((f[k]).ToString());

                    if(i == n-1)
                    {
                        // store all paths in dataset
                        var DS = new UnivariateDataset<double>() as IUnivariateDataset;
                        DS.Init(raw_paths[k]);

                        // calculate statistics

                        // sample max
                        if(double.Parse(DS.M) > max)max = double.Parse(DS.M);
                        //sample min
                        if(double.Parse(DS.m) < min)min = double.Parse(DS.m);
                        //sample range
                        range = max - min;
                        //sample mean ?
                        //mean += (double.Parse(DS.AM) - mean)/(k+1);
                        // expected value?

                        //variance?

                        myds.UnivariateDatasets.Add(DS.Name, DS);
                        myds.Labels.Add(DS.Name);

                        //distribution samples
                        // DistSamples.ListOfObservations.Add((f[k]).ToString());
                    }
                }
            }

            // var DS = new UnivariateDataset<double>() as IUnivariateDataset;
            // DS.Init(DistSamples);
            // myds.UnivariateDatasets.Add(DS.Name, DS);
            // myds.Labels.Add(DS.Name);

            //var ordered = ListOfRects.OrderBy(rect => -rect.Top);
            // int x_index = 0;

            // foreach(var rect in ListOfRects)
            // {

            //     var new_rect = new Rectangle(rect.Left, (int) (Viewport.Bottom - Viewport.Height/2 - (Viewport.Height/2)*rect.Top/1000), rect.Width, (int)(size*Viewport.Height/2));
                
            //     g.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), new_rect );
            //     g.DrawRectangle(new Pen(Color.FromArgb( 255, 255, 255)), new_rect );

            //     x_index+=1;
            // }


            // var myds = new GenericDataset();

            // var DS = new UnivariateDataset<int>();
            // DS.isNumeric = true;
            // DS.Init(AN_jumps);

            // myds.IntDictionary.Add(DS.Name, DS);
            // myds.Labels.Add(DS.Name);

            // DS = new UnivariateDataset<int>();
            // DS.isNumeric = true;
            // DS.Init(AN_cons);

            // myds.IntDictionary.Add(DS.Name, DS);
            // myds.Labels.Add(DS.Name);

            // var DDS = new UnivariateDataset<double>();
            // DDS.isNumeric = true;
            // DDS.Init(AN_Walkers);

            // myds.DoubleDictionary.Add(DDS.Name, DDS);
            // myds.Labels.Add(DDS.Name);

            // myds.Log();

            // FormUA UA = new FormUA(myds);
            // UA.Show();
            
        }

        //
        // Init Graphics
        //
        private void InitializeGraphics()
        {
            //if (pictureBox.Width < 2) pictureBox.Width = 2;
            //if (pictureBox.Height < 2) pictureBox.Height = 2;
            
            Viewport = new Rectangle(0, 0, Chart.Width - 1, Chart.Height - 2);
            int smallW = (int)(Viewport.Width*0.3);
            int bigW = (int)(Viewport.Width*0.6);
            int marginX = (int)(Viewport.Width*0.03);

            int smallH = (int)(Viewport.Height*0.3);
            int bigH = (int)(Viewport.Height*0.6);
            int marginY = (int)(Viewport.Height*0.03);

            b = new Bitmap(Chart.Width, Chart.Height);
            g = Graphics.FromImage(b);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(Color.White);
            g.DrawRectangle(Pens.Black, Viewport);

            Chart.Image = b;
        }


        //
        // Init Combo
        //
        private void InitializeCombo()
        {
            foreach(var sim in sims)
            {
                SelectSimulation.Items.Add(sim);
            }
        }

        private double getInitialValue()
        {
            if(SelectSimulation.Text == "Law Of Large Numbers")
            {
                return 0;
            }
            else if(SelectSimulation.Text == "Random Walk")
            {
                NumericStart.Value = 50;
                return 0.5;
            }else if(SelectSimulation.Text == "Poisson Process")
            {
                NumericStart.Value = 0;
                return 0;// / ((double)n* 1.7*lambda/n);
            }
            else if(SelectSimulation.Text == "Brownian Diffusion")
            {
                NumericStart.Value = 50;
                return 0.5;
            }
            else if(SelectSimulation.Text == "Geometric Brownian Diffusion")
            {
                NumericStart.Value = 80;
                return 0.8;
            }
            else if(SelectSimulation.Text == "Vasicek Process")
            {
                NumericStart.Value = 80;
                return 0.8;
            }
            // else if(SelectSimulation.Text == "Cox, Ingersoll, Ross")
            // {
            //     NumericStart.Value = 80;
            //     return 0.8;
            // }
            else if(SelectSimulation.Text == "Poisson Compound Process")
            {
                NumericStart.Value = 0;
                return 0;
            }
            else if(SelectSimulation.Text == "Basic Affine Jump Diffusion")
            {
                NumericStart.Value = 80;
                return 0.8;
            }
            else if(SelectSimulation.Text == "Merton Jump Diffusion")
            {
                NumericStart.Value = 80;
                return 0.8;
            }
            else return 0;
        }

        private double updateVal(int n, double p, double lambda, double sigma, double a, double b, double l, double current_val, double index)
        {
            double dt = (1/((double)n));

            if(SelectSimulation.Text == "Law Of Large Numbers")
            {
                return (Bernoulli(p) - current_val)/index;
            }
            else if(SelectSimulation.Text == "Random Walk")
            {
                return (Math.Pow(-1, Bernoulli(p)));
            }else if(SelectSimulation.Text == "Poisson Process")
            {
                return Bernoulli(lambda*dt) ;/// ((double) 1.7*lambda);// / ((double)n* 1.7*lambda/n);
            }
            else if(SelectSimulation.Text == "Brownian Diffusion")
            {
                return sigma * BrownianDiffusionStep(dt);
            }
            else if(SelectSimulation.Text == "Geometric Brownian Diffusion")
            {
                return sigma * dt + sigma * current_val * BrownianDiffusionStep(dt); //StdNormal();
            }
            else if(SelectSimulation.Text == "Vasicek Process")
            {
                return a*(b - current_val) * dt + sigma * BrownianDiffusionStep(dt);
            }
            // else if(SelectSimulation.Text == "Cox, Ingersoll, Ross")
            // {
            //     var mod_cv = current_val>=0? current_val: -current_val;
            //     return a*(b - current_val) * dt + sigma * Math.Sqrt(mod_cv) * BrownianDiffusionStep(dt);
            // }
            else if(SelectSimulation.Text == "Poisson Compound Process")
            {
                // with rate lambda, generate random lenght step
                return Bernoulli(lambda*dt) * l * StdNormal();
            }
            else if(SelectSimulation.Text == "Basic Affine Jump Diffusion")
            {
                var mod_cv = current_val>=0? current_val: -current_val;
                // Basic AJD
                return a*(b - current_val) * dt + sigma * Math.Sqrt(mod_cv) * BrownianDiffusionStep(dt) + Bernoulli(lambda*dt) * l * StdNormal();                   
            }
            else if(SelectSimulation.Text == "Merton Jump Diffusion")
            {
                // Merton Jump Diffusion
                return (a - lambda * l * dt ) * current_val * dt + sigma * current_val * BrownianDiffusionStep(dt) + current_val * Bernoulli(lambda*dt) * l * StdNormal();                           //Math.Sqrt(dt) *
            }
            else return 0;
            
        }

        private double BrownianDiffusionStep(double dt)
        {
            return Math.Sqrt(dt)*StdNormal();
        }
        //
        // Bernoulli
        //
        private int Bernoulli(double p)
        {
            double number = rand.NextDouble();
            if(number < p) return 1;
            else return 0;
        }

        //
        // Standard Normal
        //
        private double StdNormal()
        {

            // BOX - MULLER
            double x1 = rand.NextDouble();
            double x2 = rand.NextDouble();

            double z1 = Math.Sqrt(-2*Math.Log(x1)) * Math.Cos(2*Math.PI * x2);

            return z1;
        }
        
        
        //---------------------------------------------------------------------------------------------------//
        
        //
        //
        //
        private void onSelectSimulationChange(object sender, System.EventArgs e)
        {
            disableAllControls();
            enableSharedControls();
            
            available = true;
            
            if(SelectSimulation.Text == "Law Of Large Numbers")
            {
                NumericP.Enabled = true;
                // NumericEpsilon.Enabled = true;
            }
            else if(SelectSimulation.Text == "Poisson Process")
            {
                NumericLambda.Enabled = true;
            }
            else if(SelectSimulation.Text == "Brownian Diffusion")
            {
                NumericSigma.Enabled = true;
            }
            else if(SelectSimulation.Text == "Geometric Brownian Diffusion")
            {
                NumericSigma.Enabled = true;
            }
            else if(SelectSimulation.Text == "Vasicek Process")
            {
                NumericSigma.Enabled = true;
                NumericA.Enabled = true;
                NumericB.Enabled = true;
            }
            // else if(SelectSimulation.Text == "Cox, Ingersoll, Ross")
            // {
            //     NumericSigma.Enabled = true;
            //     NumericA.Enabled = true;
            //     NumericB.Enabled = true;
            // }
            else if(SelectSimulation.Text == "Poisson Compound Process")
            {
                NumericL.Enabled = true;
                NumericLambda.Enabled = true;
            }
            else if(SelectSimulation.Text == "Basic Affine Jump Diffusion")
            {
                NumericLambda.Enabled = true;
                NumericSigma.Enabled = true;
                NumericA.Enabled = true;
                NumericB.Enabled = true;
                NumericL.Enabled = true;
            }
            else if(SelectSimulation.Text == "Merton Jump Diffusion")
            {
                NumericLambda.Enabled = true;
                NumericSigma.Enabled = true;
                NumericA.Enabled = true;
                NumericL.Enabled = true;
            }
        }
        //
        // PLOT
        //
        private void onPlotClicked(object sender, EventArgs e)
        {
            // if(!drawing) available = false;
            // else
            // {
            //     drawing = false;
            //     available = true;
            // }
            // // if(!drawing) available = true; 
            // Console.WriteLine("AfterClick\navailable: {0}\ndrawing:{1}\n", available, drawing);
        }
        
        //
        // PLOT
        //
        private void onPlotClick(object sender, EventArgs e)
        {
            Console.WriteLine("OnClick\navailable: {0}\ndrawing:{1}\n", available, drawing);
            if(SelectSimulation.SelectedIndex >= 0 && available)
            {
                available = false;
                GenerateDataPoints();
                draw();
                // if(!drawing) available = true;
            }
            // else 
            // {
            //     if(!drawing) available = true;
            // }
        }

        //
        // PLOT
        //
        private void numericValueChanged(object sender, EventArgs e)
        {
            available = true;
        }
        //
        // CLEAR
        //
        private void onClearClick(object sender, EventArgs e)
        {
            InitializeGraphics();
        }

    }
}