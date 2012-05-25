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
            if (p_objBaseTableToVisualize.Table.Length > 0)
            {
                DataTemplate objTemplate = DataConverter.GenerateRowTemplate(p_objBaseTableToVisualize.Table[0]);

                dgvVisualizer.ItemsSource = p_objBaseTableToVisualize.Table;
                dgvVisualizer.ItemTemplate = objTemplate;
            }
        }

        #endregion

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
