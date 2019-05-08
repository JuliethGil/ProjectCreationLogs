namespace Project.Interfaces
{ 
    using Project.Models;

    public interface ILogQuery
    {
        JobLogResponseModel DataBaseInsert(LogDataBaseModel logModel);
    }
}
