using Project.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static CrearLog.Enumerators.LogEnumerators;

namespace ProjectUnitTests.DataObject
{
    public static class JobLoggerResponseDataObject
    {
        public static JobLogResponseModel CreateJobLogResponse(bool isSuccess,string message, TypeLogSourse source)
        {
            JobLogResponseModel jobLogResponseModel = new JobLogResponseModel();
            jobLogResponseModel.IsSuccess = isSuccess;
            jobLogResponseModel.Message = message;
            jobLogResponseModel.Source = source;
            return jobLogResponseModel;
        }
    }
}
