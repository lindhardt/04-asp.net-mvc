using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace OperasWebSite.WebServices
{
    /// <summary>
    /// Summary description for QuotesService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class QuotesService : System.Web.Services.WebService
    {

        [WebMethod]
        public string LatestQuote()
        {
            string[] quotes = new string[3];
            quotes[0] = "The new La Scala production of Cosi Fan Tutte is truely superb!";
            quotes[1] = "The Opera Company of Philadelphia has renewed Rigoletto with this excellent new production.";
            quotes[2] = "This production of Wozzeck by the Metropolitan Opera is a tour-de-force.";

            Random random = new Random();
            return quotes[random.Next(3)];
        }
    }
}
