using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace Visualizer.Models
{
    public class DataContext
    {
        public OdbcConnection DbConnection;
        public List<Node> Nodes = new List<Node>();
        public List<Link> Links = new List<Link>();

        public DataContext(int id, OdbcConnection сonnection)
        {
            List<Node> Node = new List<Node>();

            DbConnection = сonnection;
            Node rootNode = new Node(id, DbConnection);

            Nodes.Add(rootNode);

            List<Node> parentNodes = getRelatedNodes(Nodes, Direction.Parent);
            List<Node> childNodes = getRelatedNodes(Nodes, Direction.Child);

            Nodes.AddRange(parentNodes);
            Nodes.AddRange(childNodes);

            int a = 1;
        }

        public List<Node> getRelatedNodes(List<Node> elements, Direction direction)
        {
            List<Node> result = new List<Node>();
            string ids = "";

            foreach (Node elem in elements)
            {
                ids += ids == "" ? elem.Id.ToString() : "," + elem.Id.ToString();
            }

            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();

            switch (direction)
            {
                case Direction.Parent:
                    DbCommand.CommandText =
                        "SELECT DISTINCT P." +
                        Settings.NODE_PK + ", P.sTGLongName, C.Name FROM " +
                        Settings.NODE_TABLE_NAME + " P INNER JOIN amCaterory C ON (P.ligSubCategoryId = C.lCateroryId) WHERE P." +
                        Settings.NODE_PK +
                        " IN (SELECT DISTINCT CR." + Settings.CLIENT_ID_FK +
                        " FROM " + Settings.LINK_TABLE_NAME + " CR WHERE CR." +
                        Settings.RESOURCE_ID_FK + " IN (" + ids + "))";
                    break;
                case Direction.Child:
                    DbCommand.CommandText =
                        "SELECT DISTINCT P." +
                        Settings.NODE_PK + ", P.sTGLongName, C.Name FROM " +
                        Settings.NODE_TABLE_NAME + " P INNER JOIN amCaterory C ON (P.ligSubCategoryId = C.lCateroryId) WHERE P." +
                        Settings.NODE_PK +
                        " IN (SELECT DISTINCT CR." + Settings.RESOURCE_ID_FK +
                        " FROM " + Settings.LINK_TABLE_NAME + " CR WHERE CR." +
                        Settings.CLIENT_ID_FK + " IN (" + ids + "))";
                    break;
            }

            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    Node node = new Node();

                    node.Id = DbReader.GetInt32(0);
                    node.Name = DbReader.GetString(1);
                    node.Category = DbReader.GetString(2);

                    result.Add(node);
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            DbReader.Close();
            DbConnection.Close();

            if (result.Count != 0)
            {
                result.AddRange(getRelatedNodes(result, direction));
            }

            return result;
        }

        public List<Link> getParentLinks(int id)
        {
            List<Link> test = new List<Link>();
            return test;
        }


        public List<Link> getChildLinks(int id)
        {
            List<Link> test = new List<Link>();
            return test;
        }


    }
}
