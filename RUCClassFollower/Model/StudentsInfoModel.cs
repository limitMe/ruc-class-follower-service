using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUCClassFollower.Model
{
    public class SingleStudentInfo
    {
        public string name { get; set; }
        public string code { get; set; }
        public string school { get; set; }
        public string ssid { get; set; }
    }

    public class StudentsInfoModel : RootResponseModel
    {
        public List<SingleStudentInfo> data;

        public StudentsInfoModel()
        {
            data = new List<SingleStudentInfo>();
        }
    }
}
