using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using profitify.Util;


namespace profitify
{
    [Route("api/doacao")]
    [ApiController]
    public class DoacaoController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string APPID = "8415c4fb-f7cd-471d-ac73-5ffbb586db7f";
            string APIKey = "tSQLNweF9i8UDEg/+/J8IvI+YpM8bPirEC0AhtAqzbE=";

            bool Sucess;    

             Hall9001 objHall9001 = new Hall9001("https://profitfy.trade/",APPID,APIKey);
                       //Retorna os pares negociados na Profitfy
            List<Markets> Markets = objHall9001.Markets(out Sucess);

            return new string[] { "response", Markets[0].coinCodeFrom };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public  ActionResult<String>  Post([FromBody] DoacaoModel value)
        {
            
            string APPID = "8415c4fb-f7cd-471d-ac73-5ffbb586db7f";
            string APIKey = "tSQLNweF9i8UDEg/+/J8IvI+YpM8bPirEC0AhtAqzbE=";

            bool Sucess;    

             Hall9001 objHall9001 = new Hall9001("https://profitfy.trade/",APPID,APIKey);
                       //Retorna os pares negociados na Profitfy
            List<Markets> Markets = objHall9001.Markets(out Sucess);

            return Markets[0].coinCodeFrom;
            //return "1";


        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



        private void old( ValuesModel value){


            string getUrl = "https://bootcamp.profitfy.trade/api/v1/private/userinfo/";


            pfRequestModel jsonInfo = new pfRequestModel();

			jsonInfo.coinFrom="BRL";
			jsonInfo.coinTo= "BTC";
			//jsonInfo.amount= value.String();
			jsonInfo.reference= "0";
			jsonInfo.create = true;



                String APPId = "8415c4fb-f7cd-471d-ac73-5ffbb586db7f";
                String APIKey = "tSQLNweF9i8UDEg/+/J8IvI+YpM8bPirEC0AhtAqzbE=";
                String Authorization ="";

			    String json = JsonConvert.SerializeObject(jsonInfo);

              //  var body = new FormUrlEncodedContent(json); 
                  
                Console.WriteLine(json);

                HttpResponseMessage response = null;
                string requestContentBase64String = string.Empty;

                string requestUri = System.Web.HttpUtility.UrlEncode("https://bootcamp.profitfy.trade/api/v1/private/userinfo/");

                string requestHttpMethod = "GET";

                //Calculate UNIX time
                DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan timeSpan = DateTime.UtcNow - epochStart;
                string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

                //create random nonce for each request
                string nonce = Guid.NewGuid().ToString("N");

                //Checking if the request contains body, usually will be null wiht HTTP GET and DELETE
                if (json != null)
                {
                    byte[] content = Encoding.ASCII.GetBytes(json);
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
                    Authorization = "amx" + string.Format("{0}:{1}:{2}:{3}", APPId, requestSignatureBase64String, nonce, requestTimeStamp);
                }
        }



    }
}


