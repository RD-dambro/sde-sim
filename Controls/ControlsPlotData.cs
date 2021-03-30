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
    public class MyPlotDataView
    {
        public int stdButtonWidth = 60;
        public int stdButtonHeight = 22;
        public int marginX = 0;
        public int marginY = 0;


        public int ChartView(
            int X, int Y, int width, int height,
            System.Windows.Forms.GroupBox ChartGroupBox,
            System.Windows.Forms.PictureBox Chart
        )
        {
            if(marginX == 0) marginX = (int) (width*0.05);
            if(marginY == 0) marginY = (int)(width*0.02);
            int settingsWidth = (int)(width*0.9);

            //
            // ChartGroupBox groupbox
            //            
            ChartGroupBox.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | 
                    (System.Windows.Forms.AnchorStyles.Left)| (System.Windows.Forms.AnchorStyles.Right)
                ));
            int ChartGroupBoxX = marginX + X;
            int ChartGroupBoxY = marginY + Y;

            ChartGroupBox.Location = new System.Drawing.Point(ChartGroupBoxX, ChartGroupBoxY);

            ChartGroupBox.Width = settingsWidth;

            ChartGroupBox.Text = "Chart";
            ChartGroupBox.Name = "ChartGroupBox";

            //
            // Chart groupbox
            //            
            Chart.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | 
                    (System.Windows.Forms.AnchorStyles.Left)| (System.Windows.Forms.AnchorStyles.Right)
                ));
            int ChartX = marginX;
            int ChartY = 2*marginY + 10;

            Chart.Location = new System.Drawing.Point(ChartX, ChartY);

            Chart.Width = settingsWidth - 2*marginX;
            
            Chart.Height = Chart.Width;

            Chart.Name = "Chart";

            ChartGroupBox.Height = Chart.Height + 30;

            ChartGroupBox.Controls.Add(Chart);

            return 2*marginY + ChartGroupBox.Height + 30;
        }

        public int StatisticsControls(
            int X, int Y, int width, 
            System.Windows.Forms.GroupBox StatisticsGroupBox,
            System.Windows.Forms.Button PlotDataButton,
            System.Windows.Forms.Button ContingencyTableButton
        )
        {
            if(marginX == 0) marginX = (int) (width*0.05);
            if(marginY == 0) marginY = (int)(width*0.02);
            int settingsWidth = (int)(width*0.9);

            //---------------------------------------------------------------------------------------------------//
            //
            // StatisticsGroupBox groupbox
            //
            int StatisticsGroupBoxX = marginX + X;
            int StatisticsGroupBoxY = marginY + Y;

            StatisticsGroupBox.Location = new System.Drawing.Point(StatisticsGroupBoxX, StatisticsGroupBoxY);

            StatisticsGroupBox.Width = settingsWidth;

            StatisticsGroupBox.Text = "Statistics";
            StatisticsGroupBox.Name = "StatisticsGroupBox";

            //
            // PlotDataButton Button
            //
            PlotDataButton.Text = "Display Chart Dialog";
            PlotDataButton.Name = "PlotDataButton";

            int PlotDataButtonX = marginX;
            int PlotDataButtonY = 2*marginY + 10;

            PlotDataButton.Location = new System.Drawing.Point(PlotDataButtonX, PlotDataButtonY);

            PlotDataButton.Height = stdButtonHeight;
            PlotDataButton.Width = settingsWidth - 2*marginX;
            
            PlotDataButton.Enabled = false;
            
            //
            // ContingencyTableButton Button
            //
            ContingencyTableButton.Text = "Display Contingency Table";
            ContingencyTableButton.Name = "ContingencyTableButton";

            int ContingencyTableButtonX = marginX;
            int ContingencyTableButtonY = marginY + PlotDataButtonY + PlotDataButton.Height;

            ContingencyTableButton.Location = new System.Drawing.Point(ContingencyTableButtonX, ContingencyTableButtonY);

            ContingencyTableButton.Height = stdButtonHeight;
            ContingencyTableButton.Width = settingsWidth - 2*marginX;
            
            ContingencyTableButton.Enabled = false;

            StatisticsGroupBox.Height = 3*marginY + 20 + ContingencyTableButton.Height + PlotDataButton.Height;

            StatisticsGroupBox.Controls.Add(ContingencyTableButton);
            StatisticsGroupBox.Controls.Add(PlotDataButton);

            return StatisticsGroupBox.Height + 2*marginY;
        }


        public int VariableSettings(
            string Name, int X, int Y, int width, 
            System.Windows.Forms.GroupBox VariableSettings,
            System.Windows.Forms.ComboBox SelectVariable,
            System.Windows.Forms.Button PickColor,
            System.Windows.Forms.RichTextBox DisplayColor,
            System.Windows.Forms.NumericUpDown Interval

        )
        {
            if(marginX == 0) marginX = (int) (width*0.05);
            if(marginY == 0) marginY = (int)(width*0.02);
            int settingsWidth = (int)(width*0.9);

            //---------------------------------------------------------------------------------------------------//
            //
            // Variable settings groupbox
            //
            int VariableSettingsX = X + marginX;
            int VariableSettingsY = Y + marginY;
            VariableSettings.Location = new System.Drawing.Point(VariableSettingsX, VariableSettingsY);

            VariableSettings.Width = settingsWidth;
            VariableSettings.Text = "Var " + Name + " Settings";
            VariableSettings.Name = "VariableSettings";

            //
            // PickColor Button
            //

            int PickColorX = marginX;
            int PickColorY = 2*marginY + 10;

            PickColor.Location = new System.Drawing.Point(PickColorX, PickColorY);

            PickColor.Height = SelectVariable.Height;
            PickColor.Width = settingsWidth - 3*marginX - PickColor.Height;

            PickColor.Text = "Pick Point Color";
            PickColor.Name = "PickColor";

            //
            // DisplayColor RichTextBox
            //
            int DisplayColorX = marginX + PickColorX + PickColor.Width;
            int DisplayColorY = PickColorY;
            DisplayColor.Location = new System.Drawing.Point(DisplayColorX, DisplayColorY);

            DisplayColor.Height = PickColor.Height;
            DisplayColor.Width = PickColor.Height;

            DisplayColor.BackColor = System.Drawing.Color.Green;
            DisplayColor.Name = "DisplayColor";
            DisplayColor.Enabled = false;

            //
            // SelectVariable ComboBox
            //

            int SelectVariableX = marginX;
            int SelectVariableY = marginY + DisplayColorY + DisplayColor.Height;

            SelectVariable.Location = new System.Drawing.Point(SelectVariableX, SelectVariableY);

            SelectVariable.Width = VariableSettings.Width - 2*marginX;

            SelectVariable.Text = "-- Select Variable " + Name + " --";
            SelectVariable.Name = "SelectVariable";

            //
            // Interval NumericUpDown
            //
            int IntervalX = marginX;
            int IntervalY = marginY + SelectVariableY + SelectVariable.Height;

            Interval.Location = new System.Drawing.Point(IntervalX, IntervalY);

            Interval.Height = SelectVariable.Height;
            Interval.Width = SelectVariable.Width;

            Interval.Text = "Var " + Name + " Interval Size";
            Interval.Name = "NumericUpDown";

            // complete settings
            VariableSettings.Height = 4*marginY + 20 + PickColor.Height + SelectVariable.Height + Interval.Height;
            VariableSettings.Controls.Add(PickColor);
            VariableSettings.Controls.Add(DisplayColor);

            VariableSettings.Controls.Add(SelectVariable);
            VariableSettings.Controls.Add(Interval);

            return VariableSettings.Height + 2*marginY;
        }


        //---------------------------------------------------------------------------------------------------//
        //
        //
        //
        public int PointSettings(
            int X, int Y, int width,
            System.Windows.Forms.GroupBox PointSettings,
            System.Windows.Forms.ComboBox SelectLabel,
            System.Windows.Forms.Button PickPointColor,
            System.Windows.Forms.RichTextBox DisplayPointColor,
            System.Windows.Forms.CheckBox DisplayLabel,
            System.Windows.Forms.Button PickLabelColor,
            System.Windows.Forms.RichTextBox DisplayLabelColor
        )
        {    
            if(marginX == 0) marginX = (int) (width*0.05);
            if(marginY == 0) marginY = (int)(width*0.02);
            int settingsWidth = (int)(width*0.9);

            //
            // Point settings groupbox
            //
            int pointSettingsX = X + marginX;
            int pointSettingsY = Y + marginY;

            PointSettings.Location = new System.Drawing.Point(pointSettingsX, pointSettingsY);

            PointSettings.Width = settingsWidth;

            PointSettings.Text = "Point Settings";
            PointSettings.Name = "PointSettings";
            

            //
            // PickPointColor Button
            //
            int PickPointColorX = marginX;
            int PickPointColorY = 2*marginY + 10;

            PickPointColor.Location = new System.Drawing.Point(PickPointColorX, PickPointColorY);

            PickPointColor.Height = SelectLabel.Height;
            PickPointColor.Width = settingsWidth - 3*marginX - PickPointColor.Height;

            PickPointColor.Text = "Pick Point Color";
            PickPointColor.Name = "PickPointColor";

            //
            // DisplayPointColor RichTextBox
            //

            int DisplayPointColorX = marginX + PickPointColorX + PickPointColor.Width;
            int DisplayPointColorY = PickPointColorY;
            DisplayPointColor.Location = new System.Drawing.Point(DisplayPointColorX, DisplayPointColorY);

            DisplayPointColor.Height = PickPointColor.Height;
            DisplayPointColor.Width = PickPointColor.Height;

            DisplayPointColor.BackColor = System.Drawing.Color.Blue;
            DisplayPointColor.Name = "DisplayPointColor";
            DisplayPointColor.Enabled = false;

            //
            // DisplayLabel CheckBox
            //
            int DisplayLabelX = marginX;
            int DisplayLabelY = marginY + PickPointColorY + PickPointColor.Height;

            DisplayLabel.Location = new System.Drawing.Point(DisplayLabelX, DisplayLabelY);

            DisplayLabel.Width = settingsWidth - 2*marginX;

            DisplayLabel.Text = "Display Labels";
            DisplayLabel.Name = "DisplayLabel";

            //
            // SelectLabel ComboBox
            //
            int SelectLabelX = pointSettingsX;
            int SelectLabelY = marginY + DisplayLabelY + DisplayLabel.Height;

            SelectLabel.Location = new System.Drawing.Point(SelectLabelX, SelectLabelY);

            SelectLabel.Width = settingsWidth - 2*marginX;

            SelectLabel.Text = "-- Select Label Variable --";
            SelectLabel.Name = "SelectLabel";

            //
            // PickLabelColor Button
            //
            int PickLabelColorX = marginX;
            int PickLabelColorY = marginY + SelectLabelY + SelectLabel.Height;

            PickLabelColor.Location = new System.Drawing.Point(PickLabelColorX, PickLabelColorY);

            PickLabelColor.Height = SelectLabel.Height;
            PickLabelColor.Width = settingsWidth - 3*marginX - PickLabelColor.Height;

            PickLabelColor.Text = "Pick Label Color";
            PickLabelColor.Name = "PickLabelColor";

            //
            // DisplayLabelColor RichTextBox
            //
            int DisplayLabelColorX = marginX + PickLabelColorX + PickLabelColor.Width;
            int DisplayLabelColorY = PickLabelColorY;
            DisplayLabelColor.Location = new System.Drawing.Point(DisplayLabelColorX, DisplayLabelColorY);

            DisplayLabelColor.Height = PickLabelColor.Height;
            DisplayLabelColor.Width = PickLabelColor.Height;

            DisplayLabelColor.BackColor = System.Drawing.Color.Red;
            DisplayLabelColor.Name = "DisplayLabelColor";
            DisplayLabelColor.Enabled = false;


            // Complete PointSettings GroupBox settings
            PointSettings.Height = 5*marginY + 20 + 2*PickPointColor.Height + SelectLabel.Height + DisplayLabel.Height;

            PointSettings.Controls.Add(PickPointColor);
            PointSettings.Controls.Add(DisplayPointColor);

            PointSettings.Controls.Add(SelectLabel);

            PointSettings.Controls.Add(DisplayLabel);

            PointSettings.Controls.Add(PickLabelColor);
            PointSettings.Controls.Add(DisplayLabelColor);

            return PointSettings.Height + 2*marginY;
        }
    }
}