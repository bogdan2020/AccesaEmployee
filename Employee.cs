using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace AccesaEmployee
{
    public  class Employee
    {
       
        public const string XmlName = "employees";
        private string _name;
        private readonly EmployeePosition _position;
        private float _capacity;
        private Employee employee;
        private readonly List<string> _hobbies = new List<string>();

        public string Name => _name;
        public EmployeePosition Position => _position;
        public float Capacity => _capacity;
        public List<string> Hobbies => _hobbies;

        public IEnumerable<Employee> Employees { get; set; }

        protected Employee(string name, EmployeePosition position, float capacity)
        {
            _name = name;
            _position = position;
            _capacity = capacity;
        }

        public Employee(Employee employee)
        {
            this.employee = employee;
        }

        public Employee()
        {
        }

        public void XmlName2()
        {
            foreach (Employee employee in Employees)
            {
                JObject emp = new JObject ();
                new JProperty("Name", employee.Name);
                new JProperty("Capacity", employee.Capacity);
                new JProperty("Position", employee.Position);
                new JProperty("Hobbies", employee.Hobbies);
                File.WriteAllText("XmlName2.json", emp.ToString());
                using (StreamWriter file = File.CreateText("XmlName2.json")) 
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    emp.WriteTo(writer);
                }

            }
        }


        public void XmlName1 ()
        { 
            
                using (XmlWriter writer = XmlWriter.Create("employees.xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Employees");

                    foreach (Employee employee in Employees)
                    {
                        writer.WriteStartElement("Employee");
                        writer.WriteElementString("Name", employee.Name.ToString());
                        writer.WriteElementString("Capacity", employee.Capacity.ToString());
                        writer.WriteElementString("Position", employee.Position.ToString());
                        writer.WriteElementString("Hobbies", employee.Hobbies.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }


                using (XmlReader reader = XmlReader.Create("employees.xml"))
                {
                    reader.ReadStartElement();

                    foreach (Employee employee in Employees)
                    {
                        reader.ReadStartElement("Employee");
                        reader.ReadElementString("Name", employee._name.ToString());
                        reader.ReadElementString("Capacity", employee._capacity.ToString());
                        reader.ReadElementString("Position", employee._position.ToString());
                        reader.ReadElementString("Hobbies", employee._hobbies.ToString());
                        reader.ReadEndElement();

                    }


                    reader.ReadEndElement();
                }
            
           
        }

    

        public virtual void DisplayInfo()
        {
            var sb = new StringBuilder();
            _hobbies.ForEach(x => sb.Append(x + " "));
            Console.WriteLine($"{_name} ocupa pozitia {_position} si e angajat cu {_capacity} ore pe zi. Lui ii place {sb.ToString()}");
        }
    }
}
