using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridExamplePhCorner
{
    public class Emp
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public Emp()
        {

        }
        public Emp(int id, string name, string city)
        {
            this.ID = id;
            this.Name = name;
            this.City = city;
        }
    }
}
