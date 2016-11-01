using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace Prosty_System_zamówień
{
    public class BazaDanych
    {
        //public string connString = @"Server=DESKTOP-AEJBGEO\SQLEXPRESS;Database=Test;Trusted_Connection=True;";

        /// <summary>
        /// Pobieranie danych z bazy danych na podstawie podanego zapytania
        /// </summary>
        /// <returns></returns>
        public DataTable pobierzDane(String sqlString)
        {
            if (sqlString != null)
            {
                SqlConnection polaczenie = new SqlConnection(pobierzLancuchPolaczenia());
                try
                {
                    polaczenie.Open();
                    using (SqlCommand zapytanie = new SqlCommand(sqlString, polaczenie))
                    {
                        DataTable dt = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(zapytanie);
                        da.Fill(dt);
                        return dt;
                    }

                    polaczenie.Close();
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Nieudało się połączyć z bazą danych!");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Wysyłanie danych do bazy danych na podstawie podanego zapytania
        /// </summary>
        /// <returns></returns>
        public bool wyslijDane(String sqlString, String wiadomosc)
        {

            SqlConnection polaczenie = new SqlConnection(pobierzLancuchPolaczenia());
            try
            {
                polaczenie.Open();

                SqlCommand zapytanie = new SqlCommand(sqlString, polaczenie);
                zapytanie.ExecuteNonQuery();
                polaczenie.Close();
                return true;
            }

            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show(wiadomosc);
                return false;
            }
        }
        /// <summary>
        /// Pobieranie ID towaru lub Klienta w zależności od złożonego zapytania
        /// </summary>
        /// <returns></returns>
        public int pobierzID(String sqlString)
        {
            int Id = -1;
            if (sqlString != null)
            {
                SqlConnection polaczenie = new SqlConnection(pobierzLancuchPolaczenia());
                try
                {
                    polaczenie.Open();
                    using (SqlCommand zapytanie = new SqlCommand(sqlString, polaczenie))
                    {
                        DataTable dt = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(zapytanie);
                        da.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                          Id  = (int)row.ItemArray[0];
                        }
                        return Id;
                    }

                    polaczenie.Close();
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Nieudało się połączyć z bazą danych!");
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
        public string pobierzLancuchPolaczenia()
        {
            string line;
            // Read the file and display it line by line.
            System.IO.StreamReader file =
            new System.IO.StreamReader(@"..\Debug\polaczenie.txt");
            line = file.ReadLine();
            file.Close();
            return line;
        }
    }
}
