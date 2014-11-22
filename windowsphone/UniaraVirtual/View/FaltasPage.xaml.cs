using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using UniaraVirtual.Assets.Converters;
using UniaraVirtual.Code;
using UniaraVirtual.Code.Model;

namespace UniaraVirtual.View
{
    public partial class FaltasPage : PhoneApplicationPage
    {
        private List<Disciplina> disciplinas;
        IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        public FaltasPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            disciplinas = (List<Disciplina>)storage["Disciplinas"];

            string atualizado = disciplinas != null && disciplinas.Count > 0 ? disciplinas.First().Frequencia.Atualizacao : "Ocorreu um erro ao carregar a frequência";

            RelativeDateTimeConverter datetimeConverter = new RelativeDateTimeConverter();
            atualizado = "atualizado " + datetimeConverter.Convert(atualizado, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture).ToString();
            
            textBlock9.Text = atualizado;

            ListaFaltas.ItemsSource = disciplinas;
        }

        private void ListaFaltas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Disciplina)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }
    }
}