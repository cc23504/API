using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinFormGetAPI
{
    //enumerado conjunto de coisas que voce pode enumerar, no caso os verbos get=0, post=1,...
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    class RestClient
    {

        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }

        public RestClient()
        {
            this.endPoint = string.Empty;
            this.httpMethod = httpVerb.GET;

        }
        //faz chamada na classe formulario
        //no fim da reposta no response value
        public string makeRequest()
        {
            //empty é um string vazia
            string strResponseValue = string.Empty;

            //httpwebrequest objeto do .NET, faz papel de uma requisicao, request, cria request atrazes do Webrequest pelo create
            //passando o end que criamos no restClient
            //webRequest 
            HttpWebRequest request = WebRequest.Create(endPoint) as HttpWebRequest;
            //httpwebrequest tem um methodo chamado method que estamos declarando
            request.Method = httpMethod.ToString();

            //GetResponse seria o Enter
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                //status code sao numero 200, 300.. se for diferente de OK, lançca a excesao
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("error code: " + response.StatusCode);
                }
                //se der certo vai passar um dado de uma ponta pra outra
                //pra poder pegar o jSOn
                using (Stream responseStream = response.GetResponseStream())
                {
                    //se nao der nulo é pq veio algo escrito
                    if (responseStream != null)
                    {
                        //vai ler o que veio escrito
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            //ler do começo ate o fim
                            strResponseValue = reader.ReadToEnd();
                        }

                    }

                }
            }
            //agora a string que estava vazia vai ser devolvida
            return strResponseValue;
        }
    }
}
