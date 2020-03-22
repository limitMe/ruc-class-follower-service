using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using RUCClassFollower.Model;

namespace RUCClassFollower.ClassCatcher
{
    public partial class loadStudentComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request.Params["Key"];
            string posSsid = Request.Params["posSSID"];
            string negSsid = Request.Params["negSSID"];
            string comment = Request.Params["comment"];
            string time = DateTime.Now.ToString("yyMMddHHmm");

            var result = new RootResponseModel();

            if(key == "commentKey")
            {
                string insertSql = "insert into `comment_table` (`posSSID`,`negSSID`,`time`,`content`)"
                    + "values("+posSsid+","+negSsid+","+time+",'"+comment+"');";
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
                result.errorMsg = "身份校验失败！";
            }
            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}