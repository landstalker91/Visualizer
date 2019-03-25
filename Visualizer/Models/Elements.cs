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
            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            if (DbReader.HasRows)
            {
                while (DbReader.Read())
                {
                    Id = DbReader.GetInt32(0);
                    ModelLongName = !DbReader.IsDBNull(1) ? DbReader.GetString(1) : "";
                    ModelShortName = !DbReader.IsDBNull(2) ? DbReader.GetString(2) : "";
                    Name = !DbReader.IsDBNull(3) ? DbReader.GetString(3): "";
                    SubCategory = !DbReader.IsDBNull(4) ? DbReader.GetString(4) : "";
                    Category = !DbReader.IsDBNull(5) ? DbReader.GetString(5) : "";
                    Cost = !DbReader.IsDBNull(6) ? DbReader.GetDouble(6) : 0;
                    Status = !DbReader.IsDBNull(7) ? DbReader.GetString(7) : "";
                    Location = !DbReader.IsDBNull(8) ? DbReader.GetString(8) : "";

                    Console.WriteLine(@"" + Directory.GetCurrentDirectory() + "\\wwwroot\\" + Settings.IMAGE_PATH + "\\" + Category + Settings.IMAGE_EXTENSION);
                    string str = @"" + Directory.GetCurrentDirectory() + "\\wwwroot\\" + Settings.IMAGE_PATH + "\\" + Category + Settings.IMAGE_EXTENSION;
                    ImageName = File.Exists(@"" + Directory.GetCurrentDirectory() + "\\wwwroot\\" + Settings.IMAGE_PATH + "\\" + Category + Settings.IMAGE_EXTENSION) ? Category + Settings.IMAGE_EXTENSION : "default" + Settings.IMAGE_EXTENSION;

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
        public string SubCategory { get; set; }
        public string ImageName { get; set; }
        public string LabelColor { get; set; }
        public string ModelLongName { get; set; }
        public string ModelShortName { get; set; }
        public double Cost { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
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
