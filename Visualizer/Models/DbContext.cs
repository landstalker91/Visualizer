using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Drawing;
using System.IO;

namespace Visualizer.Models
{
    public class ElementsNetwork
    {
        public OdbcConnection DbConnection;
        public List<Node> Nodes = new List<Node>();
        public List<Link> Links = new List<Link>();
        public int RootID;

        public ElementsNetwork(int id, OdbcConnection сonnection)
        {
            DbConnection = сonnection;
            RootID = id;
            Node rootNode = new Node(id, сonnection)
            {
                LabelColor = Settings.NODE_LABEL_ROOT_COLOR
            };
            Nodes.Add(rootNode);

            List<Node> parentNodes = getRelatedNodes(Nodes, Direction.Parent);
            List<Node> childNodes = getRelatedNodes(Nodes, Direction.Child);

            List<Link> parentLinks = getRelatedLinksWrapper(Nodes, Direction.Parent);
            List<Link> childLinks = getRelatedLinksWrapper(Nodes, Direction.Child);

            Nodes.AddRange(parentNodes.GroupBy(x => x.Id).Select(x => x.First()));
            Nodes.AddRange(childNodes.GroupBy(x => x.Id).Select(x => x.First()));

            Links.AddRange(parentLinks.GroupBy(x => x.Id).Select(x => x.First()));
            Links.AddRange(childLinks.GroupBy(x => x.Id).Select(x => x.First()));

            int a = 1;
        }

        public List<Node> getRelatedNodes(List<Node> elements, Direction direction)
        {
            List<Node> result = new List<Node>();
            //List<Node> recurseResult = new List<Node>();
            List<int> relatedIds = new List<int>();
            
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
                    id1 = Settings.CLIENT_ID;
                    id2 = Settings.RESOURCE_ID;
                    break;
                case Direction.Child:
                    id1 = Settings.RESOURCE_ID;
                    id2 = Settings.CLIENT_ID;
                    break;
            }

            DbCommand.CommandText =
                " SELECT DISTINCT CR." + id1 +
                " FROM " + Settings.LINK_TABLE_NAME + " CR" +
                " WHERE CR." + id2 + " IN (" + ids + ")";

            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    relatedIds.Add(DbReader.GetInt32(0));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }

            DbReader.Close();
            DbConnection.Close();

            foreach (int id in relatedIds)
            {
                Node node = new Node(id, DbConnection);
                if (!result.Contains(node))
                {
                    result.Add(node);
                }
            }

            /*recurseResult = getRelatedNodes(result, direction);

            foreach (Node elem in recurseResult)
            {
                if (!result.Contains(elem))
                {
                    result.Add(elem);
                }
            }*/

            if (result.Count != 0)
            {
                result.AddRange(getRelatedNodes(result, direction));
            }

            return result;
        }

        public List<Link> getRelatedLinksWrapper(List<Node> elements, Direction direction)
        {
            string id = "";
            string ids = "";
            foreach (Node elem in elements)
            {
                id = elem.Id.ToString();
                ids += ids == "" ? id : "," + id;
            }

            return getRelatedLinks(ids, direction);
        }

        public List<Link> getRelatedLinksWrapper(List<Link> elements, Direction direction)
        {
            string ids = "";
            string id = "";
            foreach (Link elem in elements)
            {
                id = direction == Direction.Parent ? elem.ClientId.ToString() : elem.ResourceId.ToString();
                ids += ids == "" ? id : "," + id;
            }

            return getRelatedLinks(ids, direction);

        }

        public List<Link> getRelatedLinks(string ids, Direction direction)
        {
            List<Link> result = new List<Link>();
            List<int> relatedIds = new List<int>();

            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();

            DbCommand.CommandText =
                " SELECT DISTINCT CR." + Settings.LINK_PK +
                " FROM " + Settings.LINK_TABLE_NAME + " CR" +
                " WHERE CR." + (direction == Direction.Parent ? Settings.RESOURCE_ID : Settings.CLIENT_ID) +
                " IN (" + ids + ")";

            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    relatedIds.Add(DbReader.GetInt32(0));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }

            DbReader.Close();
            DbConnection.Close();

            foreach (int id in relatedIds)
            {
                Link link = new Link(id, DbConnection);
                result.Add(link);
            }

            if (result.Count != 0)
            {
                result.AddRange(getRelatedLinksWrapper(result, direction));
            }

            return result;
        }
    }
}
