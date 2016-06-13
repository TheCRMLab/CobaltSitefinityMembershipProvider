using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CobaltSitefinityMembershipProvider
{
    public static class CobaltWebApi
    {
        public const int MAX_NUMBER_OF_RECORDS = 5000;

        //You can use this url for testing. Replace with API Endpoint URL
        public static string COBALT_API_ENDPOINT_URL = GetConfigurationValue("CobaltApiUrl");
        //You can use this security key for testing. Replace with Cobalt Supplied API Security Key
        public static string COBALT_API_SECURITY_KEY = GetConfigurationValue("CobaltApiKey");
        public static MembershipUser GetUserById(Guid id)
        {
            var client = new RestClient(COBALT_API_ENDPOINT_URL);
            RestRequest request = CreateRequest(string.Format("api/v1.0/MembershipUser/{0}", id), Method.GET);
            // automatically deserialize result
            IRestResponse<MembershipUser> response = client.Execute<MembershipUser>(request);
            if (response.StatusCode.IsSuccessStatusCode())
            {
                return response.Data;
            }
            throw new ApplicationException(response.Content);
        }

        public static MembershipUser GetUserByUserName(string userName)
        {
            var client = new RestClient(COBALT_API_ENDPOINT_URL);
            RestRequest request = CreateRequest("api/v1.0/MembershipUser", Method.GET);
            request.AddParameter("userName", userName);
            // automatically deserialize result
            IRestResponse<MembershipUser> response = client.Execute<MembershipUser>(request);
            if (response.StatusCode.IsSuccessStatusCode())
            {
                return response.Data;
            }
            throw new ApplicationException(response.Content);
        }

        public static List<MembershipUser> GetUsers(int pageSize, int pageIndex)
        {
            var client = new RestClient(COBALT_API_ENDPOINT_URL);
            RestRequest request = CreateRequest("api/v1.0/MembershipUser", Method.GET);
            request.AddParameter("pageSize", pageSize);
            request.AddParameter("pageIndex", pageIndex);
            // automatically deserialize result
            IRestResponse<List<MembershipUser>> response = client.Execute<List<MembershipUser>>(request);
            if (response.StatusCode.IsSuccessStatusCode())
            {
                return response.Data;
            }
            throw new ApplicationException(response.Content);
        }

        public static List<MembershipRole> GetRoles()
        {
            var client = new RestClient(COBALT_API_ENDPOINT_URL);
            RestRequest request = CreateRequest("api/v1.0/MembershipRole", Method.GET);
            // automatically deserialize result
            IRestResponse<List<MembershipRole>> response = client.Execute<List<MembershipRole>>(request);
            if (response.StatusCode.IsSuccessStatusCode())
            {
                return response.Data;
            }
            throw new ApplicationException(response.Content);
        }

        public static bool ValidateUser(string userName, string password)
        {
            var client = new RestClient(COBALT_API_ENDPOINT_URL);   
            RestRequest request = CreateRequest(string.Format("api/v1.0/Authentication?userName={0}&password={1}", userName, password), Method.POST);
            request.AddParameter("userName", userName); // adds to POST or URL querystring based on Method
            request.AddParameter("password", password); // adds to POST or URL querystring based on Method
            IRestResponse<Guid> response = client.Execute<Guid>(request);
            if (response.StatusCode.IsSuccessStatusCode())
            {
                return (response.Data != Guid.Empty);
            }
            throw new ApplicationException(response.Content);
        }

        public static List<MembershipUser> GetUsersInRole(Guid roleId, int pageSize, int pageIndex)
        {
            var client = new RestClient(COBALT_API_ENDPOINT_URL);
            RestRequest request = CreateRequest(string.Format("api/v1.0/MembershipRole/{0}", roleId), Method.GET);
            request.AddParameter("pageSize", pageSize);
            request.AddParameter("pageIndex", pageIndex);
            // automatically deserialize result
            IRestResponse<List<MembershipUser>> response = client.Execute<List<MembershipUser>>(request);
            if (response.StatusCode.IsSuccessStatusCode())
            {
                return response.Data;
            }
            throw new ApplicationException(response.Content);
        }
        public static MembershipRole GetRoleById(Guid id)
        {
            var client = new RestClient(COBALT_API_ENDPOINT_URL);
            RestRequest request = CreateRequest(string.Format("api/v1.0/MembershipRole/{0}", id), Method.GET);
            // automatically deserialize result
            IRestResponse<MembershipRole> response = client.Execute<MembershipRole>(request);
            if (response.StatusCode.IsSuccessStatusCode())
            {
                return response.Data;
            }
            throw new ApplicationException(response.Content);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestApi">The path to the API method e.g. resource/{id}</param>
        /// <param name="requestMethod">The method e.g. POST, GET etc.</param>
        /// <returns></returns>
        public static RestRequest CreateRequest(string requestApi, Method requestMethod)
        {
            var request = new RestRequest(requestApi, requestMethod);
            // easily add HTTP Headers
            request.AddHeader("CobaltSecurityKey", COBALT_API_SECURITY_KEY);
            return request;
        }

        public static bool IsSuccessStatusCode(this HttpStatusCode responseCode)
        {
            int numericResponse = (int)responseCode;
            return numericResponse >= 200
                && numericResponse <= 399;
        }

        public static string GetConfigurationValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
