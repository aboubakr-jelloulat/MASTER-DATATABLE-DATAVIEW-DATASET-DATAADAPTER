using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

/**
 * ============================== DataAdapter ==============================
 *
 * A DataAdapter acts as a **bridge** between a database and a DataTable/DataSet.
 * It allows reading data **from the DB into memory**, and **sending changes back**.
 *
 * =========================================================================
 * 🔧 Key Roles:
 * =========================================================================
 * 1. Fills a DataTable/DataSet using `Fill()`.
 * 2. Pushes updates to the DB using `Update()`.
 *
 * It works in **disconnected mode**, meaning:
 *   - The data is loaded once into memory.
 *   - Changes are made offline.
 *   - Then `Update()` is called to sync with the DB.
 *
 * =========================================================================
 * 🔑 Important Properties:
 * =========================================================================
 * • SelectCommand   → SQL to fetch data.
 * • InsertCommand   → SQL to insert new records.
 * • UpdateCommand   → SQL to update changed records.
 * • DeleteCommand   → SQL to remove records.
 *
 * =========================================================================
 * DataAdapter = 🚚 The tool that loads DB data into memory and syncs it back.
 *
 * =========================================================================
 */



namespace Data_Adapter
{
    internal class Program
    {

        public static string ConnectionString = "Server=.;Database=ContactsDB;User Id=sa;Password=sa123456;";


        static void DataAdapterForMouseDevloppers()
        {

            // 1 - Create a new DataSet

            DataSet dataset = new DataSet();

            // 2 - write the SQL query to fetch data 

            string Query = "SELECT * FROM Contacts";

            // 3 - Create a DataAdapter with the SQL query and connection string

            SqlDataAdapter dataAdapter = new SqlDataAdapter(Query, ConnectionString);

            // 4 - Open the connection
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            // 5 - give the DataAdapter the connection

            dataAdapter.SelectCommand.Connection = connection;

            // 6 - fill the dataset with the data from the database and make it in new table called "MouseDevloppers"

            dataAdapter.Fill(dataset, "Contacts");

            // 7 - close the connection

            connection.Close();

            /*
             * we close the connection because the DataAdapter has already fetched the data
             * 
             * we can now work with the dataset in memory 
             * 
             */

            

            // 8 -  make a reference to the MouseDevloppers table in the dataset

            DataTable Contacts = dataset.Tables["Contacts"];

            // 8 - Display the data in the dataset

            Console.WriteLine("Data from Contacts table in the dataset:\n\n");
            foreach (DataRow row in Contacts.Rows)
            {
                Console.WriteLine("ID :   {0}, First Name :    {1}, Last Name   :    {2},  Email  :    {3}, Phone   :   {4}",
                row["ContactID"], row["FirstName"], row["LastName"], row["Email"], row["Phone"]);

            }

            // 9 - Now we can modify the data in the dataset and then update the database (add / delete / edite)


            // 10 - open the connection again to update the database

            connection.Open();

            // 11 - set the UpdateCommand for the DataAdapter

            //dataAdapter.UpdateCommand.Connection = connection;

            // 12 - updte the dataset with the changes made to the Contacts table

            dataAdapter.Update(dataset, "Contacts");

            // 13 - Close the connection

            connection.Close();



        }

        static void Main(string[] args)
        {

            DataAdapterForMouseDevloppers();

            Console.ReadKey();
        }
    }
}
