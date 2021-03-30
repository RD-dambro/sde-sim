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
    public class BivariateDataset
    {
        public List<MyPoint> DataPoints = new List<MyPoint>();
        public Tuple<string, string> AM;
        public Tuple<string, string> Max;
        public Tuple<string, string> Min;
        public Tuple<string, string> Range;
        public Tuple<string, string> Median;
        public Tuple<string, string> Var;
        public Tuple<string, string> Cov;
        public (string x0, string y0, string x1, string y1) RegrX;
        public (string x0, string y0, string x1, string y1) RegrY;
        public Tuple<Dictionary<Interval, FrequenciesForValues>, Dictionary<Interval, FrequenciesForValues>> Distribution;

        public Tuple<int, int> MaxCount;


        public KeyValuePair<Interval,FrequenciesForValues> Add(
            Dictionary<Interval, FrequenciesForValues> Distribution, 
            List<Interval> ListOfIntervals, 
            double start, 
            double size, 
            double X, 
            double Y, 
            double tot 
            //double max
        )
        {
            Interval IntervalWhereValueFalls = FindIntervalForObservation(Y, ListOfIntervals, start, size);
            ListOfIntervals.Add(IntervalWhereValueFalls);

            if (!Distribution.ContainsKey(IntervalWhereValueFalls))
                Distribution.Add(IntervalWhereValueFalls, new FrequenciesForValues());
            
            Distribution[IntervalWhereValueFalls].Update(Y, X);
            Distribution[IntervalWhereValueFalls].RelativeFrequency = Distribution[IntervalWhereValueFalls].Count/(double)tot;
            Distribution[IntervalWhereValueFalls].Percentage = ((int) (Distribution[IntervalWhereValueFalls].RelativeFrequency *100*100))/100.00;
            //if(Distribution[IntervalWhereValueFalls].Count > max) max = Distribution[IntervalWhereValueFalls].Count;

            return new KeyValuePair<Interval,FrequenciesForValues>(IntervalWhereValueFalls, Distribution[IntervalWhereValueFalls]);
        }


        public void CalculateDistributions(int start1, double size1, int start2, double size2)
        {
            var D1 = new Dictionary<Interval, FrequenciesForValues>();
            var D2 = new Dictionary<Interval, FrequenciesForValues>();
            int max1 = 0;
            int max2 = 0;

            //Console.WriteLine(start2);

            if(size1 <= 0) size1 = 1;
            if(size2 <= 0) size2 = 1;
               
            Interval Interval_0X = new Interval(start1, (int)size1);
            var Interval_0Y = new Interval(start2, (int)size2);
            
            List<Interval> ListOfIntervalsX = new List<Interval>();
            var ListOfIntervalsY = new List<Interval>();
            
            ListOfIntervalsX.Add(Interval_0X);
            ListOfIntervalsY.Add(Interval_0Y);
            
            D1.Add(Interval_0X, new FrequenciesForValues());
            D2.Add(Interval_0Y, new FrequenciesForValues());

            

            foreach (var point in DataPoints)
            {
                Interval IntervalWhereValueXFalls = FindIntervalForObservation(point.X, ListOfIntervalsX, start1, (int)size1);
                Interval IntervalWhereValueYFalls = FindIntervalForObservation(point.Y, ListOfIntervalsY, start2, (int)size2);
                
                IntervalWhereValueXFalls.rawStart = point.rawX;
                IntervalWhereValueYFalls.rawStart = point.rawY;

                ListOfIntervalsX.Add(IntervalWhereValueXFalls);
                ListOfIntervalsY.Add(IntervalWhereValueYFalls);

                if (!D1.ContainsKey(IntervalWhereValueXFalls))
                {
                    //var ffv2 = new FrequenciesForValues();
                    D1.Add(IntervalWhereValueXFalls, new FrequenciesForValues());

                }
                
                D1[IntervalWhereValueXFalls].Update(point.X, point.Y);
                D1[IntervalWhereValueXFalls].RelativeFrequency = (D1[IntervalWhereValueXFalls].Count/(double)DataPoints.Count*100)/100.00;
                D1[IntervalWhereValueXFalls].Percentage = ((int) (D1[IntervalWhereValueXFalls].RelativeFrequency *100));
                if(D1[IntervalWhereValueXFalls].Count > max1) max1 = D1[IntervalWhereValueXFalls].Count;


                if (!D2.ContainsKey(IntervalWhereValueYFalls))
                {
                    //var ffv2 = new FrequenciesForValues();
                    D2.Add(IntervalWhereValueYFalls, new FrequenciesForValues());

                }

                D2[IntervalWhereValueYFalls].Update(point.Y, point.X);
                D2[IntervalWhereValueYFalls].RelativeFrequency = D2[IntervalWhereValueYFalls].Count/(double)DataPoints.Count;
                D2[IntervalWhereValueYFalls].Percentage = ((int) (D2[IntervalWhereValueYFalls].RelativeFrequency *100*100))/100.00;
                if(D2[IntervalWhereValueYFalls].Count > max2) max2 = D2[IntervalWhereValueYFalls].Count;

                //
                if(D1[IntervalWhereValueXFalls].D.Count == 0) D1[IntervalWhereValueXFalls].D.Add(IntervalWhereValueYFalls, new FrequenciesForValues());
                else if(!D1[IntervalWhereValueXFalls].D.ContainsKey(IntervalWhereValueYFalls)) D1[IntervalWhereValueXFalls].D.Add(IntervalWhereValueYFalls, new FrequenciesForValues());
                D1[IntervalWhereValueXFalls].D[IntervalWhereValueYFalls].Update(point.X, point.Y);
                D1[IntervalWhereValueXFalls].D[IntervalWhereValueYFalls].RelativeFrequency = (D1[IntervalWhereValueXFalls].D[IntervalWhereValueYFalls].Count/(double)DataPoints.Count*100)/100.00;
                // D1[IntervalWhereValueXFalls].D[IntervalWhereValueYFalls].Percentage = ((int) (D1[IntervalWhereValueXFalls].RelativeFrequency *100*100))/100.00;
                //if(D1[IntervalWhereValueXFalls].D[IntervalWhereValueYFalls].Count > max1) max1 = D1[IntervalWhereValueXFalls].Count;
                
                if(D2[IntervalWhereValueYFalls].D.Count == 0) D2[IntervalWhereValueYFalls].D.Add(IntervalWhereValueXFalls, new FrequenciesForValues());
                if(!D2[IntervalWhereValueYFalls].D.ContainsKey(IntervalWhereValueXFalls)) D2[IntervalWhereValueYFalls].D.Add(IntervalWhereValueXFalls, new FrequenciesForValues());
                D2[IntervalWhereValueYFalls].D[IntervalWhereValueXFalls].Update(point.Y, point.X);
                D2[IntervalWhereValueYFalls].D[IntervalWhereValueXFalls].RelativeFrequency = (D2[IntervalWhereValueYFalls].D[IntervalWhereValueXFalls].Count/(double)DataPoints.Count*100)/100.00;
            }
            
            MaxCount  = new Tuple<int, int>(max1, max2);
            if(D2[Interval_0Y].Count == 0) D2.Remove(Interval_0Y);
            if(D1[Interval_0X].Count == 0) D1.Remove(Interval_0X);
            Distribution = new Tuple<Dictionary<Interval, FrequenciesForValues>, Dictionary<Interval, FrequenciesForValues>>(D1, D2);

            
        }

        //
        //
        //
        public Interval RightInterval(double target, Interval I)
        {
            double currentMin = I.getMin();
            double currentMax = I.getMax();
            double size = currentMax - currentMin;
            while (true)
            {
                currentMin += size;
                Interval temp = new Interval(currentMin, size);
                if (temp.containsValue(target))
                {
                    return temp;
                }
            }
        }

        //
        //
        //
        public Interval LeftInterval(double target, Interval I)
        {
            double currentMin = I.getMin();
            double currentMax = I.getMax();
            double size = currentMax - currentMin;
            while (true)
            {
                currentMin -= size;
                Interval temp = new Interval(currentMin, size);
                if (temp.containsValue(target))
                {
                    return temp;
                }
            }
        }

        //
        //
        //
        public Interval FindIntervalForObservation(double Obs, List<Interval> ListOfIntervals, double start, double size)
        {
            Interval leftMostInterval = new Interval(start, size);
            Interval rightMostInterval = new Interval(start, size);

            foreach (Interval I in ListOfIntervals)
            {
                if (I.containsValue(Obs))
                {
                    return I;
                }
                else
                {
                    if (leftMostInterval.getMin() > I.getMin()) leftMostInterval = I;
                    else if (rightMostInterval.getMax() < I.getMax()) rightMostInterval = I;
                }
            }

            if (Obs < leftMostInterval.getMin())
            {
                return LeftInterval(Obs, leftMostInterval);
            }
            else if (Obs > rightMostInterval.getMax())
            {
                return RightInterval(Obs, rightMostInterval);
            }
            else
            {
                return RightInterval(Obs, leftMostInterval);
            }
        }
    }

    public class MyPoint
    {
        public string Label;
        public int X;
        public int Y;
        public string rawX;
        public string rawY;
    }

    public class Interval
    {
        private double MinValue;
        private double MaxValue;
        public double Mean;

        public string rawStart;

        public Interval(double min, double size)
        {
            MinValue = min;
            MaxValue = min + size;
            Mean = min + size/2;
        }
        public bool containsValue(double v)
        {
            //Console.WriteLine("{0} in {1} - {2} : {3}", v, MinValue, MaxValue, (v >= MinValue && v < MaxValue));
            return (v >= MinValue && v < MaxValue);
        }

        public double getMin()
        {
            return MinValue;
        }

        public double getMax()
        {
            return MaxValue;
        }
    }

    public class FrequenciesForValues
    {
        public int Count = 0;
        public double MeanValue = 0;

        public double RelativeFrequency = 0;
        public double Percentage = 0;

        public double Max = double.MinValue;
        public double Min = double.MaxValue;

        public Dictionary<Interval, FrequenciesForValues> D = new Dictionary<Interval, FrequenciesForValues>();

        public void Update(double value, double f)
        {
            Count++;
            MeanValue += (value - MeanValue)/(double)Count;

            if(f > Max) Max = f;
            if(f < Min) Min = f;
        }
    }
}