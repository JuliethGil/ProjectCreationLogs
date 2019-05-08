namespace Project.Interfaces
{ 
    using Project.Models;
    using static CrearLog.Enumerators.LogEnumerators;

    public interface IFileAccessController
    {
        JobLogResponseModel InsertFile(string message, TypeMessage typeError);
    }
}
