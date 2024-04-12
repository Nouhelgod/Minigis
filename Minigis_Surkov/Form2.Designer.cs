namespace Minigis_Surkov
{
    partial class formGridTune
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textWest = new System.Windows.Forms.TextBox();
            this.textNorth = new System.Windows.Forms.TextBox();
            this.textSouth = new System.Windows.Forms.TextBox();
            this.textEast = new System.Windows.Forms.TextBox();
            this.textRadius = new System.Windows.Forms.TextBox();
            this.textCellSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.kfactor = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kfactor)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "East";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "North";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "West";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(113, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "South";
            // 
            // textWest
            // 
            this.textWest.Location = new System.Drawing.Point(10, 64);
            this.textWest.Name = "textWest";
            this.textWest.Size = new System.Drawing.Size(100, 20);
            this.textWest.TabIndex = 6;
            this.textWest.Text = "0";
            // 
            // textNorth
            // 
            this.textNorth.Location = new System.Drawing.Point(116, 25);
            this.textNorth.Name = "textNorth";
            this.textNorth.Size = new System.Drawing.Size(100, 20);
            this.textNorth.TabIndex = 7;
            this.textNorth.Text = "0";
            // 
            // textSouth
            // 
            this.textSouth.Location = new System.Drawing.Point(116, 103);
            this.textSouth.Name = "textSouth";
            this.textSouth.Size = new System.Drawing.Size(100, 20);
            this.textSouth.TabIndex = 8;
            this.textSouth.Text = "0";
            // 
            // textEast
            // 
            this.textEast.Location = new System.Drawing.Point(217, 64);
            this.textEast.Name = "textEast";
            this.textEast.Size = new System.Drawing.Size(100, 20);
            this.textEast.TabIndex = 9;
            this.textEast.Text = "0";
            // 
            // textRadius
            // 
            this.textRadius.Location = new System.Drawing.Point(336, 64);
            this.textRadius.Name = "textRadius";
            this.textRadius.Size = new System.Drawing.Size(100, 20);
            this.textRadius.TabIndex = 10;
            this.textRadius.Text = "1000";
            // 
            // textCellSize
            // 
            this.textCellSize.Location = new System.Drawing.Point(336, 24);
            this.textCellSize.Name = "textCellSize";
            this.textCellSize.Size = new System.Drawing.Size(100, 20);
            this.textCellSize.TabIndex = 11;
            this.textCellSize.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(333, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Cell size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(333, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Radius";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(336, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // kfactor
            // 
            this.kfactor.Location = new System.Drawing.Point(336, 103);
            this.kfactor.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.kfactor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.kfactor.Name = "kfactor";
            this.kfactor.Size = new System.Drawing.Size(100, 20);
            this.kfactor.TabIndex = 15;
            this.kfactor.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(336, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "K factor";
            // 
            // formGridTune
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 229);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.kfactor);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textCellSize);
            this.Controls.Add(this.textRadius);
            this.Controls.Add(this.textEast);
            this.Controls.Add(this.textSouth);
            this.Controls.Add(this.textNorth);
            this.Controls.Add(this.textWest);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "formGridTune";
            this.Text = "Model recalculation";
            ((System.ComponentModel.ISupportInitialize)(this.kfactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textWest;
        private System.Windows.Forms.TextBox textNorth;
        private System.Windows.Forms.TextBox textSouth;
        private System.Windows.Forms.TextBox textEast;
        private System.Windows.Forms.TextBox textRadius;
        private System.Windows.Forms.TextBox textCellSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown kfactor;
        private System.Windows.Forms.Label label7;
    }
}