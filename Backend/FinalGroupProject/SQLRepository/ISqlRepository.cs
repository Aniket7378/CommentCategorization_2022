using GenericLibrary.Database;
using GenericLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalGroupProject.SQLRepository
{
    public interface ISqlRepository
    {
        ISqlDbConnection DatabaseConnection { get; set; }

        List<Tag> GetTagDetails();
        List<LabelCount> GetLabelCount();

        List<CommentTag> GetComments(string orderBy,string checkBox,string comment,string name,string city,string label,int count);

        void PostTagDetail(Tag tag);
        void PostCommentDetailsFromCSV();
        void PostCommentDetails(List<Comment> comments);
        void PostCommentTagMapping(CommentTagMapping commentTag);

        //void Filter();

    }
}
