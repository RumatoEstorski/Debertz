namespace GraphicCardsNewPb
{
    partial class Form1
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
            this.pnlDeck = new System.Windows.Forms.Panel();
            this.pnlP1 = new System.Windows.Forms.Panel();
            this.pnlP2 = new System.Windows.Forms.Panel();
            this.pnlP3 = new System.Windows.Forms.Panel();
            this.pnlTable = new System.Windows.Forms.Panel();
            this.bMove = new System.Windows.Forms.Button();
            this.lP1 = new System.Windows.Forms.Label();
            this.lP2 = new System.Windows.Forms.Label();
            this.lP3 = new System.Windows.Forms.Label();
            this.lInfo = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlDeck
            // 
            this.pnlDeck.Location = new System.Drawing.Point(85, 175);
            this.pnlDeck.Name = "pnlDeck";
            this.pnlDeck.Size = new System.Drawing.Size(175, 230);
            this.pnlDeck.TabIndex = 0;
            // 
            // pnlP1
            // 
            this.pnlP1.Location = new System.Drawing.Point(329, 63);
            this.pnlP1.Name = "pnlP1";
            this.pnlP1.Size = new System.Drawing.Size(491, 146);
            this.pnlP1.TabIndex = 1;
            // 
            // pnlP2
            // 
            this.pnlP2.Location = new System.Drawing.Point(874, 259);
            this.pnlP2.Name = "pnlP2";
            this.pnlP2.Size = new System.Drawing.Size(491, 146);
            this.pnlP2.TabIndex = 2;
            // 
            // pnlP3
            // 
            this.pnlP3.Location = new System.Drawing.Point(329, 470);
            this.pnlP3.Name = "pnlP3";
            this.pnlP3.Size = new System.Drawing.Size(491, 146);
            this.pnlP3.TabIndex = 2;
            // 
            // pnlTable
            // 
            this.pnlTable.Location = new System.Drawing.Point(318, 259);
            this.pnlTable.Name = "pnlTable";
            this.pnlTable.Size = new System.Drawing.Size(491, 146);
            this.pnlTable.TabIndex = 2;
            // 
            // bMove
            // 
            this.bMove.Location = new System.Drawing.Point(891, 26);
            this.bMove.Name = "bMove";
            this.bMove.Size = new System.Drawing.Size(75, 23);
            this.bMove.TabIndex = 3;
            this.bMove.Text = "Move";
            this.bMove.UseVisualStyleBackColor = true;
            this.bMove.Click += new System.EventHandler(this.button1_Click);
            // 
            // lP1
            // 
            this.lP1.AutoSize = true;
            this.lP1.Location = new System.Drawing.Point(326, 22);
            this.lP1.Name = "lP1";
            this.lP1.Size = new System.Drawing.Size(46, 17);
            this.lP1.TabIndex = 4;
            this.lP1.Text = "label1";
            // 
            // lP2
            // 
            this.lP2.AutoSize = true;
            this.lP2.Location = new System.Drawing.Point(871, 225);
            this.lP2.Name = "lP2";
            this.lP2.Size = new System.Drawing.Size(46, 17);
            this.lP2.TabIndex = 5;
            this.lP2.Text = "label1";
            // 
            // lP3
            // 
            this.lP3.AutoSize = true;
            this.lP3.Location = new System.Drawing.Point(326, 441);
            this.lP3.Name = "lP3";
            this.lP3.Size = new System.Drawing.Size(46, 17);
            this.lP3.TabIndex = 6;
            this.lP3.Text = "label1";
            // 
            // lInfo
            // 
            this.lInfo.AutoSize = true;
            this.lInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInfo.Location = new System.Drawing.Point(82, 22);
            this.lInfo.Name = "lInfo";
            this.lInfo.Size = new System.Drawing.Size(0, 25);
            this.lInfo.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1087, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 657);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lInfo);
            this.Controls.Add(this.lP3);
            this.Controls.Add(this.lP2);
            this.Controls.Add(this.lP1);
            this.Controls.Add(this.bMove);
            this.Controls.Add(this.pnlTable);
            this.Controls.Add(this.pnlP3);
            this.Controls.Add(this.pnlP2);
            this.Controls.Add(this.pnlP1);
            this.Controls.Add(this.pnlDeck);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlDeck;
        private System.Windows.Forms.Panel pnlP1;
        private System.Windows.Forms.Panel pnlP2;
        private System.Windows.Forms.Panel pnlP3;
        private System.Windows.Forms.Panel pnlTable;
        private System.Windows.Forms.Button bMove;
        private System.Windows.Forms.Label lP1;
        private System.Windows.Forms.Label lP2;
        private System.Windows.Forms.Label lP3;
        private System.Windows.Forms.Label lInfo;
        private System.Windows.Forms.Button button2;
    }
}

