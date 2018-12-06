using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace Visualizer.Models
{
    public class ElementsNetwork
    {
        public OdbcConnection DbConnection;
        public List<Node> Nodes = new List<Node>();
        public List<Link> Links = new List<Link>();

        public ElementsNetwork(int id, OdbcConnection сonnection)
        {
            List<Node> Node = new List<Node>();

            DbConnection = сonnection;
            Node rootNode = new Node(id, DbConnection);

            Nodes.Add(rootNode);

            List<Node> parentNodes = getRelatedNodes(Nodes, Direction.Parent);
            List<Node> childNodes = getRelatedNodes(Nodes, Direction.Child);

            List<Link> parentLinks = getRelatedLinks(rootNode, Direction.Parent);
            List<Link> childLinks = getRelatedLinks(rootNode, Direction.Child);

            Nodes.AddRange(parentNodes);
            Nodes.AddRange(childNodes);

            Links.AddRange(parentLinks);
            Links.AddRange(childLinks);


            int a = 1;
        }

        public List<Node> getRelatedNodes(List<Node> elements, Direction direction)
        {
            List<Node> result = new List<Node>();
            string ids = "";
            string id1 = "";
            string id2 = "";

            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();

            foreach (Node elem in elements)
            {
                ids += ids == "" ? elem.Id.ToString() : "," + elem.Id.ToString();
            }

            switch (direction)
            {
                case Direction.Parent:
                    id1 = Settings.CLIENT_ID_FK;
                    id2 = Settings.RESOURCE_ID_FK;
                    break;
                case Direction.Child:
                    id1 = Settings.RESOURCE_ID_FK;
                    id2 = Settings.CLIENT_ID_FK;
                    break;
            }

            DbCommand.CommandText =
                "SELECT DISTINCT P." +
                Settings.NODE_PK + ", P.sTGLongName, C.Name FROM " +
                Settings.NODE_TABLE_NAME + " P INNER JOIN amCaterory C ON (P.ligSubCategoryId = C.lCateroryId) WHERE P." +
                Settings.NODE_PK +
                " IN (SELECT DISTINCT CR." + id1 +
                " FROM " + Settings.LINK_TABLE_NAME + " CR WHERE CR." +
                id2 + " IN (" + ids + "))";

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

        public List<Link> getRelatedLinks(Node element, Direction direction)
        {
            List<Link> result = new List<Link>();

            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();

            DbCommand.CommandText = "SELECT DISTINCT CR." + Settings.LINK_PK +
                ", CR." + Settings.LINK_PERCENT_OF_USE + ", CR." + Settings.CLIENT_ID_FK + ", CR." + Settings.RESOURCE_ID_FK +
                " FROM " + Settings.LINK_TABLE_NAME + " CR WHERE CR." + (direction == Direction.Parent ? Settings.RESOURCE_ID_FK : Settings.CLIENT_ID_FK ) + " = " + element.Id;

            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    Link link = new Link();

                    link.Id = DbReader.GetInt32(0);
                    link.Weight = DbReader.GetInt32(1) / 100;
                    link.ClientId = DbReader.GetInt32(2);
                    link.ResourceId = DbReader.GetInt32(3);

                    result.Add(link);
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
                result.AddRange(getRelatedLinks(result, direction));
            }

            return result;

        }

        public List<Link> getRelatedLinks(List<Link> elements, Direction direction)
        {
            List<Link> result = new List<Link>();
            string ids = "";
            string id = "";

            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();

            foreach (Link elem in elements)
            {
                id = direction == Direction.Parent ? elem.ClientId.ToString() : elem.ResourceId.ToString();
                ids += ids == "" ? id : "," + id;
            }

            DbCommand.CommandText = "SELECT DISTINCT CR." + Settings.LINK_PK +
                ", CR." + Settings.LINK_PERCENT_OF_USE + ", CR." + Settings.CLIENT_ID_FK + ", CR." + Settings.RESOURCE_ID_FK +
                " FROM " + Settings.LINK_TABLE_NAME + " CR WHERE CR." + (direction == Direction.Parent ? Settings.RESOURCE_ID_FK : Settings.CLIENT_ID_FK) + " IN (" + ids + ")";

            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    Link link = new Link();

                    link.Id = DbReader.GetInt32(0);
                    link.Weight = DbReader.GetInt32(1) / 100;
                    link.ClientId = DbReader.GetInt32(2);
                    link.ResourceId = DbReader.GetInt32(3);

                    result.Add(link);
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
                result.AddRange(getRelatedLinks(result, direction));
            }

            return result;
        }
    }
}
