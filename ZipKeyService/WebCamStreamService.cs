using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.UI.Xaml.Controls;

namespace ZipKeyService
{
    public class WebCamStreamService : IWebCamStreamService
    {
        private static MediaCapture mediaCapture;
        private static bool IsClientConnect;

        static WebCamStreamService()
        {
            InitializeAsync();
        }

        private static async Task InitializeAsync()
        {
            if (mediaCapture == null)
                mediaCapture = new MediaCapture();
            IsClientConnect = false;

            MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings
            {
                StreamingCaptureMode = StreamingCaptureMode.Video
            };

            await mediaCapture.InitializeAsync(settings);
            mediaCapture.Failed += MediaCapture_Failed;
        }

        private static void MediaCapture_Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> StartWebCamStream(CaptureElement camHostElement)
        {
            if (IsClientConnect)
                return false;

            camHostElement.Source = mediaCapture;
            await mediaCapture.StartPreviewAsync();
            IsClientConnect = true;

            return true;
        }

        public async Task<bool> StopWebCamStream()
        {
            await CleanupCameraAsync();

            return true;
        }

        private void MediaCapture_CameraStreamFailed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
        }

        private async Task CleanupCameraAsync()
        {
            IsClientConnect = false;

            if (mediaCapture != null)
            {
                await mediaCapture.StopPreviewAsync();
            }
           // mediaCapture = null;
        }
    }
}
