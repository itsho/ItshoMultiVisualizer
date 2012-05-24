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

        public VisualizerForm(VisualizerBaseTable p_objBaseTableToVisualize)
        {
            dgvVisualizer.DataContext = p_objBaseTableToVisualize.Table;
        }

        #endregion
    }
}
