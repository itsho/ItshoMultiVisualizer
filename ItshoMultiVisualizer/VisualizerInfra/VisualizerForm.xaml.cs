using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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


            DataTemplate objTemplate = DataConverter.GenerateRowTemplate(p_objBaseTableToVisualize.Table[0]);

            dgvVisualizer.ItemsSource = p_objBaseTableToVisualize.Table;
            dgvVisualizer.ItemTemplate = objTemplate;

            // If only one table sent
            if (p_objBaseTableToVisualize.Table.Length == 1)
            {
                lstTablesNames.Visibility = Visibility.Collapsed;
            }
            // If more than one table sent
            else
            {
                lstTablesNames.Visibility = Visibility.Visible;
            }
        }

        #endregion

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
