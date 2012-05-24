using System;
using System.Collections.Generic;

namespace ItshoMultiVisualizer.VisualizerInfra
{
    [Serializable]
    public class VisualizerBaseTable
    {
        public VisualizerBaseTable(object[][] p_table, Dictionary<string, Type> p_myColumns, string[] p_myPrimaryKey, string p_strTableName)
        {
            MyColumns = p_myColumns;
            MyPrimaryKey = p_myPrimaryKey;
            Table = p_table;
            TableName = p_strTableName;
        }

        #region Properties

        public string TableName { get; private set; }

        /// <summary>
        /// Raw data (without column headers)
        /// </summary>
        public object[][] Table { get; private set; }

        /// <summary>
        /// Id field (mainly for hierarchy)
        /// </summary>
        public string MyIdField { get; set; }

        /// <summary>
        /// Parent field (mainly for hierarchy)
        /// </summary>
        public string MyParentIdField { get; set; }

        /// <summary>
        /// Names of Primary keys of table 
        /// </summary>
        public string[] MyPrimaryKey { get; private set; }

        /// <summary>
        /// Columns dictionary - key is name and value is type
        /// </summary>
        public Dictionary<string, Type> MyColumns { get; private set; }

        #endregion
    }
}
