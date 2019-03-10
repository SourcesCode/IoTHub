using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Communication
{
    public class Helper
    {
        public static string GetLocalIpV4Helper()
        {
            //TODO:test
            //string ip = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            string ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(t => t.AddressFamily == AddressFamily.InterNetwork).ToString();
            return ip;
        }
    }
}
