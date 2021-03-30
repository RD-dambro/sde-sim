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
    public class GenericDataset
    {
        public List<string> Labels = new List<string>();
        public Dictionary<string, IUnivariateDataset> UnivariateDatasets = new Dictionary<string, IUnivariateDataset>();

        //---------------------------------------------------------------------------------------------------//
        //
        //
        public int GetVariableCount(string variable)
        {
            return UnivariateDatasets[variable].ANO.Count;
        }


        //---------------------------------------------------------------------------------------------------//
        //
        //
        public string GetAMAsString(string variable)
        {
            return UnivariateDatasets[variable].AM;
            //Console.WriteLine("Getting AMs of {0}", variable);
        }

        //---------------------------------------------------------------------------------------------------//
        //
        //
        public string GetMinAsString(string variable)
        {
            return UnivariateDatasets[variable].m;
        }

        //---------------------------------------------------------------------------------------------------//
        //
        //
        public string GetMaxAsString(string variable)
        {
            return UnivariateDatasets[variable].M;
        }

        //---------------------------------------------------------------------------------------------------//
        //
        //
        public string GetRangeAsString(string variable)
        {
            return UnivariateDatasets[variable].R;
        }

        //---------------------------------------------------------------------------------------------------//
        //
        // return scaled Arithmetic Mean of variable as int
        //
        public int GetAM_ScaledY(string variable, int v_bottom, int v_height)
        {           
            var Y = UnivariateDatasets[variable];
            return Y.Y_viewport(Y.AM, v_bottom, v_height);
        }

        //---------------------------------------------------------------------------------------------------//
        //
        // return scaled Arithmetic Mean of variable as int
        //
        public int GetAM_ScaledX(string variable, int v_left, int v_width)
        {            
            var X = UnivariateDatasets[variable];
            return X.X_viewport(X.AM, v_left, v_width);
        }
        
        //---------------------------------------------------------------------------------------------------//
        //
        // Get values of variable as list of strings
        //
        public List<string> getStringValues(string variable)
        {

            var Values = new List<string>();

            var Y = UnivariateDatasets[variable];

            foreach(var val in UnivariateDatasets[variable].ANO)
            {
                Values.Add(val);
            }

            return Values;
        }
        
        
        //---------------------------------------------------------------------------------------------------//
        //
        // Get Y
        //
        public List<int> getY(string variable, int v_bottom, int v_height)
        {
            var Values = new List<int>();

            var Y = UnivariateDatasets[variable];

            foreach(var y in Y.ANO)
            {
                Values.Add(Y.Y_viewport(y, v_bottom, v_height));
            }

            return Values;
        }

        //---------------------------------------------------------------------------------------------------//
        //
        // Get X
        //
        public List<int> getX(string variable, int v_left, int v_width)
        {
            var Values = new List<int>();

            var X = UnivariateDatasets[variable];

            foreach(var x in X.ANO)
            {
                Values.Add(X.X_viewport(x, v_left, v_width));
            }

            return Values;
        }

        //---------------------------------------------------------------------------------------------------//
        //
        // Get Numeric Variables List
        //
        public List<string> GetNumericVariables()
        {
            var ListOfNumericVars = new List<string>();
            foreach (var label in Labels)
            {
                if(UnivariateDatasets[label].isNumeric) ListOfNumericVars.Add(label);
            }

            return ListOfNumericVars;
        }

        //---------------------------------------------------------------------------------------------------//
        //
        // LOG 
        //
        public void Log()
        {
            foreach (var kvp in UnivariateDatasets)
            {
                kvp.Value.Log();
            }
        }
    }
}