using System.Windows;

namespace Prosty_System_zamówień
{
    /// <summary>
    /// Interaction logic for DodajOsobe.xaml
    /// </summary>
    public partial class DodajOsobe : Window
    {
        public DodajOsobe()
        {
            InitializeComponent();
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            Klient kt = new Klient();
            kt.Imie = pImie.Text;
            kt.Nazwisko = pNazwisko.Text;

            bool stanDodawania = kt.dodajOsobe();
            if (stanDodawania == true)
                MessageBox.Show("Dodawanie nowego klienta powiodło się!");
            else
                MessageBox.Show("Nowy klient nie został dodany!");      
            this.Close();
        }      
    }
}
