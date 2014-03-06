using Nancy;

namespace gilt.ham.modules
{
    public sealed class DemoModule : NancyModule
    {
        public DemoModule()
            : base()
        {
            Get[HamRoutes.DEMO] = parameters =>
            {
                return View["DemoInfo"];
            };
        }
    }
}
