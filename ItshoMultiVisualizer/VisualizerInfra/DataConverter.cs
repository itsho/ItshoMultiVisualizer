using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ItshoMultiVisualizer.VisualizerInfra
{
    internal class DataConverter
    {

        private const string SINGLE_ROW_TEMPLATE_KEY = "SingleRowTemplate";

        #region Private methods

        private static object[][] DataTableToMatrix(DataTable p_dtDataTable)
        {
            // raw-data matrix
            object[][] arrMatrix = new object[p_dtDataTable.Rows.Count][];

            // initialize whole matrix
            for (int intCurrRow = 0; intCurrRow < p_dtDataTable.Rows.Count; intCurrRow++)
            {
                arrMatrix[intCurrRow] = new object[p_dtDataTable.Columns.Count];
            }

            // for each row
            for (int intCurrRow = 0; intCurrRow < p_dtDataTable.Rows.Count; intCurrRow++)
            {
                // copy all data from row in one shot (instead of coping each cell)
                arrMatrix[intCurrRow] = p_dtDataTable.Rows[intCurrRow].ItemArray;
            }

            return arrMatrix;
        }

        #endregion

        #region Public methods

        public static DataTable BaseTableToDataTable(VisualizerBaseTable p_objInnerTable)
        {
            // New DataTable
            DataTable dtNormalDataTable = new DataTable(p_objInnerTable.TableName);

            // Create columns by type
            foreach (string strColName in p_objInnerTable.MyColumns.Keys)
            {
                dtNormalDataTable.Columns.Add(strColName, p_objInnerTable.MyColumns[strColName]);
            }

            // Create Primary Key array
            DataColumn[] arrPK = new DataColumn[p_objInnerTable.MyPrimaryKey.Length];
            for (int intCurrPk = 0; intCurrPk < p_objInnerTable.MyPrimaryKey.Length; intCurrPk++)
            {
                arrPK[intCurrPk] = dtNormalDataTable.Columns[p_objInnerTable.MyPrimaryKey[intCurrPk]];
            }

            // User primary key array
            dtNormalDataTable.PrimaryKey = arrPK;

            // For each existing row
            foreach (object[] objCurrRow in p_objInnerTable.Table)
            {
                // new row
                DataRow drNewRow = dtNormalDataTable.NewRow();

                // copy all data from row in one shot (instead of coping each cell)
                drNewRow.ItemArray = objCurrRow;

                // add row to new table
                dtNormalDataTable.Rows.Add(drNewRow);
            }

            return dtNormalDataTable;
        }

        public static BaseTable DataSetToVisualizerBase(DataSet p_objDataSetToVisualize)
        {
            // List of bast type tables
            VisualizerBaseTable[] arrTables = new VisualizerBaseTable[p_objDataSetToVisualize.Tables.Count];

            // for each table in DataSet
            for (int intCurrTable = 0; intCurrTable < p_objDataSetToVisualize.Tables.Count; intCurrTable++)
            {
                #region Convert DataTable to Base Table 

                // Get current Table
                DataTable dtSingleTableFromDS = p_objDataSetToVisualize.Tables[intCurrTable];

                // Create Dictionary of columns and types
                Dictionary<string, Type> dicColumns = new Dictionary<string, Type>();
                foreach (DataColumn column in dtSingleTableFromDS.Columns)
                {
                    dicColumns.Add(column.ColumnName, column.DataType);
                }

                // Create array of Primary-key columns
                string[] arrPrimaryKeys = new string[dtSingleTableFromDS.PrimaryKey.Length];
                for (int intCurrPK = 0; intCurrPK < arrPrimaryKeys.Length; intCurrPK++)
                {
                    arrPrimaryKeys[intCurrPK] = dtSingleTableFromDS.PrimaryKey[intCurrPK].ColumnName;
                }


                // Create Base table from current DataTable
                VisualizerBaseTable objSingleTableFromDS = new VisualizerBaseTable(DataTableToMatrix(dtSingleTableFromDS),
                                                          dicColumns,
                                                          arrPrimaryKeys,
                                                          dtSingleTableFromDS.TableName);

                // Ver2: prepare to Hierarchical DataView within Dataset
                // Ver2: objSingleTableFromDS.MyIdField = objHierarchicalDV.MyIDField;
                // Ver2: objSingleTableFromDS.MyParentIdField = objHierarchicalDV.MyParentIDField;

                #endregion

                // Add Base table to array of tables
                arrTables[intCurrTable] = objSingleTableFromDS;
            }

            // Create base object to that can go between debugger and debugee
            BaseTable objVisualizer = new BaseTable(arrTables);

            return objVisualizer;
        }

        public static BaseTable DataTableToBaseData(DataTable p_dtDataTableToVisualize)
        {
            #region Preparation

            // Create columns dictionary with types (key - tablename, value - type)
            Dictionary<string, Type> dicColumns = new Dictionary<string, Type>();
            foreach (DataColumn objDataColumn in p_dtDataTableToVisualize.Columns)
            {
                dicColumns.Add(objDataColumn.ColumnName, objDataColumn.DataType);
            }

            // Create array of primary key columns
            string[] arrPrimaryKeys = new string[p_dtDataTableToVisualize.PrimaryKey.Length];

            // for each primary column
            for (int intCurrPK = 0; intCurrPK < p_dtDataTableToVisualize.PrimaryKey.Length; intCurrPK++)
            {
                arrPrimaryKeys[intCurrPK] = p_dtDataTableToVisualize.PrimaryKey[intCurrPK].ColumnName;
            }

            #endregion

            // Create base object to that can go between debugger and debugee
            BaseTable objTableToVisualize =
                new BaseTable(DataTableToMatrix(p_dtDataTableToVisualize),
                                         dicColumns,
                                         arrPrimaryKeys,
                                         p_dtDataTableToVisualize.TableName);

            return objTableToVisualize;
        }

        [Obsolete("Generate Columns with foreach instead",true)]
        public static DataTemplate GenerateRowTemplate(object[] p_arrCellsInRow)
        {
            DataTemplate singleRowTemplate = new DataTemplate();
            {
                WrapPanel wrapPanel = new WrapPanel();

                //for each cell
                for (int intCurrCell = 0; intCurrCell < p_arrCellsInRow.Length; intCurrCell++)
                {
                    object objCell = p_arrCellsInRow[intCurrCell];
                    Label textBlockSingleCell = new Label();

                    if (objCell != null)
                    {
                        textBlockSingleCell.Content = new Binding("[" + intCurrCell + "]");
                    }
                    // Add cell to wrap panel
                    wrapPanel.Children.Add(textBlockSingleCell);
                }

                // Add wrapPanel to DataTemplate
                singleRowTemplate.Resources.Add(SINGLE_ROW_TEMPLATE_KEY, wrapPanel);
            }

            return singleRowTemplate;
        }

        #endregion
    }
}
