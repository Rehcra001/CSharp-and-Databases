using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TitleRepository : BaseRepository, ITitleRepository
    {
        public TitleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(TitleModel title)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_AddTitle";
                    command.Parameters.AddWithValue("@Title", title.Title);
                    command.Parameters.AddWithValue("@Year_Published", Convert.ToInt32(title.Year_Published));
                    command.Parameters.AddWithValue("@ISBN", title.ISBN);
                    command.Parameters.AddWithValue("@PubID", Convert.ToInt32(title.PubID));
                    command.Parameters.AddWithValue("@Description", title.Description);
                    command.Parameters.AddWithValue("@Notes", title.Notes);
                    command.Parameters.AddWithValue("@Subject", title.Subject);
                    command.Parameters.AddWithValue("@Comments", title.Comments);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string isbn)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_DeleteTitle";
                    command.Parameters.AddWithValue("@ISBN", isbn);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Edit(TitleModel title)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_AddTitle";
                    command.Parameters.AddWithValue("@Title", title.Title);
                    command.Parameters.AddWithValue("@Year_Published", Convert.ToInt32(title.Year_Published));
                    command.Parameters.AddWithValue("@ISBN", title.ISBN);
                    command.Parameters.AddWithValue("@PubID", Convert.ToInt32(title.PubID));
                    command.Parameters.AddWithValue("@Description", title.Description);
                    command.Parameters.AddWithValue("@Notes", title.Notes);
                    command.Parameters.AddWithValue("@Subject", title.Subject);
                    command.Parameters.AddWithValue("@Comments", title.Comments);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<TitleModel> GetAll()
        {
            List<TitleModel> titles = new List<TitleModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_GetAllTitles";
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {                        
                        while (reader.Read())
                        {
                            TitleModel title = new TitleModel();
                            title.Title = reader["Title"].ToString();
                            title.Year_Published = Convert.ToInt32(reader["Year_Published"]);
                            title.ISBN = reader["ISBN"].ToString();
                            title.PubID = Convert.ToInt32(reader["PubID"]);
                            title.Description = reader["Description"].ToString();
                            title.Notes = reader["Notes"].ToString();
                            title.Subject = reader["Subject"].ToString();
                            title.Comments = reader["Comments"].ToString();
                            titles.Add(title);
                        }
                    }
                }
            }

                return titles;
        }

        public IEnumerable<TitleModel> GetByValue(string searchValue)
        {
            List<TitleModel> titles = new List<TitleModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_GetTitlesByValue";
                    command.Parameters.AddWithValue("@Title", searchValue);
                    command.Parameters.AddWithValue("ISBN", searchValue);
                    command.Parameters.AddWithValue("@YearPublished", int.TryParse(searchValue, out _) ? Convert.ToInt32(searchValue) : 0);
                    command.Parameters.AddWithValue("@PubID", int.TryParse(searchValue, out _) ? Convert.ToInt32(searchValue) : 0);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TitleModel title = new TitleModel();
                            title.Title = reader["Title"].ToString();
                            title.Year_Published = Convert.ToInt32(reader["Year_Published"]);
                            title.ISBN = reader["ISBN"].ToString();
                            title.PubID = Convert.ToInt32(reader["PubID"]);
                            title.Description = reader["Description"].ToString();
                            title.Notes = reader["Notes"].ToString();
                            title.Subject = reader["Subject"].ToString();
                            title.Comments = reader["Comments"].ToString();
                            titles.Add(title);
                        }
                    }
                }
            }
            return titles;
        }
    }
}
