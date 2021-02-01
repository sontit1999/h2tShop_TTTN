using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h2tshop.Models
{
    public class UtilsDatabase
    {
       private static DBContextDataContext db = null;
       public static DBContextDataContext getDaTaBase()
        {
            if(db == null)
            {
                db = new DBContextDataContext();
            }
            return db;
        }
    }
}