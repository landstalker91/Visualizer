using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Drawing;
using System.IO;

namespace Visualizer.Models
{
    public class Node
    {
        public Node() { }
        public Node(int id, OdbcConnection DbConnection)
        {
            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();
            
            DbCommand.CommandText = Settings.NODE_QUERY + id.ToString();
                
                /*
            DbCommand.CommandText =
                "  SELECT" +
                "  P." + Settings.NODE_PK +
                ", P.sTGLongName" +
                ", C.Name" +
                ", ( " +
                "SELECT " +
                    "TOP 1 data.blbData " +
                "FROM " +
                    "amDocument doc " +
                "INNER JOIN " +
                    "amDocBlob data ON(doc.lDocBlobId = data.lDocBlobId) " +
               " WHERE " +
                    "lDocObjId = P.lPortfolioItemId " +
                "AND " +
                    "doc.FileName LIKE '%.bmp' " +
                ") as Icon " +
                ", ( " +
                "SELECT " +
                    "TOP 1 data.lLen " +
                "FROM " +
                    "amDocument doc " +
                "INNER JOIN " +
                    "amDocBlob data ON(doc.lDocBlobId = data.lDocBlobId) " +
               " WHERE " +
                    "lDocObjId = P.lPortfolioItemId " +
                "AND " +
                    "doc.FileName LIKE '%.bmp' " +
                ") as Len " +
                "  FROM " + Settings.NODE_TABLE_NAME + " P " +
                "  INNER JOIN amCaterory C ON (P.ligSubCategoryId = C.lCateroryId) " +
                "  WHERE P." + Settings.NODE_PK + " = " + id.ToString();
                */
            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    Id = DbReader.GetInt32(0);
                    Name = DbReader.GetString(1);
                    Category = DbReader.GetString(2);

                    ImageName = File.Exists(@"" + Directory.GetCurrentDirectory() + Settings.IMAGE_PATH + Category + Settings.IMAGE_EXTENSION) ? Category + Settings.IMAGE_EXTENSION : "default" + Settings.IMAGE_EXTENSION;
                    //File.Exists(@"" +  + Category + ".png") ? 
                    //string iconName = File.Exists(@"C:\icons\" + Category + ".png") ? Category : "default";
                    //byte[] buffer = File.ReadAllBytes(@"C:\icons\" + iconName + ".png");

                    //MemoryStream ms = new MemoryStream(buffer);
                    //Img = Image.FromStream(ms, true);

                    /*
                    if (!DbReader.IsDBNull(3))
                    {
                        long len = DbReader.GetInt32(4);
                        byte[] buffer = new byte[len - 4];
                        
                        DbReader.GetBytes(3, 4, buffer, 0, (int)len - 4);
                        MemoryStream ms = new MemoryStream(buffer);
                        fs = buffer;
                        Img = Image.FromStream(ms, true);
                    }
                    */
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            DbReader.Close();
            DbConnection.Close();

            LabelColor = Settings.NODE_LABEL_DEFAULT_COLOR;
        }
 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        //public Image Img { get; set; }
        public string ImageName { get; set; }
        public string LabelColor { get; set; }
    }

    public class Link
    {
        public Link() { }
        public Link(int id, OdbcConnection DbConnection)
        {
            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();

            DbCommand.CommandText = Settings.LINK_QUERY + id.ToString();

            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    Id = DbReader.GetInt32(0);
                    Weight = DbReader.GetInt32(1);
                    ClientId = DbReader.GetInt32(2);
                    ResourceId = DbReader.GetInt32(3);
                    Type = DbReader.GetInt32(4) == 0 ? "Расположен на" : "Использует";
                    //Color = "#B0C" + Convert.ToString(200 * (1 + DbReader.GetInt32(4)), 16);
                    //Color = Enum.GetValues(typeof(KnownColor)).GetValue(4 * DbReader.GetInt32(4)).ToString();
                    Color = Settings.LINK_COLORS[DbReader.GetInt32(4)];
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
        public int ClientId { get; set; }
        public int ResourceId { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }

    }
}
