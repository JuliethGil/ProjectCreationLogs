namespace Project.Controller
{
    using System;
    using static CrearLog.Enumerators.LogEnumerators;
    using Project.Interfaces;
    using Project.Constants;
    using Project.Models;

    public class FileAccessController : IFileAccessController
    {
        #region Constructors
        public FileAccessController()
        {
        }
        #endregion

        #region Interface Methods
        public JobLogResponseModel InsertFile(string message, TypeMessage typeError)
        {
            JobLogResponseModel jobLoggerResponseModel = new JobLogResponseModel();
            string dataFile = string.Empty;

            try
            {
                string NameLogFile = $"{System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"]}LogFile{DateTime.Now.ToShortDateString()}.txt";

                if (System.IO.File.Exists(NameLogFile))
                {
                    dataFile = System.IO.File.ReadAllText(NameLogFile);
                }
                else
                {
                    System.IO.File.Create(NameLogFile);
                }

                string typeErrorName = TypeErrorName(typeError);

                dataFile = $"{dataFile}{Environment.NewLine}{DateTime.Now.ToShortDateString()} - {typeErrorName} - {message}";

                System.IO.File.WriteAllText(NameLogFile, dataFile);
            }
            catch (Exception exception)
            {
                jobLoggerResponseModel.IsSuccess = false;
                jobLoggerResponseModel.Message = $"Error file: {exception.Message}";
                jobLoggerResponseModel.Source = TypeLogSourse.File;
            }

            return jobLoggerResponseModel;
        }

        private string TypeErrorName(TypeMessage typeError)
        {
            string typeMessageName = string.Empty;
            switch ((int)typeError)
            {
                case (int)TypeMessage.Error:
                    typeMessageName = TypeMessageName.TypeMessageNameError;
                    break;
                case (int)TypeMessage.Warning:
                    typeMessageName = TypeMessageName.TypeMessageNameWarning;
                    break;
                case (int)TypeMessage.Message:
                    typeMessageName = TypeMessageName.TypeMessageNameMessage;
                    break;
            }

            return typeMessageName;
        }
        #endregion
    }
}
