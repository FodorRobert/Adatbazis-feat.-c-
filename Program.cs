using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Kosarlabdacsapat
{
    internal class Program
    {
        public static Connect conn = new Connect();
        public static void GetAllData()
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM `team`";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            MySqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            do {
                var player = new
                {
                    Id = dr.GetInt32(0),
                    Name = dr.GetString(1),
                    Height = dr.GetInt32(2),
                    Weight = dr.GetInt32(3),
                    CreatedTime = dr.GetDateTime(4)

                }; Console.WriteLine($"Játékos adatok: {player.Name}, {player.CreatedTime}, {player.Weight}");
            } while (dr.Read());

            conn.Connection.Close();
        }

        public static void addNewPlayer(string name, int height, int weight)
        {
            try
            {
                conn.Connection.Open();

                string sql = $"INSERT INTO `team`(`Name`, `Height`, `Weight`) VALUES ('{name}', {height}, {weight})";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
                cmd.ExecuteNonQuery();

                conn.Connection.Close();
            }
            catch (Exception kivetel)
            {
                Console.WriteLine(kivetel.Message);   
            }
        }
        
        public static void deletePlayer(int id)
        {
            conn.Connection.Open();

            string sql = $"DELETE FROM `team` WHERE  `Id` = {id};";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }
        
        public static void updateKosar(int id, string name, int height, int weight)
        {
            conn.Connection.Open();

            string sql = $"UPDATE `team` SET `Name` = '{name}',`Height` = {height},`Weight` = {weight} WHERE `Id` = {id};";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }

        static void Main(string[] args)
        {
            //GetAllData();

            /*
            try
            {
                Console.Write("Kérem a játékos nevét: ");
                string name = Console.ReadLine();
                Console.Write("Kérem a játékos magasságát: ");
                int height = int.Parse(Console.ReadLine());
                Console.Write("Kérem a játékos súlyát: ");
                int weight = int.Parse(Console.ReadLine());

                addNewPlayer(name, height, weight);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            */

            /*
            try
            {
                Console.Write("Kérem a játékos azonosítót a törléshez: ");
                int azon = int.Parse(Console.ReadLine());
                deletePlayer(azon);
                Console.WriteLine("Sikeres törlés");
            }
            catch (Exception e)
            {
                Console.WriteLine (e.Message);
            }
            */

            try
            {
                Console.Write("Kérem a játékos azonosítót: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Kérem az új nevet: ");
                string name = Console.ReadLine();
                Console.Write("Kérem az új játékos magasságát: ");
                int height = int.Parse(Console.ReadLine());
                Console.Write("Kérem az új játékos súlyát: ");
                int weight = int.Parse(Console.ReadLine());

                updateKosar (id, name, height, weight);
                Console.WriteLine("Sikeres frissítés.");
            }
            catch (Exception e)
            {
                Console.WriteLine (e.Message);
            }
        }
    }
}
