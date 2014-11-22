using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace UniaraVirtual.View.Static
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        IsolatedStorageSettings iso = IsolatedStorageSettings.ApplicationSettings;

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (iso.Contains("AutoLogin"))
            {
                chkSalvarCredencial.IsChecked = (bool)iso["AutoLogin"];
            }
            else
            {
                chkSalvarCredencial.IsChecked = false;
            }

        }

        private void chkSalvarCredencial_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = ((CheckBox)(sender)).IsChecked.Value;
            if (isChecked)
            {
                SaveObjectToPhone("AutoLogin", true);
            }
            else
            {
                SaveObjectToPhone("AutoLogin", false);
            }
        }

        private void SaveObjectToPhone(string key, object value)
        {
            if (iso.Contains(key))
            {
                iso[key] = value;
            }
            else
            {
                iso.Add(key, value);
            }

            iso.Save();
        }
    }
}