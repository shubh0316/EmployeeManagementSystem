
using Logger;
using Excel = Microsoft.Office.Interop.Excel;
namespace EmployeeMSystem
{   
    internal static class Company 
    {
         static string filepath = @"C:\Users\ssaxena\OneDrive - WatchGuard Technologies Inc\Desktop\LogFile.txt";
         static LoggedData log = new LoggedData(filepath);
         private static int _Count = 2;
         private static readonly List<Department>DepartmentList = new();
         internal static  List<Employee>EmployeeList = new();
         private static readonly List<Employee> DeletedEmployees = new();            
        internal static void AddDepartment(string departmentName)
        {
            int bulletpoint = 1;
            Random  random = new Random();
            int randomNumber = random.Next(50);
            DepartmentList.Add(new Department(randomNumber,departmentName));
            Console.WriteLine();
            foreach (var department in DepartmentList)
            {
                Console.WriteLine(bulletpoint + ")" + " Department ID: " + department.DepartmentID + " || Department Name: " + department.DepartmentName);
                bulletpoint++;
            }
        }

        internal static void ShowMessageInLogFile(string type,string message,string operation)
        {
            if (type == "information")
                log.WriteAllData(new LogFiles(DateTime.Now, $"{type}", $"{message} {operation} successfully"));
            else if (type == "error")
                log.WriteAllData(new LogFiles(DateTime.Now, $"{type}", $"{message} occured"));
            else
                log.WriteAllData(new LogFiles(DateTime.Now, $"{type}",$"{message} occured"));
        }
        internal static void AddEmployee(Employee employees)
        {          
            int bulletPoint = 1;      
            EmployeeList.Add(new Employee(employees.EmployeeDepID,employees.EmployeeID,employees.FullName,employees.DateOfBirth,employees.Experience,employees.Designation));
            Console.WriteLine();
            foreach (var employee in EmployeeList)
            {
                Console.WriteLine("********* Employee " + bulletPoint + " *********");
                Console.WriteLine("Department ID: " + employee.EmployeeDepID);
                Console.WriteLine("Employee ID: " + employee.EmployeeID);
                Console.WriteLine("Name: " + employee.FullName);
                Console.WriteLine("Experience: " + employee.Experience);
                Console.WriteLine("Designation: " + employee.Designation);             
                Console.WriteLine();
                bulletPoint++;
            }
        }
        internal static int EditEmployee(int employeeID)
        {
            while (true)
            {
                Console.Write("\tEnter the new designation: ");
                var designation = Console.ReadLine();
                if (CheckAtFirstPlaceOfString(designation))
                {
                    List<Employee> results = EmployeeList.FindAll(employee => employee.EmployeeID == employeeID);
                    foreach (var find in results)
                    {
                        if (designation == null)
                        {
                            throw new Exception("the entered string is null here");
                        }
                        else
                        {
                            find.Designation = designation;
                        }
                    }
                    Console.WriteLine("\tEmployee's designation has changed now");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid format of designation");
                    ShowMessageInLogFile("warning", "warning", "");
                    continue;
                }
            }
            return 0;
        }
        internal static int DeleteEmployee(int employeeid)
        {
            var itemToRemove = EmployeeList.SingleOrDefault(employeeIdForDelete => employeeIdForDelete.EmployeeID == employeeid);
            if (itemToRemove != null)
            {     
                if(_Count > 0)
                {
                    DeletedEmployees.Add(itemToRemove);
                    _Count--;
                }
                else
                {
                    DeletedEmployees.RemoveAt(0);
                    DeletedEmployees.Add(itemToRemove);
                }
                EmployeeList.Remove(itemToRemove);
                Console.WriteLine("Employee " + employeeid + " is removed!");
                ShowMessageInLogFile("information","employee","deleted");
               return employeeid;
            }
            else
                return 0;
        }
        public static void ReadingExcelSheet()
         {
            DepartmentList.Add(new Department(1, "development"));
            DepartmentList.Add(new Department(2, "testing"));          
                Excel.Application excelApp = new Excel.Application();
            try { 
                Excel.Workbook workBook = excelApp.Workbooks.Open(@"C:\Users\ssaxena\OneDrive - WatchGuard Technologies Inc\Desktop\StoredData1.xlsx");
                Excel.Worksheet worksheet = workBook.Sheets[1];
                for (int row = 2; row < 6; row++)
                {
                    Employee employee = new Employee();
                    if (worksheet.Cells[row, 1].Text() == string.Empty)
                    {
                        employee.FullName = "Name not found";
                    }
                    else
                    {
                        employee.FullName = worksheet.Cells[row, 1].value().ToString();
                    }
                    if (!IsValidatForCompleteString(employee.FullName))
                    {
                        employee.FullName = "Invalid Name";
                    }
                    if (double.TryParse(worksheet.Cells[row, 2].Text(), out double value))
                    {
                        employee.Experience = value;
                    }
                    else
                    {
                        employee.Experience = 0.0;
                    }
                    if (worksheet.Cells[row, 3].Text() == string.Empty)
                    {
                        employee.Designation = "Designation not found";
                    }
                    else
                    {
                        employee.Designation = worksheet.Cells[row, 3].value().ToString();
                    }
                    if (!CheckAtFirstPlaceOfString(employee.Designation))
                    {
                        employee.Designation = "Invalid Designation";

                    }
                    employee.EmployeeDepID = DepartmentList[new Random().Next(1, 2)].DepartmentID;
                    EmployeeList.Add(employee);
                 
                }
                workBook.Close();          
                excelApp.Quit();               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        internal static int ViewDepartment()
        {
            int bulletPoint = 1;
            Console.WriteLine();
            foreach (var department in DepartmentList)
            {
                Console.WriteLine(bulletPoint + ")" + " Department => ID: " + department.DepartmentID + " | Name: " + department.DepartmentName);
                bulletPoint++;
            }
            return 0;
        }
        internal static string Menu()
        {

            Console.Write("Press any key to Continue..");
            Console.ReadKey(false);
            Console.Clear();
            Console.WriteLine("\n***** EMPLOYEE MANAGEMENT SYSTEM *****");
            Console.WriteLine();
            Console.WriteLine("\t1) Add Department");
            Console.WriteLine("\t2) Add Employee");
            Console.WriteLine("\t3) Edit Employee details");
            Console.WriteLine("\t4) View Employee details");
            Console.WriteLine("\t5) Remove Employee");
            Console.WriteLine("\t6) View All Department");
            Console.WriteLine("\t7) Undo Removed Employees");
            Console.WriteLine("\t8) Exit");
            Console.WriteLine();
            Console.Write("\tPlease input your option:");
            var choice = Console.ReadLine();
            return choice!;
        }
        private static bool CheckAtFirstPlaceOfString(string? name)
        {

            if (name == null)
                return false;
            if (name.Length == 0)
                return false;
            if (name[0] == ' ' || name[0] == '!' ) return false;
            if (name[0] == '.' || name[0] == '0' || name[0] == '1'
                || name[0] == '2' || name[0] == '3'
                || name[0] == '4' || name[0] == '5'
                || name[0] == '5' || name[0] == '7'
                || name[0] == '6' || name[0] == '9'
            || name[0] == '@' || name[0] == '#'
            || name[0] == '$' || name[0] == '%'
            || name[0] == '^' || name[0] == '&'
            || name[0] == '*' || name[0] == '('
            || name[0] == ')' || name[0] == '-'
            || name[0] == '_' || name[0] == '='
            || name[0] == '+' || name[0] == '/'
            || name[0] == '"' || name[0] == ':'
            || name[0] == '>' || name[0] == '<'
            || name[0] == '{' || name[0] == '}'
            || name[0] == ']' || name[0] == '|'
            || name[0] == '~' || name[0] == '`'
            || name[0] == '?' || name[0] == ']'
             )
                return false;
            return true;
        }
        private static bool IsValidatForCompleteString(string NAME)
        {
            var nameChar = NAME.ToCharArray();
            if (NAME.Trim() == "" || nameChar.Any(c => (!char.IsLetter(c) && !char.IsWhiteSpace(c))))
            {
                return false;
            }
            return true;

        }
        private static void AdditionOfDepartment()
        {
            Console.WriteLine("******** ADD DEPARTMENT ********");
            Console.WriteLine();
            while (true)
            {
                Console.Write("\nEnter the department name you want to add: ");
                var DepartmentName = Console.ReadLine();

                bool flag = false;
                if (CheckAtFirstPlaceOfString(DepartmentName))
                {
                    foreach (Department items in DepartmentList)
                    {
                        if (items.DepartmentName.ToLower() == DepartmentName!.ToLower())
                        {
                            flag = true;
                            Console.WriteLine("Department already exists");
                            ShowMessageInLogFile("warning","warning", "");
                            break;
                        }
                    }
                    if (flag!= true)
                    {
                        AddDepartment(DepartmentName!.ToLower());
                        Console.WriteLine("The department added successfully");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid department name");
                    ShowMessageInLogFile("error", "error", "");
                    continue;
                }
            }
        }
        private static void AdditionForEmployee()
        {
            var employee = new Employee();
            Console.WriteLine("******** ADD EMPLOYEE ********");
            if (DepartmentList.Count == 0)
            {
                Console.WriteLine("Add a department first");
            }
            Console.WriteLine("List of available Departments:");
            ViewDepartment();
            selectDepartment:;
            Console.WriteLine("Enter the department id where you want to add employee: ");
            if (int.TryParse(Console.ReadLine(), out var deptId) && DepartmentList.Any(d => d.DepartmentID == deptId))
            {
                employee.EmployeeDepID = deptId;
            }
            else
            {
                Console.WriteLine("Please enter the valid input, Press any key to continue...");
                ShowMessageInLogFile("warning", "warning", "");
                Console.ReadKey();
                goto selectDepartment;
            }
            Console.Write("Enter employee name: ");
            var Name = Console.ReadLine();
            while (!IsValidatForCompleteString(Name!))
            {
                Console.Write("Invalid format, Please retry:");
                ShowMessageInLogFile("error", "error", "");
                Name = Console.ReadLine();
            }
            employee.FullName = Name!;
        EnterExperience:;
            Console.Write("Enter your experience (in years) : ");
            if (double.TryParse(Console.ReadLine(), out var experience) && (experience >= 1 && experience <= 30))
            {
                employee.Experience = experience;
            }
            else
            {
                Console.WriteLine("Please enter the valid input,Press any key to continue...");
                ShowMessageInLogFile("warning","warning","");
                Console.ReadKey();
                goto EnterExperience;
            }
            while (true)
            {
                Console.Write("\nEnter your designation here ");
                employee.Designation = Console.ReadLine()!.ToLower();
                if (CheckAtFirstPlaceOfString(employee.Designation))
                {
                    AddEmployee(employee);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid format of designation");
                    continue;
                }
            }
            Console.WriteLine("Employee Added successfully");
            ShowMessageInLogFile("information","employee","added");

        }
        private static void EditForEmployee()
        {
            var employee = new Employee();
            Console.WriteLine("******** EDIT EMPLOYEE DETAILS ********");
            while (true)
            {
                int bulletPoint = 1;
                foreach (Employee item in EmployeeList)
                {
                    Console.WriteLine("********* Employee " + bulletPoint + " *********");
                    Console.Write("Employee ID: " + item.EmployeeID);
                    Console.Write("\tName: " + item.FullName + "\n");
                    bulletPoint++;
                }
                EditEmployee:;
                Console.WriteLine();
                if (EmployeeList.Count == 0)
                {
                    Console.WriteLine("Employee not found");
                    ShowMessageInLogFile("error", "error", "");
                    break;
                }
                Console.Write("\nEnter the EmployeeID of whom you want to change the designation : ");
                if (int.TryParse(Console.ReadLine(), out var empId))
                {
                    if (EmployeeList.Any(employee => employee.EmployeeID == empId))
                    {
                        EditEmployee(empId);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\tNo employee exist with this Id, Press any key to continue...");
                        ShowMessageInLogFile("error", "error", "");
                        Console.ReadKey();
                        goto EditEmployee;
                    }
                }
                else
                {
                    Console.WriteLine("\tPlease Enter the valid employee ID, Press any key to continue...");
                    ShowMessageInLogFile("warning", "warning", "");
                    Console.ReadKey();
                    goto EditEmployee;
                }
            }
        }
    /*    private static void ViewDetailsOfEmployeesOfId(int emp)
        {
            var empl = new Employee();
            var employee = new Employee();
            if ( EmployeeList.Any(empl => empl.EmployeeID == emp))
            {
                foreach (Employee items in EmployeeList)
                {
                    Console.WriteLine("name of the employee: {0}", items.FullName);
                }
            }
        }*/
        private static void ViewDetailsOfEmployee()
        {
           var employee = new Employee();
           Console.WriteLine("******** VIEW EMPLOYEE DETAILS ********");
            while (true)
             {
                if (EmployeeList.Count == 0)
                {
                    Console.WriteLine("Employee do not exists");
                    break;
                }
                
                
                   /* Console.WriteLine("type the employee id you want to list...");
                    int id = Convert.ToInt32(Console.ReadLine());
                    ViewDetailsOfEmployeesOfId(id);*/

                

               else
                {
                    foreach (Employee items in EmployeeList)
                    {
                        Console.WriteLine("\tDepartment ID: {0}", items.EmployeeDepID);
                        
                        Console.WriteLine("\tEmployee ID: {0}", items.EmployeeID);
                       
                        Console.WriteLine("\tName: {0}", items.FullName);
                    
                        Console.WriteLine("\tExperience : {0}", items.Experience);
                       
                        Console.WriteLine("\tDesignation : {0}", items.Designation);
                        Console.WriteLine();
                    }
                    break;
                }           
            }           
        }
        private static void RemoveDetailsOfEmployee()
        {
            var employee = new Employee();
            Console.WriteLine("******** REMOVE EMPLOYEE ********");
            Console.WriteLine();
            while (true)
            {
                int bulletoint = 1;
                foreach (Employee item in EmployeeList)
                {
                    Console.WriteLine("********* Employee " + bulletoint + " *********");
                    Console.Write("Employee ID: " + item.EmployeeID);
                    Console.Write("\tName: " + item.FullName + "\n");
                    bulletoint++;
                }
                RemoveDetailsOfEmployee:
                Console.WriteLine();
                if (EmployeeList.Count == 0)
                {
                    Console.WriteLine("Employee not found");
                    ShowMessageInLogFile("error", "error", "");
                    break;
                }
                Console.Write("Enter the ID of the employee you want to remove: ");
                if (int.TryParse(Console.ReadLine(), out var empID))
                {   
                    if (EmployeeList.Any(employee => employee.EmployeeID == empID))
                    {
                        DeleteEmployee(empID);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter employee ID corectly, Please any key continue...");
                        Console.ReadKey();
                        goto RemoveDetailsOfEmployee;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter employee ID corectly, Please any key continue...");
                    Console.ReadKey();
                    goto RemoveDetailsOfEmployee;
                }
            }
        }
        private static void ViewDetailsOfDepartment()
        {
            Console.WriteLine("******** VIEW ALL DEPARTMENT ********");
            bool checkDepartment = false;
            foreach (Department item in DepartmentList)
            {
                if (item.DepartmentName != null)
                {
                    checkDepartment = true;
                }
            }
            if (checkDepartment == false)
            {
                Console.WriteLine("\tDepartment do not exist");
                ShowMessageInLogFile("error", "error", "");
                
            }
            ViewDepartment();
        }
        internal static void UndoRemovedEmployee()
        {
            if (DeletedEmployees.Count == 0)
            {
            Console.WriteLine("Employee cannot be restored, You can restore only twice");
                ShowMessageInLogFile("warning", "warning", "");
            return;
            }
            EmployeeList.Add(DeletedEmployees[DeletedEmployees.Count - 1]);
            DeletedEmployees.RemoveAt(DeletedEmployees.Count - 1);
            _Count++;
            Console.WriteLine("EmployeeId {0} has been restored successfully", EmployeeList[EmployeeList.Count - 1].EmployeeID);
            ShowMessageInLogFile("information", "employee", "restored");
        }
        internal static void  ExitApplication()
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("the error occured: {0}", ex.Message);
            }

        }
        internal  static void Implementation()
        {
            ReadingExcelSheet();
            bool loopContinue = true;
            string choice;   
            while (loopContinue)
            {
                    choice = Menu();
                    Console.WriteLine();
                    switch (choice)
                    {
                        case "1":
                            AdditionOfDepartment();                
                            break;
                        case "2":
                            AdditionForEmployee();
                            break;
                        case "3":
                            EditForEmployee();
                            break;
                        case "4":
                            ViewDetailsOfEmployee();
                            break;
                        case "5":
                            RemoveDetailsOfEmployee();
                            break;
                        case "6":
                            ViewDetailsOfDepartment();
                            break;
                       case "7":
                            UndoRemovedEmployee();
                           break;
                        case "8":
                            Console.WriteLine("Program exited...");
                            ExitApplication();                 
                            break;
                       default:
                            Console.WriteLine("\tPlease press key's from 1 to 8 only");
                            loopContinue = true;
                            break;
                    }                
            }
            if (loopContinue)
                        Console.WriteLine("\nReturning to main menu.");
                        Console.WriteLine();  
            }
        }
    }


