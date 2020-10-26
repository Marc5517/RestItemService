using ModelLib.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestItemService.DBUtil
{
    public class ManageItems
    {
        private const String connectionString = "";

        private const String GET_ALL = "Select * from Item"; 

        public IEnumerable<Item> Get()
        {
            List<Item> liste = new List<Item>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Item item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }
            return liste;
        }

        public Item Get(int id)
        {

        }

        public void Post(Item value)
        {

        }

        public void Put(int id, Item value)
        {

        }

        public void Delete(int id)
        {

        }

        protected Item ReadNextElement(SqlDataReader reader)
        {
            Item item = new Item();
            item.Id = reader.GetInt32(0);
            item.Name = reader.GetString(1);
            item.Quality = reader.GetString(2);
            item.Quantity = reader.GetDouble(3);
            return item;
        }
    }
}
