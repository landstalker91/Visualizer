using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visualizer.Models
{
    public enum Direction
    {
        Parent,
        Child
    }
    public class Settings
    {
        public static int ID = 3866451;
        public static string CONNECTION_STRING = "DSN=GAAMDB_64;Server=10.1.8.95;UID=itam;PWD=Qaz12345;Database=GAAMDB;";
        public static string NODE_TABLE_NAME = "amPortfolio";
        public static string LINK_TABLE_NAME = "amIgClientResource";
        public static string NODE_PK = "lPortfolioItemId";
        public static string LINK_PK = "lIgClientResourceId";
        public static string CLIENT_ID_FK = "lClientId";
        public static string RESOURCE_ID_FK = "lResourseId";
        public static string LINK_PERCENT_OF_USE = "PercentOfUse";
        public static string IMAGE_PATH = "\\wwwroot\\images\\";
    }
}
