namespace Project.DataAccess
{
    using System.Configuration;
    using System.Data.SqlClient;

    public class DataBaseContext
    {
        private SqlConnection Connection;
        
        public void ConctionDataBase()
        {
            Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            Connection.Open();
        }

        public void ClosedConctionDataBase()
        {
            Connection.Close();
        }
    }
}
