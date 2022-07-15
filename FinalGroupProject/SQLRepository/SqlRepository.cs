using GenericLibrary.Database;
using GenericLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using CsvHelper;
using System.Globalization;
using Newtonsoft.Json;

namespace FinalGroupProject.SQLRepository
{
    public class SqlRepository : ISqlRepository
    {
        public ISqlDbConnection DatabaseConnection { get; set; }

        public List<Tag> GetTagDetails()
        {
            List<Tag> tags = new List<Tag>();
            try
            {
                string query = @"select * from tag";

                DatabaseConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query))
                {
                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                    SqlDataReader readAllInfo = sqlCommand.ExecuteReader();

                    while (readAllInfo.Read())
                    {
                        Tag tag = new Tag();

                        tag.Id = (int)readAllInfo["id"];
                        tag.Label = (string)readAllInfo["label"];
                        tag.Color = (string)readAllInfo["color"];

                        tags.Add(tag);
                    }
                    readAllInfo.Close();
                }
                DatabaseConnection.Close();
                return tags;
            }
            catch (Exception ex)
            {
                DatabaseConnection.Close();
                throw ex;
            }
        }

        public void PostTagDetails(List<Tag> tags)
        {
            DatabaseConnection.Open();
            SqlTransaction sqlTransaction = DatabaseConnection.SqlConnectionToDb.BeginTransaction();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Transaction = sqlTransaction;

                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                    sqlCommand.CommandText = string.Empty;
                    sqlCommand.CommandText = "insert into tag (label,color) values(@Label,@Color)";

                    sqlCommand.Parameters.Add(new SqlParameter("@Label", SqlDbType.NVarChar));
                    sqlCommand.Parameters.Add(new SqlParameter("@Color", SqlDbType.NVarChar));

                    foreach (Tag tag in tags)
                    {
                        sqlCommand.Parameters["@Label"].Value = tag.Label;
                        sqlCommand.Parameters["@Color"].Value = tag.Color;

                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlTransaction.Commit();
                }
                DatabaseConnection.Close();
            }

            catch (Exception ex)
            {
                DatabaseConnection.Close();
                sqlTransaction.Rollback();
                throw ex;
            }
        }

        public void PostCommentDetails(List<Comment> comments)
        {
            DatabaseConnection.Open();
            SqlTransaction sqlTransaction = DatabaseConnection.SqlConnectionToDb.BeginTransaction();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Transaction = sqlTransaction;

                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                    sqlCommand.CommandText = string.Empty;
                    sqlCommand.CommandText = "insert into comment (name,comment_date,city,user_comment) values(@Name,@CommentDate,@City,@UserComment)";

                    sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                    sqlCommand.Parameters.Add(new SqlParameter("@CommentDate", SqlDbType.Date));
                    sqlCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar));
                    sqlCommand.Parameters.Add(new SqlParameter("@UserComment", SqlDbType.NVarChar));

                    foreach (Comment comment in comments)
                    {
                        sqlCommand.Parameters["@Name"].Value = comment.Name;
                        sqlCommand.Parameters["@CommentDate"].Value = comment.Date;
                        sqlCommand.Parameters["@City"].Value = comment.City;
                        sqlCommand.Parameters["@UserComment"].Value = comment.UserComment;

                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlTransaction.Commit();
                }
                DatabaseConnection.Close();
            }

            catch (Exception ex)
            {
                DatabaseConnection.Close();
                sqlTransaction.Rollback();
                throw ex;
            }
        }
        
        public void PostCommentDetailsFromCSV()
        {
            string json;
            using (StreamReader reader = new StreamReader(@"C:\DummyDir\FinalGroupProjectData.csv"))
            {
                using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    using (CsvDataReader csvDataReader = new CsvDataReader(csv))
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("Name", typeof(string));
                        dataTable.Columns.Add("Date", typeof(DateTime));
                        dataTable.Columns.Add("City", typeof(string));
                        dataTable.Columns.Add("UserComment", typeof(string));

                        dataTable.Load(csvDataReader);
                        json = JsonConvert.SerializeObject(dataTable);
                    }
                }
            }
            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(json);

            PostCommentDetails(comments);
        }

        public List<LabelCount> GetLabelCount()
        {
            List<LabelCount> labelCounts = new List<LabelCount>();
            try
            {
                string query = @"select tag.label,COUNT(*)as labelCount from commentTag_mapping inner join tag on tag.id = commentTag_mapping.tag_id group by tag.label";

                DatabaseConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query))
                {
                    sqlCommand.Connection = DatabaseConnection.SqlConnectionToDb;

                    SqlDataReader readAllInfo = sqlCommand.ExecuteReader();

                    while (readAllInfo.Read())
                    {
                        LabelCount labelCount = new LabelCount();

                        //labelCount.Id = (int)readAllInfo["id"];
                        labelCount.Label = (string)readAllInfo["label"];
                        labelCount.Count = (int)readAllInfo["labelCount"];

                        labelCounts.Add(labelCount);
                    }
                    readAllInfo.Close();
                }
                DatabaseConnection.Close();
                return labelCounts;
            }
            catch (Exception ex)
            {
                DatabaseConnection.Close();
                throw ex;
            }
        }

    }
}
