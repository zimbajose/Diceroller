namespace Diceroller
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.expressionbox = new System.Windows.Forms.TextBox();
            this.textformlabel = new System.Windows.Forms.Label();
            this.rollbutton = new System.Windows.Forms.Button();
            this.resultlabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // expressionbox
            // 
            this.expressionbox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.expressionbox.Location = new System.Drawing.Point(233, 228);
            this.expressionbox.Name = "expressionbox";
            this.expressionbox.Size = new System.Drawing.Size(307, 23);
            this.expressionbox.TabIndex = 0;
            // 
            // textformlabel
            // 
            this.textformlabel.AutoSize = true;
            this.textformlabel.Location = new System.Drawing.Point(319, 210);
            this.textformlabel.Name = "textformlabel";
            this.textformlabel.Size = new System.Drawing.Size(105, 15);
            this.textformlabel.TabIndex = 1;
            this.textformlabel.Text = "Rolagem de dados\r\n";
            this.textformlabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // rollbutton
            // 
            this.rollbutton.Location = new System.Drawing.Point(573, 205);
            this.rollbutton.Name = "rollbutton";
            this.rollbutton.Size = new System.Drawing.Size(75, 67);
            this.rollbutton.TabIndex = 2;
            this.rollbutton.Text = "Rolar";
            this.rollbutton.UseVisualStyleBackColor = true;
            this.rollbutton.Click += new System.EventHandler(this.rollbutton_Click);
            // 
            // resultlabel
            // 
            this.resultlabel.AutoSize = true;
            this.resultlabel.Location = new System.Drawing.Point(233, 295);
            this.resultlabel.Name = "resultlabel";
            this.resultlabel.Size = new System.Drawing.Size(62, 15);
            this.resultlabel.TabIndex = 3;
            this.resultlabel.Text = "Resultado:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 450);
            this.Controls.Add(this.resultlabel);
            this.Controls.Add(this.rollbutton);
            this.Controls.Add(this.textformlabel);
            this.Controls.Add(this.expressionbox);
            this.Name = "Form1";
            this.Text = "Rola dados";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox expressionbox;
        private Label textformlabel;
        private Button rollbutton;
        private Label resultlabel;
    }
}