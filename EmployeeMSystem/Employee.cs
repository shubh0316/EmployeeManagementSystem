
namespace EmployeeMSystem
{
    internal class Employee
    {
        static  int _id = 1;
        private int _employeeDepID;
        private int _employeeID;
        private string _fullName="";
        private DateTime _dateOfBirth;
        private double _experience;
        private string _designation="";
        public Employee()
        {
            _employeeID = _id++;      
            _fullName = string.Empty;
            _designation = string.Empty;
        }
        public Employee(int employeeDepID, int employeeID, string fullName, DateTime dateOfBirth, double experience, string designation)
        {
            this.EmployeeDepID = employeeDepID;
            this.EmployeeID = employeeID;
            this.FullName = fullName;
            this.DateOfBirth = dateOfBirth;
            this.Experience = experience;
            this.Designation = designation;
           
        }
        internal int EmployeeDepID { get => _employeeDepID; set => _employeeDepID = value; }
        internal int EmployeeID { get => _employeeID; set => _employeeID = value; }
        internal string FullName { get => _fullName; set => _fullName = value; }
        internal DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
        internal double Experience { get => _experience; set => _experience = value; }
        internal string Designation { get => _designation; set => _designation = value; }       
    }
}
    

