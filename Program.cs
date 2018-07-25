using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace AccesaEmployee
{
	class Program 
	{
        static void Main(string[] args)
        {
            // XmlDocument myXml = new XmlDocument();
            //XPathNavigator Xnav = myXml.CreateNavigator();
            //XmlSerializer x = new XmlSerializer (myEmployee)
             
           var officeManagement = new OfficeManagement();
            /*officeManagement.DisplayAllProjects();

			officeManagement.DeleteEmployee(dev);
			officeManagement.DisplayAllEmployees();
			officeManagement.DisplayAllProjects();*/

            PopulateEmployeeList(officeManagement);
            officeManagement.DisplayAllEmployees();
            // var employee = new Employee();

            string content = JsonConvert.SerializeObject(officeManagement);
            File.WriteAllText("office.json", content);

            var ds = new DataContractSerializer(typeof(OfficeManagement));
            MemoryStream stream = new MemoryStream();
            ds.WriteObject(stream, officeManagement);

            using (Stream a = File.OpenRead("siguuur.xml"))
            {
                ds.WriteObject(a, officeManagement);
            }

            Console.ReadLine();

        }

        private static void PopulateEmployeeList(OfficeManagement officeManagement)
		{
			var allInformation = File.ReadAllText(@"C:\Users\Bogdan.Barar\Downloads\officeDB.txt");

			var employees = allInformation.Split(new string[] {nameof(Employee),"{", "}"}, StringSplitOptions.RemoveEmptyEntries);
			foreach (var record in employees)
			{
				var trimmedRecord = record.TrimStart(new char[] {'\r', '\n'});
				if (trimmedRecord.StartsWith(nameof(Project)) || trimmedRecord.Equals("\r\n")) continue;
				GetEmployeeFromText(trimmedRecord, officeManagement);
			}
		}

		private static Employee GetEmployeeFromText(string info, OfficeManagement officeMangement)
		{
			if (string.IsNullOrEmpty(info)) return null;
			var lines = info
					.Replace("\t",string.Empty)
					.Trim()
					.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
			if (!lines.Any()) return null;

			var name = GetPropertyValue(nameof(Employee.Name), lines);
			var position= GetPropertyValue(nameof(Employee.Position), lines);
			var capacity = GetPropertyValue(nameof(Employee.Capacity), lines);
			var hobbies = GetPropertyValue(nameof(Employee.Hobbies), lines).Split(',');

			var positionType = EmployeePosition.Intern;
			if (!Enum.TryParse(position, out positionType))
				Console.WriteLine($"Pentru {name} nu s-a putut stabili position type");

			return officeMangement.AddEmployee(name, positionType , Convert.ToSingle(capacity, CultureInfo.InvariantCulture), hobbies.ToList());
		}

		private static string GetPropertyValue(string propertyName, string[] values)
		{
			if (!values.Any()) return string.Empty;
			var adjustedProperty = propertyName + ":";
			return values.SingleOrDefault(x => x.StartsWith(adjustedProperty))
				.TrimStart(adjustedProperty.ToCharArray())
				.TrimEnd("\r\n".ToCharArray());
		}
	}
}
