﻿namespace Bai1
{
    partial class Bai1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bai1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnNewFolder = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.treeView = new System.Windows.Forms.TreeView();
            this.listView = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnNewFolder,
            this.btnCopy,
            this.btnDelete,
            this.toolStripButton1,
            this.toolStripTextBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(710, 36);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnOpen
            // 
            this.btnOpen.AutoSize = false;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(76, 34);
            this.btnOpen.Text = "Open";
            // 
            // btnNewFolder
            // 
            this.btnNewFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnNewFolder.Image")));
            this.btnNewFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewFolder.Name = "btnNewFolder";
            this.btnNewFolder.Size = new System.Drawing.Size(101, 33);
            this.btnNewFolder.Text = "New Folder";
            // 
            // btnCopy
            // 
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(69, 33);
            this.btnCopy.Text = "Copy";
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 33);
            this.btnDelete.Text = "Delete";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 33);
            this.toolStripButton1.Text = "Search";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(150, 36);
            this.toolStripTextBox1.Tag = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 325);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(710, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView.Location = new System.Drawing.Point(0, 36);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(227, 289);
            this.treeView.TabIndex = 2;
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.date,
            this.type});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Location = new System.Drawing.Point(227, 36);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(483, 289);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // name
            // 
            this.name.Text = "Name";
            // 
            // date
            // 
            this.date.Text = "Date modified";
            // 
            // type
            // 
            this.type.Text = "Type";
            // 
            // Bai1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 347);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "Bai1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnNewFolder;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader date;
        private System.Windows.Forms.ColumnHeader type;
    }
}
