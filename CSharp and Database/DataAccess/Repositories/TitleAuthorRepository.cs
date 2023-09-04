using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess.Repositories
{
    public class TitleAuthorRepository : BaseRepository, ITitleAuthorRepository
    {
        public TitleAuthorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(List<TitleAuthorModel> authors)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_AddTitleAuthor";
                    SqlParameter isbn = new SqlParameter("@ISBN", SqlDbType.NVarChar);
                    SqlParameter au_id = new SqlParameter("@Au_ID", SqlDbType.Int);
                    command.Parameters.Add(isbn);
                    command.Parameters.Add(au_id);
                    foreach (TitleAuthorModel item in authors)
                    {
                        isbn.Value = item.ISBN;
                        au_id.Value = item.Au_ID;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Delete(List<TitleAuthorModel> authors)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_DeleteTitleAuthor";
                    SqlParameter isbn = new SqlParameter("@ISBN", SqlDbType.NVarChar);
                    SqlParameter au_id = new SqlParameter("@Au_ID", SqlDbType.Int);
                    command.Parameters.Add(isbn);
                    command.Parameters.Add(au_id);
                    foreach (TitleAuthorModel item in authors)
                    {
                        isbn.Value = item.ISBN;
                        au_id.Value = item.Au_ID;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void DeleteByISBN(TitleModel title)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_DeleteTitleAuthorByISBN";
                    command.Parameters.AddWithValue("@ISBN", title.ISBN);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
