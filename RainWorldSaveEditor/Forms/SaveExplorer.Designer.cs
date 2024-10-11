namespace RainWorldSaveEditor.Forms
{
    partial class SaveExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            explorerTreeView = new TreeView();
            nodeContextMenuStrip = new ContextMenuStrip(components);
            addToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            saveElementContextMenuStrip = new ContextMenuStrip(components);
            elementListView = new ListView();
            nameColumnHeader = new ColumnHeader();
            typeColumnHeader = new ColumnHeader();
            valueColumnHeader = new ColumnHeader();
            nodeContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // explorerTreeView
            // 
            explorerTreeView.Dock = DockStyle.Left;
            explorerTreeView.Location = new Point(0, 0);
            explorerTreeView.Name = "explorerTreeView";
            explorerTreeView.PathSeparator = ".";
            explorerTreeView.Size = new Size(224, 450);
            explorerTreeView.TabIndex = 0;
            explorerTreeView.AfterSelect += explorerTreeView_AfterSelect;
            // 
            // nodeContextMenuStrip
            // 
            nodeContextMenuStrip.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, deleteToolStripMenuItem });
            nodeContextMenuStrip.Name = "nodeContextMenuStrip";
            nodeContextMenuStrip.Size = new Size(108, 48);
            nodeContextMenuStrip.Opening += nodeContextMenuStrip_Opening;
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(107, 22);
            addToolStripMenuItem.Text = "Add";
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(107, 22);
            deleteToolStripMenuItem.Text = "Delete";
            // 
            // saveElementContextMenuStrip
            // 
            saveElementContextMenuStrip.Name = "saveElementContextMenuStrip";
            saveElementContextMenuStrip.Size = new Size(61, 4);
            // 
            // elementListView
            // 
            elementListView.Columns.AddRange(new ColumnHeader[] { nameColumnHeader, typeColumnHeader, valueColumnHeader });
            elementListView.Dock = DockStyle.Fill;
            elementListView.Location = new Point(224, 0);
            elementListView.Name = "elementListView";
            elementListView.Size = new Size(576, 450);
            elementListView.TabIndex = 3;
            elementListView.UseCompatibleStateImageBehavior = false;
            elementListView.View = View.Details;
            // 
            // nameColumnHeader
            // 
            nameColumnHeader.Text = "Name";
            // 
            // typeColumnHeader
            // 
            typeColumnHeader.Text = "Type";
            // 
            // valueColumnHeader
            // 
            valueColumnHeader.Text = "Value";
            // 
            // SaveExplorer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(elementListView);
            Controls.Add(explorerTreeView);
            Name = "SaveExplorer";
            Text = "SaveExplorer";
            Load += SaveExplorer_Load;
            nodeContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TreeView explorerTreeView;
        private ContextMenuStrip nodeContextMenuStrip;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ContextMenuStrip saveElementContextMenuStrip;
        private ListView elementListView;
        private ColumnHeader nameColumnHeader;
        private ColumnHeader typeColumnHeader;
        private ColumnHeader valueColumnHeader;
    }
}