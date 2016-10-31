using System;
using System.Windows;
using System.Windows.Controls;

namespace Prosty_System_zamówień
{
    /// <summary>
    /// Interaction logic for DodajTowar.xaml
    /// </summary>
    public partial class DodajTowar : Window
    {
        public DodajTowar()
        {
            InitializeComponent();
        }
        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            
            Towar tow = new Towar();
            tow.Nazwa = pNazwe.Text;
            if (procent5.IsChecked == false && procent8.IsChecked == false && procent23.IsChecked == false)
            {
                MessageBox.Show("Przed zapisaniem prosze wybrać opodatkowanie produktu.");
            }
            else
            {
                if (procent5.IsChecked == true)
                {
                    tow.CenaBrutto = Convert.ToDecimal(pCene.Text) + (Convert.ToDecimal(pCene.Text) * (decimal)0.05);
                    tow.wielkoscPodatku = 5;
                }
                else if (procent8.IsChecked == true)
                {
                    tow.CenaBrutto = Convert.ToDecimal(pCene.Text) + (Convert.ToDecimal(pCene.Text) * (decimal)0.08);
                    tow.wielkoscPodatku = 8;
                }
                else if (procent23.IsChecked == true)
                {
                    tow.CenaBrutto = Convert.ToDecimal(pCene.Text) + (Convert.ToDecimal(pCene.Text) * (decimal)0.23);
                    tow.wielkoscPodatku = 23;
                }
                try
                {
                    tow.Cena = Convert.ToDecimal(pCene.Text);
                    bool stanDodawania = tow.dodajTowar();
                    if (stanDodawania == true)
                        MessageBox.Show("Dodawanie nowego towaru powiodło się!");
                    else
                        MessageBox.Show("Nowy towar nie został dodany!");
                }
                catch (Exception)
                {
                    MessageBox.Show("Zły format danych. Poprawny format XX,XX!");
                }
                this.Close();
            }
            
                
        }

        private void pCene_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
