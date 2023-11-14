namespace Forms2
{
    partial class InfoCursos
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
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button5 = new System.Windows.Forms.Button();
            this.nombreCurso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idCorso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionCurso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cupoActualCurso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cupoMaxCurso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.horarioCurso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 20F);
            this.label4.Location = new System.Drawing.Point(127, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 35);
            this.label4.TabIndex = 8;
            this.label4.Text = "Gestion de cursos";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(12, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 21);
            this.button1.TabIndex = 9;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(69, 316);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 21);
            this.button2.TabIndex = 10;
            this.button2.Text = "Agregar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(186, 316);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 21);
            this.button3.TabIndex = 11;
            this.button3.Text = "Editar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(303, 316);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 21);
            this.button4.TabIndex = 12;
            this.button4.Text = "Eliminar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombreCurso,
            this.idCorso,
            this.descripcionCurso,
            this.cupoActualCurso,
            this.cupoMaxCurso,
            this.horarioCurso});
            this.dataGridView1.Location = new System.Drawing.Point(12, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(444, 249);
            this.dataGridView1.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(12, 34);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 21);
            this.button5.TabIndex = 13;
            this.button5.Text = "Actualizar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // nombreCurso
            // 
            this.nombreCurso.HeaderText = "Nombre";
            this.nombreCurso.Name = "nombreCurso";
            this.nombreCurso.ReadOnly = true;
            this.nombreCurso.Width = 90;
            // 
            // idCorso
            // 
            this.idCorso.HeaderText = "ID";
            this.idCorso.Name = "idCorso";
            this.idCorso.ReadOnly = true;
            this.idCorso.Width = 70;
            // 
            // descripcionCurso
            // 
            this.descripcionCurso.HeaderText = "Descripcion";
            this.descripcionCurso.Name = "descripcionCurso";
            this.descripcionCurso.ReadOnly = true;
            this.descripcionCurso.Width = 90;
            // 
            // cupoActualCurso
            // 
            this.cupoActualCurso.HeaderText = "Cupo Actual";
            this.cupoActualCurso.Name = "cupoActualCurso";
            this.cupoActualCurso.ReadOnly = true;
            this.cupoActualCurso.Width = 50;
            // 
            // cupoMaxCurso
            // 
            this.cupoMaxCurso.HeaderText = "Cupo Max.";
            this.cupoMaxCurso.Name = "cupoMaxCurso";
            this.cupoMaxCurso.ReadOnly = true;
            this.cupoMaxCurso.Width = 50;
            // 
            // horarioCurso
            // 
            this.horarioCurso.HeaderText = "Horario";
            this.horarioCurso.Name = "horarioCurso";
            this.horarioCurso.ReadOnly = true;
            this.horarioCurso.Width = 120;
            // 
            // InfoCursos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(468, 372);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Name = "InfoCursos";
            this.Text = "InfoCursos";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreCurso;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCorso;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionCurso;
        private System.Windows.Forms.DataGridViewTextBoxColumn cupoActualCurso;
        private System.Windows.Forms.DataGridViewTextBoxColumn cupoMaxCurso;
        private System.Windows.Forms.DataGridViewTextBoxColumn horarioCurso;
    }
}