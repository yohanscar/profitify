using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace profitify.Util
{
    public class Hall9001
    {
        private string _apiBaseAddress = "https://profitfy.trade/";
        private string _APPId = "";
        private string _APIKey = "";

        public Hall9001(string pEndPoint, string pAPPId,string pAPIKey)
        {
            _APPId = pAPPId;
            _APIKey = pAPIKey;
            if (pEndPoint.Last() != '/')
            {
                pEndPoint += "/";
            }
            _apiBaseAddress = pEndPoint;
        }
        public Hall9001(string pEndPoint)
        {
            if(pEndPoint.Last() !='/')
            {
                pEndPoint += "/";
            }
            _apiBaseAddress = pEndPoint;
        }
        public UserInfo UserInfo(out bool Sucess)
        {
            UserInfo objRet = new UserInfo();

            var HttpReturn = Get(true, "userinfo/").Result;
            if(HttpReturn.IsSuccessStatusCode)
            {
                objRet = JsonConvert.DeserializeObject<UserInfo>(HttpReturn.Data);

            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }
         public List<Markets> Markets(out bool Sucess)
         {
             List<Markets> objRet = new List<Markets>();

             var HttpReturn = Get(false, "markets/").Result;
             if(HttpReturn.IsSuccessStatusCode)
             {
                 objRet = JsonConvert.DeserializeObject<List<Markets>>(HttpReturn.Data);
             }
             Sucess = HttpReturn.IsSuccessStatusCode;
             return objRet;
         }
        public List<Ticker> Ticker(string pCoinFrom, string pCoinTo, out bool Sucess)
        {
            List<Ticker> objRet = new List<Ticker>();

            var HttpReturn = Get(false, "ticker/" + pCoinFrom + "/" + pCoinTo).Result;
            if(HttpReturn.IsSuccessStatusCode)
            {
               objRet = JsonConvert.DeserializeObject<List<Ticker>>(HttpReturn.Data);
            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }
        public List<OrderBook> OrderBook(string pCoinFrom, string pCoinTo, out bool Sucess)
        {
            List<OrderBook> objRet = new List<OrderBook>();

            var HttpReturn = Get(false, "orderbook/" + pCoinFrom + "/" + pCoinTo).Result;
            if(HttpReturn.IsSuccessStatusCode)
            {
                objRet = JsonConvert.DeserializeObject<List<OrderBook>>(HttpReturn.Data);
            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }

        public Orders CreateOrderSell(string pCoinFrom, string pCointo, decimal pAmount, decimal pPrice, out bool Sucess)
        {
            OrdersParam pOrder = new OrdersParam()
            {
                Amount = pAmount,
                CoinFrom = pCoinFrom,
                CoinTo = pCointo,
                Price = pPrice,
            };
            var HttpReturn = Post(true, "orders/sell/", pOrder).Result;
            Orders objRet = new Orders();
            if (HttpReturn.IsSuccessStatusCode)
            {
                objRet = JsonConvert.DeserializeObject<Orders>(HttpReturn.Data);
            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }
        public List<Orders> GetOrderSell(long? pId, out bool Sucess)
        {
            List<Orders> objRet = new List<Orders>();

            var HttpReturn = Get(true, "orders/sell/"+ pId.ToString()).Result;

            if(HttpReturn.IsSuccessStatusCode)
            {
                objRet = JsonConvert.DeserializeObject<List<Orders>>(HttpReturn.Data);
            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }
        public List<Orders> DeleteOrderSell(long? pId, out bool Sucess)
        {
            List<Orders> objRet = new List<Orders>();

            var HttpReturn = Delete(true, "orders/sell/" + pId.ToString()).Result;

            if(HttpReturn.IsSuccessStatusCode)
            {
                objRet = JsonConvert.DeserializeObject<List<Orders>>(HttpReturn.Data);
            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }

        public Orders CreateOrderBuy(string pCoinFrom, string pCointo, decimal pAmount, decimal pPrice, out bool Sucess)
        {
            OrdersParam pOrder = new OrdersParam()
            {
                Amount = pAmount,
                CoinFrom = pCoinFrom,
                CoinTo = pCointo,
                Price = pPrice,
            };
            var HttpReturn = Post(true, "orders/buy/", pOrder).Result;
            Orders objRet = new Orders();
            if (HttpReturn.IsSuccessStatusCode)
            {
                objRet = JsonConvert.DeserializeObject<Orders>(HttpReturn.Data);
            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }
        public List<Orders> GetOrderBuy(long? pId, out bool Sucess)
        {
            List<Orders> objRet = new List<Orders>();

            var HttpReturn = Get(true, "orders/buy/" + pId.ToString()).Result;

            if (HttpReturn.IsSuccessStatusCode)
            {
                objRet = JsonConvert.DeserializeObject<List<Orders>>(HttpReturn.Data);
            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }
        public List<Orders> DeleteOrderBuy(long? pId, out bool Sucess)
        {
            List<Orders> objRet = new List<Orders>();

            var HttpReturn = Delete(true, "orders/buy/" + pId.ToString()).Result;

            if (HttpReturn.IsSuccessStatusCode)
            {
                objRet = JsonConvert.DeserializeObject<List<Orders>>(HttpReturn.Data);
            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }

        public List<Orders> GetOrderAll(long? pId, out bool Sucess)
        {
            List<Orders> objRet = new List<Orders>();

            var HttpReturn = Get(true, "orders/all/" + pId.ToString()).Result;

            if (HttpReturn.IsSuccessStatusCode)
            {
                objRet = JsonConvert.DeserializeObject<List<Orders>>(HttpReturn.Data);
            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }
        public List<Orders> DeleteOrderAll(long? pId, out bool Sucess)
        {
            List<Orders> objRet = new List<Orders>();

            var HttpReturn = Delete(true, "orders/all/" + pId.ToString()).Result;

            if (HttpReturn.IsSuccessStatusCode)
            {
                objRet = JsonConvert.DeserializeObject<List<Orders>>(HttpReturn.Data);
            }
            Sucess = HttpReturn.IsSuccessStatusCode;
            return objRet;
        }

        #region Pure_HTTP_Methods
        private async Task<HttpMethodResult> Get(bool pPrivate, string pEndPoint)
        {


            CustomDelegatingHandler customDelegatingHandler = new CustomDelegatingHandler(_APIKey,_APPId);
            HttpClient client;
            string apiAcessType = "";
            string responseString = "";

            if (pPrivate)
            {
                client = HttpClientFactory.Create(customDelegatingHandler);
                apiAcessType = "api/v1/private/";
            }
            else
            {
                client = HttpClientFactory.Create();
                apiAcessType = "api/v1/public/";
            }

            HttpResponseMessage response = await client.GetAsync(_apiBaseAddress + apiAcessType + pEndPoint);

            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(responseString);
                Console.WriteLine("HTTP Status: {0}, Reason {1}. Press ENTER to exit", response.StatusCode, response.ReasonPhrase);
                Console.ResetColor();
            }
            else
            {
                responseString = await response.Content.ReadAsStringAsync();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(responseString);
                Console.WriteLine("Failed to call the API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase);
                Console.ResetColor();
                Console.Beep(300, 1500);
            }
            HttpMethodResult ret = new HttpMethodResult()
            { 
                Data = responseString,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
            };

            return ret;
        }
        private async Task<HttpMethodResult> Post(bool pPrivate, string pEndPoint, object pBody)
        {

            CustomDelegatingHandler customDelegatingHandler = new CustomDelegatingHandler(_APIKey, _APPId);
            HttpClient client;
            string apiAcessType = "";
            string responseString = "";

            if (pPrivate)
            {
                client = HttpClientFactory.Create(customDelegatingHandler);
                apiAcessType = "api/v1/private/";
            }
            else
            {
               client = HttpClientFactory.Create();
                apiAcessType = "api/v1/public/";
            }
                        

            HttpResponseMessage response = await client.PostAsJsonAsync(_apiBaseAddress + apiAcessType + pEndPoint, pBody);

            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(responseString);
                Console.WriteLine("HTTP Status: {0}, Reason {1}. Press ENTER to exit", response.StatusCode, response.ReasonPhrase);
                Console.ResetColor();
            }
            else
            {
                responseString = await response.Content.ReadAsStringAsync();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(responseString);
                Console.WriteLine("Failed to call the API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase);
                Console.ResetColor();
                Console.Beep(300, 1500);
            }

            HttpMethodResult ret = new HttpMethodResult()
            {
                Data = responseString,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
            };
            return ret;

        }
        private async Task<HttpMethodResult> Delete(bool pPrivate, string pEndPoint)
        {
            CustomDelegatingHandler customDelegatingHandler = new CustomDelegatingHandler(_APIKey, _APPId);
            HttpClient client;
            string apiAcessType = "";
            string responseString;

            if (pPrivate)
            {
                client = HttpClientFactory.Create(customDelegatingHandler);
                apiAcessType = "api/v1/private/";
            }
            else
            {
                client = HttpClientFactory.Create();
                apiAcessType = "api/v1/public/";
            }

            HttpResponseMessage response = await client.DeleteAsync(_apiBaseAddress + apiAcessType + pEndPoint);

            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(responseString);
                Console.WriteLine("HTTP Status: {0}, Reason {1}. Press ENTER to exit", response.StatusCode, response.ReasonPhrase);
                Console.ResetColor();
            }
            else
            {
                responseString = await response.Content.ReadAsStringAsync();

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(responseString);
                Console.WriteLine("Failed to call the API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase);
                Console.ResetColor();
                Console.Beep(300, 1500);
            }

            HttpMethodResult ret = new HttpMethodResult()
            {
                Data = responseString,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
            };

            return ret;

        }
        private class CustomDelegatingHandler : DelegatingHandler
        {
            string APIKey;
            string APPId;
            public CustomDelegatingHandler(string pAPIKey, string pAPPId)
            {
                //Obtained from the server earlier
                APIKey = pAPIKey;
                APPId = pAPPId;
            }



            protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {

                HttpResponseMessage response = null;
                string requestContentBase64String = string.Empty;

                string requestUri = System.Web.HttpUtility.UrlEncode(request.RequestUri.AbsoluteUri.ToLower());

                string requestHttpMethod = request.Method.Method;
                request.Headers.Add("User-Agent", "Hall 9001/1.0");

                //Calculate UNIX time
                DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan timeSpan = DateTime.UtcNow - epochStart;
                string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

                //create random nonce for each request
                string nonce = Guid.NewGuid().ToString("N");

                //Checking if the request contains body, usually will be null wiht HTTP GET and DELETE
                if (request.Content != null)
                {
                    byte[] content = await request.Content.ReadAsByteArrayAsync();
                    MD5 md5 = MD5.Create();
                    //Hashing the request body, any change in request body will result in different hash, we'll incure message integrity
                    byte[] requestContentHash = md5.ComputeHash(content);
                    requestContentBase64String = Convert.ToBase64String(requestContentHash);
                }

                //Creating the raw signature string
                string signatureRawData = String.Format("{0}{1}{2}{3}{4}{5}", APPId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

                var secretKeyByteArray = Convert.FromBase64String(APIKey);

                byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

                using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
                {
                    byte[] signatureBytes = hmac.ComputeHash(signature);
                    string requestSignatureBase64String = Convert.ToBase64String(signatureBytes);
                    //Setting the values in the Authorization header using custom scheme (amx)
                    request.Headers.Authorization = new AuthenticationHeaderValue("amx", string.Format("{0}:{1}:{2}:{3}", APPId, requestSignatureBase64String, nonce, requestTimeStamp));
                }

                response = await base.SendAsync(request, cancellationToken);

                return response;
            }
        }
        private class HttpMethodResult
        {
            public string Data { get; set; }
            public bool IsSuccessStatusCode { get; set; }
        }
        #endregion
    }
}
