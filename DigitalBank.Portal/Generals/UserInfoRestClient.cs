

namespace DigitalBank.Portal
{
    using DigitalBank.Portal.Models;
    using RestSharp;
    using System.Net;
    using System.Security.Policy;

    public class UserInfoRestClient : IUserInfoRestClient
    {
        private readonly RestClient _client;
        private readonly string _url = "https://localhost:7129/";
      
        public UserInfoRestClient()
        {
            _client = new RestClient ( _url );
          
        }

        public IEnumerable<UserInfo> GetAll()
        {
            var request = new RestRequest("api/v1/users", Method.Get) { RequestFormat = DataFormat.Json };

            var response = _client.Execute<List<UserInfo>>(request);

            if (response.Data == null)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }

        public UserInfo GetById(int id)
        {
            var request = new RestRequest("api/v1/users/{id}", Method.Get) { RequestFormat = DataFormat.Json };

            request.AddParameter("id", id, ParameterType.UrlSegment);
            
            RestResponse<List<UserInfo>> response =  _client.Execute<List<UserInfo>>(request);
          
            if (response.Data == null)
                throw new Exception(response.ErrorMessage);

            return (new UserInfo {  Id = response.Data[0].Id, 
                                    Name = response.Data[0].Name, 
                                    Birthdate = response.Data[0].Birthdate, 
                                    Sex = response.Data[0].Sex}
            );
        }
       

        public void Post(UserInfo  userInfo)
        {
            var request = new RestRequest("api/v1/users", Method.Post).AddJsonBody(userInfo);
            var response =  _client.ExecutePost<UserInfo>(request);
        }

        public void Put(UserInfo userInfo)
        {
            var request = new RestRequest("api/v1/users/{id}", Method.Put).AddJsonBody(userInfo); 
            request.AddParameter("id", userInfo.Id, ParameterType.UrlSegment);         

            var response = _client.ExecutePut<UserInfo>(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception(response.ErrorMessage);
        }

        public void Delete(int id)
        {
            var request = new RestRequest("api/v1/users/{id}", Method.Delete);
            request.AddParameter("id", id, ParameterType.UrlSegment);

            var response = _client.Execute<UserInfo>(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception(response.ErrorMessage);
        }
    }
}
