using System;
using System.Data;
using System.Windows;
using ItshoMultiVisualizer.Visualizers.DataTable;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace Tester
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Consts

        private const int TOTAL_TEAMS = 50;
        private const int TOTAL_PLAYERS = 5000;

        private const string FLD_PLAYER_ID = "ID";
        private const string FLD_PLAYER_NAME = "PLAYER_NAME";
        private const string FLD_TEAM_ID = "TEAM_ID";

        #endregion

        #region Data members

        /// <summary>
        /// Randomizer
        /// </summary>
        private Random m_objRandom = new Random(5000);

        #endregion

        #region Ctor

        public MainWindow()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Private methods

        private DataTable CreateTempPlayerDataTable()
        {
            DataTable dtTemp = new DataTable("T_TEMP_DATA_TABLE");

            dtTemp.Columns.Add(FLD_PLAYER_ID, typeof(int));
            dtTemp.Columns.Add(FLD_PLAYER_NAME, typeof(string));
            dtTemp.Columns.Add(FLD_TEAM_ID, typeof(int));

            DataColumn[] arrPK = new DataColumn[1];
            arrPK[0] = dtTemp.Columns[FLD_PLAYER_ID];
            dtTemp.PrimaryKey = arrPK;

            for (int intCurrRow = 0; intCurrRow < TOTAL_PLAYERS; intCurrRow++)
            {
                DataRow drNewTeam = dtTemp.NewRow();
                drNewTeam[FLD_PLAYER_ID] = intCurrRow;
                drNewTeam[FLD_PLAYER_NAME] = "Player " + intCurrRow;
                drNewTeam[FLD_TEAM_ID] = intCurrRow % TOTAL_TEAMS;
                dtTemp.Rows.Add(drNewTeam);
            }

            return dtTemp;
        }

        private DataTable CreateDataTableManyColumns(int p_intTotalColumns,int p_intTotalRows)
        {
            DataTable dtTemp = new DataTable("T_MANY_COLUMNS");

            for (int intCurrColumn = 0; intCurrColumn < p_intTotalColumns; intCurrColumn++)
            {
                dtTemp.Columns.Add("COLUMN_" + intCurrColumn, typeof(int));                
            }

            for (int intCurrRow = 0; intCurrRow < p_intTotalRows; intCurrRow++)
            {
                DataRow drNewTeam = dtTemp.NewRow();
                for (int intCurrCol = 0; intCurrCol < p_intTotalColumns; intCurrCol++)
                {
                    drNewTeam[intCurrCol] = m_objRandom.Next(p_intTotalRows - intCurrRow);
                }
                dtTemp.Rows.Add(drNewTeam);
            }

            return dtTemp;
        }

        #endregion

        #region Form Events

        private void btnDataSet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDataTable_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtTemp = CreateTempPlayerDataTable();

            // Hoster of any Visualizer
            VisualizerDevelopmentHost myProxyVisualizerHost =
                new VisualizerDevelopmentHost(dtTemp,
                                              typeof(DataTableDebuggerVisualizer),
                                              typeof(DataTableObjectSource));

            // Here you can Debug the visualizer by using Step Into (F11)
            myProxyVisualizerHost.ShowVisualizer();
        }

        private void btnDataRow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDataView_Click(object sender, RoutedEventArgs e)
        {

        }
        
        #endregion

        private void btnDataTableLotsColumns_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtTemp = CreateDataTableManyColumns(150,50);

            // Hoster of any Visualizer
            VisualizerDevelopmentHost myProxyVisualizerHost =
                new VisualizerDevelopmentHost(dtTemp,
                                              typeof(DataTableDebuggerVisualizer),
                                              typeof(DataTableObjectSource));

            // Here you can Debug the visualizer by using Step Into (F11)
            myProxyVisualizerHost.ShowVisualizer();
        }
    }
}
