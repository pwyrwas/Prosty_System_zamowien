using System.Windows;

namespace Prosty_System_zamówień
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        Klient kt;
        public Window2(Klient _kt)
        {
            InitializeComponent();
            NoweImie.Text = _kt.Imie;
            NoweNazwisko.Text = _kt.Nazwisko;
            _eID.Content = _kt.Id.ToString();
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            Klient kt = new Klient();
            kt.Id = int.Parse(_eID.Content.ToString());
            kt.Imie = NoweImie.Text;
            kt.Nazwisko = NoweNazwisko.Text;

            bool stanAktualizacji = kt.aktualizujKlienta();
            if (stanAktualizacji == true)
                MessageBox.Show("Aktualizacja powiodła się!");
            else
                MessageBox.Show("Dane nie zostaną zaktualizowne!");
            this.Close();
        }
        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void UsunKlienta_Click(object sender, RoutedEventArgs e)
        {
            Klient kt = new Klient();
            kt.Id = int.Parse(_eID.Content.ToString());
            kt.Imie = NoweImie.Text;
            kt.Nazwisko = NoweNazwisko.Text;

            bool stanUsuwania = kt.usunKlienta();
            if (stanUsuwania == true)
                MessageBox.Show("Usuwanie powiodło się!");
            else
                MessageBox.Show("Dane nie zostaną usunięte!");

            this.Close();
        }
    }
}
