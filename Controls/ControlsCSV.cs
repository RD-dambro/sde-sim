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
    public class MyImportCSV
    {

        public int stdButtonWidth = 60;
        public int stdButtonHeight = 22;
        public int marginX = 0;
        public int marginY = 0;
        //---------------------------------------------------------------------------------------------------//

        public int DisplayData(
            int X, int Y, int width, int height,
            System.Windows.Forms.GroupBox DisplayDataSettings,
            System.Windows.Forms.ListView DataTable
        )
        {
            if(marginX == 0) marginX = (int) (width*0.05);
            if(marginY == 0) marginY = (int)(width*0.02);
            int settingsWidth = (int)(width*0.9);

            //
            // DisplayDataSettings groupbox
            //            
            DisplayDataSettings.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | 
                    (System.Windows.Forms.AnchorStyles.Left)| (System.Windows.Forms.AnchorStyles.Right)
                ));
            int DisplayDataSettingsX = marginX + X;
            int DisplayDataSettingsY = marginY + Y;

            DisplayDataSettings.Location = new System.Drawing.Point(DisplayDataSettingsX, DisplayDataSettingsY);

            DisplayDataSettings.Width = settingsWidth;

            DisplayDataSettings.Text = "Data Table";
            DisplayDataSettings.Name = "DisplayDataSettings";

            //
            // DataTable groupbox
            //            
            DataTable.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | 
                    (System.Windows.Forms.AnchorStyles.Left)| (System.Windows.Forms.AnchorStyles.Right)
                ));
            int DataTableX = marginX;
            int DataTableY = 2*marginY + 10;

            DataTable.Location = new System.Drawing.Point(DataTableX, DataTableY);

            DataTable.Width = settingsWidth - 2*marginX;
            
            if(height > 0 ) DataTable.Height = height;
            else DataTable.Height = DataTable.Width;

            DataTable.Name = "DataTable";

            DisplayDataSettings.Height = DataTable.Height + 30;

            DisplayDataSettings.Controls.Add(DataTable);

            return 2*marginY + DisplayDataSettings.Height + 30;
        }


        //---------------------------------------------------------------------------------------------------//

        public int DisplayVariableTypes(
            int X, int Y, int width, 
            System.Windows.Forms.GroupBox DisplayTypesSettings,
            System.Windows.Forms.TreeView TypesTreeView
        )
        {
            if(marginX == 0) marginX = (int) (width*0.05);
            if(marginY == 0) marginY = (int)(width*0.02);
            int settingsWidth = (int)(width*0.9);

            //
            // DisplayTypesSettings groupbox
            //            
            DisplayTypesSettings.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom)
                ));
            int DisplayTypesSettingsX = marginX + X;
            int DisplayTypesSettingsY = marginY + Y;

            DisplayTypesSettings.Location = new System.Drawing.Point(DisplayTypesSettingsX, DisplayTypesSettingsY);

            DisplayTypesSettings.Width = settingsWidth;

            DisplayTypesSettings.Text = "Variable Types";
            DisplayTypesSettings.Name = "DisplayTypesSettings";

            //
            // TypesTreeView groupbox
            //            
            TypesTreeView.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom)
                ));
            int TypesTreeViewX = marginX;
            int TypesTreeViewY = 2*marginY + 10;

            TypesTreeView.Location = new System.Drawing.Point(TypesTreeViewX, TypesTreeViewY);

            TypesTreeView.Width = settingsWidth - 2*marginX;
            TypesTreeView.Height = 100;

            TypesTreeView.Name = "TypesTreeView";

            DisplayTypesSettings.Height = TypesTreeView.Height + 30;

            DisplayTypesSettings.Controls.Add(TypesTreeView);

            return 2*marginY + DisplayTypesSettings.Height + 30;
        }


        //---------------------------------------------------------------------------------------------------//

        public int ImportSettings(
            int X, int Y, int width,
            System.Windows.Forms.GroupBox ImportSettingsSettings,
            System.Windows.Forms.Button DeleteSelectedButton,
            System.Windows.Forms.CheckBox ImportLabels,
            System.Windows.Forms.Label DelimiterLabel,
            System.Windows.Forms.TextBox Delimiter,
            System.Windows.Forms.Button ConfirmButton 
        )
        {
            int marginX = (int) (width*0.05);
            int marginY = (int)(width*0.02);
            int settingsWidth = (int)(width*0.9);

            //
            // ImportSettingsSettings groupbox
            //
            ImportSettingsSettings.Anchor = (
                (System.Windows.Forms.AnchorStyles)(
                    (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Top)
                ));
            
            int ImportSettingsSettingsX = marginX + X;
            int ImportSettingsSettingsY = marginY + Y;

            ImportSettingsSettings.Location = new System.Drawing.Point(ImportSettingsSettingsX, ImportSettingsSettingsY);

            ImportSettingsSettings.Width = settingsWidth;

            ImportSettingsSettings.Text = "Import Settings";
            ImportSettingsSettings.Name = "ImportSettingsSettings";
            
            
            //
            // DeleteSelected Button
            //
            DeleteSelectedButton.Text = "Delete Selected Rows";
            DeleteSelectedButton.Name = "DeleteSelectedButton";

            int DeleteSelectedButtonX = marginX;
            int DeleteSelectedButtonY = 2*marginY + 10;

            DeleteSelectedButton.Location = new System.Drawing.Point(DeleteSelectedButtonX, DeleteSelectedButtonY);

            DeleteSelectedButton.Height = stdButtonHeight;
            DeleteSelectedButton.Width = settingsWidth - 2*marginX;

            DeleteSelectedButton.Enabled = false;
            
            //
            // ImportLabels CheckBox
            //
            int ImportLabelsX = marginX;
            int ImportLabelsY = marginY + DeleteSelectedButtonY + DeleteSelectedButton.Height;

            ImportLabels.Location = new System.Drawing.Point(ImportLabelsX, ImportLabelsY);

            ImportLabels.Width = ImportSettingsSettings.Width - 2*marginX;

            ImportLabels.Checked = true;
            ImportLabels.Text = "Import Labels";
            ImportLabels.Name = "ImportLabels";

            //
            // Delimiter Label
            //
            int DelimiterLabelX = marginX;
            int DelimiterLabelY = marginY + ImportLabelsY + ImportLabels.Height;

            DelimiterLabel.Location = new System.Drawing.Point(DelimiterLabelX, DelimiterLabelY);

            DelimiterLabel.Width = (int)(ImportSettingsSettings.Width*0.6);

            DelimiterLabel.Text = "Custom Delimiter";
            DelimiterLabel.Name = "DelimiterLabel";

            //
            // Delimiter Input
            //
            int DelimiterX = marginX + DelimiterLabelX + DelimiterLabel.Width;
            int DelimiterY = DelimiterLabelY;

            Delimiter.Location = new System.Drawing.Point(DelimiterX, DelimiterY);

            Delimiter.Width = ImportSettingsSettings.Width - DelimiterLabelX - DelimiterLabel.Width - 2*marginX;

            Delimiter.Clear();
            Delimiter.Name = "Delimiter";

            //
            // ConfirmButton Button
            //
            ConfirmButton.Text = "Confirm";
            ConfirmButton.Name = "ConfirmButton";

            int ConfirmButtonX = marginX;
            int ConfirmButtonY = marginY + DelimiterLabelY + DelimiterLabel.Height;

            ConfirmButton.Location = new System.Drawing.Point(ConfirmButtonX, ConfirmButtonY);

            ConfirmButton.Height = stdButtonHeight;
            ConfirmButton.Width = settingsWidth - 2*marginX;

            ConfirmButton.Enabled = false;

            ImportSettingsSettings.Height = 5*marginY + 20 + DeleteSelectedButton.Height + ImportLabels.Height + DelimiterLabel.Height + ConfirmButton.Height;


            ImportSettingsSettings.Controls.Add(DeleteSelectedButton);
            ImportSettingsSettings.Controls.Add(ImportLabels);
            ImportSettingsSettings.Controls.Add(DelimiterLabel);
            ImportSettingsSettings.Controls.Add(Delimiter);
            ImportSettingsSettings.Controls.Add(ConfirmButton);

            // ImportSettingsSettings.Controls.Add(BrowseButton);
            // ImportSettingsSettings.Controls.Add(ClearButton);
            // ImportSettingsSettings.Controls.Add(Path);

            return 2*marginY + ImportSettingsSettings.Height;
        }

        //---------------------------------------------------------------------------------------------------//

        public int ImportCSV (
            int X, int Y, int width,
            System.Windows.Forms.GroupBox ImportDataSettings,
            System.Windows.Forms.Button BrowseButton,
            System.Windows.Forms.Button ClearButton,
            System.Windows.Forms.RichTextBox Path,
            System.Windows.Forms.Button LoadButton 
        )
        {
            
            if(marginX == 0) marginX = (int) (width*0.05);
            if(marginY == 0) marginY = (int)(width*0.02);
            int settingsWidth = (int)(width*0.9);

            //
            // ImportDataSettings groupbox
            //
            // ImportDataSettings.Anchor = (
            //     (System.Windows.Forms.AnchorStyles)(
            //         (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Top)
            //     ));
            
            int ImportDataSettingsX = marginX + X;
            int ImportDataSettingsY = marginY + Y;

            ImportDataSettings.Location = new System.Drawing.Point(ImportDataSettingsX, ImportDataSettingsY);

            ImportDataSettings.Width = settingsWidth;

            ImportDataSettings.Text = "Import Data";
            ImportDataSettings.Name = "ImportDataSettings";
            
            
            //
            // BrowseButton Button
            //
            BrowseButton.Text = "Browse";
            BrowseButton.Name = "BrowseButton";

            int BrowseButtonX = marginX;
            int BrowseButtonY = 2*marginY + 10;

            BrowseButton.Location = new System.Drawing.Point(BrowseButtonX, BrowseButtonY);

            BrowseButton.Height = stdButtonHeight;
            BrowseButton.Width = stdButtonWidth;


            //
            // Path RichTextBox
            //
            Path.Name = "Path";

            int PathX = marginX + BrowseButtonX + BrowseButton.Width;
            int PathY = BrowseButtonY;
            Path.Location = new System.Drawing.Point(PathX, PathY);

            Path.Height = BrowseButton.Height;
            Path.Width = settingsWidth - BrowseButton.Width - BrowseButtonX - 2*marginX;
            
            //
            // ClearButton Button
            //
            ClearButton.Text = "Clear";
            ClearButton.Name = "ClearButton";

            int ClearButtonX = marginX;
            int ClearButtonY = marginY + BrowseButtonY + BrowseButton.Height;

            ClearButton.Location = new System.Drawing.Point(ClearButtonX, ClearButtonY);

            ClearButton.Height = stdButtonHeight;
            ClearButton.Width = stdButtonWidth;

            //
            // LoadButton Button
            //
            LoadButton.Text = "Load";
            LoadButton.Name = "LoadButton";

            int LoadButtonX = marginX + ClearButtonX + ClearButton.Width;
            int LoadButtonY = marginY + BrowseButton.Height + BrowseButtonY;

            LoadButton.Location = new System.Drawing.Point(LoadButtonX, LoadButtonY);

            LoadButton.Height = BrowseButton.Height;
            LoadButton.Width = settingsWidth - 3*marginX - ClearButton.Width;

            ImportDataSettings.Height = 4*marginY + 20 + BrowseButton.Height + LoadButton.Height;

            ImportDataSettings.Controls.Add(BrowseButton);
            ImportDataSettings.Controls.Add(ClearButton);
            ImportDataSettings.Controls.Add(Path);
            ImportDataSettings.Controls.Add(LoadButton);

            return 2*marginY + ImportDataSettings.Height;
        }
    }
}