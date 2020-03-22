using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RUCClassFollower.Model;
using Newtonsoft.Json;

namespace RUCClassFollower.ClassCatcher
{
    public partial class loadRelationRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request.Params["Key"];
            string posSsid = Request.Params["posSSID"];
            string negSsid = Request.Params["negSSID"];
            string time = DateTime.Now.ToString("yyMMddHHmm");

            RootResponseModel result = new RootResponseModel();

            if(key == "simpleKey")
            {
                string insertSql = "insert into `log_table` (`posSSID`,`negSSID`,`time`)"
                    + " values(" +posSsid + "," + negSsid + "," + time + ");";
                try
                {
                    DatabaseManager.getSinglton().excuteNonQuery(insertSql);
                }
                catch(Exception ex)
                {
                    result.errorMsg = ex.Message;
                }
                result.status = true;
            }
            else
            {
                result.errorMsg = "认证失败";
            }
            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}