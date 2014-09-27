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
            this.SuspendLayout();
            // 
            // listRecursos
            // 
            this.listRecursos.FormattingEnabled = true;
            this.listRecursos.Location = new System.Drawing.Point(13, 13);
            this.listRecursos.Name = "listRecursos";
            this.listRecursos.Size = new System.Drawing.Size(120, 95);
            this.listRecursos.TabIndex = 0;
            // 
            // FrmCiudad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.listRecursos);
            this.Name = "FrmCiudad";
            this.Text = "FrmCiudad";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listRecursos;
    }
}