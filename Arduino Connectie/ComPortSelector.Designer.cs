namespace Arduino_Connectie
{
    partial class ComPortSelector
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
            this.cbbLock = new System.Windows.Forms.ComboBox();
            this.cbbTv = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbbLock
            // 
            this.cbbLock.FormattingEnabled = true;
            this.cbbLock.Location = new System.Drawing.Point(253, 62);
            this.cbbLock.Name = "cbbLock";
            this.cbbLock.Size = new System.Drawing.Size(121, 24);
            this.cbbLock.TabIndex = 0;
            // 
            // cbbTv
            // 
            this.cbbTv.FormattingEnabled = true;
            this.cbbTv.Location = new System.Drawing.Point(253, 150);
            this.cbbTv.Name = "cbbTv";
            this.cbbTv.Size = new System.Drawing.Size(121, 24);
            this.cbbTv.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lock Arduino port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tv Arduino port:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(369, 218);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // ComPortSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 253);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbTv);
            this.Controls.Add(this.cbbLock);
            this.Name = "ComPortSelector";
            this.Text = "ComPortSelector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbLock;
        private System.Windows.Forms.ComboBox cbbTv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSubmit;
    }
}