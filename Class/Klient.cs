using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace Prosty_System_zamówień
{
    public class Klient
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        /// <summary>
        /// Pobieranie ID klienta znając Imie i Nazwisko
        /// </summary>
        /// <returns></returns>
        public int pobierzIDKlienta()
        {
            string sqlString = "SELECT ID FROM  Klienci WHERE Imie LIKE '"+Imie+"' AND Nazwisko LIKE '"+Nazwisko+"';";
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
                        foreach (DataRow row in dt.Rows)
                        {
                            Id = (int)row.ItemArray[0];
                          
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
        /// <summary>
        /// Aktualizacja danych klienta (Imie, Nazwisko, ID)
        /// </summary>
        /// <returns></returns>
        public bool aktualizujKlienta()
        {
            string sqlString = "UPDATE Klienci SET Imie = '" + Imie + "', Nazwisko = '" + Nazwisko + "'  WHERE id = '" +Id + "';";
            string wiadomosc = "Nieudało się zaktualizować danych!";
            BazaDanych bd = new BazaDanych();

            return bd.wyslijDane(sqlString, wiadomosc);
        }
        /// <summary>
        /// Usuwanie klienta. Usuwanie rekordu na podsatwie numeru ID klienta
        /// </summary>
        /// <returns></returns>
        public bool usunKlienta()
        {
            string sqlString = "DELETE FROM Klienci WHERE ID  = " + Id + ";";
            string wiadomosc = "Nie udało się usunąc klienta!";
            BazaDanych bd = new BazaDanych();
             
            return bd.wyslijDane(sqlString, wiadomosc);
        }
        /// <summary>
        /// Dodawanie klienta. Dodawanie poprzez znajomość Imienia i Nazwiska
        /// </summary>
        /// <returns></returns>
        public bool dodajOsobe()
        {
            string sqlString = "INSERT INTO Klienci ([Imie], [Nazwisko]) VALUES ('" +Imie + "', '" + Nazwisko + "');";
            string wiadomosc = "Nieudało się dodać klienta!";
            BazaDanych bd = new BazaDanych();
            return bd.wyslijDane(sqlString, wiadomosc);
        }
    }
}
