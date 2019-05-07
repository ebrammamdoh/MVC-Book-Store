using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Context
{
    public class EmailSetting
    {
        public string mailToAddress = "orders@xbookstore.com";
        public string mailFromAddress = "mymail@gmail.com";
        public bool useSSL = true;
        public string userName = "info";
        public string password = "password";
        public string serverName = "smtp.gmail.com";
        public int serverPort = 587;
        public bool writeAsFile = false;
        public string fileLocation = @"C:\orders";

    }
}
