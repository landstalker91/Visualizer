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

            DbCommand.CommandText = 
                "SELECT" +
                "  P." + Settings.NODE_PK +
                ", P.sTGLongName" +
                ", C.Name" +
                "  FROM " + Settings.NODE_TABLE_NAME +
                "  P INNER JOIN amCaterory C ON (P.ligSubCategoryId = C.lCateroryId) " +
                "  WHERE P." + Settings.NODE_PK + " = " + id.ToString();

            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
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

    public class Link
    {
        public Link(int id, OdbcConnection DbConnection)
        {
            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();

            DbCommand.CommandText =
                "SELECT" +
                "  CR." + Settings.LINK_PK +
                ", CR." + Settings.LINK_PERCENT_OF_USE +
                ", CR." + Settings.CLIENT_ID_FK +
                ", CR." + Settings.RESOURCE_ID_FK +
                " FROM " + Settings.LINK_TABLE_NAME + " CR " +
                " WHERE CR." + Settings.LINK_PK + " = " + id.ToString();

            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    Id = DbReader.GetInt32(0);
                    Weight = DbReader.GetInt32(1) / 100;
                    SrcNodeId = DbReader.GetInt32(2);
                    DstNodeId = DbReader.GetInt32(3);
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
        public int SrcNodeId { get; set; }
        public int DstNodeId { get; set; }
        public double Weight { get; set; }


    }
}
