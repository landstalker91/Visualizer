using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;

namespace Visualizer.Models
{
    public enum Direction
    {
        Parent,
        Child
    }
    public static class Settings
    {
        //public static int ID = 3866451;
        //Connection
        //public static string DSN_NAME = "GAAMDB_TEST";
        //public static string SERVER = "10.1.8.95";
        //public static string USER = "sa";
        // public static string PWD = "Qaz12345";
        //public static string DATABASE = "GAAMDB_TEST";
        //public static string CONNECTION_STRING = "DSN=" + DSN_NAME + ";Server=" + SERVER + ";UID=" + USER + ";PWD=" + PWD + ";Database=" + DATABASE + ";";
        public static string CONNECTION_STRING;
        public static string NODE_QUERY;
        public static string LINK_QUERY;
        public static string NODE_LABEL_DEFAULT_COLOR;
        public static string NODE_LABEL_ROOT_COLOR;
        public static string LINK_DEFAULT_COLOR;
        public static string[] LINK_COLORS;
        public static string LINK_SELECTED_COLOR;
        public static string LINK_PK;
        public static string CLIENT_ID;
        public static string RESOURCE_ID;
        public static string NODE_PK;
        public static string IMAGE_PATH;
        public static string IMAGE_EXTENSION;
        public static string NODE_TABLE_NAME;
        public static string LINK_TABLE_NAME;

        /*
        //Tabel query params
        public static string NODE_TABLE_NAME = "amPortfolio";
        public static string LINK_TABLE_NAME = "amIgClientResource";
        public static string NODE_PK = "lPortfolioItemId";
        public static string LINK_PK = "lIgClientResourceId";
        public static string CLIENT_ID_FK = "lClientId";
        public static string RESOURCE_ID_FK = "lResourseId";
        public static string LINK_PERCENT_OF_USE = "PercentOfUse";
        public static string LINK_TYPE = "Type";

        //Environment and main params
        public static string IMAGE_PATH = "\\wwwroot\\images\\";
        public static string IMAGE_EXTENSION = ".png";
        public static string LABEL_DEFAULT_COLOR = "#21313B";
        public static string LABEL_ROOT_COLOR = "#702C1A";
        public static string LINK_DEFAULT_COLOR = "#B0C6CF";
        public static string DEFAULT_DIRECTION = "LR";
        */

    }
}
