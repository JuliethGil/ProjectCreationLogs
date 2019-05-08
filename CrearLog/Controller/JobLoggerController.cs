namespace CrearLog.Controller
{
    using System;
    using System.Collections.Generic;
    using static CrearLog.Enumerators.LogEnumerators;
    using Project.DataAccess.Queries;
    using Project.Interfaces;
    using Project.Constants;
    using Project.Controller;
    using Project.Models;

    public class JobLoggerController : IJobLoggerController
    {
        #region Attributes
        private bool logToFile;
        private bool logToConsole;
        private bool logToDatabase;
        private readonly TypeMessage typeMessage;
        public List<JobLogResponseModel> jobLoggerResponse;
        private readonly IFileAccessController fileAccessController;
        private readonly IConsoleAccessController consoleAccessController;
        private readonly ILogQuery logQuery;

        #endregion

        #region Contructors
        public JobLoggerController(bool logToFile, bool logToConsole, bool logToDatabase, TypeMessage typeError)
        {
            this.logToFile = logToFile;
            this.logToConsole = logToConsole;
            this.logToDatabase = logToDatabase;
            this.typeMessage = typeError;
            jobLoggerResponse = new List<JobLogResponseModel>();
            fileAccessController = new FileAccessController();
            consoleAccessController = new ConsoleAccessController();
            logQuery = new LogQuery();
        }

        public JobLoggerController(
            bool logToFile,
            bool logToConsole,
            bool logToDatabase,
            TypeMessage typeMessage,
            IFileAccessController fileAccessController,
            IConsoleAccessController consoleAccessController,
            ILogQuery logQuery
            )
        {
            this.logToFile = logToFile;
            this.logToConsole = logToConsole;
            this.logToDatabase = logToDatabase;
            this.typeMessage = typeMessage;
            jobLoggerResponse = new List<JobLogResponseModel>();
            this.fileAccessController = fileAccessController;
            this.consoleAccessController = consoleAccessController;
            this.logQuery = logQuery;
        }
        #endregion

        #region Interface Methods
        public List<JobLogResponseModel> LogMessage(string message)
        {
            string messageTrim = message.Trim();
            if (string.IsNullOrEmpty(message))
            {
                throw new Exception(ErrorMessages.ErrorTypeMessage);
            }

            if (IsSelectedTypeStorage())
            { 
                throw new Exception(ErrorMessages.ErrorTypeStorage);
            }

            JobLogResponseModel jobLogResponse = new JobLogResponseModel();

            if (logToDatabase)
            {
                jobLogResponse = LogDataBase(messageTrim);
                jobLoggerResponse.Add(jobLogResponse);
            }

            if (logToFile)
            {
                jobLogResponse = LogFile(messageTrim);
                jobLoggerResponse.Add(jobLogResponse);
            }

            if (logToConsole)
            {
                jobLogResponse = LogConsole(messageTrim);
                jobLoggerResponse.Add(jobLogResponse);
            }

            return jobLoggerResponse;
        }
        #endregion

        #region Private Methods
        private bool IsSelectedTypeStorage()
        {
            return (!logToConsole && !logToFile && !logToDatabase);
        }

        private JobLogResponseModel LogDataBase(string message)
        {
            LogDataBaseModel logModel = new LogDataBaseModel
            {
                Message = message,
                TypeMessage = (int)typeMessage
            };
            return logQuery.DataBaseInsert(logModel);
        }

        private JobLogResponseModel LogFile(string message)
        {
            return fileAccessController.InsertFile(message, typeMessage);
        }

        private JobLogResponseModel LogConsole(string message)
        {
            return consoleAccessController.ViewConsole(message, typeMessage);
        }
        #endregion
    }
}
