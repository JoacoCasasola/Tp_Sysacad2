using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms2
{
    public partial class FormasDePago : Form
    {
        public enum TipoPago
        {
            TargetaDébito,
            TargetaCrédito,
            TransferenciaBancaria
        }

        public enum Empresa
        {
            Visa,
            MasterCard,
            AmericanExpress
        }

        public string valorAPagar { get; set; }
        public TipoPago TipoPagoSeleccionado { get; set; }
        public Empresa EmpresaSeleccionada { get; set; }


        public FormasDePago()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(TipoPago));
            comboBox2.DataSource = Enum.GetValues(typeof (Empresa));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormasDePago_Load(object sender, EventArgs e)
        {
            label2.Text += valorAPagar;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string codigo = textBox1.Text;
            string patron = @"^\d{4}$";

            if(Regex.IsMatch(codigo, patron))
            {
                MessageBox.Show($"Pago realizado exitosamente", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show($"Codigo invalido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
