﻿using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace Prosty_System_zamówień
{
    public class Towar
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public Decimal Cena { get; set; }
        public Decimal CenaBrutto { get; set; }
        public int wielkoscPodatku { get; set; }

        /// <summary>
        /// Pobieranie ID towaru na podstawie podanej NazwyTowaru
        /// </summary>
        /// <returns></returns>
        public int pobierzIDTowaru()
        {
            string sqlString = "SELECT id FROM  Towary WHERE NazwaTowaru LIKE '" + Nazwa + "';";
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
        /// Aktualizowanie danych o towarze. NazwaTowaru oraz Cena
        /// </summary>
        /// <returns></returns>
        public bool aktualizujTowar()
        {
            string sqlString = "UPDATE Towary SET NazwaTowaru = '" + Nazwa + "', CenaNetto = '" + Convert.ToString(Cena).Replace(',', '.') + "', CenaBrutto = '" + Convert.ToString(CenaBrutto).Replace(',', '.') + "', ProcentPodatku = '" + wielkoscPodatku.ToString() + "'  WHERE id = '" + Id + "';";
            String wiadomosc = "Nieudało się zaktualizować danych!";
            BazaDanych bd = new BazaDanych();
            return bd.wyslijDane(sqlString, wiadomosc);
        }
        /// <summary>
        /// Usuwanie towaru na podstawie ID towaru.
        /// </summary>
        /// <returns></returns>
        public bool usunTowar()
        {
            string sqlString = "DELETE FROM Towary WHERE ID  = " + Id + ";";
            string wiadomosc = "Nieudało się usunąć Towaru!";
            BazaDanych bd = new BazaDanych();
            return bd.wyslijDane(sqlString, wiadomosc);
        }
        /// <summary>
        /// Dodawanie towaru do bazy danych. Dodanie Nazwy i Ceny towaru.
        /// </summary>
        /// <returns></returns>
        public bool dodajTowar()
        {
            string sqlString = "INSERT INTO Towary ([NazwaTowaru], [CenaNetto], [CenaBrutto], [ProcentPodatku]) VALUES ('" + Nazwa + "', '" + Convert.ToString(Cena).Replace(',', '.') + "', '" + Convert.ToString(CenaBrutto).Replace(',', '.') + "','" + wielkoscPodatku.ToString() + "');";
            string wiadomosc = "Nieudało się dodać towaru!";
            BazaDanych bd = new BazaDanych();
            return bd.wyslijDane(sqlString, wiadomosc);
        }
    }
}


