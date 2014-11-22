using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace UniaraVirtual.View
{
    public partial class BimestresPage : PhoneApplicationPage
    {
        public BimestresPage()
        {
            InitializeComponent();
        }

        private void NavigateTo(string nome, int value)
        {
            NavigationService.Navigate(new Uri("/View/NotasPage.xaml?title="+nome+"&value="+value, UriKind.RelativeOrAbsolute));
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo("primeiro bimestre", 1);
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo("segundo bimestre", 2);
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo("terceiro bimestre", 3);
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo("quarto bimestre", 4);
        }

        private void sub_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo("sub", 5);
        }

        private void exame_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo("exame", 6);
        }

    }
}