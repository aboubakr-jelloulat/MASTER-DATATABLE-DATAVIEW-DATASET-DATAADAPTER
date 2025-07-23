using System;
using System.Linq;
using System.Data;

/**
 * ======================== DataSet Deep Explanation ==========================
 *
 * A DataSet is an in-memory data container that holds one or more DataTables,
 * and optionally, the relationships between those tables (via DataRelation).
 *
 * 📦 Structure:
 * - Tables: A collection of DataTable objects.
 * - Relations: Define parent-child relations between tables.
 *
 * 🧠 Key Features:
 * - Disconnected: Works offline, after being filled from a database.
 * - Multi-table support: Can simulate a mini relational database in memory.
 * - Serializable: Can be saved/transferred using XML or JSON.  - ⚠️⚠️⚠️⚠️⚠️ bad practice ⚠️⚠️⚠️⚠️
 *
 * 🔄 Usage Flow:
 * - Fill the DataSet using a DataAdapter (e.g., from a SQL query).
 * - Modify the data (add/edit/delete rows) in memory.
 * - Optionally update the actual DB using the DataAdapter’s .Update() method.
 *
 * ✅ Best Use Cases:
 * - Complex data scenarios with multiple related tables.
 * - Need to manipulate and view data offline, then sync later.
 * - Applications with data layers that are decoupled from live DB access.
 *
 * ⚠️ Notes:
 * - Heavier than a single DataTable in terms of memory.
 * - Avoid if you only need to work with one simple table.
 *
 * ============================================================================
 */


public class Program // Renamed class to avoid conflict with System.Data.DataSet
{

    static void DisplayDataTableContent(DataTable EmployeeDataTable)
    {
        Console.WriteLine("\n*********  Employees List : *********\n");
        foreach (DataRow Recordrow in EmployeeDataTable.Rows)
        {
            Console.WriteLine("ID : {0}\t Name : {1}\t Country : {2}\t Salary : {3}\t Date : {4}\n",
                    Recordrow["ID"], Recordrow["Name"], Recordrow["Country"], Recordrow["Salary"], Recordrow["Date"]);
        }
    }

    static void CreateDataSet()
    {
        // 1 - Create a new Data Table Employees

        DataTable EmployeesDataTable = new DataTable();
        EmployeesDataTable.Columns.Add("ID", typeof(int));
        EmployeesDataTable.Columns.Add("Name", typeof(string));
        EmployeesDataTable.Columns.Add("Country", typeof(string));
        EmployeesDataTable.Columns.Add("Salary", typeof(string));
        EmployeesDataTable.Columns.Add("Date", typeof(DateTime));

        // 2 - Add rows to the DataTable

        EmployeesDataTable.Rows.Add(1, "Bakr Jelloulat  ", "Morocco    ", 80000, DateTime.Now);
        EmployeesDataTable.Rows.Add(2, "Sander bos      ", "Nethrlands ", 75000, DateTime.Now);
        EmployeesDataTable.Rows.Add(3, "John Doe        ", "USA        ", 70000, DateTime.Now);
        EmployeesDataTable.Rows.Add(4, "Jane bolch      ", "Sweden     ", 72000, DateTime.Now);

        DisplayDataTableContent(EmployeesDataTable);


        // 3 - Create a new Data Table  Departments

        DataTable DepartmentsDataTable = new DataTable();
        DepartmentsDataTable.Columns.Add("DepartmentID", typeof(int));
        DepartmentsDataTable.Columns.Add("DepartmentName", typeof(string));

        // 4 - Add rows to the DataTable

        DepartmentsDataTable.Rows.Add(1, "HR");
        DepartmentsDataTable.Rows.Add(2, "IT");
        DepartmentsDataTable.Rows.Add(3, "Finance");


        // 5 - display the content of the Departments DataTable

        Console.WriteLine("\n*********  Departments List : *********\n");
        foreach (DataRow Recordrow in DepartmentsDataTable.Rows)
        {
            Console.WriteLine("DepartmentID : {0}\t DepartmentName : {1}\n",
                    Recordrow["DepartmentID"], Recordrow["DepartmentName"]);
        }


        // 6 - Create a DataSet and add the DataTables to it

        DataSet DataSet1 = new DataSet();

        // 7 - Add the DataTables to the DataSet

        DataSet1.Tables.Add(EmployeesDataTable);
        DataSet1.Tables.Add(DepartmentsDataTable);

        // 8 - Display the content of the DataSet [Employees]

        Console.WriteLine("\n*********  DataSet Content [Employees] : *********\n");

        foreach (DataRow RecordRow in DataSet1.Tables[0].Rows)
        {
            Console.WriteLine("ID : {0}\t Name : {1}\t Country : {2}\t Salary : {3}\t Date : {4}\n",
                    RecordRow["ID"], RecordRow["Name"], RecordRow["Country"], RecordRow["Salary"], RecordRow["Date"]);

        }

        // 9 - Display the content of the DataSet [Departments]

        Console.WriteLine("\n*********  DataSet Content [Departments] : *********\n");

        foreach (DataRow RecordRow in DataSet1.Tables[1].Rows)
        {
            Console.WriteLine("DepartmentID : {0}\t DepartmentName : {1}\n",
                    RecordRow["DepartmentID"], RecordRow["DepartmentName"]);
        }

    }


    static void AccessDataTablesInsideDataSetByName()
    {
        // 1 - Create a new Data Table Employees

        DataTable EmployeesDataTable = new DataTable("Employees"); // Naming the DataTable for easier access by name
        EmployeesDataTable.Columns.Add("ID", typeof(int));
        EmployeesDataTable.Columns.Add("Name", typeof(string));
        EmployeesDataTable.Columns.Add("Country", typeof(string));
        EmployeesDataTable.Columns.Add("Salary", typeof(string));
        EmployeesDataTable.Columns.Add("Date", typeof(DateTime));

        // 2 - Add rows to the DataTable

        EmployeesDataTable.Rows.Add(1, "Bakr Jelloulat  ", "Morocco    ", 80000, DateTime.Now);
        EmployeesDataTable.Rows.Add(2, "Sander bos      ", "Nethrlands ", 75000, DateTime.Now);
        EmployeesDataTable.Rows.Add(3, "John Doe        ", "USA        ", 70000, DateTime.Now);
        EmployeesDataTable.Rows.Add(4, "Jane bolch      ", "Sweden     ", 72000, DateTime.Now);

        DisplayDataTableContent(EmployeesDataTable);


        // 3 - Create a new Data Table  Departments

        DataTable DepartmentsDataTable = new DataTable("Departments"); // Naming the DataTable for easier access by name
        DepartmentsDataTable.Columns.Add("DepartmentID", typeof(int));
        DepartmentsDataTable.Columns.Add("DepartmentName", typeof(string));

        // 4 - Add rows to the DataTable

        DepartmentsDataTable.Rows.Add(1, "HR");
        DepartmentsDataTable.Rows.Add(2, "IT");
        DepartmentsDataTable.Rows.Add(3, "Finance");


        // 5 - display the content of the Departments DataTable

        Console.WriteLine("\n*********  Departments List : *********\n");
        foreach (DataRow Recordrow in DepartmentsDataTable.Rows)
        {
            Console.WriteLine("DepartmentID : {0}\t DepartmentName : {1}\n",
                    Recordrow["DepartmentID"], Recordrow["DepartmentName"]);
        }


        // 6 - Create a DataSet and add the DataTables to it

        DataSet DataSet1 = new DataSet();

        // 7 - Add the DataTables to the DataSet

        DataSet1.Tables.Add(EmployeesDataTable);
        DataSet1.Tables.Add(DepartmentsDataTable);

        // 8 - Display the content of the DataSet [Employees]

        Console.WriteLine("\n*********  DataSet Content [Employees] : *********\n");

        foreach (DataRow RecordRow in DataSet1.Tables["Employees"].Rows)
        {
            Console.WriteLine("ID : {0}\t Name : {1}\t Country : {2}\t Salary : {3}\t Date : {4}\n",
                    RecordRow["ID"], RecordRow["Name"], RecordRow["Country"], RecordRow["Salary"], RecordRow["Date"]);

        }

        // 9 - Display the content of the DataSet [Departments]

        Console.WriteLine("\n*********  DataSet Content [Departments] : *********\n");

        foreach (DataRow RecordRow in DataSet1.Tables["Departments"].Rows)
        {
            Console.WriteLine("DepartmentID : {0}\t DepartmentName : {1}\n",
                    RecordRow["DepartmentID"], RecordRow["DepartmentName"]);
        }

    }
        

    static void Main()
    {
        CreateDataSet();


        AccessDataTablesInsideDataSetByName();

        Console.ReadKey();
    }

}
