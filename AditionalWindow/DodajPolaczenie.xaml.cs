using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Prosty_System_zamówień
{
    /// <summary>
    /// Interaction logic for DodajPolaczenie.xaml
    /// </summary>
    public partial class DodajPolaczenie : Window
    {
        public DodajPolaczenie()
        {
            InitializeComponent();
        }

        private void polacz_Click(object sender, RoutedEventArgs e)
        {
            string conString = polaczenie.Text.ToString();
            try
            {
                System.IO.File.WriteAllText(@"..\Debug\Polaczenie.txt", conString);
                BazaDanych bd = new BazaDanych();
                bool stan = bd.wyslijDane("SELECT * FROM Zamowienia","Niepoprawny łańcuch połączenia, Sprawdź jego poprawność,");
                if (stan == true)
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Łańcuch znaków jest niepoprawny!");
            }


        }
    }
}
