using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DataAccessTier;

namespace Lottotry.Members
{
    /// <summary>
    /// Summary description for WSGetDrawNumbers
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WSGetDrawNumbers : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] GetDrawNumbers(string prefixText, Database contextKey)
        {
            //Database db = DataAccessLayer._db;          
            DataAccessLayer dataAccess = new DataAccessLayer();
            return dataAccess.SpAllDrawNumbers(contextKey, prefixText);
        }
    }
}
