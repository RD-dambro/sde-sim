using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyControls
{
    public class MyNumeric
    {
        public int stdButtonWidth = 60;
        public int stdButtonHeight = 22;
        public int marginX = 0;
        public int marginY = 0;
        //---------------------------------------------------------------------------------------------------//
        public int Add(
            System.Windows.Forms.GroupBox Settings,
            System.Windows.Forms.NumericUpDown Numeric, 
            string label
        )
        {
            int settingsWidth = Settings.Width;            
            
            //
            // Label groupbox
            //  
            var Label = new System.Windows.Forms.Label();        
            int LabelX = marginX;
            int LabelY = Settings.Height;

            Label.Location = new System.Drawing.Point(LabelX, LabelY);

            Label.Width = settingsWidth - 2*marginX;
            Label.Text = label;
            Label.Height = 12;
            
            //
            // Numeric groupbox
            //            
            int NumericX = marginX;
            int NumericY = 5 + LabelY + Label.Height;

            Numeric.Location = new System.Drawing.Point(NumericX, NumericY);

            Numeric.Width = settingsWidth - 2*marginX;
            Numeric.Height = stdButtonHeight;

            Settings.Height += Label.Height + Numeric.Height + 5;
            Settings.Controls.Add(Label);
            Settings.Controls.Add(Numeric);

            return 2*marginY + Settings.Height;
        }

    //---------------------------------------------------------------------------------------------------//
        public int Init(
            int X, int Y, int width,
            System.Windows.Forms.GroupBox Settings,
            System.Windows.Forms.NumericUpDown Numeric, 
            string label
        )
        {
            if(marginX == 0) marginX = (int) (width*0.05);
            if(marginY == 0) marginY = (int)(width*0.02);
            int settingsWidth = (int)(width*0.9);

            //
            // Settings groupbox
            //            
            int SettingsX = marginX + X;
            int SettingsY = marginY + Y;

            Settings.Location = new System.Drawing.Point(SettingsX, SettingsY);

            Settings.Width = settingsWidth;

            Settings.Text = "Simulation Parameters";
            Settings.Name = "Settings";

            //
            // Label groupbox
            //  
            var Label = new System.Windows.Forms.Label();        
            int LabelX = marginX;
            int LabelY = 2*marginY + 10;

            Label.Location = new System.Drawing.Point(LabelX, LabelY);

            Label.Width = settingsWidth - 2*marginX;

            Label.Name = "Label";
            Label.Text = label;
            Label.Height = 12;
            
            //
            // Numeric groupbox
            //            
            int NumericX = marginX;
            int NumericY = 10 + LabelY + Label.Height;

            Numeric.Location = new System.Drawing.Point(NumericX, NumericY);

            Numeric.Width = settingsWidth - 2*marginX;

            Numeric.Name = "Numeric";
            Numeric.Height = stdButtonHeight;

            Settings.Height = Label.Height + Numeric.Height + 30;
            Settings.Controls.Add(Label);
            Settings.Controls.Add(Numeric);

            return 2*marginY + Settings.Height + 20;
        }
    }
}