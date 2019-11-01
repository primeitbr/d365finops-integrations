﻿using AuthenticationUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace OAuthXppConsoleApplication
{
    class Program
    {
        // In the AOT you will find UserSessionService in Service Groups and AifUserSessionService under Services.
        public static string sessionUrl = "/api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo";
        
        static void Main(string[] args)
        {
            /* When making service requets to Sandbox or Prod AX environemnts it must be ensured that TLS version is 1.2
             * .NET 4.5 supports TLS 1.2 but it is not the default protocol. The below statement can set TLS version explicity.
             * Note that this statement may not work in .NET 4.0, 3.0 or below.
             * Also note that in .NET 4.6 and above TLS 1.2 is the default protocol.
             */

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var request = HttpWebRequest.Create(GetUserSessionOperationPath);
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            request.Method = "POST";
            request.ContentLength = 0;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string responseString = streamReader.ReadToEnd();

                        Console.WriteLine(responseString);
                    }
                }
            }

            Console.ReadLine();
        }

    }
}
