using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AccesaEmployee
{
	[DataContract] public class Intern:Employee
	{
		[DataMember] private string _universityName;
		[DataMember] private int _yearOfStudy;
		[DataMember] private EmployeePosition _targetPosition;

		public string UniversityName => _universityName;
		public int YearOfStudy => _yearOfStudy;
		public EmployeePosition TargetPosition => _targetPosition;
		public Intern(string name, float capacity) 
			: base(name, EmployeePosition.Intern, capacity)
        {

        }
      //  public void Json()
       // {
        //  new  JProperty("UniversityName", _universityName);
          // new JProperty("YearOfStudy", _yearOfStudy);
          // new JProperty("TargetPosition", _targetPosition);

       // }
	}
}
