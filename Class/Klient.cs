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
            BazaDanych bd = new BazaDanych();
            string sqlString = "SELECT ID FROM  Klienci WHERE Imie LIKE '"+Imie+"' AND Nazwisko LIKE '"+Nazwisko+"';";
            Id  = bd.pobierzID(sqlString);

            return Id;
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
