using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Windows.Controls;
using System.Windows.Navigation;
using UniaraVirtual.Code;
using UniaraVirtual.Code.Model;

namespace UniaraVirtual.View
{
    public partial class NotasPage : PhoneApplicationPage
    {
        private List<Disciplina> disciplinas;
        private IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        public NotasPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string title = NavigationContext.QueryString["title"];
            string value = NavigationContext.QueryString["value"];

            textBlock.Text = title;

            disciplinas = (List<Disciplina>)storage["Disciplinas"];
            
            if(value == "1")
            {
                PrimeiroBimestre.ItemsSource = disciplinas;
                PrimeiroBimestre.Visibility = System.Windows.Visibility.Visible;
            }
            else if (value == "2")
            {
                SegundoBimestre.ItemsSource = disciplinas;
                SegundoBimestre.Visibility = System.Windows.Visibility.Visible;
            }
            else if (value == "3")
            {
                TerceiroBimestre.ItemsSource = disciplinas;
                TerceiroBimestre.Visibility = System.Windows.Visibility.Visible;
            }
            else if (value == "4")
            {
                QuartoBimestre.ItemsSource = disciplinas;
                QuartoBimestre.Visibility = System.Windows.Visibility.Visible;
            }
            else if (value == "Sub")
            {
                Sub.ItemsSource = disciplinas;
                Sub.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Exame.ItemsSource = disciplinas;
                Exame.Visibility = System.Windows.Visibility.Visible;
            }

        }

        private void PrimeiroBimestre_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Disciplina)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }

        private void SegundoBimestre_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Disciplina)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }

        private void TerceiroBimestre_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Disciplina)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }

        private void QuartoBimestre_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Disciplina)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }

        private void Sub_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Disciplina)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }

        private void Exame_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Disciplina)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }
    }
}