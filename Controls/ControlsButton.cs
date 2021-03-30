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
    public class MyButton
    {
        public int stdButtonWidth = 60;
        public int stdButtonHeight = 22;
        public int marginX = 0;
        public int marginY = 0;
        //---------------------------------------------------------------------------------------------------//
        public int Add(
            System.Windows.Forms.GroupBox Settings,
            System.Windows.Forms.Button Button, 
            string label
        )
        {
            int settingsWidth = Settings.Width;            
            
            
            //
            // Button groupbox
            //            
            int ButtonX = marginX;
            int ButtonY = Settings.Height;

            Button.Location = new System.Drawing.Point(ButtonX, ButtonY);

            Button.Width = settingsWidth - 2*marginX;
            Button.Height = stdButtonHeight;
            Button.Text = label;
            
            Settings.Height += Button.Height + 10;

            Settings.Controls.Add(Button);

            return 2*marginY + Settings.Height;
        }

        //---------------------------------------------------------------------------------------------------//
        // Combo Init override
        public int Init(
            int X, int Y, int width,
            System.Windows.Forms.GroupBox Settings,
            System.Windows.Forms.ComboBox Combo, 
            string GroupLabel, string ComboLabel
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

            Settings.Text = GroupLabel;
            Settings.Name = "Settings";
            
            //
            // Button groupbox
            //            
            int NumericX = marginX;
            int NumericY = 2*marginY + 10;

            Combo.Location = new System.Drawing.Point(NumericX, NumericY);

            Combo.Width = settingsWidth - 2*marginX;

            Combo.Name = "Button";
            Combo.Height = stdButtonHeight;
            Combo.Text = ComboLabel;

            Settings.Height = Combo.Height + 30;
            Settings.Controls.Add(Combo);

            return 2*marginY + Settings.Height + 30;
        }

        //---------------------------------------------------------------------------------------------------//
        public int Init(
            int X, int Y, int width,
            System.Windows.Forms.GroupBox Settings,
            System.Windows.Forms.Button Button, 
            string GroupLabel, string ButtonLabel
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

            Settings.Text = GroupLabel;
            Settings.Name = "Settings";
            
            //
            // Button groupbox
            //            
            int NumericX = marginX;
            int NumericY = 2*marginY + 10;

            Button.Location = new System.Drawing.Point(NumericX, NumericY);

            Button.Width = settingsWidth - 2*marginX;

            Button.Name = "Button";
            Button.Height = stdButtonHeight;
            Button.Text = ButtonLabel;

            Settings.Height = Button.Height + 30;
            Settings.Controls.Add(Button);

            return 2*marginY + Settings.Height + 30;
        }
    }
}