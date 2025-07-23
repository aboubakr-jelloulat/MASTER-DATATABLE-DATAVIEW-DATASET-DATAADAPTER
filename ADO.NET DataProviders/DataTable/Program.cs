using System;
using System.Linq;
using System.Data;

public class Example
{

    static DataTable CreateEmployeeDataTable()
    {
        // Create a DataTable
        DataTable EmployeeDataTable = new DataTable();

        // Add columns to the DataTable
        EmployeeDataTable.Columns.Add("ID", typeof(int));
        EmployeeDataTable.Columns.Add("Name", typeof(string));
        EmployeeDataTable.Columns.Add("Country", typeof(string));
        EmployeeDataTable.Columns.Add("Salary", typeof(double));
        EmployeeDataTable.Columns.Add("Date", typeof(DateTime));

        // Add rows to the DataTable
        EmployeeDataTable.Rows.Add(1, "baker Jelloulat", "Morocco    ", 15000, DateTime.Now);
        EmployeeDataTable.Rows.Add(2, "John Doe       ", "USA        ", 20000, DateTime.Now);
        EmployeeDataTable.Rows.Add(3, "Jane Smith     ", "UK         ", 25000, DateTime.Now);
        EmployeeDataTable.Rows.Add(4, "Sander Bos     ", "Netherlands", 30000, DateTime.Now);
        EmployeeDataTable.Rows.Add(5, "Adam salem     ", "Morocco    ", 18000, DateTime.Now);
        EmployeeDataTable.Rows.Add(6, "weld lahj      ", "Morocco    ", 90000, DateTime.Now);
        EmployeeDataTable.Rows.Add(7, "Ali Ahmed      ", "Egypt      ", 22000, DateTime.Now);
        EmployeeDataTable.Rows.Add(8, "AbdAllah Tahri ", "Algeria    ", 12000, DateTime.Now);
        EmployeeDataTable.Rows.Add(9, "Hassan El Fassi", "Morocco    ", 17000, DateTime.Now);

        return EmployeeDataTable;
    }

    static void DisplayDataTableContent(DataTable EmployeeDataTable)
    {
        // 1)  -  Display the DataTable content :
        Console.WriteLine("\n*********  Employees List : *********\n");
        foreach (DataRow Recordrow in EmployeeDataTable.Rows)
        {
            Console.WriteLine("ID : {0}\t Name : {1}\t Country : {2}\t Salary : {3}\t Date : {4}\n",
                    Recordrow["ID"], Recordrow["Name"], Recordrow["Country"], Recordrow["Salary"], Recordrow["Date"]);
        }
    }

    static void ShowAggregateFunctions(DataTable EmployeeDataTable)
    {
        // 2)  -  Agregate Functions :
        int TotalEmployees = EmployeeDataTable.Rows.Count;
        double TotalSalary = Convert.ToDouble(EmployeeDataTable.Compute("SUM(Salary)", string.Empty));
        double AvgSalary = Convert.ToDouble(EmployeeDataTable.Compute("Avg(Salary)", string.Empty));
        double MaxSalary = Convert.ToDouble(EmployeeDataTable.Compute("Max(Salary)", string.Empty));
        double MinSalary = Convert.ToDouble(EmployeeDataTable.Compute("Min(Salary)", string.Empty));

        Console.WriteLine("\n\n********* Agregate Functions : *********\n");
        Console.WriteLine("Total Employees  : {0}", TotalEmployees);
        Console.WriteLine("Total Salary     : {0}", TotalSalary);
        Console.WriteLine("Average Salary   : " + AvgSalary);
        Console.WriteLine("Maximum Salary   : " + MaxSalary);
        Console.WriteLine("Minimum Salary   : " + MinSalary);
    }

    static void FilterDataTable(DataTable EmployeeDataTable)
    {
        // 3)  -  Filter the DataTable :
        Console.WriteLine("\n********* Filter Data by Country =  Morocco : *********\n\n");

        DataRow[] ResultRows;
        ResultRows = EmployeeDataTable.Select("Country = 'Morocco'");

        foreach (var item in ResultRows)
        {
            Console.WriteLine("ID : {0}\t Name : {1}\t Country : {2}\t Salary : {3}\t Date : {4}\n",
                    item["ID"], item["Name"], item["Country"], item["Salary"], item["Date"]);
        }

        // ===== filter by argegate function
        Console.WriteLine("\n\nargegate function + country =  'Morocco' : \n\n");

        int ResultCount = ResultRows.Count();
        double TotalSalaryMorocco = Convert.ToDouble(EmployeeDataTable.Compute("SUM(Salary)", "Country = 'Morocco'"));
        double AvgSalaryMorocco = Convert.ToDouble(EmployeeDataTable.Compute("Avg(Salary)", "Country = 'Morocco'"));
        double MaxSalaryMorocco = Convert.ToDouble(EmployeeDataTable.Compute("Max(Salary)", "Country = 'Morocco'"));
        double MinSalaryMorocco = Convert.ToDouble(EmployeeDataTable.Compute("Min(Salary)", "Country = 'Morocco'"));

        Console.WriteLine("Total Employees in Morocco  : {0}", ResultCount);
        Console.WriteLine("Total Salary in Morocco     : {0}", TotalSalaryMorocco);
        Console.WriteLine("Average Salary in Morocco   : " + AvgSalaryMorocco);
        Console.WriteLine("Maximum Salary in Morocco   : " + MaxSalaryMorocco);
        Console.WriteLine("Minimum Salary in Morocco   : " + MinSalaryMorocco);

        // ==== Filter by Country Morocco or Netherlands
        ResultRows = EmployeeDataTable.Select("Country = 'Morocco' or Country = 'Netherlands'");

        Console.WriteLine("\n\nFilter by Country Morocco or Netherlands : \n\n");

        foreach (DataRow item in ResultRows)
        {
            Console.WriteLine("ID : {0}\t Name : {1}\t Country : {2}\t Salary : {3}\t Date : {4}\n",
                    item["ID"], item["Name"], item["Country"], item["Salary"], item["Date"]);
        }

        // ==== Filter by Salary > 20000 and Country = 'Morocco' or 'Netherlands'
        Console.WriteLine("\n\nFilter by Salary > 20000 and Country = 'Morocco' or 'Netherlands' : \n\n");

        ResultRows = EmployeeDataTable.Select("Salary > 2000 and Country = 'Morocco' or Country = 'Netherlands'");

        foreach (DataRow item in ResultRows)
        {
            Console.WriteLine("ID : {0}\t Name : {1}\t Country : {2}\t Salary : {3}\t Date : {4}\n",
                    item["ID"], item["Name"], item["Country"], item["Salary"], item["Date"]);
        }
    }

    static void SortDataTable(DataTable EmployeeDataTable)
    {
        // 4)  -  Sort the DataTable by Salary :

        Console.WriteLine("\n********* Sort Data by ID DESC : *********\n");

        EmployeeDataTable.DefaultView.Sort = "ID DESC";
        EmployeeDataTable = EmployeeDataTable.DefaultView.ToTable();

        foreach (DataRow Recordrow in EmployeeDataTable.Rows)
        {
            Console.WriteLine("ID : {0}\t Name : {1}\t Country : {2}\t Salary : {3}\t Date : {4}\n",
                    Recordrow["ID"], Recordrow["Name"], Recordrow["Country"], Recordrow["Salary"], Recordrow["Date"]);
        }

        Console.WriteLine("\n********* Sort Data by Name ASC : *********\n");

        EmployeeDataTable.DefaultView.Sort = "Name ASC";
        EmployeeDataTable = EmployeeDataTable.DefaultView.ToTable();

        foreach (var item in EmployeeDataTable.Rows)
        {
            Console.WriteLine("ID : {0}\t Name : {1}\t Country : {2}\t Salary : {3}\t Date : {4}\n",
                    ((DataRow)item)["ID"], ((DataRow)item)["Name"], ((DataRow)item)["Country"], ((DataRow)item)["Salary"], ((DataRow)item)["Date"]);

        }
    }

    static void DeleteRowsInDataTable(DataTable EmployeeDataTable)
    {
        // 5)  -  Delete rows in the DataTable :

        Console.WriteLine("\n********* Delete Rows where ID = 7 : *********\n");

        // Find the row with ID = 7
        DataRow[] ReturnValue = EmployeeDataTable.Select("ID = 7");

        foreach (var row in ReturnValue)
        {
            // Delete the row
            row.Delete();
        }


        // Dlete rows Where ID = 8 or ID = 9
        Console.WriteLine("\n********* Delete Rows where ID = 8 or ID = 9 : *********\n");
        ReturnValue = EmployeeDataTable.Select("ID = 8 or ID = 9");

        foreach (var row in ReturnValue)
        {
            row.Delete();
        }

        EmployeeDataTable.AcceptChanges(); //  used if you want to commit the changes if you are linking the DataTable to a database

        // Display remaining rows
        DisplayDataTableContent(EmployeeDataTable);
    }

    static void UpdateRowInDataTable(DataTable EmployeeDataTable)
    {
        // 6)  -  Update a row in the DataTable :

        Console.WriteLine("\n********* Update Row where ID = 9 : *********\n");

        DataRow[] ReturnValue = EmployeeDataTable.Select("ID = 9");

        foreach (DataRow row in ReturnValue)
        {
            // Update the row
            row["Name"] = "veni perira ";
            row["Country"] = "Brazil   ";
            row["Salary"] = 30000;
            row["Date"] = DateTime.Now;
        }

        EmployeeDataTable.AcceptChanges(); // in this case we are not needing to commit because we are not linking the DataTable to a database

        // Display updated rows
        DisplayDataTableContent(EmployeeDataTable);
    }

    static void ClearAllDataInDataTable(DataTable EmployeeDataTable)
    {
        // 7)  -  Clear all data in the DataTable :

        Console.WriteLine("\n********* Clear All Data in the DataTable : *********\n");

        EmployeeDataTable.Clear();

        EmployeeDataTable.AcceptChanges();

        // Display the DataTable content after clearing
        DisplayDataTableContent(EmployeeDataTable);
    }


    static void PrimaryKeyInDataTable()
    {
        // 8)  -  Set Primary Key in the DataTable :

        Console.WriteLine("\n********* Set Primary Key in the DataTable : *********\n\n");

        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Country", typeof(string));
        dt.Columns.Add("Salary", typeof(double));
        dt.Columns.Add("Date", typeof(DateTime));

        // Make the ID column the primary key

        DataColumn[] PrimaryKeyColumns = new DataColumn[1];

        PrimaryKeyColumns[0] = dt.Columns["ID"];

        dt.PrimaryKey = PrimaryKeyColumns;

        // Add rows to the DataTable

        dt.Rows.Add(1, "Ayoub Chiki    ", "Morocco    ", 19000, DateTime.Now);
        dt.Rows.Add(2, "Ali Zahr       ", "KSA        ", 12000, DateTime.Now);
        /*
         * Try to add a row with the same primary key value (ID = 1)
         *  the program will throw an exception hahah
         * 
         */
        dt.Rows.Add(3, "Iris Sanders   ", "Netherlands", 70000, DateTime.Now);

        // display the DataTable content

        DisplayDataTableContent(dt);

    }

    static void AutoincrementDataTable()
    {
        // 9)  -  Autoincrement DataTable :

        DataTable DevlopperDataTable = new DataTable("DevlopperDataTable");

        DataColumn dtColum;
        dtColum = new DataColumn();
        dtColum.ColumnName = "ID";
        dtColum.DataType = typeof(int);
        dtColum.AutoIncrement = true;           // Enable AutoIncrement
        dtColum.AutoIncrementSeed = 1;          // Start from 1
        dtColum.AutoIncrementStep = 1;          // Increment by 1
        dtColum.AllowDBNull = false;            // Disallow null values
        dtColum.Unique = true;                  // Ensure uniqueness
        dtColum.Caption = "Developer ID";       // Optional caption for the column
        dtColum.ReadOnly = true;                // Make the column read-only

        DevlopperDataTable.Columns.Add(dtColum);

        dtColum = new DataColumn();
        dtColum.ColumnName = "Name";
        dtColum.DataType = typeof(string);
        dtColum.AllowDBNull = false;            // Disallow null values
        dtColum.Caption = "Developer Name";     // Optional caption for the column
        dtColum.ReadOnly = true;
        dtColum.MaxLength = 50;                 // Set maximum length for the string
        dtColum.ReadOnly = false;               // Make the column writable
        dtColum.Unique = false;                // Allow duplicate names

        DevlopperDataTable.Columns.Add(dtColum);

        dtColum = new DataColumn();
        dtColum.ColumnName = "Country";
        dtColum.DataType = typeof(string);
        dtColum.AllowDBNull = true;            // allow null values
        dtColum.Caption = "Country of Origin"; // Optional caption for the column
        dtColum.ReadOnly = false;              // Make the column writable
        dtColum.Unique = false;               // Allow duplicate countries

        DevlopperDataTable.Columns.Add(dtColum);

        dtColum = new DataColumn();
        dtColum.ColumnName = "Salary";
        dtColum.DataType = typeof(double);
        dtColum.AllowDBNull = false;            // Disallow null values
        dtColum.Caption = "Salary";             // Optional caption for the column
        dtColum.ReadOnly = false;              // Make the column writable
        dtColum.Unique = false;                // Allow duplicate salaries

        DevlopperDataTable.Columns.Add(dtColum);

        dtColum = new DataColumn();
        dtColum.ColumnName = "Date";
        dtColum.DataType = typeof(DateTime);
        dtColum.AllowDBNull = true;
        dtColum.Caption = "Joining Date";      // Optional caption for the column
        dtColum.ReadOnly = true;

        DevlopperDataTable.Columns.Add(dtColum);


        // Add rows to the DataTable
        DevlopperDataTable.Rows.Add(null, "Alice Johnson", "USA     ", 80000, DateTime.Now);
        DevlopperDataTable.Rows.Add(null, "Bob Smith    ", "UK      ", 75000, DateTime.Now);
        DevlopperDataTable.Rows.Add(null, "Charlie Brown", "Canada  ", 70000, DateTime.Now);
        DevlopperDataTable.Rows.Add(null, "David Wilson ", "Australia", 72000, DateTime.Now);
        DevlopperDataTable.Rows.Add(null, "Eva Green    ", "Germany  ", 78000, DateTime.Now);
        DevlopperDataTable.Rows.Add(null, "Frank White  ", "France   ", 76000, DateTime.Now);



        DisplayDataTableContent(DevlopperDataTable);

    }
    public static void Main()
    {
        DataTable EmployeeDataTable = CreateEmployeeDataTable();

        DisplayDataTableContent(EmployeeDataTable);

        //ShowAggregateFunctions(EmployeeDataTable);

        // FilterDataTable(EmployeeDataTable);

        //SortDataTable(EmployeeDataTable);

        //DeleteRowsInDataTable(EmployeeDataTable);

        //UpdateRowInDataTable(EmployeeDataTable);

        //ClearAllDataInDataTable(EmployeeDataTable);

        //PrimaryKeyInDataTable();

        // AutoincrementDataTable();

        Console.ReadKey();
    }
}

