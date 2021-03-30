using MyControls;
using System.Windows.Forms;
namespace SDE_Sim{
    partial class SDESim
    {
        private void InitializeComponent(int width, int h)
        {
            this.Width = width;
            int sidebarWidth = (int)(this.Width*0.3);
            MyPlotDataView view = new MyPlotDataView();

            this.ClearChartButton = new System.Windows.Forms.Button();             
            
            this.StatisticsGroupBox = new System.Windows.Forms.GroupBox();
            this.SelectSimulation = new System.Windows.Forms.ComboBox();            
            this.ClearChartButton = new System.Windows.Forms.Button();            
            this.PlotDataButton = new System.Windows.Forms.Button();

            this.SelectSimulation.SelectedIndexChanged += new System.EventHandler(this.onSelectSimulationChange);
            this.ClearChartButton.Click += new System.EventHandler(this.onClearClick);
            this.PlotDataButton.Click += new System.EventHandler(this.onPlotClick);
            this.PlotDataButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onPlotClicked);

            MyButton b = new MyButton();
            this.Height += b.Init(0, 0, sidebarWidth, StatisticsGroupBox, SelectSimulation, "Chart Controls",  "-- Simulation Output --");
            this.Height = b.Add(StatisticsGroupBox, PlotDataButton, "Begin Simulation");
            this.Height = b.Add(StatisticsGroupBox, ClearChartButton, "Clear Chart");

            this.DisplayDataSettings = new System.Windows.Forms.GroupBox();
            this.DataTable = new System.Windows.Forms.ListView();

            this.DataTable.View = View.Details;


            UserSelectableSettings = new System.Windows.Forms.GroupBox();
            NumericStart = new System.Windows.Forms.NumericUpDown();
            NumericStart.ValueChanged += new System.EventHandler(this.numericValueChanged);
            NumericN = new System.Windows.Forms.NumericUpDown();
            NumericM = new System.Windows.Forms.NumericUpDown();
            NumericCheck = new System.Windows.Forms.NumericUpDown();
            NumericDistN = new System.Windows.Forms.NumericUpDown();


            // bernoulli
            NumericP = new System.Windows.Forms.NumericUpDown();
            
            // law of large numbers
            NumericLambda = new System.Windows.Forms.NumericUpDown();

            // law of large numbers
            // NumericEpsilon = new System.Windows.Forms.NumericUpDown();

            // brown
            NumericSigma = new System.Windows.Forms.NumericUpDown();
            
            // vasiceck 
            NumericA = new System.Windows.Forms.NumericUpDown();
            NumericB = new System.Windows.Forms.NumericUpDown();

            // Jump Diffusion        
            NumericL = new System.Windows.Forms.NumericUpDown();

            NumericStart.Value = 0;
            NumericStart.Maximum = decimal.MaxValue;

            NumericN.Maximum = decimal.MaxValue;
            NumericN.Value = 800;

            NumericM.Maximum = decimal.MaxValue;
            NumericM.Value = 1600;

            NumericCheck.Maximum = decimal.MaxValue;
            NumericCheck.Value = NumericN.Value/2;

            NumericDistN.Value = 42;

            NumericP.Value = 50;

            // NumericEpsilon.Value = 10;

            NumericLambda.Minimum = 0;
            NumericLambda.Maximum = decimal.MaxValue;
            NumericLambda.Value = 30;

            NumericSigma.Minimum = 1;
            NumericSigma.Maximum = decimal.MaxValue;
            NumericSigma.Value = 30;

            NumericA.Maximum = decimal.MaxValue;
            NumericA.Value = 100;

            NumericB.Maximum = decimal.MaxValue;
            NumericB.Value = 250;

            NumericL.Minimum = 0;
            NumericL.Maximum = decimal.MaxValue;
            NumericL.Value = 50;
            
            disableAllControls();

            MyNumeric num = new MyNumeric();

            num.Init(0, this.Height, sidebarWidth,UserSelectableSettings, NumericStart, "Start Position (X_0 = 0.01 * SP)");
            num.Add(UserSelectableSettings, NumericN, "N: samples per path");
            num.Add(UserSelectableSettings, NumericM, "M: paths");
            num.Add(UserSelectableSettings, NumericCheck, "Control Distribution");
            num.Add(UserSelectableSettings, NumericDistN, "Max Number of Intervals");
            num.Add(UserSelectableSettings, NumericP, "P: Probability of Bernoulli");
            // num.Add(UserSelectableSettings, NumericEpsilon, "Epsilon");
            num.Add(UserSelectableSettings, NumericLambda, "Lambda");
            num.Add(UserSelectableSettings, NumericSigma, "Sigma (sigma = 0.01 * Sigma)");
            num.Add(UserSelectableSettings, NumericA, "A (a = 0.01 * A)");
            num.Add(UserSelectableSettings, NumericB, "B (b = 0.01 * B)");
            this.Height+=num.Add(UserSelectableSettings, NumericL, "L (l = 0.01 * L)");

            MyImportCSV ctr = new MyImportCSV();
            this.Height += ctr.DisplayData(0, this.Height, sidebarWidth, 0, this.DisplayDataSettings, this.DataTable);

            DisplayDataSettings.Text = "Calculated Statistics";

            this.DisplayDataSettings.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | 
                    (System.Windows.Forms.AnchorStyles.Left)
                ));
            this.DataTable.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | 
                    (System.Windows.Forms.AnchorStyles.Left)
                ));

            this.ChartGroupBox = new System.Windows.Forms.GroupBox();
            this.Chart = new System.Windows.Forms.PictureBox();

            int temp = view.ChartView(sidebarWidth, 0, this.Width - sidebarWidth, 0, this.ChartGroupBox, this.Chart);
            
                
            if(temp > this.Height) 
            {
                this.Height = temp;
            }
            else
            {
                this.ChartGroupBox.Height = this.Height - 50;
                this.Chart.Height -= 10;
            }

            // this.ChartGroupBox2 = new System.Windows.Forms.GroupBox();
            // this.Chart2 = new System.Windows.Forms.PictureBox();

            // view.ChartView(ChartGroupBox.Right, 0, (this.Width - sidebarWidth)/2, 0, this.ChartGroupBox2, this.Chart2);

            // ChartGroupBox2.Anchor = (
            //     (System.Windows.Forms.AnchorStyles)(
            //         (System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) |
            //         (System.Windows.Forms.AnchorStyles.Right)
            //     ));
            // this.Controls.Add(this.ChartGroupBox2);

            this.Controls.Add(this.UserSelectableSettings);
            this.Controls.Add(this.ChartGroupBox);
            this.Controls.Add(this.DisplayDataSettings);

            this.Controls.Add(this.StatisticsGroupBox);

            this.ClientSize = new System.Drawing.Size(this.Width, this.Height);
            
            this.Text = "Stochastic Differential Equation Simulator";

            
        }

        private System.Windows.Forms.GroupBox StatisticsGroupBox;
        private System.Windows.Forms.ComboBox SelectSimulation;
        private System.Windows.Forms.Button PlotDataButton;
        private System.Windows.Forms.Button ClearChartButton;
        
        private System.Windows.Forms.GroupBox UserSelectableSettings;
        private System.Windows.Forms.NumericUpDown NumericStart;
        private System.Windows.Forms.NumericUpDown NumericN;
        private System.Windows.Forms.NumericUpDown NumericM;
        private System.Windows.Forms.NumericUpDown NumericCheck;
        private System.Windows.Forms.NumericUpDown NumericDistN;
        private System.Windows.Forms.NumericUpDown NumericSigma;
        private System.Windows.Forms.NumericUpDown NumericA;
        private System.Windows.Forms.NumericUpDown NumericB;
        private System.Windows.Forms.NumericUpDown NumericP;
        private System.Windows.Forms.NumericUpDown NumericLambda;
        // private System.Windows.Forms.NumericUpDown NumericEpsilon;
        private System.Windows.Forms.NumericUpDown NumericL;

        private System.Windows.Forms.GroupBox DisplayDataSettings;
        private System.Windows.Forms.ListView DataTable;

        private System.Windows.Forms.GroupBox ChartGroupBox;
        private System.Windows.Forms.PictureBox Chart;
    }
}