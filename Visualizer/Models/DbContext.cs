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
            DbConnection = сonnection;
            Node root = new Node(id, DbConnection);
            Nodes.Add(root);

            //int currentElemId = id;

            List<Node> parentNodes = getParentNodes(Nodes);

            int a = 1;
        }

        public List<Node> getParentNodes(List<Node> elements)
        {
            List<Node> result = new List<Node>();
            string ids = "";

            foreach (Node elem in elements)
            {
                ids += ids == "" ? elem.Id.ToString() : "," + elem.Id.ToString();
            }

            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();

            DbCommand.CommandText = "SELECT DISTINCT P.lPortfolioItemId, P.sTGLongName, C.Name FROM amPortfolio P INNER JOIN amCaterory C ON (P.ligSubCategoryId = C.lCateroryId) WHERE lPortfolioItemId IN (SELECT DISTINCT CR.lClientId FROM amIGClientResource CR WHERE CR.lResourseId IN (" + ids + "))";
            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    Node node = new Node();

                    //Console.WriteLine("{0}\t{1}", DbReader.GetInt32(0), DbReader.GetString(1));

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
                result.AddRange(getParentNodes(result));
            }

            return result;
        }

        public List<Link> getParentLinks(int id)
        {
            List<Link> test = new List<Link>();
            return test;
        }

        public List<Node> getChildNodes(int id)
        {
            List<Node> test = new List<Node>();
            return test;
        }

        public List<Link> getChildLinks(int id)
        {
            List<Link> test = new List<Link>();
            return test;
        }


    }
}
