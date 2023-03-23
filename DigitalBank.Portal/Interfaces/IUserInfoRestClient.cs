namespace DigitalBank.Portal
{
    using DigitalBank.Portal.Models;

    public interface IUserInfoRestClient
    {
        void Post(UserInfo userInfo);
        
        IEnumerable<UserInfo> GetAll();
        UserInfo GetById(int id);
        
        void Put(UserInfo userInfo);
        void Delete(int id);
    }
}
