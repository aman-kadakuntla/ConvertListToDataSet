
using ConvertListToDataSet;
using System.Data;

Console.WriteLine("converting list to dataset code...");

List<Employee> employees = new List<Employee>();
employees.Add(new Employee() { EmployeeName = "aman", EmployeeAge = 24, EmployeeId = 123 });
employees.Add(new Employee() { EmployeeName = "sruthi", EmployeeAge = 25, EmployeeId = 234 });
employees.Add(new Employee() { EmployeeName = "akash", EmployeeAge = 39, EmployeeId = 453 });
employees.Add(new Employee() { EmployeeName = "archana", EmployeeAge = 28, EmployeeId = 234 });

employees.ForEach(emp =>
{
    Console.WriteLine($"{emp.EmployeeName}\t{emp.EmployeeAge}\t{emp.EmployeeId}");
});

DataSet dataSet = employees.ToDataSet();

DataTable dt = dataSet.Tables[0];

//data set
Console.WriteLine("Displaying Data set");
foreach (DataRow row in dt.Rows)
{
    Console.WriteLine(String.Join(" ",row.ItemArray));
}
