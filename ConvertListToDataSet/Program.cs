using Dapper;
using ConvertListToDataSet;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("converting list to dataset code...");

List<Employee> employees = new List<Employee>();
employees.Add(new Employee() { EmployeeName = "John", EmployeeAge = 24, EmployeeId = 123 });
employees.Add(new Employee() { EmployeeName = "Stella", EmployeeAge = 25, EmployeeId = 234 });
employees.Add(new Employee() { EmployeeName = "Peter", EmployeeAge = 39, EmployeeId = 453 });
employees.Add(new Employee() { EmployeeName = "Harris", EmployeeAge = 28, EmployeeId = 234 });

//printing the list
employees.ForEach(emp =>
{
    Console.WriteLine($"{emp.EmployeeName}\t{emp.EmployeeAge}\t{emp.EmployeeId}");
});

// initialising a ireadonlylist
IReadOnlyList<Employee> employees1 = employees;
DataSet dataSet = employees1.ToDataSet();
DataTable dt = dataSet.Tables[0];
Console.WriteLine("Displaying Data set");
foreach (DataRow row in dt.Rows)
{
    Console.WriteLine(String.Join(" ", row.ItemArray));
}


//querying the db to check if query result can be converted to dataset
string connectionString = "Data Source=<server name>; initial catalog=EmployeeDetails; integrated security=true";
IDbConnection dbConnection=new SqlConnection (connectionString);
dbConnection.Open();
string query = "select * from EmployeeDetails";
var result = dbConnection.ExecuteReader(query);//executes the query and return an IDataReader
/*
 * 123 John 24
234 Stella 25
453 Peter 39
234 Harris 28
 */
DataSet ds = new DataSet();
dt = new DataTable();
dt.Load(result);
ds.Tables.Add(dt);

//To print the dataset we need to access the data table and run the loop.

DataTable dtable=ds.Tables[0];
Console.WriteLine("Data from the DataSet..");
foreach(DataRow row in dtable.Rows)
{
    Console.WriteLine($"{row.Field<int>(0)}\t{row.Field<string>(1)}\t{row.Field<string>(2)}");
}


