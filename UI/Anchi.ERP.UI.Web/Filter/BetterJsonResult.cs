﻿using Anchi.ERP.Common;
using System.Web.Mvc;

namespace Anchi.ERP.UI.Web.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class BetterJsonResult : ActionResult
    {
        public BetterJsonResult(object data, bool success = false)
        {
            this.Data = data;
            this.Success = success;
        }

        object Data
        {
            get;
        }

        bool Success
        {
            get;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";

            var response = JsonUtils.SerializeObject(new
            {
                Success = this.Success,
                Message = this.Data,
            });
            context.HttpContext.Response.Output.Write(response);
        }
    }
}