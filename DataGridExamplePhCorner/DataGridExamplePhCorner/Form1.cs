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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected List<Emp> GetEmpList()
        {
            List<Emp> lEmp = new List<Emp>();
            Emp oemp = new Emp(1234, "Devesh Omar", "GZB");
            lEmp.Add(oemp);
            oemp = new Emp(1234, "ROLI", "GZB");
            lEmp.Add(oemp);
            oemp = new Emp(1235, "ROLI", "MainPuri");
            lEmp.Add(oemp);
            oemp = new Emp(1236, "ROLI", "Kanpur");
            lEmp.Add(oemp);
            oemp = new Emp(1237, "Manish Omar", "GZB");
            lEmp.Add(oemp);
            oemp = new Emp(1238, "ROLI1", "MainPuri");
            lEmp.Add(oemp);
            oemp = new Emp(1239, "ROLI2", "MainPuri");
            lEmp.Add(oemp);
            oemp = new Emp(1230, "ROLI3", "CNB");
            lEmp.Add(oemp);
            oemp = new Emp(1231, "ROLI4", "CNB-UP");
            lEmp.Add(oemp);
            oemp = new Emp(1232, "ROLI5", "GHAZIABAD");
            lEmp.Add(oemp);
            oemp = new Emp(1233, "ROLI6", "UP");
            lEmp.Add(oemp);
            return lEmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetEmpList();
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;

            DataGridViewRow dgvr = dataGridView1.Rows[e.Row.Index];
            Emp emp = new Emp();
            emp.ID = int.Parse(dgvr.Cells[0].Value.ToString());
            emp.Name= dgvr.Cells[1].Value.ToString();
            emp.City= dgvr.Cells[2].Value.ToString();
            Form2 f2 = new Form2();
            f2.Employee = emp;
            f2.ShowDialog();
        }
    }
}
