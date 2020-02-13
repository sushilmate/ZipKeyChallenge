using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ZipKeyService
{
    public interface IWebCamStreamService
    {
        Task<bool> StartWebCamStream(CaptureElement camHostElement);

        Task<bool> StopWebCamStream();
    }
}