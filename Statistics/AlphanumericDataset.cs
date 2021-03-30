using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Statistics2020Library
{
    public class AlphanumericDataset
    {
        public string Name;
        public List<string> ListOfObservations;
        
        private Type data_type;
        public Type DataType
        {
            get { return data_type; }
            set { data_type = value; }
        }

        public HashSet<Type> ObservedDataTypes; 

        public void InferTypes(HashSet<Type> ObservableTypes)
        {
            ObservedDataTypes = new HashSet<Type>();
            
            foreach (var T in ObservableTypes)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(T);
                
                foreach (var obs in ListOfObservations)
                {
                    if(obs.Trim() != "")
                    {
                        try
                        {
                            var ConvertedValue = converter.ConvertFromInvariantString(obs);
                            if(!ObservedDataTypes.Contains(T)) ObservedDataTypes.Add(T);
                        }
                        catch (System.Exception e)
                        {
                            // Console.WriteLine(Name + " " + T + " is no good");
                            if(ObservedDataTypes.Contains(T)) ObservedDataTypes.Remove(T);
                            break;
                        }   
                    }
                }

            }

            //Log();
        }
        public void Log()
        {
            Console.Write(Name + ": ");
            if(ListOfObservations != null)
            {
                foreach (var obs in ListOfObservations)
                {
                    Console.Write(obs + " ");
                }
            }
            else Console.WriteLine("ListOfObservations is null");

            if(ObservedDataTypes != null)
            {
                foreach (var type in ObservedDataTypes)
                {
                    Console.Write(type + " ");
                }
            }
            else Console.WriteLine("ObservedDataTypes is null");

            if(DataType != null)
            {
                Console.Write("\nSelected: " + DataType);
            }
            else Console.WriteLine("DataType is null");

            Console.WriteLine("\n----");
        }
    }
}