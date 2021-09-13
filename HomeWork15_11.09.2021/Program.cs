using System;
using System.Data.SqlClient;

namespace HomeWork15_11._09._2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var constring = "Data source=localhost; Initial catalog = HomeWork15_11.09.2021; Integrated security = true";
            SqlConnection sqlConnection = new SqlConnection(constring);
            sqlConnection.Open();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection is opened");
            }
            Console.WriteLine();
            Console.WriteLine("Enter your operation: \n1. Insert;\n2. Select All;\n3. Select by ID;\n4. Update;\n5. Delete;");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1: Console.WriteLine("Enter last name:");
                    string lastname = Console.ReadLine();
                    Console.WriteLine("Enter first name:");
                    string firstname = Console.ReadLine();
                    Console.WriteLine("Enter date of birth (e.g. 2000-02-03):");
                    string birthdate = Console.ReadLine();
                    Insert(sqlConnection, new Person { LastName = lastname, FirstName = firstname, BirthDate = birthdate });
                    break;

                case 2: SelectAll (sqlConnection);
                    break;

                case 3: Console.WriteLine("Enter the ID you want to select:");
                    int selectId = int.Parse(Console.ReadLine());
                    SelectById(sqlConnection, selectId);
                    break;

                case 4: Console.WriteLine("Enter the ID you want to update:");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter last name:");
                    string lastname_update = Console.ReadLine();
                    Console.WriteLine("Enter first name:");
                    string firstname_update = Console.ReadLine();
                    Console.WriteLine("Enter date of birth (e.g. 2000-02-03):");
                    string birthdate_update = Console.ReadLine();
                    Update(sqlConnection, updateId, new Person {LastName = lastname_update, FirstName = firstname_update, BirthDate = birthdate_update});
                    break;

                case 5:
                    Console.WriteLine("Enter the ID you want to delete:");
                    int deleteId = int.Parse(Console.ReadLine());
                    Delete(sqlConnection, deleteId);
                    break;

            }

            sqlConnection.Close();
            Console.WriteLine();
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                Console.WriteLine("Connection is closed");
            }
           static void Insert (SqlConnection sqlConnection, Person person)
            {
                var sqlQuery = $"INSERT INTO Person (LastName, FirstName, BirthDate) VALUES ('{person.LastName}', '{person.FirstName}', '{person.BirthDate}');";
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = sqlQuery;
                var result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Student added successfully");
                }

                else
                {
                    Console.WriteLine("Operation was not succeeded");
                }
            }

            static void SelectAll (SqlConnection sqlConnection)
            {
                var sqlQuery = "SELECT * FROM Person;";
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = sqlQuery;
                var sqlreader = sqlCommand.ExecuteReader();
                while (sqlreader.Read())
                    {
                        Console.WriteLine($"ID: {sqlreader.GetValue(0)}, Last name: {sqlreader.GetValue(1)}, First name: {sqlreader.GetValue(2)}, Date of Birth: {sqlreader.GetValue(4)}");
                    }
                sqlreader.Close();

            }

            static void SelectById(SqlConnection sqlConnection, int selectId)
            {
                var sqlQuery = $"SELECT * FROM Person WHERE ID = {selectId};";
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = sqlQuery;
                var sqlreader = sqlCommand.ExecuteReader();
                while (sqlreader.Read())
                {
                    Console.WriteLine($"ID: {sqlreader.GetValue(0)}, Last name: {sqlreader.GetValue(1)}, First name: {sqlreader.GetValue(2)}, Date of Birth: {sqlreader.GetValue(4)}");
                }

                sqlreader.Close();

            }

            static void Delete(SqlConnection sqlConnection, int deleteId)
            {
                var sqlQuery = $"DELETE FROM Person WHERE ID = {deleteId};";
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = sqlQuery;
                var result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Student deleted successfully");
                }

                else
                {
                    Console.WriteLine("Operation was not succeeded");
                }
            }

            static void Update(SqlConnection sqlConnection, int updateId, Person person)
            {
                var sqlQuery = $"UPDATE Person SET LastName = '{person.LastName}', FirstName = '{person.FirstName}', BirthDate = '{person.BirthDate}' WHERE ID = {updateId};";
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = sqlQuery;
                var result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Student updated successfully");
                }

                else
                {
                    Console.WriteLine("Operation was not succeeded");
                }
            }



        }
    }

    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string BirthDate { get; set; }
    }

}

   
