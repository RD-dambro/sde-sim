using System;
using System.Collections.Generic;

namespace Calculator 
{
    public interface ICalculator { }
    
    public interface ICalculator<T> : ICalculator {
        T Add(T a, T b);
        T Divide(T a, T b);
        T DivideByInt(T a, int b);
        T Multiply(T a, T b);
        T Multiply2Divide1(T a, T b, T c);
        T Mean_OnlineAlgo(T val, T prev_mean, T count) ;
        int Viewport(T worldVal, T worldMin, T worldRange, T viewMin, T viewRange);
        T Subtract(T a, T b);
    }

    static class Calculators {
        public static readonly Dictionary<Type, ICalculator> calculators = new Dictionary<Type, ICalculator>() {
            { typeof(int), new IntCalculator() },
            { typeof(double), new DoubleCalculator() },
            { typeof(DateTime), new DateTimeCalculator() },
            { typeof(string), new StringCalculator() }
        };

        public static ICalculator<T> GetInstance<T>() {
            return (ICalculator<T>) calculators[typeof(T)];
        }
    }

    class IntCalculator : ICalculator<int> {
        public int Add(int a, int b) { return a + b; }
        public int Divide(int a, int b) { return (int)((double)a / (double)b); }
        public int DivideByInt(int a, int b) { return a / b; }
        public int Multiply(int a, int b) { return (int)((double)a * (double)b); }
        public int Multiply2Divide1(int a, int b, int c) { return (int)((double)a * (double)b / (double)c); }
        public int Subtract(int a, int b) { return a - b; } 

        public int Mean_OnlineAlgo(int val, int prev_mean, int count) 
        {   return (int)(prev_mean + (val - prev_mean)/(double) count); }

        public int Viewport(int worldVal, int worldMin, int worldRange, int viewMin, int viewRange) 
        {   return (int)(viewMin + viewRange * (worldVal - worldMin) / (double)worldRange);    }
    }

    class DoubleCalculator : ICalculator<double> {
        public double Add(double a, double b) { return a + b; }
        public double Divide(double a, double b) { return a / b; }
        public double DivideByInt(double a, int b) { return a / b; }
        public double Multiply(double a, double b) { return a * b; }
        public double Multiply2Divide1(double a, double b, double c) { return a * b / c; }
        public double Subtract(double a, double b) { return a - b; }
        
        public double Mean_OnlineAlgo(double val, double prev_mean, double count) 
        {   return (prev_mean + (val - prev_mean) / count); }
        
        public int Viewport(double worldVal, double worldMin, double worldRange, double viewMin, double viewRange) 
        {   return (int)(viewMin + viewRange * (worldVal - worldMin) / worldRange);    }
    }

    class DateTimeCalculator : ICalculator<DateTime> {
        public DateTime Add(DateTime a, DateTime b) { return a; }
        public DateTime Divide(DateTime a, DateTime b) { return a; }
        public DateTime DivideByInt(DateTime a, int b) { return a; }
        public DateTime Multiply(DateTime a, DateTime b) { return a; }
        public DateTime Multiply2Divide1(DateTime a, DateTime b, DateTime c) { return a; }
        public DateTime Subtract(DateTime a, DateTime b) { return a; }
        public DateTime Mean_OnlineAlgo(DateTime val, DateTime prev_mean, DateTime count) {return val;}
        public int Viewport(DateTime worldVal, DateTime worldMin, DateTime worldRange, DateTime viewMin, DateTime viewRange) 
        {   return 0;    }

    }

    class StringCalculator : ICalculator<string> {
        public string Add(string a, string b) { return a; }
        public string Divide(string a, string b) { return a; }
        public string DivideByInt(string a, int b) { return a; }
        public string Multiply(string a, string b) { return a; }
        public string Multiply2Divide1(string a, string b, string c) { return a; }
        public string Subtract(string a, string b) { return a; }
        public string Mean_OnlineAlgo(string val, string prev_mean, string count) {return val;}
        public int Viewport(string worldVal, string worldMin, string worldRange, string viewMin, string viewRange) 
        {   return 0;    }
    }
}