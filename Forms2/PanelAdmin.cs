using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms2
{
    public partial class PanelAdmin : Form
    {
        public PanelAdmin()
        {
            InitializeComponent();
            PersonalizarDiseño();
        }

        public void PersonalizarDiseño()
        {
            panelRegistro.Visible = false;
            panelDeReportes.Visible = false;
            panelListaEspera.Visible = false;
        }

        public void OcultarSubMenu()
        {
            if(panelRegistro.Visible)
            {
                panelRegistro.Visible = false;
            }
            if(panelDeReportes.Visible)
            {
                panelDeReportes.Visible = false;
            }
            if (panelListaEspera.Visible)
            {
                panelListaEspera.Visible = false;
            }
        }



        public void MostrarSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                OcultarSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }



        private void btnRegistro_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelRegistro);
        }

        private void btnRegistrarAlumno_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new FormRegistrarAlumno());
            OcultarSubMenu();
        }

        private void btnRegistrarProfesor_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new FormRegistrarProfesor());
            OcultarSubMenu();
        }

        private void btnRegistrarAdmin_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new FormRegistrarAdmin());
            OcultarSubMenu();
        }





        private void btnGestionCursos_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new InfoCursos());
        }





        private void btnReportes_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelDeReportes);
        }
        private void btnReporte1_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
        }

        private void btnReporte2_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new ReporteAlumnosCurso());
            OcultarSubMenu();
        }

        private void btnReporte3_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new ReporteConceptoPago());
            OcultarSubMenu();
        }

        private void btnReporte4_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new ReporteAlumnosCarrera());
            OcultarSubMenu();
        }

        private void btnReporte5_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new ReporteListaEspera());
            OcultarSubMenu();
        }





        private void button1_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelListaEspera);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new EliminaAlumnoLista());
            OcultarSubMenu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new AgregarAlumnoLista());
            OcultarSubMenu();
        }


        private void btnInfoUsuario_Click_1(object sender, EventArgs e)
        {
            AbrirFormHijo(new InfoAdmin());
            OcultarSubMenu();
        }



        private Form activarForm = null;
        private void AbrirFormHijo(Form formHijo)
        {
            if(activarForm != null)
            {
                activarForm.Close();
            }
            activarForm = formHijo;
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            panelHijo.Controls.Add(formHijo);
            panelHijo.Tag = formHijo;
            formHijo.BringToFront();
            formHijo.Show();
        }

        
    }
}

