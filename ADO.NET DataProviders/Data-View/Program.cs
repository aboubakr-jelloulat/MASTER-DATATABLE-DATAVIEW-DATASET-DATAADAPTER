using System;
using System.Linq;
using System.Data;


/**
 * ====================== DataView Deep Explanation ======================
 *
 * A DataView is a customizable view of a DataTable.
 * It does NOT copy the data — instead, it provides a window to view,
 * sort, filter, and search the data stored in the DataTable.
 *
 * 🔗 Relationship to DataTable:
 * - DataView is directly connected to the DataTable.
 * - Any changes you make to data via the DataView (like editing a cell)
 *   will reflect in the original DataTable.
 * - This is because both DataView and DataTable share the same data in memory.
 *
 * ✅ Example:
 *   DataView view = table.DefaultView;
 *   view[0]["Name"] = "John"; // This also updates table.Rows[0]["Name"]
 *
 * 🔍 Sorting and Filtering:
 * - You can sort and filter the data using:
 *     view.Sort = "ID DESC";
 *     view.RowFilter = "Country = 'UK'";
 * - These operations only affect the view, NOT the actual DataTable structure.
 *
 * 📋 Creating a copy:
 * - If you use view.ToTable(), a new DataTable is created with the view's data.
 *   This is a full copy and is no longer linked to the original.
 *
 * ⚡ Performance:
 * - DataView is efficient because it works in-place without duplicating data.
 * - It's especially useful for data binding (e.g., in UI apps).
 * - Using ToTable() adds overhead because it duplicates rows.
 *
 * ⚠️ Summary:
 * | Action               | Affects DataTable | Performance |
 * |----------------------|-------------------|-------------|
 * | Edit via DataView    | ✅ Yes             | Fast        |
 * | Sort/Filter in view  | ❌ No              | Fast        |
 * | ToTable() (copy)     | ❌ No              | Slower      |
 *
 * =======================================================================
 */

public class Data
{
    static DataTable CreateDataTable()
    {
        // - create DataTable :

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
        DevlopperDataTable.Rows.Add(null, "bakr jelloulat","Morocco ", 82000, DateTime.Now);

        return DevlopperDataTable;

    }

    static void DisplayDataTableContent(DataTable EmployeeDataTable)
    {
        Console.WriteLine("\n*********  Employees List : *********\n");
        foreach (DataRow Recordrow in EmployeeDataTable.Rows)
        {
            Console.WriteLine("ID : {0}\t Name : {1}\t Country : {2}\t Salary : {3}\t Date : {4}\n",
                    Recordrow["ID"], Recordrow["Name"], Recordrow["Country"], Recordrow["Salary"], Recordrow["Date"]);
        }
    }


    static void Create_DiplayDataView(DataTable EmployeeDataTable)
    {
        // - create DataView :

        DataView EmployeeDataView1 = EmployeeDataTable.DefaultView;

        for (int i = 0; i < EmployeeDataView1.Count; i++)
        {
            Console.WriteLine("ID :: {0}\t Name :: {1}\t Country :: {2}\t Salary :: {3}\t Date :: {4}\n",
                    EmployeeDataView1[i]["ID"], EmployeeDataView1[i]["Name"], EmployeeDataView1[i]["Country"], EmployeeDataView1[i][3], EmployeeDataView1[i][4]);
        }
    }


    static void FilterDataView(DataTable EmployeeDataTable)
    {
        // - create DataView with filter :

        DataView EmployeeDataView2 = EmployeeDataTable.DefaultView;

        // Filter for employees from the USA
        EmployeeDataView2.RowFilter = "Country = 'Morocco'";

        Console.WriteLine("\n*********  Employees from Morocco : *********\n");

        for (int i = 0; i < EmployeeDataView2.Count; i++)
        {
            Console.WriteLine("ID :: {0}\t Name :: {1}\t Country :: {2}\t Salary :: {3}\t Date :: {4}\n",
                    EmployeeDataView2[i]["ID"], EmployeeDataView2[i]["Name"], EmployeeDataView2[i]["Country"], EmployeeDataView2[i][3], EmployeeDataView2[i][4]);
        }
    }

    static void SortingDataInDataView(DataTable EmployeeDataTable)
    {
        // - create DataView with sorting :
        DataView EmployeeDataView3 = EmployeeDataTable.DefaultView;

        // Sort by Name in descending order
        EmployeeDataView3.Sort = "Name ASC";

        Console.WriteLine("\n*********  Employees sorted by Name (ASC) : *********\n");
        for (int i = 0; i < EmployeeDataView3.Count; i++)
        {
            Console.WriteLine("ID :: {0}\t Name :: {1}\t Country :: {2}\t Salary :: {3}\t Date :: {4}\n",
                    EmployeeDataView3[i]["ID"], EmployeeDataView3[i]["Name"], EmployeeDataView3[i]["Country"], EmployeeDataView3[i][3], EmployeeDataView3[i][4]);
        }
    }

    static void Main(string[] args)
    {
        
        DataTable EmployeeDataTable = CreateDataTable();

        //DisplayDataTableContent(EmployeeDataTable);

        //Create_DiplayDataView(EmployeeDataTable);
        
        //FilterDataView(EmployeeDataTable);

        //SortingDataInDataView(EmployeeDataTable);


        Console.ReadKey(); // Wait for user input before closing the console window

    }

}
