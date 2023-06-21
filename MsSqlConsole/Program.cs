using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTYS_Final_Hazırlık_Sql_Bağlantı
{
    public class Program
    {
        static SqlConnection baglanti;
        static SqlCommand komut;
        static SqlDataReader reader;
        static void Main(string[] args)
        {
            string MsSql = "Server = .; Database = deneme; Trusted_Connection = True";
            string komut = "select * from tbltest";
            SqlConnection sqlConnection = new SqlConnection(MsSql);
          
            Listele(MsSql,komut);

            string ab;
            string ac;
            int a;
            int b;
            Console.WriteLine("ogr ad Giriniz");
            ab = Console.ReadLine();
            Console.WriteLine("ogr soyad giriniz");
            ac = Console.ReadLine();
            Console.WriteLine("Maaşı Gİriniz");
            a = Convert.ToInt32( Console.ReadLine());
            Console.WriteLine("Renk id Giriniz");
            b=Convert.ToInt32( Console.ReadLine());
            SqlConnection sqlConnection1 = new SqlConnection(MsSql);
            string komut2 = "insert into tbltest (ograd,ogrsoyad,ogrmaas,renkid) values(@p1,@p2,@p3,@p4)";
            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand(komut2,sqlConnection1);
            cmd.Parameters.AddWithValue("@p1",ab);
            cmd.Parameters.AddWithValue("@p2",ac);
            cmd.Parameters.AddWithValue("@p3",a);
            cmd.Parameters.AddWithValue("@p4",b);
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();





        }



        public static void Listele(string mssql,string komut2)
        {
           
            
            baglanti = new SqlConnection();
            baglanti.ConnectionString = mssql;
            komut = new SqlCommand() ;
            komut.Connection = baglanti;
            komut.CommandText = komut2;
            baglanti.Open();
            reader = komut.ExecuteReader();



            while (reader.Read())
            {


                Console.WriteLine(reader[0] + ":" + reader[1] + " " + reader[2]);

            }

            baglanti.Close();

            Console.ReadKey();

        }
        
    }

}
