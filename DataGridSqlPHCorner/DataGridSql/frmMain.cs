using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridSql
{
    public partial class frmMain : Form
    {
        //Para sa Mapipiling Row sa DataGrid
        Employee selectedEmp = new Employee();

        public frmMain()
        {
            InitializeComponent();
            Init();
        }
        //Para lang pagsamahin ang lahat ng pagstart ng Application
        private void Init()
        {
            //Gumawa ng Database
            Server.CreateDatabase();
            //I-Load ang Data sa DataGrid
            RefreshGrid();
        }


        //Para sa Pag close ng Application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Galing Sa ContextMenuStrip as Pag-Add ng Employee
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Mag-instantiate ng frmMaintenance
            frmMaintenance fmaint = new frmMaintenance();

            //Sabihin sa frmMaintenance na ang Action na gusto mo ay Add
            fmaint.Action = ActionType.Add;

            //I-checheck kung btnAction ang pinindot
            if (fmaint.ShowDialog() == DialogResult.OK)
            {
                //Kapag napindont yung btnAction Ibig sabihin success ang operation
                //I-refresh ulit ang datagrid
                RefreshGrid();
            }

        }
        //function ng pagrefresh ng DataGridView
        private void RefreshGrid()
        {
            //Ilagay ang resulta ng Server.GetEmployee sa DataGridView
            dataGridView1.DataSource = Server.GetEmployees();
        }
        //function ng magdedetect ng pag-click sa row ng datagridview
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //Kapag hindi Selected ang action sa datagrid return - ibig sabigin walang gawin
            if (e.StateChanged != DataGridViewElementStates.Selected) return;

            //DataGridViewRow lagayan ng row ng DataGridView
            DataGridViewRow dgvr = dataGridView1.Rows[e.Row.Index];

            //I-reset ang selectedEmp para bago lagi ang laman
            selectedEmp = new Employee();

            //Ang pag bilang ng column ay simula sa 0
            //Ang Column = 0 ay EmployeeId depende kung pano mo sinulat ang query
            selectedEmp.EmployeeId = int.Parse(dgvr.Cells[0].Value.ToString());
            selectedEmp.FirstName = dgvr.Cells[1].Value.ToString();
            selectedEmp.LastName = dgvr.Cells[2].Value.ToString();
            selectedEmp.Age = int.Parse(dgvr.Cells[3].Value.ToString());

        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(selectedEmp.EmployeeId<=0)
            {
                MessageBox.Show("No Employee Selected");
                return;
            }
            // Mag - instantiate ng frmMaintenance
            frmMaintenance fmaint = new frmMaintenance();

            //Sabihin sa frmMaintenance na ang Action na gusto mo ay Add
            fmaint.Action = ActionType.Update;

            //Kailangan mong ipasa ang napiling Employee from DataGridView
            fmaint.Employee = selectedEmp;

            //I-checheck kung btnAction ang pinindot
            if (fmaint.ShowDialog() == DialogResult.OK)
            {
                //Kapag napindont yung btnAction Ibig sabihin success ang operation
                //I-refresh ulit ang datagrid
                RefreshGrid();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedEmp.EmployeeId <= 0)
            {
                MessageBox.Show("No Employee Selected");
                return;
            }
            // Mag - instantiate ng frmMaintenance
            frmMaintenance fmaint = new frmMaintenance();

            //Sabihin sa frmMaintenance na ang Action na gusto mo ay Add
            fmaint.Action = ActionType.Delete;

            //Kailangan mong ipasa ang napiling Employee from DataGridView para i-edit
            //Kung walang laman edi walang data na i-uupdate
            fmaint.Employee = selectedEmp;

            //I-checheck kung btnAction ang pinindot
            if (fmaint.ShowDialog() == DialogResult.OK)
            {
                //Kapag napindont yung btnAction Ibig sabihin success ang operation
                //I-refresh ulit ang datagrid
                RefreshGrid();
            }
        }
    }
}
