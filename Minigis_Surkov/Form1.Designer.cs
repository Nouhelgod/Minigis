﻿
namespace Minigis_Surkov
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Pan = new System.Windows.Forms.ToolStripButton();
            this.ZoomIn = new System.Windows.Forms.ToolStripButton();
            this.ZoomOut = new System.Windows.Forms.ToolStripButton();
            this.Select = new System.Windows.Forms.ToolStripButton();
            this.ZoomAll = new System.Windows.Forms.ToolStripButton();
            this.Measure = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.GetValue = new System.Windows.Forms.ToolStripButton();
            this.RestoreGrid = new System.Windows.Forms.ToolStripButton();
            this.testRun = new System.Windows.Forms.ToolStripButton();
            this.saveGridToFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.minToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabelX = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelY = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelMS = new System.Windows.Forms.ToolStripStatusLabel();
            this.activeToolLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonAddLayer = new System.Windows.Forms.Button();
            this.LabelValue = new System.Windows.Forms.Label();
            this.openLayerDialog = new System.Windows.Forms.OpenFileDialog();
            this.colorDialogMain = new System.Windows.Forms.ColorDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.layerControl1 = new Minigis_Surkov.LayerControl();
            this.map1 = new Minigis_Surkov.Map();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Pan,
            this.ZoomIn,
            this.ZoomOut,
            this.Select,
            this.ZoomAll,
            this.Measure,
            this.toolStripSeparator1,
            this.GetValue,
            this.RestoreGrid,
            this.testRun,
            this.saveGridToFileButton,
            this.toolStripSeparator2,
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(896, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // Pan
            // 
            this.Pan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Pan.Image = ((System.Drawing.Image)(resources.GetObject("Pan.Image")));
            this.Pan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Pan.Name = "Pan";
            this.Pan.Size = new System.Drawing.Size(23, 22);
            this.Pan.Text = "Pan";
            this.Pan.Click += new System.EventHandler(this.Pan_Click);
            // 
            // ZoomIn
            // 
            this.ZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ZoomIn.Image")));
            this.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.Size = new System.Drawing.Size(23, 22);
            this.ZoomIn.Text = "Zoom in";
            this.ZoomIn.Click += new System.EventHandler(this.ZoomIn_Click);
            // 
            // ZoomOut
            // 
            this.ZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOut.Image")));
            this.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.Size = new System.Drawing.Size(23, 22);
            this.ZoomOut.Text = "Zoom out";
            this.ZoomOut.Click += new System.EventHandler(this.ZoomOut_Click);
            // 
            // Select
            // 
            this.Select.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Select.Image = ((System.Drawing.Image)(resources.GetObject("Select.Image")));
            this.Select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(23, 22);
            this.Select.Text = "Select";
            this.Select.Click += new System.EventHandler(this.Select_Click);
            // 
            // ZoomAll
            // 
            this.ZoomAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomAll.Image = ((System.Drawing.Image)(resources.GetObject("ZoomAll.Image")));
            this.ZoomAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomAll.Name = "ZoomAll";
            this.ZoomAll.Size = new System.Drawing.Size(23, 22);
            this.ZoomAll.Text = "Fit";
            this.ZoomAll.Click += new System.EventHandler(this.ZoomAll_Click);
            // 
            // Measure
            // 
            this.Measure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Measure.Image = ((System.Drawing.Image)(resources.GetObject("Measure.Image")));
            this.Measure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Measure.Name = "Measure";
            this.Measure.Size = new System.Drawing.Size(23, 22);
            this.Measure.Text = "Measure";
            this.Measure.Click += new System.EventHandler(this.Measure_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // GetValue
            // 
            this.GetValue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GetValue.Image = ((System.Drawing.Image)(resources.GetObject("GetValue.Image")));
            this.GetValue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GetValue.Name = "GetValue";
            this.GetValue.Size = new System.Drawing.Size(23, 22);
            this.GetValue.Text = "Extract value from grid layer";
            this.GetValue.Click += new System.EventHandler(this.GetValue_Click);
            // 
            // RestoreGrid
            // 
            this.RestoreGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RestoreGrid.Image = ((System.Drawing.Image)(resources.GetObject("RestoreGrid.Image")));
            this.RestoreGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RestoreGrid.Name = "RestoreGrid";
            this.RestoreGrid.Size = new System.Drawing.Size(23, 22);
            this.RestoreGrid.Text = "Gridify layer";
            this.RestoreGrid.Click += new System.EventHandler(this.RestoreGrid_Click);
            // 
            // testRun
            // 
            this.testRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.testRun.Image = ((System.Drawing.Image)(resources.GetObject("testRun.Image")));
            this.testRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.testRun.Name = "testRun";
            this.testRun.Size = new System.Drawing.Size(23, 22);
            this.testRun.Text = "Run test ";
            this.testRun.Click += new System.EventHandler(this.testRun_Click);
            // 
            // saveGridToFileButton
            // 
            this.saveGridToFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveGridToFileButton.Image = ((System.Drawing.Image)(resources.GetObject("saveGridToFileButton.Image")));
            this.saveGridToFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveGridToFileButton.Name = "saveGridToFileButton";
            this.saveGridToFileButton.Size = new System.Drawing.Size(23, 22);
            this.saveGridToFileButton.Text = "Save grid layer to file";
            this.saveGridToFileButton.Click += new System.EventHandler(this.saveGridToFileButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minToolStripMenuItem,
            this.maxToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton1.Text = "Change colors";
            // 
            // minToolStripMenuItem
            // 
            this.minToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minToolStripMenuItem.Name = "minToolStripMenuItem";
            this.minToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.minToolStripMenuItem.Text = "Min";
            this.minToolStripMenuItem.Click += new System.EventHandler(this.minToolStripMenuItem_Click);
            // 
            // maxToolStripMenuItem
            // 
            this.maxToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maxToolStripMenuItem.Name = "maxToolStripMenuItem";
            this.maxToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.maxToolStripMenuItem.Text = "Max";
            this.maxToolStripMenuItem.Click += new System.EventHandler(this.maxToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelX,
            this.toolStripStatusLabel1,
            this.statusLabelY,
            this.toolStripStatusLabel3,
            this.statusLabelMS,
            this.activeToolLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 500);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(896, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabelX
            // 
            this.statusLabelX.Name = "statusLabelX";
            this.statusLabelX.Size = new System.Drawing.Size(17, 17);
            this.statusLabelX.Text = "X:";
            this.statusLabelX.Click += new System.EventHandler(this.statusLabelX_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // statusLabelY
            // 
            this.statusLabelY.Name = "statusLabelY";
            this.statusLabelY.Size = new System.Drawing.Size(17, 17);
            this.statusLabelY.Text = "Y:";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // statusLabelMS
            // 
            this.statusLabelMS.Name = "statusLabelMS";
            this.statusLabelMS.Size = new System.Drawing.Size(38, 17);
            this.statusLabelMS.Text = " Tool: ";
            // 
            // activeToolLabel
            // 
            this.activeToolLabel.Name = "activeToolLabel";
            this.activeToolLabel.Size = new System.Drawing.Size(46, 17);
            this.activeToolLabel.Text = "✋ | Pan";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Silver;
            this.splitContainer1.Panel1.Controls.Add(this.buttonAddLayer);
            this.splitContainer1.Panel1.Controls.Add(this.layerControl1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LabelValue);
            this.splitContainer1.Panel2.Controls.Add(this.map1);
            this.splitContainer1.Size = new System.Drawing.Size(896, 475);
            this.splitContainer1.SplitterDistance = 149;
            this.splitContainer1.TabIndex = 2;
            // 
            // buttonAddLayer
            // 
            this.buttonAddLayer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonAddLayer.Location = new System.Drawing.Point(0, 452);
            this.buttonAddLayer.Name = "buttonAddLayer";
            this.buttonAddLayer.Size = new System.Drawing.Size(149, 23);
            this.buttonAddLayer.TabIndex = 3;
            this.buttonAddLayer.Text = "Add layer";
            this.buttonAddLayer.UseVisualStyleBackColor = true;
            this.buttonAddLayer.Click += new System.EventHandler(this.buttonAddLayer_Click);
            // 
            // LabelValue
            // 
            this.LabelValue.AutoSize = true;
            this.LabelValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelValue.Location = new System.Drawing.Point(743, 0);
            this.LabelValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelValue.Name = "LabelValue";
            this.LabelValue.Size = new System.Drawing.Size(0, 13);
            this.LabelValue.TabIndex = 4;
            // 
            // openLayerDialog
            // 
            this.openLayerDialog.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Grid layers (*.grd) | *.grd";
            // 
            // layerControl1
            // 
            this.layerControl1.BackColor = System.Drawing.Color.Silver;
            this.layerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerControl1.Location = new System.Drawing.Point(0, 0);
            this.layerControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layerControl1.Name = "layerControl1";
            this.layerControl1.Size = new System.Drawing.Size(149, 475);
            this.layerControl1.TabIndex = 0;
            this.layerControl1.Load += new System.EventHandler(this.layerControl1_Load);
            // 
            // map1
            // 
            this.map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map1.Location = new System.Drawing.Point(0, 0);
            this.map1.Margin = new System.Windows.Forms.Padding(4);
            this.map1.Name = "map1";
            this.map1.Size = new System.Drawing.Size(743, 475);
            this.map1.TabIndex = 3;
            this.map1.Load += new System.EventHandler(this.map1_Load_1);
            this.map1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.map1_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 522);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "MINIgis-Surkov";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Pan;
        private System.Windows.Forms.ToolStripButton ZoomIn;
        private System.Windows.Forms.ToolStripButton ZoomOut;
        private System.Windows.Forms.ToolStripButton Select;
        private System.Windows.Forms.ToolStripButton ZoomAll;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelY;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelMS;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelX;
        private System.Windows.Forms.ToolStripStatusLabel activeToolLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Map map1;
        private LayerControl layerControl1;
        private System.Windows.Forms.Button buttonAddLayer;
        private System.Windows.Forms.OpenFileDialog openLayerDialog;
        private System.Windows.Forms.ToolStripButton Measure;
        private System.Windows.Forms.ToolStripButton GetValue;
        private System.Windows.Forms.ToolStripButton RestoreGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton testRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem minToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maxToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialogMain;
        private System.Windows.Forms.ToolStripButton saveGridToFileButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label LabelValue;
    }
}

