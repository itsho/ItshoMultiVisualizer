using System;
using System.Collections.Generic;

namespace ItshoMultiVisualizer.VisualizerInfra
{
    [Serializable]
    public class BaseTable
    {
        /// <summary>
        /// List of tables to visualize
        /// </summary>
        private readonly List<VisualizerBaseTable> m_lstTableToVisualize = new List<VisualizerBaseTable>();

        /// <summary>
        /// List of tables to visualize
        /// </summary>
        public List<VisualizerBaseTable> MyTableToVisualize
        {
            get
            {
                return m_lstTableToVisualize;
            }
        }

        #region Ctor

        /// <summary>
        /// Basic Table
        /// </summary>
        /// <param name="p_arrTable">Data source</param>
        /// <param name="p_dicColumns">columns names and types</param>
        /// <param name="p_arrPrimaryKeys">Primary keys</param>
        /// <param name="p_strTableName">Name of table</param>
        public BaseTable(object[][] p_arrTable, Dictionary<string, Type> p_dicColumns, string[] p_arrPrimaryKeys, string p_strTableName)
        {
            VisualizerBaseTable objTableToVisualize = new VisualizerBaseTable(p_arrTable, p_dicColumns, p_arrPrimaryKeys, p_strTableName);
            m_lstTableToVisualize.Add(objTableToVisualize);
        }

        /// <summary>
        /// Hierarchical Table
        /// </summary>
        /// <param name="p_arrTable">Data source</param>
        /// <param name="p_dicColumns">columns names and types</param>
        /// <param name="p_arrPrimaryKeys">Primary keys</param>
        /// <param name="p_strIDField">Id field</param>
        /// <param name="p_strParentID">Parent Id field</param>
        public BaseTable(object[][] p_arrTable, Dictionary<string, Type> p_dicColumns, string[] p_arrPrimaryKeys, string p_strTableName, string p_strIDField, string p_strParentID)
            : this(p_arrTable, p_dicColumns, p_arrPrimaryKeys, p_strTableName)
        {
            m_lstTableToVisualize[0].MyIdField = p_strIDField;
            m_lstTableToVisualize[0].MyParentIdField = p_strParentID;
        }

        /// <summary>
        /// Visualizer for multiple tables (aka DataSet)
        /// </summary>
        /// <param name="p_arrTables">List of base tables</param>
        public BaseTable(IEnumerable<VisualizerBaseTable> p_arrTables)
        {
            m_lstTableToVisualize.AddRange(p_arrTables);
        }

        #endregion
    }
}
