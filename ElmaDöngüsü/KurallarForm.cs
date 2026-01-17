using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElmaDöngüsü
{
    public partial class KurallarForm: Form
    {
        public KurallarForm()
        {
            InitializeComponent();

            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;

            btnKapat.BackColor = Color.Transparent;
            btnKapat.FlatStyle = FlatStyle.Flat;
            btnKapat.FlatAppearance.BorderSize = 0; 
            btnKapat.Text = "";
            btnKapat.ForeColor = Color.White;

            btnKapat.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnKapat.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
