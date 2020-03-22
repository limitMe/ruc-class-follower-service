using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUCClassFollower.Model
{
    public class SingleCommentObj
    {
        public string posSSID { get; set; }
        public string negSSID { get; set; }
        public string comment { get; set; }
        public string time { get; set; }
    }

    public class CommentListModel:RootResponseModel
    {
        public List<SingleCommentObj> comments { get; set; }

        public CommentListModel()
        {
            comments = new List<SingleCommentObj>();
        }
    }
}
