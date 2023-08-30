using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading.Tasks.Dataflow;

namespace DataAccess.Repositories
{
    public class PublisherRepository : BaseRepository, IPublisherRepository
    {
        public PublisherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string Add(PublisherModel publisher)
        {
            string? errorMessage = null; //return errorMessage if error raised null otherwise
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_AddPublisher";
                    command.Parameters.AddWithValue("@Name", publisher.Name);
                    command.Parameters.AddWithValue("@CompanyName", publisher.Company_Name);
                    command.Parameters.AddWithValue("@Address", publisher.Address);
                    command.Parameters.AddWithValue("@City", publisher.City);
                    command.Parameters.AddWithValue("@State", publisher.State);
                    command.Parameters.AddWithValue("@Zip", publisher.Zip);
                    command.Parameters.AddWithValue("@Telephone", publisher.Telephone);
                    command.Parameters.AddWithValue("@Fax", publisher.Fax);
                    command.Parameters.AddWithValue("@Comments", publisher.Comments);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["Message"] is not DBNull)
                            {
                                errorMessage = reader["Message"].ToString();
                            }
                        }
                    }
                }
            }
            return errorMessage;
        }

        public string Delete(int id)
        {
            string? errorMessage = null; //return errorMessage if raised, null otherwise
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {                
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_DeletePublisher";
                    command.Parameters.AddWithValue("@PubID", id);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["Message"] is not DBNull)
                            {
                                errorMessage = reader["Message"].ToString();
                            }
                        }
                    }
                }
            }
            return errorMessage;
        }

        public string Edit(PublisherModel publisher)
        {
            string? errorMessage = null; //return errorMessage if error raised null otherwise
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_UpdatePublisher";
                    command.Parameters.AddWithValue("@PubID", publisher.PubID);
                    command.Parameters.AddWithValue("@Name", publisher.Name);
                    command.Parameters.AddWithValue("@CompanyName", publisher.Company_Name);
                    command.Parameters.AddWithValue("@Address", publisher.Address);
                    command.Parameters.AddWithValue("@City", publisher.City);
                    command.Parameters.AddWithValue("@State", publisher.State);
                    command.Parameters.AddWithValue("@Zip", publisher.Zip);
                    command.Parameters.AddWithValue("@Telephone", publisher.Telephone);
                    command.Parameters.AddWithValue("@Fax", publisher.Fax);
                    command.Parameters.AddWithValue("@Comments", publisher.Comments);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["Message"] is not DBNull)
                            {
                                errorMessage = reader["Message"].ToString();
                            }
                        }
                    }
                }
            }
            return errorMessage;
        }

        public IEnumerable<PublisherModel> GetAll()
        {
            List<PublisherModel> publishers = new List<PublisherModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_GetAllPublishers";
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            PublisherModel publisher = new PublisherModel();
                            publisher.PubID = Convert.ToInt32(reader["PubID"]);
                            publisher.Name = reader["Name"].ToString();
                            publisher.Company_Name = reader["Company_Name"].ToString();
                            publisher.Address = reader["Address"].ToString();
                            publisher.City = reader["City"].ToString();
                            publisher.Zip = reader["Zip"].ToString();
                            publisher.State = reader["State"].ToString();
                            publisher.Telephone = reader["Telephone"].ToString();
                            publisher.Fax = reader["Fax"].ToString();
                            publisher.Comments = reader["Comments"].ToString();
                            publishers.Add(publisher);
                        }
                    }
                }
            }

                return publishers;
        }

        public PublisherModel GetByID(int id)
        {
            PublisherModel publisher = new PublisherModel();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_GetPublisher";
                    command.Parameters.Add("@PubID", SqlDbType.Int).Value = id;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            publisher.PubID = Convert.ToInt32(reader["PubID"]);
                            publisher.Name = reader["Name"].ToString();
                            publisher.Company_Name = reader["Company_Name"].ToString();
                            publisher.Address = reader["Address"].ToString();
                            publisher.City = reader["City"].ToString();
                            publisher.Zip = reader["Zip"].ToString();
                            publisher.State = reader["State"].ToString();
                            publisher.Telephone = reader["Telephone"].ToString();
                            publisher.Fax = reader["Fax"].ToString();
                            publisher.Comments = reader["Comments"].ToString();
                        }
                    }
                }
            }
            return publisher;
        }

        public IEnumerable<PublisherModel> GetByValue(string searchValue)
        {
            List<PublisherModel> publishers = new List<PublisherModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_GetPublisherByName";
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = searchValue;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PublisherModel publisher = new PublisherModel();
                            publisher.PubID = Convert.ToInt32(reader["PubID"]);
                            publisher.Name = reader["Name"].ToString();
                            publisher.Company_Name = reader["Company_Name"].ToString();
                            publisher.Address = reader["Address"].ToString();
                            publisher.City = reader["City"].ToString();
                            publisher.Zip = reader["Zip"].ToString();
                            publisher.State = reader["State"].ToString();
                            publisher.Telephone = reader["Telephone"].ToString();
                            publisher.Fax = reader["Fax"].ToString();
                            publisher.Comments = reader["Comments"].ToString();
                            publishers.Add(publisher);
                        }
                    }
                }
            }
            return publishers;
        }
    }
}
