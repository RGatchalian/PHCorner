using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridSql
{
    //Ang class at Object kung saan maraming properties or entities
    public class Employee
    {
        public long EmployeeId{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
