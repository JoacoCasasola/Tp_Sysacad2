﻿using Forms2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libreriaForms
{
    public partial class PanelAlumno : Form
    {
        public PanelAlumno()
        {
            InitializeComponent();
            PersonalizarDiseño();
        }

        public void PersonalizarDiseño()
        {
            panelMenu.Visible = false;
        }

        public void OcultarSubMenu()
        {
            if (panelMenu.Visible)
            {
                panelMenu.Visible = false;
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


        private void btnMenu_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelMenu);
        }

        private void btnInscripcionCurso_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new InscripcionACurso());
            OcultarSubMenu();
        }

        private void btnConsultaHorario_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new ConsultaDeHorarios());
            OcultarSubMenu();
        }

        private void btnRegistrarPagos_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new PagosAlumno());
            OcultarSubMenu();
        }



        private void btnAtuda_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new InfoAlumno());
            OcultarSubMenu();
        }



        private Form activarForm = null;
        private void AbrirFormHijo(Form formHijo)
        {
            if (activarForm != null)
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
