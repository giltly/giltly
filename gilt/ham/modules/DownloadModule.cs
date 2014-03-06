using Nancy;

namespace gilt.ham.modules
{
    public sealed class DownloadModule : NancyModule
    {
        public DownloadModule()
            : base()
        {
            Get[HamRoutes.DOWNLOAD] = parameters =>
            {
                return View["Download/Download"];
            };
        }
    }
}
