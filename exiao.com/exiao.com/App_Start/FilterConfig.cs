﻿using System.Web;
using System.Web.Mvc;

namespace exiao.com
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilterAttribute());
        }
    }
}
