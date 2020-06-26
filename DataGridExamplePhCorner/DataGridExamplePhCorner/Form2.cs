using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridExamplePhCorner
{
    public partial class Form2 : Form
    {
        private Emp _emp = new Emp();
        public Emp Employee
        {
            get
            {
                return _emp;
            }
            set
            {
                _emp = value;
                txtID.Text = _emp.ID.ToString();
                txtName.Text = _emp.Name;
                txtCity.Text = _emp.City;
            }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
