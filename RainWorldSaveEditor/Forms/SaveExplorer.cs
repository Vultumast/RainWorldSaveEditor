using RainWorldSaveAPI;
using RainWorldSaveAPI.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RainWorldSaveEditor.Forms;

public partial class SaveExplorer : Form
{
    public SaveExplorer()
    {
        InitializeComponent();
    }
    public RainWorldSave Save { get; set; } = new RainWorldSave();

    TreeNode RootNode = null!;

    private void SaveExplorer_Load(object sender, EventArgs e)
    {
        RootNode = explorerTreeView.Nodes.Add("RainWorldSave");

        RootNode.Tag = new SaveExplorerNodeTag(RootNode, Save, typeof(SaveExplorer).GetProperty("Save"),false);

        AddEntriesToTreeNode(Save, RootNode);
    }

    void AddEntriesToTreeNode(object target, TreeNode parentNode)
    {
        foreach (PropertyInfo prop in target.GetType().GetProperties())
        {
            // Attribute? attribute = prop.GetCustomAttribute(typeof(SaveFieldAttribute));
            bool isMultiList = false;
            if (prop.PropertyType.IsGenericType)
                isMultiList = prop.PropertyType.GetGenericTypeDefinition() == typeof(MultiList<>);

            if (isMultiList)
            {
                dynamic prop22 = prop.GetValue(target, null)!;
                var node = parentNode.Nodes.Add(prop.Name);
                var prop2 = target.GetType().GetProperty(prop.Name);
                var aa = prop2.GetValue(target, null);
                node.Tag = new SaveExplorerNodeTag(node, aa, prop, false);

                node.ContextMenuStrip = nodeContextMenuStrip;

                for (var i = 0; i < prop22.Count; i++)
                {
                    Console.WriteLine(prop22.GetType());
                    if (prop22[i].GetType() == typeof(SaveState))
                        node.Nodes.Add(prop22[i].SaveStateNumber);

                    // if (aa is not null)
                    //    AddEntriesToTreeNode(aa, node);
                }
            }
            else if (prop.PropertyType.IsSubclassOf(typeof(SaveElementContainer)) || isMultiList)
            {
                var node = parentNode.Nodes.Add(prop.Name);

                var prop2 = target.GetType().GetProperty(prop.Name);
                
                var aa = prop2.GetValue(target, null);

                node.Tag = new SaveExplorerNodeTag(node, aa, prop, false);
                
                node.ContextMenuStrip = nodeContextMenuStrip;

                if (aa is not null)
                    AddEntriesToTreeNode(aa, node);
            }

        }
    }



    private void nodeContextMenuStrip_Opening(object sender, CancelEventArgs e)
    {
        Console.WriteLine(sender);
    }


    private void explorerTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
        elementListView.Items.Clear();
        if (e.Node.Tag is null)
            return;

        var nodeTag = ((SaveExplorerNodeTag)e.Node.Tag);
        var type = nodeTag.PropertyInfo.PropertyType;

        object parent = Save;

        var pathParts = e.Node.FullPath.Split('.', StringSplitOptions.RemoveEmptyEntries);




        foreach (PropertyInfo prop in type.GetProperties())
        {
            if (prop.PropertyType.IsSubclassOf(typeof(SaveElementContainer)))
                continue;

            var value = "(value was not set)";


            if (nodeTag is not null)
                value = prop.GetValue(nodeTag.Target)?.ToString() ?? "(value was not set)";

            elementListView.Items.Add(new ListViewItem([prop.Name, prop.PropertyType.Name, value], 0));

        }

    }

}
