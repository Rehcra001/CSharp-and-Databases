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
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string Add(AuthorModel author)
        {
            throw new NotImplementedException();
        }

        public string Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string Edit(AuthorModel author)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorModel> GetAll()
        {
            List<AuthorModel> authors = new List<AuthorModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_GetAuthors";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {                        
                        while (reader.Read())
                        {
                            AuthorModel author = new AuthorModel();
                            author.Au_ID = Convert.ToInt32(reader["Au_ID"]);
                            author.Author = reader["Author"].ToString();
                            if (reader["Year_Born"] is not DBNull)
                            {
                                author.Year_Born = Convert.ToInt32(reader["Year_Born"]);
                            }
                            authors.Add(author);
                        }
                    }
                }
            }

                return authors;
        }

        public AuthorModel GetAuthorByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorModel> GetAuthorsByISBN(string ISBN)
        {
            List<AuthorModel> authors = new List<AuthorModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_GetAuthorsPerTitle";
                    command.Parameters.AddWithValue("@ISBN", ISBN);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AuthorModel author = new AuthorModel();
                            author.Au_ID = Convert.ToInt32(reader["Au_ID"]);
                            author.Author = reader["Author"].ToString();
                            if (reader["Year_Born"] is not DBNull)
                            {
                                author.Year_Born = Convert.ToInt32(reader["Year_Born"]);
                            }
                            authors.Add(author);
                        }
                    }
                }
            }
            return authors;
        }

        public IEnumerable<AuthorModel> GetByValue(string searchValue)
        {
            throw new NotImplementedException();
        }
    }
}
