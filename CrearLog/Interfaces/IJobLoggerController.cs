namespace Project.Interfaces
{
    using System.Collections.Generic;
    using Project.Models;
    interface IJobLoggerController
    {
        List<JobLogResponseModel> LogMessage(string message);
    }
}
