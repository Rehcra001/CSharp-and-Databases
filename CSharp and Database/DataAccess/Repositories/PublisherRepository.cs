using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccess.Repositories
{
    public class PublisherRepository : BaseRepository, IPublisherRepository
    {
        public PublisherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(PublisherModel publisher)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(PublisherModel publisher)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IEnumerable<PublisherModel> GetByValue(string searchValue)
        {
            throw new NotImplementedException();
        }
    }
}
