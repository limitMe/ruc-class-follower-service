using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RUCClassFollower.Model;

namespace RUCClassFollower.ClassCatcher
{
    public partial class getStudentCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string queryS = Request.Params["queryS"];
            int queryId;
            string querySql;
            var studentsResult = new StudentsInfoModel();

            //成功则执行学号识别
            if(int.TryParse(queryS, out queryId))
            {
                querySql = "SELECT `ssid`,`studentid`,`studentname`,`school` from `student_table`"
                    + "where `ssid` = " + queryId + ";";
            }
            else
            {
                querySql = "SELECT `ssid`,`studentid`,`studentname`,`school` from `student_table`"
                    + "where `studentname` = '" + queryS + "';";
            }

            using (MySqlConnection conn = new MySqlConnection(DatabaseManager.connStr))
            {
                lock (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(querySql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var student = new SingleStudentInfo();
                        student.ssid = reader[0].ToString();
                        student.code = reader[1].ToString();
                        student.name = reader[2].ToString();
                        student.school = reader[3].ToString();
                        studentsResult.data.Add(student);
                    }
                    reader.Close();
                }
            }

            if(studentsResult.data.Count == 0)
            {
                studentsResult.errorMsg = "No result found!";
            }
            else
            {
                studentsResult.status = true;
            }

            Response.Write(JsonConvert.SerializeObject(studentsResult));
        }
    }
}