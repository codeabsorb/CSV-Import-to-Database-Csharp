using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCSVImportDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var lineNumber = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-L864DRT\SQLEXPRESS; Integrated Security= true"))
            {
                conn.Open();
                //Put your file location here:
                using (StreamReader reader = new StreamReader(@"C:\Users\Example\Documents\Products\stock_list.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (lineNumber != 0)
                        {
                            var values = line.Split(',');

                            var sql = "INSERT INTO CSVImport.dbo.Products VALUES ('" + values[0] + "','" + values[1] + "'," + values[2] + ")";

                            var cmd = new SqlCommand();
                            cmd.CommandText = sql;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = conn;
                            cmd.ExecuteNonQuery();
                        }
                        lineNumber++;
                    }
                }
                conn.Close();
            }
            Console.WriteLine("Products Import Complete");
            Console.ReadLine();
        }
    }
}
