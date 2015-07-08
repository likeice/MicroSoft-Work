using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;

namespace ListPageAnalysis
{
    public class HTMLParse
    {
        
        static string[] exPattern = { ".css", ".jpg", ".xml", ".ico", ".png", "download", ".apk", "aboutus", "help" };
        public static bool filter(string url)
        {
            url = url.Trim();
            string urlPattern1 = @"^(http)://(www\.)(letv\.com)()/(ptv/\w+/\d+[\d/]*\.html)$";
            string urlPattern2 = @"^(http)://(www\.)(letv\.com)()/(ptv/vplay/\d+)$";
            int flag = 0;
            for (int i = 0; i < exPattern.Length; i++)
            {
                if (url.IndexOf(exPattern[i]) != -1)
                    flag = 1;
            }
            if (url.Length <= 8 || url.IndexOf("letv") == -1 || flag == 1/* || srcUrl.Equals(url)*/ || Regex.IsMatch(url, urlPattern1) || Regex.IsMatch(url, urlPattern2))
            {
                return true;
            
            }
            return false;
        }
        public static string parser(string htmlResponse)
        {
            HashSet<string> visited = new HashSet<string>();
            string result = "";
            

            string tag = "href=\"";
            int pos = htmlResponse.IndexOf(tag);
            
            while (pos != -1)
            {
                pos += tag.Length;
                int nextp = htmlResponse.IndexOf("\"", pos);
                if (nextp != -1)
                {
                    string url = htmlResponse.Substring(pos, nextp - pos);
                    url = url.Trim();  //次句未测试
                    if (!visited.Contains(url))
                    {
                        
                        if (!filter(url))
                        {
                            visited.Add(url);
                            if (result.Length == 0)
                                result = url;
                            else
                                result = result + ";" + url;
                        }
                    }
                    //pos = htmlResponse.IndexOf(tag, pos);
                }
                pos = htmlResponse.IndexOf(tag, pos);
            }

           
            return result;
        }

        public static string parserList(string htmlResponse)  //Regex.IsMatch(url, urlPattern2)
        {
            HashSet<string> visited = new HashSet<string>();
            string urlPattern1 = @"^(http)://(www\.)(letv\.com)()/(ptv/\w+/\d+[\d/]*\.html)$";
            string urlPattern2 = @"^(http)://(www\.)(letv\.com)()/(ptv/vplay/\d+)$";
            string result = "";

            string tag = "href=\"";
            int pos = htmlResponse.IndexOf(tag);
           

            while (pos != -1)
            {
                pos += tag.Length;
                int nextp = htmlResponse.IndexOf("\"", pos);
                if (nextp != -1)
                {
                    string url = htmlResponse.Substring(pos, nextp - pos);
                    url = url.Trim();  //次句未测试
                    if (!visited.Contains(url))
                    {
                        visited.Add(url);
                        //string output = srcUrl + "    " + url;
                        if (Regex.IsMatch(url, urlPattern1) || Regex.IsMatch(url, urlPattern2))
                        {
                            //visited.Add(url);
                            if (result.Length == 0)
                                result = url;
                            else
                                result = result + ";" + url;


                        }
                        
                    }
                    //pos = htmlResponse.IndexOf(tag, pos);
                }
                pos = htmlResponse.IndexOf(tag, pos);
            }

           
            return result;
        }

        
    }
}
