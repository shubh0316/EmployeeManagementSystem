
namespace EmployeeMSystem
{
    internal class Department : IDepartment
    {  
        private int _departmentID;
        private string _departmentName = "";
        public Department(){
            _departmentName="";
            }
            public Department(int departmentId, string departmentName)
            {
                this.DepartmentID = departmentId;
                this.DepartmentName = departmentName;
            }    
            public int DepartmentID { get => _departmentID; set => _departmentID = value; }
            public string DepartmentName { get => _departmentName; set => _departmentName = value; }
        }
    }


