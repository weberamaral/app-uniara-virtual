using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace UniaraVirtual.View.Static
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MarketplaceReviewTask task = new Microsoft.Phone.Tasks.MarketplaceReviewTask();
            task.Show();
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            EmailComposeTask task = new EmailComposeTask();
            task.Subject = "Uniara Virtual Bugs!";
            task.Body = "Encontrei um erro no Uniara Virtual...";
            task.To = "weberamaral@outlook.com";
            task.Show();
        }

        private void HyperlinkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MarketplaceSearchTask task = new MarketplaceSearchTask();
            task.SearchTerms = "weber amaral";
            task.ContentType = MarketplaceContentType.Applications;
            task.Show();
        }

        private void HyperlinkButton_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new System.Uri("https://github.com/weber-amaral/uniara-virtual-service-rest");
            task.Show();
        }

        private void HyperlinkButton_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new System.Uri("https://github.com/weber-amaral/uniara-virtual-windowsphone");
            task.Show();
        }
    }
}