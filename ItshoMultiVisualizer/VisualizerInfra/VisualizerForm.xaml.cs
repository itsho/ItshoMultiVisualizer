using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ItshoMultiVisualizer.VisualizerInfra;

namespace ItshoMultiVisualizer
{
    /// <summary>
    /// Visualizer Form
    /// </summary>
    public partial class VisualizerForm : Window
    {
        #region Ctor

        public VisualizerForm()
        {
            InitializeComponent();
        }

        public VisualizerForm(VisualizerBaseTable p_objBaseTableToVisualize) : this()
        {
            // If no data sent
            if (p_objBaseTableToVisualize.Table.Length == 0)
            {
                return;
            }

            SetDataToDataGrid(p_objBaseTableToVisualize);

            // Since only one table sent
            objExpander.Visibility = Visibility.Collapsed;
        }

        public VisualizerForm(IList<VisualizerBaseTable> p_lstTables) : this()
        {
            // If no data sent
            if (p_lstTables.Count == 0)
            {
                return;
            }

            SetDataToDataGrid(p_lstTables[0]);

            // Since more than one table sent
            objExpander.Visibility = Visibility.Visible;

            // We use the list of table names
            lstTablesNames.ItemsSource = p_lstTables;

            // Display table names
            lstTablesNames.DisplayMemberPath = "TableName";
        }

        #endregion

        #region Private methods

        private void SetDataToDataGrid(VisualizerBaseTable p_objBaseTableToVisualize)
        {
            // Clear dummy columns from Visual Studio Designer
            dgvVisualizer.Columns.Clear();

            // Set source of data
            dgvVisualizer.ItemsSource = p_objBaseTableToVisualize.Table;

            // TODO: allow user to hide and show columns using another form that will be called VisualizerColumnsManager

            // foreach column
            foreach (string strColumnName in p_objBaseTableToVisualize.MyColumns.Keys)
            {
                // We add a column to the DataGrid
                dgvVisualizer.Columns.Add(new DataGridTextColumn()
                                              {
                                                  // Title of Column
                                                  Header = strColumnName,
                                                  // Data in column is taken with binding from the row in cell number X
                                                  Binding = new Binding("[" + dgvVisualizer.Columns.Count + "]")
                                              });
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        

        private void SetColumnsWidth(DataGridLength p_gridLengthNewValue)
        {
            dgvVisualizer.ColumnWidth = p_gridLengthNewValue;
        }

        private void dgvVisualizer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnFitToCells_Click(object sender, RoutedEventArgs e)
        {
            SetColumnsWidth(DataGridLength.SizeToCells);
        }

        private void btnFitToHeaders_Click(object sender, RoutedEventArgs e)
        {
            SetColumnsWidth(DataGridLength.SizeToHeader);
        }

        private void btnFitToAuto_Click(object sender, RoutedEventArgs e)
        {
            SetColumnsWidth(DataGridLength.Auto);
        }

        #endregion

    }
}
