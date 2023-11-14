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
    public partial class PanelAdmin : Form
    {
        private PanelAdmin panelAdmin;

        public PanelAdmin()
        {
            InitializeComponent();
            Diseño();
        }

        private void FormUsuario_BotonEjecutado()
        {
            button1.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
        }


        private void Diseño()
        {
            panel1.Visible = false;
            panel2.Visible = false;
            button1.Visible = false;
        }


        private void OcultarSubMenu()
        {
            if (panel1.Visible == true)
            {
                panel1.Visible = false;
            }
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
        }

        private void MostrarSubMenu(Panel subMenu)
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


        private void button1_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panel1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
        }





        private void button5_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panel2);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
        }



        private void button9_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
        }
    }
}
