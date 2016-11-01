using System;
using System.Data.SqlClient;
using System.Windows;
using System.Data;


namespace Prosty_System_zamówień
{
    class Zamowienia
    {
        public int Id { get; set; }
        public int ID_Klienta { get; set; }
        public int ID_Towaru { get; set; }
        public DateTime dataDodania { get; set; }

        /// <summary>
        /// Składanie zamówienia na podsatwie wybranego Klienta i Towaru
        /// </summary>
        /// <returns></returns>
        public bool zlozZamowienie(Klient kt, Towar tow)
        {
            ID_Klienta = kt.pobierzIDKlienta();
            ID_Towaru = tow.pobierzIDTowaru();
            dataDodania = DateTime.Now;

            string connString = @"Server=DESKTOP-AEJBGEO\SQLEXPRESS;Database=Test;Trusted_Connection=True;";
            string sqlString = "INSERT INTO Zamowienia ([ID_Klienta], [ID_Towaru], [Data_Zamowienia]) VALUES ('" + ID_Klienta + "', '" + ID_Towaru + "' , '"+dataDodania.ToString("MM-dd-yyyy HH:mm:ss")+"');";

            BazaDanych bd = new BazaDanych();
            return bd.wyslijDane(sqlString, "Nie udalo się połączyć z bazą danych!");
        }
        /// <summary>
        /// Pobranie zamówień do DataTable. Wszystkich jeżeli podano null, Konkretnego Klienta jeżeli przekazano dane Klienta.
        /// </summary>
        /// <returns></returns>
        public DataTable pobierzZamowienia(Klient kt)
        {
            DataTable zam = new DataTable();
            string sqlString;
            if (kt == null)
            {
                sqlString  = "SELECT Zamowienia.Id, Data_Zamowienia, Imie, Nazwisko, NazwaTowaru, CenaNetto, CenaBrutto, ProcentPodatku FROM Zamowienia JOIN Klienci ON ID_Klienta = Klienci.ID JOIN Towary ON ID_Towaru = Towary.id";
            }
            else
            {
                sqlString = "SELECT Zamowienia.Id, Data_Zamowienia, Imie, Nazwisko, NazwaTowaru, CenaNetto, CenaBrutto, ProcentPodatku FROM Zamowienia JOIN Klienci ON ID_Klienta = Klienci.ID JOIN Towary ON ID_Towaru = Towary.id WHERE Klienci.ID = " + kt.Id.ToString() +"";
            }
            
            if (sqlString != null)
            {
                BazaDanych bd = new BazaDanych();
                string connString = bd.pobierzLancuchPolaczenia();

                SqlConnection polaczenie = new SqlConnection(connString);
                try
                {
                    polaczenie.Open();
                    using (SqlCommand zapytanie = new SqlCommand(sqlString, polaczenie))
                    {
                        DataTable dt = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(zapytanie);
                        da.Fill(dt);
                        if(dt.Rows.Count == 0)
                            MessageBox.Show("W systemie nie ma wyników spełniających kryteria.");
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
        public DataTable pobierzZamowieniaT(Towar tow)
        {
            DataTable zam = new DataTable();
            string sqlString;
            if (tow == null)
            {
                sqlString = "SELECT Zamowienia.Id, Data_Zamowienia, Imie, Nazwisko, NazwaTowaru, CenaNetto, CenaBrutto, ProcentPodatku FROM Zamowienia JOIN Klienci ON ID_Klienta = Klienci.ID JOIN Towary ON ID_Towaru = Towary.id";
            }
            else
            {
                sqlString = "SELECT Zamowienia.Id, Data_Zamowienia, Imie, Nazwisko, NazwaTowaru, CenaNetto, CenaBrutto, ProcentPodatku FROM Zamowienia JOIN Klienci ON ID_Klienta = Klienci.ID JOIN Towary ON ID_Towaru = Towary.id WHERE Towary.id = " + tow.Id.ToString() + "";
            }

            if (sqlString != null)
            {
                BazaDanych bd = new BazaDanych();
                string connString = bd.pobierzLancuchPolaczenia();

                SqlConnection polaczenie = new SqlConnection(connString);
                try
                {
                    polaczenie.Open();
                    using (SqlCommand zapytanie = new SqlCommand(sqlString, polaczenie))
                    {
                        DataTable dt = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(zapytanie);
                        da.Fill(dt);
                        if (dt.Rows.Count == 0)
                            MessageBox.Show("W systemie nie ma wyników spełniających kryteria.");
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
    }
}
