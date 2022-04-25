using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace RentalCar_konzol
{
    class Program
    {
        MySqlCommand sql = null;
        static void Main(string[] args)
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.UserID = "root";
            sb.Password = "";
            sb.Database = "rentalcar";
            sb.CharacterSet = "utf8";
            MySqlConnection connection = new MySqlConnection(sb.ToString());
           // MySqlConnection connection2 = new MySqlConnection(sb.ToString());
            MySqlCommand sql = connection.CreateCommand();
            List<Auto> autoList = new List<Auto>();
            List<Kolcsonzes> kolcsonList = new List<Kolcsonzes>();

            try
            {
                connection.Open();
                sql.CommandText = "SELECT * FROM kolcsonzes ;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                       
                           //     Console.WriteLine(dr.GetString("tol").Split(".")[0]);
                            //    Console.WriteLine(dr.GetString("tol").Split(".")[0] == "2021");
                           if(dr.GetString("tol").Split(".")[0] == "2021")
                        {
                           // Console.WriteLine(dr.GetString("tol").Split(".")[0]);
                          //  Console.WriteLine(dr.GetString("tol") + dr.GetString("ig"));
                            Kolcsonzes tmpKolcsonzes = new Kolcsonzes(dr.GetInt32("id"), Convert.ToDateTime(dr.GetString("tol")), Convert.ToDateTime(dr.GetString("ig")));
                            kolcsonList.Add(tmpKolcsonzes);
                        }
                        
                        
                        // Console.WriteLine("********************");
                    }

                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);

            }
            try
            {
                connection.Open();
                sql.CommandText = "SELECT * FROM auto ;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        

                        Auto tmpAuto = new Auto(dr.GetInt32("id"), dr.GetString("rendszam"), dr.GetString("marka"), dr.GetString("model"), dr.GetInt32("ar"));
                        autoList.Add(tmpAuto);
                     //   Console.WriteLine(tmpAuto.Rendszam);
                       //  Console.WriteLine("********************");
                    }

                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);

            }
          /*  Console.WriteLine("++++++++++++++++++++++++++");
            foreach (var myAuto in autoList)
            {

                Console.WriteLine("Auto : {0}-{1}-{2}-{3}-{4} ", myAuto.Id, myAuto.Model, myAuto.Marka, myAuto.Rendszam, myAuto.Ar);

            }
            foreach (var kolcs in kolcsonList)
            {
                Console.WriteLine("Kolcsonzesek {0} - {1} - {2}", kolcs.Id, kolcs.Tol,kolcs.Ig);
            }*/
            Console.WriteLine("+++++++++++++++++++++++++++++");



           
            try
            {
                connection.Open();
                sql.CommandText = "SELECT * FROM `auto` ORDER by ar LIMIT 1;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine("Legkisebb napi kölcsönzési díja a {0} rendszámú autonak van/{1} Ft/ ",dr.GetString("rendszam"), dr.GetInt32("ar"));

                    }
                }
                connection.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("A gépjárművek éves bevétele:");




            try
            {
                connection.Open();
                sql.CommandText = "SELECT rendszam, SUM( (ig-tol)*ar) as kolcsonzes FROM `auto`JOIN kolcsonzes USING(id) GROUP by id;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine(" {0} bevétele: {1} Ft ", dr.GetString("rendszam"), dr.GetInt32("kolcsonzes"));

                    }
                }
                connection.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
     

            try
            {
                connection.Open();
                sql.CommandText = "SELECT rendszam FROM auto WHERE id NOT IN (SELECT id FROM `auto`JOIN kolcsonzes USING(id) WHERE (ig-tol )=11);";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine("Soha nem kölcsönözték 11 napra: {0} ", dr.GetString("rendszam"));

                    }
                }
                connection.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Program vége!");

        }
    }
}
