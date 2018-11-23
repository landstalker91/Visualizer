using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visualizer.Models
{
    public class Link
    {

        public Link(int id)
        {
            // select from amRel ...
            // SrcNodeId = lPortfolioItemId
            // DstNodeId = AssetTag
            // 
        }

        public int Id { get; set; }
        public int SrcNodeId { get; set; }
        public int DstNodeId { get; set; }
        public double Weight { get; set; }


    }
}
