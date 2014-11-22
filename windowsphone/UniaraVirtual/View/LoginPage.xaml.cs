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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using UniaraVirtual.Code;

namespace UniaraVirtual.View
{
    public partial class LoginPage : PhoneApplicationPage
    {
        string matricula = null;
        string senha = null;
        IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
        private const string url = "https://uniaravirtualservice-rest.azurewebsites.net/rest/aluno/perfil";

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if(storage.Contains("Matricula") && storage.Contains("Senha"))
            {
                lblMatricula.Visibility = System.Windows.Visibility.Collapsed;
                lblSenha.Visibility = System.Windows.Visibility.Collapsed;

                txtMatricula.Text = (string)storage["Matricula"];
                txtSenha.Password = (string)storage["Senha"];
            }

            if(storage.Contains("AutoLogin"))
            {
                bool autoLogin = (bool)storage["AutoLogin"];
                if(autoLogin)
                {
                    Login();
                }
            }

            while (NavigationService.BackStack.Count() > 0)
            {
                NavigationService.RemoveBackEntry();
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (IsConnected())
            {
                Login();
            }
            else
            {
                MessageBox.Show("Não foi identificado uma conexão com a internet. Ative-a e tente novamente", "sem conexão", MessageBoxButton.OK);
            }
        }

        private void Login()
        {
            matricula = txtMatricula.Text;
            senha = txtSenha.Password;

            if (string.IsNullOrEmpty(matricula))
            {
                MessageBox.Show("O campo matrícula deve ser preenchido", "atenção", MessageBoxButton.OK);
                txtMatricula.Focus();
                return;
            }

            if (string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("O campo Senha deve ser preenchido", "atenção", MessageBoxButton.OK);
                txtSenha.Focus();
                return;
            }

            // mostra a loading bar
            loading.IsIndeterminate = true;
            loading.Visibility = System.Windows.Visibility.Visible;

            // desabilita controlers
            lblInfo.Opacity = 0.5;
            lblTitulo.Opacity = 0.5;
            txtMatricula.IsEnabled = false;
            txtSenha.IsEnabled = false;
            btnLogin.IsEnabled = false;

            RestClient client = new RestClient();
            RestRequest request = new RestRequest(url, Method.GET);

            StringBuilder credentialEncoded = new StringBuilder();
            credentialEncoded.Append("Basic ");
            credentialEncoded.Append(EncodeCredentials(matricula, senha));

            request.AddHeader("Authorization", credentialEncoded.ToString());

            client.ExecuteAsync<PerfilResponse>(request, response =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (response.Data.StatusCodeResult == 403)
                    {
                        MessageBox.Show("Verifique se você está usando a mesma credencial que é usado no site da Uniara Virtual", response.Data.MessageResult.ToLower(), MessageBoxButton.OK);

                        // esconde a loading bar
                        loading.IsIndeterminate = false;
                        loading.Visibility = System.Windows.Visibility.Collapsed;

                        // habilita controlers
                        lblInfo.Opacity = 1;
                        lblTitulo.Opacity = 1;
                        txtMatricula.IsEnabled = true;
                        txtSenha.IsEnabled = true;
                        btnLogin.IsEnabled = true;
                    }
                    else if (response.Data.StatusCodeResult == 500)
                    {
                        MessageBox.Show("Verifique sua conexão com a internet e tente novamente. Caso o problema persista contate o suporte do aplicativo indo em: \nSobre -> Suporte", response.Data.MessageResult.ToLower(), MessageBoxButton.OK);

                        // esconde a loading bar
                        loading.IsIndeterminate = false;
                        loading.Visibility = System.Windows.Visibility.Collapsed;

                        // habilita controlers
                        lblInfo.Opacity = 1;
                        lblTitulo.Opacity = 1;
                        txtMatricula.IsEnabled = true;
                        txtSenha.IsEnabled = true;
                        btnLogin.IsEnabled = true;
                    }
                    else
                    {
                        SaveObjectToPhone("Perfil", response.Data.DataResult);
                        SaveObjectToPhone("Matricula", matricula);
                        SaveObjectToPhone("Senha", senha);

                        NavigationService.Navigate(new Uri("/View/MenuPage.xaml", UriKind.RelativeOrAbsolute));
                    }
                }
                else
                {
                    MessageBox.Show("Não foi possivel estabelecer uma conexão válida com o servidor. Verifique sua internet e tente novamente.\nCaso o problema persista contate o administrador do sistema", "erro ao contatar o servidor", MessageBoxButton.OK);

                    // esconde a loading bar
                    loading.IsIndeterminate = false;
                    loading.Visibility = System.Windows.Visibility.Collapsed;

                    // habilita controlers
                    lblInfo.Opacity = 1;
                    lblTitulo.Opacity = 1;
                    txtMatricula.IsEnabled = true;
                    txtSenha.IsEnabled = true;
                    btnLogin.IsEnabled = true;
                }

            });
        }

        private void OnFocusInMatricula(object sender, RoutedEventArgs e)
        {
            lblMatricula.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void onFocusOutMatricula(object sender, RoutedEventArgs e)
        {
            if(sender is TextBox)
            {
                var text = sender as TextBox;
                if(string.IsNullOrEmpty(text.Text))
                {
                    lblMatricula.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void OnFocusInSenha(object sender, RoutedEventArgs e)
        {
            lblSenha.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OnFocusOutSenha(object sender, RoutedEventArgs e)
        {
            if(sender is PasswordBox)
            {
                var pass = sender as PasswordBox;
                if(string.IsNullOrEmpty(pass.Password))
                {
                    lblSenha.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void txtSenha_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Login();
            }
        }
    }
}