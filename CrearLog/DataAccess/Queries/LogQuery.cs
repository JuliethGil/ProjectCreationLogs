namespace Project.DataAccess.Queries
{
    using System;
    using System.Data.SqlClient;
    using static CrearLog.Enumerators.LogEnumerators;
    using Project.DataAccess;
    using Project.Interfaces;
    using Project.Models;

    public class LogQuery : ILogQuery
    {
        public JobLogResponseModel DataBaseInsert(LogDataBaseModel logDataBaseModel)
        {
            JobLogResponseModel dataBaseResponse = new JobLogResponseModel();

            try
            {
                DataBaseContext dataBaseContext = new DataBaseContext();
                dataBaseContext.ConctionDataBase();
                SqlCommand command = new SqlCommand("INSERT INTO Log(Message, typeMessage) (VALUES('" + logDataBaseModel.Message + "', " + logDataBaseModel.TypeMessage + ")");
                command.ExecuteNonQuery();
                dataBaseContext.ClosedConctionDataBase();
            }
            catch (Exception exception)
            {
                dataBaseResponse.IsSuccess = false;
                dataBaseResponse.Message = $"Error DataBase: {exception.Message}";
                dataBaseResponse.Source = TypeLogSourse.Console;
            }
            return dataBaseResponse;
        }
    }
}
