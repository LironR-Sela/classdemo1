using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BanlAccountClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!!");
            Console.WriteLine("Please enter your email address:");
            var emailAddress = Console.ReadLine();

            Console.WriteLine("Please enter your password:");
            var password = Console.ReadLine();

            var payload = new {
                EmailAddress = emailAddress,
                Password = password
            };

            var selectedAccount = SendPostCommand(payload).Result;
            var accountId = int.Parse(JToken.Parse(selectedAccount)["id"].ToString());
            Console.WriteLine(selectedAccount);
            Console.WriteLine(accountId);

            Console.WriteLine("Please enter amount:");
            var amount =Console.ReadLine();
            Console.WriteLine($"New amount: {SendPutCommand(accountId, amount).Result}");

            Console.ReadKey();
        }

        static async Task<string> SendPostCommand(object payload)
        {
            HttpClient httpClient = new HttpClient();
            string userInfo = JsonConvert.SerializeObject(payload);
            var response = await httpClient.PostAsync($"https://localhost:44318/api/accounts/", new StringContent(userInfo, Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        static async Task<string> SendPutCommand(int accountId, string amount)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PutAsync($"https://localhost:44318/api/accounts/{accountId}", new StringContent(amount, Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
