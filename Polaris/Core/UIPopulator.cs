using Lightning.Core;
using Lightning.Core.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Polaris.Core
{
    /// <summary>
    /// UIPopulator
    /// 
    /// May 20, 2021
    /// 
    /// Populates UI.
    /// </summary>
    public class UIPopulator
    {
        public void PopulateTreeView(PolarisState PS, TreeView TreeView)
        {
            if (PS.DataModel == null)
            {
                ErrorManager.ThrowError("Polaris UI Populator", "PolarisNoDataModelLoadedException");
                return; 
            }
            else
            {
                DataModel NewDataModel = PS.DataModel;

                Workspace Ws = DataModel.GetWorkspace();

                TreeView.Items.Add(Ws);

                // Not sure if we can implement ObservableCollection here without having tons of annoying hacky code as Lightning
                // obviously doesn't depend on WPF...

                // Implement a "fake" ObservableCollection and wrap a real one around it perhaps?

                GetMultiInstanceResult GMIR = Ws.GetChildren();

                if (GMIR.Successful
                    && GMIR.Instances != null)
                {
                    List<Instance> InstanceChildren = GMIR.Instances;

                    foreach (Instance ID in InstanceChildren)
                    {
                        TreeView.Items.Add(ID);
                    }
                }

            }
        }

        public void PopulateTabs(PolarisState PS, TabControl TC)
        {

        }
    }
}
