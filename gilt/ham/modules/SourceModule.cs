using Nancy;

namespace gilt.ham.modules
{
    public sealed class SourceModule : NancyModule
    {
        public SourceModule()
            : base()
        {
            Get[HamRoutes.SOURCE] = parameters =>
            {
                return View["Source/Source"];
            };
        }
    }
}
