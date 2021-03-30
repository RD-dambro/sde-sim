using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;

namespace Statistics2020Library
{
    public class Median
    {
        private double _value;
        public double Value
        {
            get{return _value;}
            set{ _value = value; }
        }

        private List<double> OrderedAboveValue;
        private List<double> OrderedBelowValue;


        public void Init()
        {
            Value = 0;
            OrderedBelowValue = new List<double>();
            OrderedAboveValue = new List<double>();
        }

        public void CalculateOnline(double newValue)
        {
            if(OrderedBelowValue.Count == 0)
            {
                OrderedBelowValue.Add(newValue);
                Value = newValue;
                return;
            }

            if(newValue > Value)
            {
                if(OrderedAboveValue.Count == 0)
                {
                    OrderedAboveValue.Add(newValue);
                    return;
                }

                var HighHead = OrderedAboveValue[0];
                if(newValue <= HighHead)
                {
                    OrderedAboveValue.Insert(0, newValue);
                }
                else 
                {
                    OrderedAboveValue.Add(newValue);
                }
            }
            else 
            {
                var LowHead = OrderedBelowValue[0];
                if(newValue >= LowHead)
                {
                    OrderedBelowValue.Insert(0, newValue);
                }
                else 
                {
                    OrderedBelowValue.Add(newValue);
                }
            }

            // swap heads
            if(OrderedBelowValue.Count - OrderedAboveValue.Count < -1)
            {
                OrderedBelowValue.Insert(0, OrderedAboveValue[0]);
                OrderedAboveValue.RemoveAt(0);
            } else if(OrderedBelowValue.Count - OrderedAboveValue.Count > 1)
            {
                OrderedAboveValue.Insert(0, OrderedBelowValue[0]);
                OrderedBelowValue.RemoveAt(0);
            } 

            Value = (OrderedAboveValue[0] + OrderedBelowValue[0])/2;

            Log();
        }

        public void CalculateQuartiles()
        {
            var move_up = OrderedBelowValue.Where(v => v > Value).ToList();
            var move_down = OrderedAboveValue.Where(v => v < Value).ToList();

            OrderedBelowValue = OrderedBelowValue.Where(v => v <= Value).ToList();
            OrderedAboveValue = OrderedAboveValue.Where(v => v >= Value).ToList();

            //Log();
        }

        public void Log()
        {
            Console.WriteLine("Median: {0}\n"+
            "Values Below: {1}\n" +
            "Values Above: {2}\n" +
            "Heads: {3} {4}"
            , Value, OrderedBelowValue.Count, OrderedAboveValue.Count, OrderedBelowValue[0], OrderedAboveValue[0]);

            foreach(var v in OrderedBelowValue)
            {
                if(v > Value)
                    Console.WriteLine(v);
            }
            Console.WriteLine("------------------");
            foreach(var v in OrderedAboveValue)
            {
                if(v < Value)
                    Console.WriteLine(v);
            }
            
        }

    }
}