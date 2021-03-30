using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using Calculator;
using Comparator;

namespace Statistics2020Library
{
    public interface IUnivariateDataset
    { 
        string Name {get; set;}
        bool isNumeric { get; set;}
        List<string> ANO { get; set;}
        string AM { get; }
        string R { get; }
        string M { get; }
        string m { get; }
        void Log();
        void Init(AlphanumericDataset Observations);
        int X_viewport(string X, int v_left, int v_width);
        int Y_viewport(string Y, int v_bottom, int v_height);
        string getObservationAt(int i);
    }
    
    public interface IUnivariateDataset<T> : IUnivariateDataset
    {
        
    }

    public class UD 
    {
        public IUnivariateDataset GetInstance<T>() 
        {
            if(typeof(T) == typeof(int)) return new UnivariateDataset<int>();
            else if (typeof(T) == typeof(double)) return new UnivariateDataset<double>();
            else if (typeof(T) == typeof(DateTime)) return new UnivariateDataset<DateTime>();
            else if (typeof(T) == typeof(string)) return new UnivariateDataset<string>();
            else return null;
        }
    }

    public class UnivariateDataset<T> : IUnivariateDataset
    {
        private static readonly ICalculator<T> Calc = Calculators.GetInstance<T>();
        private static readonly IComparator<T> Comp = Comparators.GetInstance<T>();

        private string name;
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }

        public List<T> ListOfObservations = new List<T>();

        private List<string> ano = new List<string>();
        public List<string> ANO 
        {
            get { return ano; }
            set { ano = value; }
        }

        private bool is_numeric;
        public bool isNumeric
        {
            get { return is_numeric; }
            set { is_numeric = value; }
        }
        

        //
        // Statistics
        //
        
        public T Min;
        public string m 
        {
            get { return Min.ToString(); }
            set { Min = ConvertValue(value); }
        }

        public T Max;
        public string M 
        {
            get { return Max.ToString(); }
            set { Max = ConvertValue(value); }
        }

        public T Range;
        public string R 
        {
            get { return Range.ToString(); }
            set { Range = ConvertValue(value); }
        }

        public T ArithmeticMean;
        public string AM 
        {
            get { return ArithmeticMean.ToString(); }
            set { ArithmeticMean = ConvertValue(value); }
        }
        
        public string getObservationAt(int i)
        {
            return ListOfObservations[i].ToString();
        }

        //
        // X viewport
        //
        public int X_viewport(string X_string, int v_left, int v_width)
        { 
            var X = ConvertValue(X_string);
            var sub = Calc.Subtract(X, Min);
            //Console.Write("\n{0}: {4} + {3} * ({0} - {1}) / {2} = ", X, Min, Range, v_width, v_left);

            if (Comp.Greater(Range, ConvertValue("0")) && Comp.Not(sub, ConvertValue("0")) )
            {               
                // return Calc.Add(ConvertValue(v_left.ToString()),Calc.Multiply2Divide1(ConvertValue(v_width.ToString()),sub, Range));
                return Calc.Viewport(X, Min, Range,  ConvertValue(v_left.ToString()),  ConvertValue(v_width.ToString()));
            }
            else
            {
                return v_left;
            }
        }

        //
        // Y viewport
        //
        public int Y_viewport(string Y_string, int v_bottom, int v_height)
        {
            var Y = ConvertValue(Y_string);
            var sub = Calc.Subtract(Y, Min);

            //Console.WriteLine("{0}: {4} + {3} * ({0} - {1}) / {2}", Y, Min, Range, v_height, v_bottom);
            if (Comp.Greater(Range, ConvertValue("0")) && Comp.Not(sub, ConvertValue("0")))
            {
                // return Calc.Subtract(ConvertValue(v_bottom.ToString()),Calc.Multiply2Divide1(ConvertValue(v_height.ToString()),sub, Range));
                return Calc.Viewport(Y, Min, Range,  ConvertValue(v_bottom.ToString()),  ConvertValue((-v_height).ToString()));
            }
            else
            {
                return v_bottom;
            }
        }

        //
        // Arithmetic Mean Online Algorithm
        //
        public T AMOA(T value)
        {
            if(ListOfObservations.Count == 1) return value;
            // T diff = Calc.Subtract(value, ArithmeticMean);
            // T q = Calc.DivideByInt(diff, ListOfObservations.Count);
            return Calc.Mean_OnlineAlgo(value, ArithmeticMean, ConvertValue(ListOfObservations.Count.ToString())); 
            // Calc.Add(ArithmeticMean, q);
        }

        //
        // ADD
        //
        public void Add(T value)
        {
            this.ListOfObservations.Add(value);
            ANO.Add(value.ToString());
            if(isNumeric)
            {
                if(Comp.Smaller(value, Min)) Min = value;
                if(Comp.Greater(value, Max)) Max = value;

                ArithmeticMean = AMOA(value);
                //Console.WriteLine("{0}: {1}", Name, ArithmeticMean);
            }

        }
        //
        // Get Type Max
        //
        private T GetMin()
        {
            FieldInfo minValueField = typeof(T).GetField("MinValue", BindingFlags.Public | BindingFlags.Static);
            T minValue = (T)minValueField.GetValue(null);
            return minValue;
        }

        //
        // Get Type Max
        //
        private T GetMax()
        {
            FieldInfo maxValueField = typeof(T).GetField("MaxValue", BindingFlags.Public | BindingFlags.Static);
            T maxValue = (T)maxValueField.GetValue(null);
            return maxValue;
        }

        //
        // INIT
        //
        public void Init(AlphanumericDataset Observations)
        {

            if(typeof(T) == typeof(double) || typeof(T) == typeof(int)) isNumeric = true;
            else isNumeric = false;

            Name = Observations.Name;
            if(isNumeric)
            {
                Min = GetMax();
                Max = GetMin();
            }

            foreach(var obs in Observations.ListOfObservations)
            {
                T val;
                if(isNumeric && obs == "") val = ConvertValue("0");
                else val = ConvertValue(obs);

                //Console.WriteLine(val.GetType());
                this.Add(val);
            }
            if(isNumeric) Range = Calc.Subtract(Max, Min);

            
            //Console.WriteLine("{0}: Max is {1}, Min is {2}", Name, Max, Min);
        }

        public void Log()
        {
            if(isNumeric)
                Console.WriteLine("{0}({1}): Min = {2}, Max = {3}, Mean = {4}, Range = {5}", 
                    Name, typeof(T), Min, Max, ArithmeticMean, Range);
            else
                Console.WriteLine("{0}({1}): is not numeric", Name, typeof(T));
        }

        //
        //
        //
        public static T ConvertValue(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
         
    }
}