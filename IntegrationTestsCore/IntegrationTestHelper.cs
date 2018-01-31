using System;
using System.Data.SqlClient;
using System.IO;

namespace IntegrationTestsCore
{
    public abstract class IntegrationTestHelper
    {
        public void CreateTestDatabase()
        {
            if (!System.IO.File.Exists(Constants.FullTestDBFileName))
            {
                var connection = new SqlConnection(
                    @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=master; Integrated Security=true;");

                using (connection)
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText =
                            String.Format($"IF (DB_ID('{Constants.DB_NAME}') IS NULL) CREATE DATABASE {Constants.DB_NAME} ON PRIMARY (NAME={Constants.DB_NAME}, FILENAME='{Constants.FullTestDBFileName}')");
                        command.ExecuteNonQuery();

                        command.CommandText =
                            String.Format("EXEC sp_detach_db '{0}', 'true'", Constants.DB_NAME);
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                string sqlConnectionString = $@"Data Source=(localdb)\mssqllocaldb;
                          AttachDbFilename = {Constants.FullTestDBFileName};
            Integrated Security = True;
            Connect Timeout = 30;";

                connection = new SqlConnection(sqlConnectionString);

                using (connection)
                {
                    connection.Open();

                    FileInfo file = new FileInfo(Constants.CreateScriptTestDB);
                    string script = file.OpenText().ReadToEnd();
                    script = script.Replace("GO", "");

                    using (var command = new SqlCommand(script, connection))
                    {
                        command.CommandText = script;

                        command.ExecuteNonQuery();
                    }

                    file = new FileInfo(Constants.InsertTestDataScriptTestDB);
                    script = file.OpenText().ReadToEnd();
                    script = script.Replace("GO", "");

                    using (var command = new SqlCommand(script, connection))
                    {
                        command.CommandText = script;

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }

        public void DetachTestDatabase()
        {
            var connection = new SqlConnection(
                    @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=master; Integrated Security=true;");

            using (connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
    String.Format($"ALTER DATABASE [{Constants.FullTestDBFileName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", Constants.FullTestDBFileName);
                    command.ExecuteNonQuery();

                    command.CommandText =
                        String.Format("EXEC sp_detach_db '{0}', 'true'", Constants.FullTestDBFileName);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Delete temporary  mdf file.
        /// </summary>
        public void DeleteTestDatabase()
        {
            if (File.Exists(Constants.FullTestDBFileName))
            {
                File.Delete(Constants.FullTestDBFileName);
            }

            if (File.Exists(Constants.FullTestDBLogFileName))
            {
                File.Delete(Constants.FullTestDBLogFileName);
            }
        }
    }
}
