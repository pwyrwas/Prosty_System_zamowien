using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.IO;



namespace Prosty_System_zamówień
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Zarządzanie klientami

            dataGrid.Visibility = Visibility.Hidden;
            edytujKlienta.Visibility = Visibility.Hidden;
            infCombobox.Visibility = Visibility.Hidden;
            comboBox.Visibility = Visibility.Hidden;

            //Zarządzanie towarami

            listaTowarow.Visibility = Visibility.Hidden;
            edytujTowar.Visibility = Visibility.Hidden;
            infWybierzTowar.Visibility = Visibility.Hidden;
            WybierzTowar.Visibility = Visibility.Hidden;
        }

        private void edytujKlienta_Click(object sender, RoutedEventArgs e)
        {
            string sqlString = null;
            Klient kl = new Klient();
            try
            {
                string wybranyKlient = comboBox.SelectedValue.ToString(); //wczyatnie wartości z comboboxa do stringa

                String[] substrings = wybranyKlient.Split(' '); // podzielenie stringa na imie i nazwisko
                kl.Imie = substrings[0];
                kl.Nazwisko = substrings[1];

                sqlString = "SELECT * FROM Klienci WHERE Imie Like '" + kl.Imie + "' AND Nazwisko Like '" + kl.Nazwisko + "';";
                DataTable dt = new DataTable();
                BazaDanych bd = new BazaDanych();
                dt = bd.pobierzDane(sqlString);
                foreach (DataRow row in dt.Rows)
                {
                    kl.Id = (int)row.ItemArray[0];
                }

                Window2 wnd = new Window2(kl);  //przekazanie do nowego okna
                wnd.Show();


            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Nie wybrano klienta do edycji! Prosze wybrać klienta z listy rozwijanej.");
            }

        }

        private void WybierzTowar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dodaj_towar(object sender, RoutedEventArgs e)
        {

            DodajTowar wnd = new DodajTowar();  //przekazanie do nowego okna
            wnd.Show();
        }

        private void pobierz_towary(object sender, RoutedEventArgs e)
        {
            string sqlString = "SELECT * FROM Towary";

            DataTable dt = new DataTable();
            BazaDanych bd = new BazaDanych();

            dt = bd.pobierzDane(sqlString);

            listaTowarow.Visibility = Visibility.Visible;
            listaTowarow.ItemsSource = dt.DefaultView;
            WybierzTowar.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string etykieta = row.ItemArray[1].ToString();
                WybierzTowar.Items.Add(etykieta);
            }
            if (dt != null)
            {
                edytujTowar.Visibility = Visibility.Visible;
                infWybierzTowar.Visibility = Visibility.Visible;
                WybierzTowar.Visibility = Visibility.Visible;
            }
        }

        private void edytujTowar_Click(object sender, RoutedEventArgs e)
        {
            string sqlString = null;
            Towar tow = new Towar();
            try
            {
                tow.Nazwa = WybierzTowar.SelectedValue.ToString(); //wczyatnie wartości z comboboxa do stringa
                sqlString = "SELECT * FROM Towary WHERE NazwaTowaru Like '" + tow.Nazwa + "';";
                DataTable dt = new DataTable();
                BazaDanych bd = new BazaDanych();
                dt = bd.pobierzDane(sqlString);
                foreach (DataRow row in dt.Rows)
                {
                    tow.Id = (int)row.ItemArray[0];
                    tow.Cena = Convert.ToDecimal(row.ItemArray[2].ToString()); //żeby użyć trzeba zmienić w klasie typ z string na int
                    tow.wielkoscPodatku = (int)row.ItemArray[4];
                }
                EdytujTowar wnd = new EdytujTowar(tow);  //przekazanie do nowego okna
                wnd.Show();
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Nie wybrano towaru do edycji! Prosze wybrać towar z listy rozwijanej.");
            }
     }

        private void dodajKlientaPrzyZamawianiu_Click(object sender, RoutedEventArgs e)
        {
            DodajOsobe wnd = new DodajOsobe();  //przekazanie do nowego okna
            wnd.Show();
        }   

        private void comboKlienci_DropDownOpened(object sender, EventArgs e)
        {
            //pobierz dane klientów
            string sqlStringKlienci = "SELECT * FROM Klienci";
            DataTable klienci = new DataTable();
            BazaDanych bd = new BazaDanych();
            klienci = bd.pobierzDane(sqlStringKlienci);

            comboKlienci.Items.Clear();
            lKlientowDoWys.Items.Clear();
            try
            {
                klienci = bd.pobierzDane(sqlStringKlienci);
                foreach (DataRow row in klienci.Rows)
                {
                    string etykieta = row.ItemArray[1].ToString() + " " + row.ItemArray[2].ToString();
                    comboKlienci.Items.Add(etykieta);
                    lKlientowDoWys.Items.Add(etykieta);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się załadować listy klientów!");
            }          
        }

        private void comboTowary_DropDownOpened(object sender, EventArgs e)
        {
            string sqlStringTowary = "SELECT * FROM Towary";
            BazaDanych bd = new BazaDanych();
            DataTable towary = new DataTable();
            towary = bd.pobierzDane(sqlStringTowary);

            comboTowary.Items.Clear();
            lTowarowDoWys.Items.Clear();
            try
            {
                towary = bd.pobierzDane(sqlStringTowary);
                foreach (DataRow row in towary.Rows)
                {
                    string etykieta = row.ItemArray[1].ToString();
                    comboTowary.Items.Add(etykieta);
                    lTowarowDoWys.Items.Add(etykieta);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się załadować listy towarów!");
            }
        }

        private void ZlozZamowienie_Click(object sender, RoutedEventArgs e)
        {
            if (comboKlienci.SelectedItem == null && comboTowary.SelectedItem == null)
            {
                MessageBox.Show("Proszę wybrać towar oraz klienta, który go zamowił!");
            }
            else
            {
                //Tutaj dodawanie zamówienia do bazy danych.
                Klient kt = new Klient();
                Towar tow = new Towar();
                Zamowienia zamowienie = new Zamowienia();
                try
                {
                tow.Nazwa = comboTowary.SelectedValue.ToString();
                String[] substrings = comboKlienci.SelectedValue.ToString().Split(' '); // podzielenie stringa na imie i nazwisko
                kt.Imie = substrings[0];
                kt.Nazwisko = substrings[1];

                bool stanDodawania = zamowienie.zlozZamowienie(kt, tow);
                try
                {
                    if (stanDodawania == true)
                        MessageBox.Show("Dodawanie nowego zamówienia powiodło się!");
                    else
                        MessageBox.Show("Nowe zamówienie nie zostało dodane!");
                }
                catch (Exception)
                {

                    MessageBox.Show("Nie udało się dodać zamówienia!");
                }
                }
                catch (Exception)
                {

                    MessageBox.Show("Proszę uzupełnić pola wybór klienta oraz wybór towaru!");
                }
            }
        }

        private void pobierzAll_Click(object sender, RoutedEventArgs e)
        {
            Klient kt = new Klient();
            Zamowienia zam = new Zamowienia();
            DataTable dt = new DataTable();
           
            dt = zam.pobierzZamowienia(null);
            dataZamowienia.ItemsSource = dt.DefaultView;   
        }

        private void filtruj_Click(object sender, RoutedEventArgs e)
        {
            Klient kt = new Klient();
            Zamowienia zam = new Zamowienia();
            DataTable dt = new DataTable();
            try
            {
                String[] substrings = lKlientowDoWys.SelectedValue.ToString().Split(' '); // podzielenie stringa na imie i nazwisko
                kt.Imie = substrings[0];
                kt.Nazwisko = substrings[1];
                kt.pobierzIDKlienta();
            }
            catch (Exception)
            {
                MessageBox.Show("Proszę wybrać klienta z listy rozwijanej.");
            }

            dt = zam.pobierzZamowienia(kt);
            dataZamowienia.ItemsSource = dt.DefaultView;
        }

        private void eksport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "ListaZamowien"; // Domyślna nazwa pliku
            dlg.DefaultExt = ".txt"; // Domyślne rozszerzenie pliku
            dlg.Filter = "Text documents (.xml)|*.xml"; // Filtr plików
            DataTable dt = new DataTable();
            System.IO.StringWriter writer = new System.IO.StringWriter();

            try // sprawdza czy pobrano dane z bazy danych
            {
                dt = ((DataView)dataZamowienia.ItemsSource).ToTable();
                //Ustawienie ID Columny jako primary key
                dt.PrimaryKey = new DataColumn[] { dt.Columns[1] };
                dt.TableName = "Lista zamowień";
                dt.AcceptChanges();

                dt.WriteXml(writer, false);
                // Pokarz okno wyboru plików
                Nullable<bool> result = dlg.ShowDialog();
                
                // Zrób gdy wybrano jakiś plik
                if (result == true)
                {
                    try
                    {
                        FileStream fs = new FileStream(dlg.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        using (StreamWriter swriter = new StreamWriter(fs))
                        {
                            swriter.Write(writer.ToString());
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nie udało się zapisać pliku!");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nie pobrano danych o zamówieniach!");
            } 
        }

        private void pobierzKlienta_Click(object sender, RoutedEventArgs e)
        {
            string sqlString = "SELECT * FROM Klienci";
            BazaDanych bd = new BazaDanych();
            DataTable dt = new DataTable();
            dt = bd.pobierzDane(sqlString);

            dataGrid.Visibility = Visibility.Visible;
            dataGrid.ItemsSource = dt.DefaultView;
            comboBox.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string etykieta = row.ItemArray[1].ToString() + " " + row.ItemArray[2].ToString();
                comboBox.Items.Add(etykieta);
            }
            if (dt != null)
            {
                edytujKlienta.Visibility = Visibility.Visible;
                infCombobox.Visibility = Visibility.Visible;
                comboBox.Visibility = Visibility.Visible;
            }
        }

        private void dodajKlienta_Click(object sender, RoutedEventArgs e)
        {
            DodajOsobe wnd = new DodajOsobe();  //przekazanie do nowego okna
            wnd.Show();
        }

        private void filtrujTowary_Click(object sender, RoutedEventArgs e)
        {
            Towar tow = new Towar();
            Zamowienia zam = new Zamowienia();
            DataTable dt = new DataTable();
            try
            {
                tow.Nazwa = lTowarowDoWys.SelectedValue.ToString();
                tow.pobierzIDTowaru();
            }
            catch (Exception)
            {
                MessageBox.Show("Proszę wybrać klienta z listy rozwijanej.");
            }

            dt = zam.pobierzZamowieniaT(tow);
            dataZamowienia.ItemsSource = dt.DefaultView;
        }

        private void Grid_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void lKlientowDoWys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void lTowarowDoWys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
