using System;
using System.Collections.Generic;

namespace Comparator {
    public interface IComparator { }
    
    public interface IComparator<T> : IComparator {
        bool Greater(T a, T b);
        bool Equal(T a, T b);
        bool Not(T a, T b);
        bool Smaller(T a, T b);
    }

    static class Comparators {
        public static readonly Dictionary<Type, IComparator> comparators = new Dictionary<Type, IComparator>() {
            { typeof(int), new IntComparator() },
            { typeof(double), new DoubleComparator() },
            { typeof(DateTime), new DateTimeComparator() },
            { typeof(string), new StringComparator() }
        };

        public static IComparator<T> GetInstance<T>() {
            return (IComparator<T>) comparators[typeof(T)];
        }
    }

    class IntComparator : IComparator<int> {
        public bool Greater(int a, int b) { return a > b; }
        public bool GreaterEqual(int a, int b) { return a >= b; }
        public bool Equal(int a, int b) { return a == b; }
        public bool Not(int a, int b) { return a != b; }
        public bool SmallerEqual(int a, int b) { return a <= b; }
        public bool Smaller(int a, int b) { return a < b; }
    }

    class DoubleComparator : IComparator<double> {
        public bool Greater(double a, double b) { return a > b; }
        public bool GreaterEqual(double a, double b) { return a >= b; }
        public bool Equal(double a, double b) { return a == b; }
        public bool Not(double a, double b) { return a != b; }
        public bool SmallerEqual(double a, double b) { return a <= b; }
        public bool Smaller(double a, double b) { return a < b; }
    }
        class DateTimeComparator : IComparator<DateTime> {
        public bool Greater(DateTime a, DateTime b) { return a > b; }
        public bool GreaterEqual(DateTime a, DateTime b) { return a >= b; }
        public bool Equal(DateTime a, DateTime b) { return a == b; }
        public bool Not(DateTime a, DateTime b) { return a != b; }
        public bool SmallerEqual(DateTime a, DateTime b) { return a <= b; }
        public bool Smaller(DateTime a, DateTime b) { return a < b; }
    }

    class StringComparator : IComparator<string> {
        public bool Greater(string a, string b) { return false; }
        public bool GreaterEqual(string a, string b) { return false; }
        public bool Equal(string a, string b) { return a==b; }
        public bool Not(string a, string b) { return a!=b; }
        public bool SmallerEqual(string a, string b) { return false; }
        public bool Smaller(string a, string b) { return false; }
    }
}