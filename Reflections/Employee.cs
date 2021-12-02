using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reflections
{
    class Employee
    {
        private int _Rate;
        public int Rate
        {
            get
            {
                return _Rate;
            }
            set
            {
                _Rate = value;
            }
        }
        public string Name { get; set; }

        public Employee()
        {
            this.Rate = 0;
            this.Name = "John Doe";
        }
        public Employee(string Name, int Rate)
        {
            this.Rate = Rate;
            this.Name = Name;
        }

        private void PrintInfo()
        {
            Console.WriteLine($"Employee name : {Name}; Rate : {Rate}");
        }

        public int Payment(int Hours, int inpRate = 1)
        {
            int factinpRate = (_Rate > 0 && _Rate < 100) ? _Rate : inpRate;
            return Hours * factinpRate;
        }
    }
}
