namespace CivWin
{
    partial class FrmCiudad
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
            this.listRecursos = new System.Windows.Forms.ListBox();
            this.textInfo = new System.Windows.Forms.TextBox();
            this.listEdificios = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listRecursos
            // 
            this.listRecursos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listRecursos.FormattingEnabled = true;
            this.listRecursos.Location = new System.Drawing.Point(13, 13);
            this.listRecursos.MultiColumn = true;
            this.listRecursos.Name = "listRecursos";
            this.listRecursos.Size = new System.Drawing.Size(120, 290);
            this.listRecursos.TabIndex = 0;
            // 
            // textInfo
            // 
            this.textInfo.Location = new System.Drawing.Point(140, 13);
            this.textInfo.Multiline = true;
            this.textInfo.Name = "textInfo";
            this.textInfo.ReadOnly = true;
            this.textInfo.Size = new System.Drawing.Size(182, 118);
            this.textInfo.TabIndex = 1;
            // 
            // listEdificios
            // 
            this.listEdificios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listEdificios.FormattingEnabled = true;
            this.listEdificios.Location = new System.Drawing.Point(140, 138);
            this.listEdificios.Name = "listEdificios";
            this.listEdificios.Size = new System.Drawing.Size(182, 160);
            this.listEdificios.TabIndex = 2;
            // 
            // FrmCiudad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 315);
            this.Controls.Add(this.listEdificios);
            this.Controls.Add(this.textInfo);
            this.Controls.Add(this.listRecursos);
            this.Name = "FrmCiudad";
            this.Text = "FrmCiudad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listRecursos;
        private System.Windows.Forms.TextBox textInfo;
        private System.Windows.Forms.ListBox listEdificios;
    }
}