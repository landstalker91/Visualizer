using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace Visualizer.Models
{
    public class Node
    {
        public Node()
        {}
        public Node(int id, OdbcConnection DbConnection)
        {
            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();

            DbCommand.CommandText = "SELECT P.lPortfolioItemId, P.sTGLongName, C.Name FROM amPortfolio P INNER JOIN amCaterory C ON (P.ligSubCategoryId = C.lCateroryId) WHERE lPortfolioItemId = " + id.ToString();
            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    Console.WriteLine("{0}\t{1}", DbReader.GetInt32(0), DbReader.GetString(1));
                    Id = DbReader.GetInt32(0);
                    Name = DbReader.GetString(1);
                    Category = DbReader.GetString(2);
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            DbReader.Close();
            DbConnection.Close();
        }
 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
