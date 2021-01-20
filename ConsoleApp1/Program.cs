using System;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string username = "8317-98DpNRv3";
            string password = "4a59915ada274f92ac6e128491855841";
            string webapi = "https://sandbox.billogram.com/api/v2/customer/12345";

            string StrBase64 = username + ":" + password;

            string encodedText = ToBase64Encode(StrBase64);
            Console.WriteLine("Base64 Encoded String: " + encodedText);

            string decodedText = ToBase64Decode(encodedText);
            Console.WriteLine("Base64 Decoded String: " + decodedText);

            string result = ExternalRequest(webapi, username, password);

            //Test
            //BaseClient clientbase = new BaseClient("https://website.com/api/v2/", "username", "password");
            //BaseResponse response = new BaseResponse();
            //BaseResponse response = clientbase.GetCallV2Async("Candidate").Result;

            Console.ReadLine();
        }

        //public async Task<BaseResponse> GetCallAsync(string endpoint)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await client.GetAsync(endpoint + "/").ConfigureAwait(false);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            baseresponse.ResponseMessage = await response.Content.ReadAsStringAsync();
        //            baseresponse.StatusCode = (int)response.StatusCode;
        //        }
        //        else
        //        {
        //            baseresponse.ResponseMessage = await response.Content.ReadAsStringAsync();
        //            baseresponse.StatusCode = (int)response.StatusCode;
        //        }
        //        return baseresponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        baseresponse.StatusCode = 0;
        //        baseresponse.ResponseMessage = (ex.Message ?? ex.InnerException.ToString());
        //    }
        //    return baseresponse;
        //}

        public static string ExternalRequest(string myurl,string user, string password)
        {
            string url = String.Format("http://example.com"); //here I have the correct url for my API
            url = myurl;
            HttpWebRequest requestObj = (HttpWebRequest)WebRequest.Create(url);
            requestObj.Method = "Get";
            requestObj.PreAuthenticate = true;
            //requestObj.Credentials = new NetworkCredential("testing", "123456");
            requestObj.Headers["Content-Type"] = "application/json";
            requestObj.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("username:password"));
            HttpWebResponse responseObj = null;
            responseObj = (HttpWebResponse)requestObj.GetResponse();
            string strresult = null;
            using (Stream stream = responseObj.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresult = sr.ReadToEnd();
                sr.Close();
            }
            return strresult;
        }


        public static string ToBase64Encode(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        public static string ToBase64Decode(string base64EncodedText)
        {
            if (String.IsNullOrEmpty(base64EncodedText))
            {
                return base64EncodedText;
            }

            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }

    public static class EncodingForBase64
    {
        public static string EncodeBase64(this System.Text.Encoding encoding, string text)
        {
            if (text == null)
            {
                return null;
            }

            byte[] textAsBytes = encoding.GetBytes(text);
            return System.Convert.ToBase64String(textAsBytes);
        }

        public static string DecodeBase64(this System.Text.Encoding encoding, string encodedText)
        {
            if (encodedText == null)
            {
                return null;
            }

            byte[] textAsBytes = System.Convert.FromBase64String(encodedText);
            return encoding.GetString(textAsBytes);
        }
    }

    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public string ResponseMessage { get; set; }
    }

    public class BaseClient
    {
        readonly HttpClient client;
        readonly BaseResponse baseresponse;

        public BaseClient(string baseAddress, string username, string password)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://127.0.0.1:8888"),
                UseProxy = false,
            };

            client = new HttpClient(handler);
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            baseresponse = new BaseResponse();

        }
    }
    }
