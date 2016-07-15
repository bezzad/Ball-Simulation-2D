namespace Balls
{
    partial class MainForm
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
            this.btnNewObj = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SpeedBar = new System.Windows.Forms.TrackBar();
            this.board = new Balls.Board();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedBar)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNewObj
            // 
            this.btnNewObj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewObj.Location = new System.Drawing.Point(12, 14);
            this.btnNewObj.Name = "btnNewObj";
            this.btnNewObj.Size = new System.Drawing.Size(127, 36);
            this.btnNewObj.TabIndex = 0;
            this.btnNewObj.Text = "Add New Ball";
            this.btnNewObj.UseVisualStyleBackColor = true;
            this.btnNewObj.Click += new System.EventHandler(this.btnNewObj_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.Maroon;
            this.lblCount.Location = new System.Drawing.Point(307, 17);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(23, 25);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "0";
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(145, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 36);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SpeedBar
            // 
            this.SpeedBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedBar.Location = new System.Drawing.Point(399, 12);
            this.SpeedBar.Maximum = 90;
            this.SpeedBar.Name = "SpeedBar";
            this.SpeedBar.Size = new System.Drawing.Size(427, 45);
            this.SpeedBar.TabIndex = 4;
            this.SpeedBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.SpeedBar.Value = 60;
            // 
            // board
            // 
            this.board.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.board.BackColor = System.Drawing.Color.White;
            this.board.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.board.Location = new System.Drawing.Point(12, 63);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(814, 647);
            this.board.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 722);
            this.Controls.Add(this.board);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNewObj);
            this.Controls.Add(this.SpeedBar);
            this.Name = "MainForm";
            this.Text = "Balls";
            ((System.ComponentModel.ISupportInitialize)(this.SpeedBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewObj;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TrackBar SpeedBar;
        private Board board;
    }
}

