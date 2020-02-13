using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ZipKeyService;

namespace ZipKeyClient
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            WebCamService = new WebCamStreamService();
        }

        public IWebCamStreamService WebCamService { get; set; }

        private async void StartStream_Click(object sender, RoutedEventArgs e)
        {
            await WebCamService.StartWebCamStream(CamPreview);
        }

        private async void StopStream_Click(object sender, RoutedEventArgs e)
        {
            await WebCamService.StopWebCamStream();
            this.CamPreview.Source = null;
        }
    }
}
