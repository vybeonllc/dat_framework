using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Xml;

namespace Dat.V1.Utils.Net
{
        [Serializable]
  public class WebRequest
  {

    CookieContainer _cookieContainer;

    public WebRequest()
    {
      _cookieContainer = new CookieContainer();
    }

    public String Post(String url, String referer, String query, String postData)
    {
      return Post(url, referer, query, postData, true);
    }

    public String XML(String url, String xml)  {

      byte[] RequestBytes = UTF8Encoding.UTF8.GetBytes(xml);

      Uri uri = new Uri(url);
      HttpWebRequest request = System.Net.WebRequest.Create(new Uri(url)) as HttpWebRequest;

      request.ContentLength = RequestBytes.Length;

      request.Method = "POST";

      request.ContentType = "application/xml;charset=utf-8";

      Stream RequestStream = request.GetRequestStream();
      RequestStream.Write(RequestBytes, 0, RequestBytes.Length);
      RequestStream.Close();

      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      StreamReader reader = new StreamReader(response.GetResponseStream());
      string ResponseMessage = reader.ReadToEnd();
      response.Close();

      return ResponseMessage;

    }

    public String Post(String url, String referer, String query, String postData, Boolean allowRedirects) {
      return Post(url, String.Empty, referer, query, postData, allowRedirects);
    }

    public String Post(String url, String header, String referer, String query, String postData, Boolean allowRedirects)
    {

      String responseText = String.Empty;
      Byte[] data = null;
      HttpWebRequest request = null;
      HttpWebResponse response = null;
      Stream requestStream = null;
      Stream responseStream = null;
      StreamReader streamReader = null;
      ASCIIEncoding encoding = null;

      try
      {

        encoding = new ASCIIEncoding();
        data = encoding.GetBytes(postData);
        request = (HttpWebRequest)System.Net.WebRequest.Create(url + query);
        request.CookieContainer = _cookieContainer;
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";

        if (!String.IsNullOrEmpty(referer)) request.Referer = referer;
        if (!String.IsNullOrWhiteSpace(header)) request.Headers.Add(header);

        request.MaximumAutomaticRedirections = 50;
        request.AllowAutoRedirect = true;
        request.KeepAlive = true;
        request.ContentLength = data.Length;
        //request.ContentType = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
        //request.UserAgent   = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";

        requestStream = request.GetRequestStream();
        requestStream.Write(data, 0, data.Length);
        requestStream.Close();

        response = (HttpWebResponse)request.GetResponse();
        responseStream = response.GetResponseStream();
        response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
        streamReader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8, true);
        responseText = streamReader.ReadToEnd();

        System.Threading.Thread.Sleep(750);

      }
      catch (Exception ex)
      {
        throw new Exception("Error posting data.", ex);
      }

      return responseText;

    }

    public String TextPost(String url, String query, String postData)
    {

        String responseText = String.Empty;
        Byte[] data = null;
        HttpWebRequest request = null;
        HttpWebResponse response = null;
        Stream requestStream = null;
        Stream responseStream = null;
        StreamReader streamReader = null;
        ASCIIEncoding encoding = null;

        try
        {

            encoding = new ASCIIEncoding();
            data = encoding.GetBytes(postData);
            request = (HttpWebRequest)System.Net.WebRequest.Create(url + query);
            request.CookieContainer = _cookieContainer;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            request.MaximumAutomaticRedirections = 50;
            request.AllowAutoRedirect = true;
            request.KeepAlive = true;
            request.ContentLength = data.Length;
            //request.ContentType = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
            //request.UserAgent   = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";

            requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            response = (HttpWebResponse)request.GetResponse();
            responseStream = response.GetResponseStream();
            response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
            streamReader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8, true);
            responseText = streamReader.ReadToEnd();

            System.Threading.Thread.Sleep(750);

        }
        catch (Exception ex)
        {
            throw new Exception("Error posting data.", ex);
        }

        return responseText;
    }

    public String Get(String url, String query)
    {
      return Get(url, query, String.Empty, true);
    }

    public String Get(String url, String query, String postData)
    {
      return Get(url, query, postData, true);
    }

    public String Get(String url, String query, String postData, Boolean allowRedirect) {
      return Get(url, String.Empty, query, postData, allowRedirect);
    }

    public String Get(String url, String header, String query, String postData, Boolean allowRedirect)
    {

      String responseText = String.Empty;
      Byte[] data = null;
      HttpWebRequest request = null;
      HttpWebResponse response = null;
      Stream requestStream = null;
      Stream responseStream = null;
      StreamReader streamReader = null;
      ASCIIEncoding encoding = null;

      try
      {

        encoding = new ASCIIEncoding();

        if (!String.IsNullOrEmpty(postData)) data = encoding.GetBytes(postData);

        request = (HttpWebRequest)System.Net.WebRequest.Create(url + query);
        request.CookieContainer = _cookieContainer;
        request.Method = "GET";
        request.MaximumAutomaticRedirections = 50;
        request.AllowAutoRedirect = allowRedirect;
        request.KeepAlive = true;
        request.ContentType = "application/x-www-form-urlencoded";
        if (!String.IsNullOrWhiteSpace(header)) request.Headers.Add(header);
        //request.ContentType = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
        //request.UserAgent   = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";

        if (!String.IsNullOrEmpty(postData))
        {
          request.ContentLength = data.Length;
          requestStream = request.GetRequestStream();
          requestStream.Write(data, 0, data.Length);
          requestStream.Close();
        }

        response = (HttpWebResponse)request.GetResponse();
        responseStream = response.GetResponseStream();
        response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
        streamReader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8, true);
        responseText = streamReader.ReadToEnd();

        System.Threading.Thread.Sleep(750);

      }
      catch (Exception ex)
      {
        throw new Exception("Error getting data.", ex);
      }

      return responseText;

    }



  }
}
