using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visualizer.Models
{
    public class DbContext
    {

        public DbContext(int id)
        {
            Node root = new Node(id);
            //Nodes.Add(root);

            int currentElemId = id;

            //OdbcConnection DbConnection = new OdbcConnection("DSN=GAAMDB");



            List<Node> parentNodes = getParentNodes(currentElemId);

            for (int i = 0; i <= parentNodes.Count; i++)
            {

            }
        }

        public List<Node> getParentNodes(int id)
        {

            List<Node> test = new List<Node>();
            return test;
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

        public List<Node> Nodes;
        public List<Link> Links;
    }
}
