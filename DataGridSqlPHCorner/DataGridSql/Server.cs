using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;

namespace DataGridSql
{
    //static para hindi mo na kailangang gawan ng variable
    //Halimabawa kung wala siyang static 
    // Server server=new Server();
    // Dahil sa static diretso na ang gamit.
    public static class Server
    {
        //Ang Application.StartPath ay kung saan nakalagay ang nabuong .exe nitong program.
        static string location = Application.StartupPath;

        //pangalan ng gusto mong database.
        static string filename = "mydatabase.db";
        //Pinagsamang location at filename
        static string fullpath = Path.Combine(location, filename);

        //Eto ang property para makuha ang value ng Connection string
        public static string ConnectionString
        {
            get
            {
                //Connection String ng SQLite
                return String.Format("Data Source= {0}", fullpath);
            }
        }

        //Pag-gawa ng SQLite Database
        public static void CreateDatabase()
        {
            if (!File.Exists(fullpath))
            {
                //SQL sa paggawa ng Table 
                string createTable = "CREATE TABLE `Employee` ("
                    + "`employee_id`	INTEGER UNIQUE,"
                    + "`employee_fname`	TEXT,"
                    + "`employee_lname`	TEXT,"
                    + "`employee_age`	INTEGER,"
                    + "PRIMARY KEY(`employee_id` AUTOINCREMENT)"
                    + "); ";

                //Ang using ay ginagamit para malimit ang scope ng Variable
                //At madaling maalis sa memonry
                //SQLiteConnection ay Class na Gamit ng SQLite sa paggawa ng connection
                using (SQLiteConnection sqlCon = new SQLiteConnection(ConnectionString))
                {
                    //SQLiteCommand ay class sa pag execute ng command sa SQLite
                    SQLiteCommand cmd = new SQLiteCommand(createTable, sqlCon);

                    //I-Open ang connection
                    sqlCon.Open();

                    //I-execute ang command
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static bool CheckIfEmployeeExist(Employee employee)
        {
            //Ang using ay ginagamit para malimit ang scope ng Variable
            //At madaling maalis sa memonry
            //SQLiteConnection ay Class na Gamit ng SQLite sa paggawa ng connection
            using (SQLiteConnection sqlCon = new SQLiteConnection(ConnectionString))
            {
                //Query sa pagkuha ng laman ng table bases sa Employee information
                string insertCommand = "SELECT * from Employee where employee_fname=@fname and employee_lname=@lname  and employee_age=@age";

                //SQLiteCommand ay class sa pag execute ng command sa SQLite
                SQLiteCommand cmd = new SQLiteCommand(insertCommand, sqlCon);

                //I-pasa ang value ng employee.FirstName sa @fname
                cmd.Parameters.AddWithValue("@fname", employee.FirstName);

                //I-pasa ang value ng employee.LastName sa @lname
                cmd.Parameters.AddWithValue("@lname", employee.LastName);

                //I-pasa ang value ng employee.Age sa @age
                cmd.Parameters.AddWithValue("@age", employee.Age);

                //I-Open ang connection
                sqlCon.Open();

                //SQLiteDataReader class kung saan nilalagay ang result ng query
                //If lang kasi gusto mo lang malakaman kung meron ba o wala
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //kapag may nakita na record pasok dito sa loob kaya true
                    return true;
                }
                //kapag walang nakita na record pasok dito sa loob kaya true
                return false;
            }
        }
        public static List<Employee> GetEmployees()
        {
            //List ay maramihang lagayan ng Employees
            List<Employee> _employees = new List<Employee>();

            //Ang using ay ginagamit para malimit ang scope ng Variable
            //At madaling maalis sa memonry
            //SQLiteConnection ay Class na Gamit ng SQLite sa paggawa ng connection
            using (SQLiteConnection sqlCon = new SQLiteConnection(ConnectionString))
            {

                //Query sa pagkuha ng Lahat laman ng table 
                string qryCommand = "SELECT employee_id,employee_fname,employee_lname,employee_age from Employee";

                //SQLiteCommand ay class sa pag execute ng command sa SQLite
                SQLiteCommand cmd = new SQLiteCommand(qryCommand, sqlCon);

                //I-Open ang connection
                sqlCon.Open();

                //SQLiteDataReader class kung saan nilalagay ang result ng query
                SQLiteDataReader reader = cmd.ExecuteReader();

                //while dahil maraming resulta ang babalik
                while (reader.Read())
                {
                    //Gawa ka ng class para sa employee na nahanap
                    Employee _employee = new Employee();
                    _employee.EmployeeId= int.Parse(string.IsNullOrWhiteSpace(reader.GetValue(0).ToString()) ? "0" : reader.GetValue(0).ToString());
                    _employee.FirstName = reader.GetValue(1).ToString();
                    _employee.LastName = reader.GetValue(2).ToString();
                    _employee.Age = int.Parse(string.IsNullOrWhiteSpace(reader.GetValue(3).ToString()) ? "0" : reader.GetValue(3).ToString());

                    //Ilagay ang Employee sa Listahan
                    _employees.Add(_employee);
                }

            }
            return _employees;
        }

        private static Employee GetEmployee(Employee employee)
        {
            //Gawa ka ng class para sa employee na mahahanap
            //sa labas kasi para ibalik mo sa GetEmployee function sa pamamagitan ng return;
            Employee _employee = new Employee();


            //Ang using ay ginagamit para malimit ang scope ng Variable
            //At madaling maalis sa memonry
            //SQLiteConnection ay Class na Gamit ng SQLite sa paggawa ng connection
            using (SQLiteConnection sqlCon = new SQLiteConnection(ConnectionString))
            {

                string qryCommand = "SELECT * from Employee where employee_fname=@fname and employee_lname=@lname  and employee_age=@age";

                //SQLiteCommand ay class sa pag execute ng command sa SQLite
                SQLiteCommand cmd = new SQLiteCommand(qryCommand, sqlCon);
                cmd.Parameters.AddWithValue("@fname", employee.FirstName);
                cmd.Parameters.AddWithValue("@lname", employee.LastName);

                //I-Open ang connection
                sqlCon.Open();

                //SQLiteDataReader class kung saan nilalagay ang result ng query
                SQLiteDataReader reader = cmd.ExecuteReader();

                //If lang ginagamit dahil isa lang ang expected mong makuha
                if (reader.Read())
                {
                    //Ilagay lahat ng value
                    _employee.FirstName = reader.GetValue(0).ToString();
                    _employee.LastName = reader.GetValue(1).ToString();
                    _employee.Age = int.Parse(string.IsNullOrWhiteSpace(reader.GetValue(1).ToString()) ? "0" : reader.GetValue(1).ToString());
                }

            }
            return _employee;
        }
        public static bool AddEmployee(Employee employee)
        {
            if (CheckIfEmployeeExist(employee)) return false;
            using (SQLiteConnection sqlCon = new SQLiteConnection(ConnectionString))
            {
                //SQL insert command
                string insertCommand = "INSERT INTO Employee(employee_fname,employee_lname,employee_age)"
                        + "values (@fname,@lname,@age)";

                //SQLiteCommand ay class sa pag execute ng command sa SQLite
                SQLiteCommand cmd = new SQLiteCommand(insertCommand, sqlCon);



                //I-pasa ang value ng employee.FirstName sa @fname
                cmd.Parameters.AddWithValue("@fname", employee.FirstName);

                //I-pasa ang value ng employee.LastName sa @lname
                cmd.Parameters.AddWithValue("@lname", employee.LastName);

                //I-pasa ang value ng employee.Age sa @age
                cmd.Parameters.AddWithValue("@age", employee.Age);

                //I-Open ang connection
                sqlCon.Open();
                
                //kailangan ng result para malaman mo kung success o hindi kapag 0 ang lumabas failed kapag 1 success
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool UpdateEmployee(Employee employee)
        {
            using (SQLiteConnection sqlCon = new SQLiteConnection(ConnectionString))
            {
                //SQL UPDATE command
                string updateCommand = "UPDATE Employee "
                    + " set employee_fname=@fname"
                    + " ,employee_lname=@lname"
                    + " ,employee_age=@age"
                    + " WHERE employee_id=@empid";

                //SQLiteCommand ay class sa pag execute ng command sa SQLite
                SQLiteCommand cmd = new SQLiteCommand(updateCommand, sqlCon);

                //gust mo lang ng specific na employee id kasi ito ang unique
                //I-pasa ang value ng employee.FirstName sa @fname
                cmd.Parameters.AddWithValue("@fname", employee.FirstName);

                //I-pasa ang value ng employee.LastName sa @lname
                cmd.Parameters.AddWithValue("@lname", employee.LastName);

                //I-pasa ang value ng employee.Age sa @age
                cmd.Parameters.AddWithValue("@age", employee.Age);

                cmd.Parameters.AddWithValue("@empid", employee.EmployeeId);

                //I-Open ang connection
                sqlCon.Open();

                //kailangan ng result para malaman mo kung success o hindi kapag 0 ang lumabas failed kapag 1 success
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool DeleteEmployee(long id)
        {
            using (SQLiteConnection sqlCon = new SQLiteConnection(ConnectionString))
            {
                string insertCommand = "DELETE FROM Employee "
                    + " WHERE employee_id=@empid";

                //SQLiteCommand ay class sa pag execute ng command sa SQLite
                SQLiteCommand cmd = new SQLiteCommand(insertCommand, sqlCon);

                //gust mo lang ng specific na employee id kasi ito ang unique
                cmd.Parameters.AddWithValue("@empid", id);

                //I-Open ang connection
                sqlCon.Open();

                //kailangan ng result para malaman mo kung success o hindi kapag 0 ang lumabas failed kapag 1 success
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
