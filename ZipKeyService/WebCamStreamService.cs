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
            if (mediaCapture == null)
                mediaCapture = new MediaCapture();
            IsClientConnect = false;
        }
        
        public async Task<bool> StartWebCamStream(CaptureElement camHostElement)
        {
            if (IsClientConnect)
                return false;

            MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings
            {
                StreamingCaptureMode = StreamingCaptureMode.Video
            };
           
            await mediaCapture.InitializeAsync(settings);
            mediaCapture.Failed += this.MediaCapture_CameraStreamFailed;

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
            if (mediaCapture != null)
            {
                await mediaCapture.StopPreviewAsync();
            }
            mediaCapture = null;
        }
    }
}
