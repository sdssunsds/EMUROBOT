using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace EMU.Util
{
    public class HttpAgreementManager
    {
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url">请求地址（含参数）</param>
        /// <param name="code">编码</param>
        /// <returns>返回请求结果</returns>
        public static string Get(string url, Encoding code = null)
        {
            if (code == null)
            {
                code = Encoding.UTF8;
            }
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), code);
                string returnXml = reader.ReadToEnd();
                reader.Close();
                myResponse.Close();
                return returnXml;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="arg">请求参数</param>
        /// <returns>返回请求结果</returns>
        public static string Get(string url, string arg)
        {
            try
            {
                url = url + "?" + arg;
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "无结果";
        }
        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url">请求地址（含参数）</param>
        /// <returns>返回请求结果</returns>
        public static string Post(string url)
        {
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webrequest.Method = "post";
                webrequest.ContentType = "application/x-www-form-urlencoded;charset=gb2312";
                byte[] postdatabyte = Encoding.UTF8.GetBytes("");
                webrequest.ContentLength = postdatabyte.Length;
                Stream stream;
                stream = webrequest.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                stream.Close();
                using (var httpWebResponse = webrequest.GetResponse())
                using (StreamReader responseStream = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    return responseStream.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="arg">请求参数</param>
        /// <returns>返回请求结果</returns>
        public static string Post(string url, string arg)
        {
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webrequest.Method = "post";
                webrequest.ContentType = "application/json";
                byte[] postdatabyte = Encoding.UTF8.GetBytes(arg);
                webrequest.ContentLength = postdatabyte.Length;
                Stream stream;
                stream = webrequest.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                stream.Close();
                using (var httpWebResponse = webrequest.GetResponse())
                using (StreamReader responseStream = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    return responseStream.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="arg">请求参数</param>
        /// <param name="code">编码</param>
        /// <returns>返回请求结果</returns>
        public static string Post(string url, string arg, Encoding code = null)
        {
            if (code == null)
            {
                code = Encoding.UTF8;
            }
            try
            {
                var str = JsonManager.ObjectToJson(arg);
                var httpContent = new StringContent(str, Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "无结果";
        }
    }
}
