using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using UniaraVirtual.Code;
using UniaraVirtual.Code.Model;

namespace UniaraVirtual.View
{
    public partial class AulasPage : PhoneApplicationPage
    {
        private IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
        private const string url = "https://uniaravirtualservice-rest.azurewebsites.net/rest/aluno/aulas";

        public AulasPage()
        {
            InitializeComponent();            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (IsConnected())
            {
                if (storage.Contains("Aulas"))
                {
                    ShowAulas((List<Aula>)storage["Aulas"]);
                }
                else
                {
                    LoadAulas();
                }
            }
            else
            {
                MessageBox.Show("Não foi identificado uma conexão com a internet. Ative-a e tente novamente", "sem conexão", MessageBoxButton.OK);
            }
        }

        private void LoadAulas()
        {
            // mostra loading bar
            loading.IsIndeterminate = true;
            loading.Visibility = System.Windows.Visibility.Visible;

            // desabilita itens
            LayoutRoot.IsHitTestVisible = false;
            TitlePanel.Opacity = 0.5;
            ContentPanel.Opacity = 0.5;

            RestClient client = new RestClient();
            RestRequest request = new RestRequest(url, Method.GET);

            StringBuilder credentialEncoded = new StringBuilder();
            credentialEncoded.Append("Basic ");
            credentialEncoded.Append(EncodeCredentials(storage["Matricula"].ToString(), storage["Senha"].ToString()));

            request.AddHeader("Authorization", credentialEncoded.ToString());

            client.ExecuteAsync<AulaResponse>(request, response =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (response.Data.StatusCodeResult == 403)
                    {
                        MessageBox.Show("Verifique se você está usando a mesma credencial que é usado no site da Uniara Virtual", response.Data.MessageResult.ToLower(), MessageBoxButton.OK);
                    }
                    else if (response.Data.StatusCodeResult == 500)
                    {
                        MessageBox.Show("Verifique sua conexão com a internet e tente novamente. Caso o problema persista contate o suporte do aplicativo indo em: \nSobre -> Suporte", response.Data.MessageResult.ToLower(), MessageBoxButton.OK);
                    }
                    else
                    {
                        ShowAulas(response.Data.DataResult);
                        SaveObjectToPhone("Aulas", response.Data.DataResult);
                    }
                }
                else
                {
                    MessageBox.Show("Não foi possivel estabelecer uma conexão válida com o servidor. Verifique sua internet e tente novamente.\nCaso o problema persista contate o administrador do sistema", "erro ao contatar o servidor", MessageBoxButton.OK);
                }

                // esconde loading bar
                loading.IsIndeterminate = false;
                loading.Visibility = System.Windows.Visibility.Collapsed;

                // habilita itens
                LayoutRoot.IsHitTestVisible = true;
                TitlePanel.Opacity = 1;
                ContentPanel.Opacity = 1; 
            });
        }

        private void ShowAulas(List<Aula> aulas)
        {

            if(aulas == null || aulas.Count == 0)
            {
                string msg = "Não foi encontrado aulas semanais para seu curso. Verifique no site da Uniara Virtual se suas aulas estão disponíveis";
                MessageBox.Show(msg, "aulas indisponíveis", MessageBoxButton.OK);
                NavigationService.GoBack();
            }

            List<Aula> segunda = aulas.Where(x => x.DiaSemana.Equals("Segunda")).ToList();
            List<Aula> terca = aulas.Where(x => x.DiaSemana.Equals("Terça")).ToList();
            List<Aula> qusrta = aulas.Where(x => x.DiaSemana.Equals("Quarta")).ToList();
            List<Aula> quinta = aulas.Where(x => x.DiaSemana.Equals("Quinta")).ToList();
            List<Aula> sexta = aulas.Where(x => x.DiaSemana.Equals("Sexta")).ToList();

            if(segunda != null)
            {
                SegundaText.Visibility = System.Windows.Visibility.Visible;
                SegundaAulas.Visibility = System.Windows.Visibility.Visible;
                SegundaAulas.ItemsSource = segunda;
            }
            if (terca != null)
            {
                TercaText.Visibility = System.Windows.Visibility.Visible;
                TercaAulas.Visibility = System.Windows.Visibility.Visible;
                TercaAulas.ItemsSource = terca;
            }
            if (qusrta != null)
            {
                QuartaText.Visibility = System.Windows.Visibility.Visible;
                QuartaAulas.Visibility = System.Windows.Visibility.Visible;
                QuartaAulas.ItemsSource = qusrta;
            }
            if (quinta != null)
            {
                QuintaText.Visibility = System.Windows.Visibility.Visible;
                QuintaAulas.Visibility = System.Windows.Visibility.Visible;
                QuintaAulas.ItemsSource = quinta;
            }
            if (sexta != null)
            {
                SextaText.Visibility = System.Windows.Visibility.Visible;
                SextaAulas.Visibility = System.Windows.Visibility.Visible;
                SextaAulas.ItemsSource = sexta;
            }

        }

        private string EncodeCredentials(string username, string password)
        {
            byte[] array = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password));
            return Convert.ToBase64String(array, 0, array.Length);
        }

        private void SaveObjectToPhone(string key, object value)
        {
            if (storage.Contains(key))
            {
                storage[key] = value;
            }
            else
            {
                storage.Add(key, value);
            }

            storage.Save();
        }

        private bool IsConnected()
        {
            bool hasNetwork = false;

            if (DeviceNetworkInformation.IsNetworkAvailable)
            {
                if (DeviceNetworkInformation.IsCellularDataEnabled && DeviceNetworkInformation.IsWiFiEnabled)
                {
                    hasNetwork = true;
                }
                if (DeviceNetworkInformation.IsCellularDataEnabled && !DeviceNetworkInformation.IsWiFiEnabled)
                {
                    hasNetwork = true;
                }
                if (!DeviceNetworkInformation.IsCellularDataEnabled && DeviceNetworkInformation.IsWiFiEnabled)
                {
                    hasNetwork = true;
                }

                // Apenas em teste com Emulador
                if (Microsoft.Devices.Environment.DeviceType == DeviceType.Emulator)
                {
                    hasNetwork = true;
                }
            }

            return hasNetwork;
        }

        private void SegundaAulas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Aula)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina="+disciplina, UriKind.RelativeOrAbsolute));
        }

        private void TercaAulas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Aula)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }

        private void QuartaAulas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Aula)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }

        private void QuintaAulas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Aula)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }

        private void SextaAulas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string disciplina = ((Aula)((ListBox)sender).SelectedItem).Nome;
            NavigationService.Navigate(new Uri("/View/DetailsPage.xaml?disciplina=" + disciplina, UriKind.RelativeOrAbsolute));
        }
    }
}