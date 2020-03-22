using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using RUCClassFollower.Model;

namespace RUCClassFollower.ClassCatcher
{
    public partial class getCommentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Ssid = Request.Params["SSID"];
            string comment = Request.Params["comment"];
            string time = DateTime.Now.ToString("yyMMddHHmm");

            var result = new CommentListModel();

            int s;
            if(int.TryParse(Ssid, out s))
            {
                try {
                    string querySql = "select `posSSID`,`negSSID`,`content`,`time` from"
                        + "`comment_table` where `negSSID` = " + Ssid + ";";
                    using (MySqlConnection conn = new MySqlConnection(DatabaseManager.connStr))
                    {
                        lock (conn)
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand(querySql, conn);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                var commentObj = new SingleCommentObj();
                                commentObj.posSSID = reader[0].ToString();
                                commentObj.negSSID = reader[1].ToString();
                                commentObj.comment = reader[2].ToString();
                                commentObj.time = reader[3].ToString();
                                result.comments.Add(commentObj);
                            }
                            reader.Close();
                        }
                    }
                    result.status = true;
                }
                catch(Exception ex)
                {
                    result.errorMsg = ex.Message;
                }
            }
            else
            {
                result.errorMsg = "参数错误。已记录您的这次操作";
            }
            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}