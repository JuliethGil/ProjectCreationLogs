namespace Project.Interfaces
{
    using Project.Models;
    using static CrearLog.Enumerators.LogEnumerators;

    public interface IConsoleAccessController
    {
        JobLogResponseModel ViewConsole(string message, TypeMessage typeError);
    }
}
