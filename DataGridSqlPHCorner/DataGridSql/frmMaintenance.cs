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
    public partial class frmMaintenance : Form
    {
        private ActionType _action = ActionType.Add;
        private Employee _employee = new Employee();

        //Property na mag hohold ng value sa loob at labas ng Form. Public para magamit siya sa labas.
        //Para sa Employee na mapipili sa frmMain
        public Employee Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                //Kapag narecieve ang value ilagay ang value sa mga controls
                _employee = value;
                txtEmployeeId.Text = _employee.EmployeeId.ToString();
                txtFName.Text = _employee.FirstName;
                txtLName.Text = _employee.LastName;
                numAge.Value = _employee.Age;
            }
        }

        //Property na mag hohold ng value sa loob at labas ng Form. Public para magamit siya sa labas
        //Para sa Action na ibibigay ng frmmain
        public ActionType Action
        {
            get
            {
                return _action;
            }
            set
            {
                //Kapag narecieve ang value palitan ang display text ng btnAction para makita kung anong operation ang gagawin
                _action = value;
                if (_action == ActionType.Add)
                {
                    btnAction.Text = "Add";
                }
                else if (_action == ActionType.Update)
                {
                    btnAction.Text = "Update";
                }
                else if (_action == ActionType.Delete)
                {
                    btnAction.Text = "Delete";
                }
            }

        }
        public frmMaintenance()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Kapag kin-lick to ibig sabihin walang action na nangyari kaya Cancel ang value ng DialogResult
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            

            //Kailangan mong tiyakin na may laman ang FirstName
            if (string.IsNullOrWhiteSpace(txtFName.Text))
            {
                MessageBox.Show("Please Enter First Name");
                return;
            }
            //Kailangan mong tiyakin na may laman ang LastName
            if (string.IsNullOrWhiteSpace(txtLName.Text))
            {
                MessageBox.Show("Please Enter Last Name");
                return;
            }
            //Ilagay ang value ng control sa Employee Classs
            this.Employee.FirstName = txtFName.Text;
            this.Employee.LastName = txtLName.Text;
            this.Employee.Age = (int)numAge.Value;
            this.Employee.EmployeeId = long.Parse(string.IsNullOrWhiteSpace(txtEmployeeId.Text) ? "0" : txtEmployeeId.Text);
            if (_action == ActionType.Add)
            {
                if (numAge.Value <= 0)
                {
                    MessageBox.Show("Invalid Age");
                    return;
                }
                //Tawaging ang pag-add ng employeer mula sa Server class
                bool returnVal =Server.AddEmployee(this.Employee);
                if (returnVal == false)
                {
                    MessageBox.Show("Problem Adding Employee");
                }

            }
            else if (_action == ActionType.Update)
            {
                if (numAge.Value <= 0)
                {
                    MessageBox.Show("Invalid Age");
                    return;
                }
                //Tawaging ang pag-update ng employeer mula sa Server class
                bool returnVal = Server.UpdateEmployee(this.Employee);
                if (returnVal == false)
                {
                    MessageBox.Show("Problem Updating Employee");
                }
            }
            else if (_action == ActionType.Delete)
            {
                if (MessageBox.Show(String.Format("Are you sure you want to Delete {0}?", this.Employee.LastName + "," + this.Employee.FirstName), "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Tawaging ang pag-delete ng employeer mula sa Server class
                    bool returnVal = Server.DeleteEmployee(this.Employee.EmployeeId);
                    if (returnVal == false)
                    {
                        MessageBox.Show("Problem Deleting Employee");
                    }
                }

            }

            //Para malaman ng tatawag na form tulad ng frmMain na Ok ang value ng DialogResult at pwede na niyang i-refresh ang DataGridView
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    //Enumeration ng value ng action para hindi numbers or text ang ilalagay kundi ang pangalan
    public enum ActionType
    {
        Add = 0
        , Update
        , Delete
    }
}
