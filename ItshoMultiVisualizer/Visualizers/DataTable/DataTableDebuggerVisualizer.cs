using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ItshoMultiVisualizer.VisualizerInfra;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ItshoMultiVisualizer.Visualizers.DataTable
{
    public class DataTableDebuggerVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService p_windowService, IVisualizerObjectProvider p_objectProvider)
        {
            try
            {
                // Get data from VisualizerBase as Stream
                using (Stream objStreamData = p_objectProvider.GetData())
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    // Create instance from that stream
                    BaseTable objBaseVisualizer = (BaseTable)bf.Deserialize(objStreamData);

                    // We get the first table in list (since we only use this one in DataTable visualizer)
                    VisualizerBaseTable objTable = objBaseVisualizer.MyTableToVisualize[0];

                    // We create new Form 
                    VisualizerForm frmVisualizerForm = new VisualizerForm(objTable);

                    // And walla
                    frmVisualizerForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                UiHelper.MyMessageBox("Error while opening " + UiHelper.APP_NAME + " - DataTable Visualizer", ex);
            }
        }
    }
}
