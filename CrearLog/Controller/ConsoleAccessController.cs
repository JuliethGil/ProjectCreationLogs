namespace Project.Controller
{
    using System;
    using static CrearLog.Enumerators.LogEnumerators;
    using Project.Interfaces;
    using Project.Constants;
    using Project.Models;

    public class ConsoleAccessController : IConsoleAccessController
    {
        #region Constuctors
        public ConsoleAccessController()
        {
        }
        #endregion

        #region Interface Methods
        public JobLogResponseModel ViewConsole(string message, TypeMessage typeError)
        {
            JobLogResponseModel jobLoggerResponse = new JobLogResponseModel();
            jobLoggerResponse.Source = TypeLogSourse.Console;

            try
            {
                string typeMessageName = string.Empty;

                switch ((int)typeError)
                {
                    case (int)TypeMessage.Error:
                        typeMessageName = TypeMessageName.TypeMessageNameError;
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case (int)TypeMessage.Warning:
                        typeMessageName = TypeMessageName.TypeMessageNameWarning;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case (int)TypeMessage.Message:
                        typeMessageName = TypeMessageName.TypeMessageNameMessage;
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                Console.WriteLine($"{DateTime.Now.ToShortDateString()} - {typeMessageName} -  {message}");

                jobLoggerResponse.IsSuccess = true;
                jobLoggerResponse.Message = message;
            }
            catch (Exception exception)
            {
                jobLoggerResponse.IsSuccess = false;
                jobLoggerResponse.Message = $"Error console: {exception.Message}";
            }

            return jobLoggerResponse;
        }
        #endregion
    }
}
