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

        void PostTagDetails(List<Tag> tags);

        void PostCommentDetails(List<Comment> comments);

        void PostCommentDetailsFromCSV();

        List<LabelCount> GetLabelCount();
    }
}
