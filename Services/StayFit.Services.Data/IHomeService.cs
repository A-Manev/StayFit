namespace StayFit.Services.Data
{
    public interface IHomeService
    {
        T GetUserInfo<T>(string id);
    }
}
