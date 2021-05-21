using Lightning.Core;
using Lightning.Core.API;
using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;
using System.Windows.Controls;

namespace Polaris.Core
{
    /// <summary>
    /// UIPopulator
    /// 
    /// May 20, 2021 (modified May 21, 2021)
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
            if (PS.Tabs == null)
            {
                ErrorManager.ThrowError("Polaris UI Populator", "PolarisCannotPopulateTabUIWithNoTabsException");
                return; 
            }
            else
            {
                foreach (Tab Tab in PS.Tabs)
                {
                    TabItem TI = new TabItem();
#if DEBUG
                    TI.Header = $"{Tab.FriendlyName} ({Tab.Name})";
#else
                    TI.Header = Tab.FriendlyName;
#endif
                    try
                    {
                        Type NewType = null;

                        string UCClassName = Tab.UserControlClassName;

                        if (UCClassName.Contains('.')) // Load from a namespace
                        {
                            if (XmlUtil.CheckIfValidTypeForInstantiation(UCClassName))
                            {
                                NewType = Type.GetType($"{UCClassName}, {TabCollection.POLARIS_TAB_COLLECTION_NAMESPACE_PATH}");
                            }

                            // might be better to use a result for this? idk?

                        }
                        else
                        {
                            NewType = Type.GetType($"{TabCollection.POLARIS_TAB_COLLECTION_NAMESPACE_PATH}.{UCClassName}, {TabCollection.POLARIS_TAB_COLLECTION_NAMESPACE_PATH}");
                        }

                        if (NewType != null)
                        {
                            if (!NewType.IsSubclassOf(typeof(UserControl)))
                            {
                                ErrorManager.ThrowError("Polaris UI Populator", "PolarisCannotLoadNonUserControlForTabUseException", $"The type {Tab.UserControlClassName} is not a UserControl and cannot be used as a result!");
                                return;
                            }
                            else
                            {
                                object NewTabItem = Activator.CreateInstance(NewType);

                                TI.Content = (UserControl)NewTabItem;

                                TC.Items.Add(TI);

                                if (TC.Items.Count > 0) TC.SelectedIndex = 0; 
                            }
                        }
                        else
                        {
                            ErrorManager.ThrowError("Polaris UI Populator", "PolarisCannotLoadNonexistentUserControlForTabUseException", $"An error occurred loading the type {Tab.UserControlClassName} -- Tab UserControls must be in Polaris.UI if a namespace is not EXPLICTLY specified!");
                            return;
                        }
                        
                    }
                    catch (TypeLoadException err)
                    {
                        ErrorManager.ThrowError("Polaris UI Populator", "PolarisCannotLoadNonexistentUserControlForTabUseException", $"Cannot load the nonexistent type {Tab.UserControlClassName} -- Tab UserControls must be in Polaris.UI if a namespace is not EXPLICTLY specified!", err);
                        return;
                    }
                    catch (Exception err)
                    {
                        ErrorManager.ThrowError("Polaris UI Populator", "PolarisCannotLoadNonexistentUserControlForTabUseException", $"An error occurred loading the type {Tab.UserControlClassName} -- Tab UserControls must be in Polaris.UI if a namespace is not EXPLICTLY specified!", err);
                        return;
                    }
                    
                }
            }
        }
    }
}
