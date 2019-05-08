namespace CrearLog.Enumerators
{
    public class LogEnumerators
    {
        public enum TypeMessage
        {
            Message = 1,
            Error = 2,
            Warning = 3
        }

        public enum TypeLogSourse
        {
            DataBase = 1,
            File = 2,
            Console = 3
        }
    }
}
