using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using RestSharp;
using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using UniaraVirtual.Code;
using UniaraVirtual.Code.Model;

namespace UniaraVirtual.View
{
    public partial class MenuPage : PhoneApplicationPage
    {
        private string nome;
        private IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
        private const string url = "https://uniaravirtualservice-rest.azurewebsites.net/rest/aluno/notas";
        private bool isLoaded = false;

        public MenuPage()
        {
            InitializeComponent();
            nome = ((Perfil)storage["Perfil"]).Nome;
            textBlock.Text = nome.ToLower();        
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            while (NavigationService.BackStack.Count() > 0)
            {
                NavigationService.RemoveBackEntry();
            }

            if (IsConnected())
            {
                if(!isLoaded)
                {
                    LoadNotas();
                }
                isLoaded = true;
            }
            else
            {
                MessageBox.Show("Não foi identificado uma conexão com a internet. Ative-a e tente novamente", "sem conexão", MessageBoxButton.OK);
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


        private void LoadNotas()
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

            client.ExecuteAsync<DisciplinaResponse>(request, response =>
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
                            SaveObjectToPhone("Disciplinas", response.Data.DataResult);
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

        private void Logout()
        {
            storage.Clear();
            NavigationService.Navigate(new Uri("/View/LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }
    }
}