using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visualizer.Models
{
    public class Node
    {
        public Node(int id)
        {
            // select from amPortfolio ...
            // Id = lPortfolioItemId
            // Name = AssetTag
            // 
        }
 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
