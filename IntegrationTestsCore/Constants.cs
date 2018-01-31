using System.IO;

namespace IntegrationTestsCore
{
    public static class Constants
    {
        public const string DB_NAME = "testdb";
        const string DB_FILE_NAME = "testdb.mdf";
        const string DB_LOG_FILE_NAME = "testdb_log.ldf";

        public static string FullTestDBFileName = System.IO.Path.Combine(Path.GetTempPath(), DB_FILE_NAME);
        public static string FullTestDBLogFileName = System.IO.Path.Combine(Path.GetTempPath(), DB_LOG_FILE_NAME);
        public static string TestConnectionString = 
            $@"Data Source=(localdb)\mssqllocaldb;
            AttachDbFilename = {Constants.FullTestDBFileName};
            Integrated Security = True;
            Connect Timeout = 30;";

        public static string CreateScriptTestDB = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"Scripts\CreateScript.sql");
        public static string InsertTestDataScriptTestDB = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"Scripts\InsertTestDataScript.sql");
    }
}
