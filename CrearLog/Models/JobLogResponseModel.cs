namespace Project.Models
{
    using static CrearLog.Enumerators.LogEnumerators;

    public class JobLogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TypeLogSourse Source { get; set; }
    }
}
