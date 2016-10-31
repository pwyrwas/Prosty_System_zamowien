using System;
using System.Windows;

namespace Prosty_System_zamówień
{
    /// <summary>
    /// Interaction logic for EdytujTowar.xaml
    /// </summary>
    public partial class EdytujTowar : Window
    {
        public EdytujTowar(Towar _tow)
        {
            InitializeComponent();

            NowaNazwa.Text = _tow.Nazwa;
            NowaCena.Text = _tow.Cena.ToString();
            _eID.Content = _tow.Id.ToString();
            if(_tow.wielkoscPodatku == 5)
            {
                procent5.IsChecked = true; 
            } else if(_tow.wielkoscPodatku == 8)
            {
                procent8.IsChecked = true;
            }else if(_tow.wielkoscPodatku == 23)
            {
                procent23.IsChecked = true;
            }
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            Towar tow = new Towar();
            tow.Id = int.Parse(_eID.Content.ToString());

            tow.Nazwa = NowaNazwa.Text;

            if (procent5.IsChecked == true)
            {
                tow.CenaBrutto = Convert.ToDecimal(NowaCena.Text) + (Convert.ToDecimal(NowaCena.Text) * (decimal)0.05);
                tow.wielkoscPodatku = 5;
            }
            else if (procent8.IsChecked == true)
            {
                tow.CenaBrutto = Convert.ToDecimal(NowaCena.Text) + (Convert.ToDecimal(NowaCena.Text) * (decimal)0.08);
                tow.wielkoscPodatku = 8;
            }
            else if (procent23.IsChecked == true)
            {
                tow.CenaBrutto = Convert.ToDecimal(NowaCena.Text) + (Convert.ToDecimal(NowaCena.Text) * (decimal)0.23);
                tow.wielkoscPodatku = 23;
            }
            try
            {
                tow.Cena = decimal.Parse(NowaCena.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Podano wartość w złym formacie. Prawidłowy format: XX,XX!");
            }

            bool stanAktualizacji = tow.aktualizujTowar();
            if (stanAktualizacji == true)
                MessageBox.Show("Aktualizacja powiodła się!");
            else
                MessageBox.Show("Dane nie zostaną zaktualizowne!");

            this.Close();
        }

        private void UsunTowar_Click(object sender, RoutedEventArgs e)
        {
            Towar tow = new Towar();
            tow.Id = int.Parse(_eID.Content.ToString());
            tow.Nazwa = NowaNazwa.Text;

            bool stanUsuwania = tow.usunTowar();
            if (stanUsuwania == true)
                MessageBox.Show("Usuwanie powiodło się!");
            else
                MessageBox.Show("Dane nie zostaną usunięte!");

            this.Close();
        }
    } 
}
