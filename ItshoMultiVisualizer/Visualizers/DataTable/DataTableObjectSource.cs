using System.Runtime.Serialization.Formatters.Binary;
using ItshoMultiVisualizer.VisualizerInfra;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ItshoMultiVisualizer.Visualizers.DataTable
{
    public class DataTableObjectSource : VisualizerObjectSource
    {
        public override void GetData(object p_target, System.IO.Stream p_outgoingData)
        {
            // Get DataTable from "Magnifier icon" :-)
            System.Data.DataTable dtDataTableToVisualize = (System.Data.DataTable)p_target;

            // Convert it to base table
            BaseTable objTableToVisualize = DataConverter.DataTableToBaseData(dtDataTableToVisualize);

            // Convert to stream using BinaryFormatter (NOT by the Microsoft.VisualStudio.DebuggerVisualizers)
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(p_outgoingData, objTableToVisualize);
        }
    }
}
