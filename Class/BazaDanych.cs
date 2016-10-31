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
        public string connString = @"Server=DESKTOP-AEJBGEO\SQLEXPRESS;Database=Test;Trusted_Connection=True;";

        /// <summary>
        /// Pobieranie danych z bazy danych na podstawie podanego zapytania
        /// </summary>
        /// <returns></returns>
        public DataTable pobierzDane(String sqlString)
        {
            if (sqlString != null)
            {
                string connString = @"Server=DESKTOP-AEJBGEO\SQLEXPRESS;Database=Test;Trusted_Connection=True;";

                SqlConnection polaczenie = new SqlConnection(connString);
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
            //wiadomość to wiadomość pokazywana w MessageBox w przypadku wystapienia błędu
            string connString = @"Server=DESKTOP-AEJBGEO\SQLEXPRESS;Database=Test;Trusted_Connection=True;";

            SqlConnection polaczenie = new SqlConnection(connString);
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
    }
}
