using Nancy;

namespace gilt.ham.modules
{
    public sealed class FeaturesModule : NancyModule
    {
        public FeaturesModule()
            : base()
        {
            Get[HamRoutes.FEATURES] = parameters =>
            {
                return View["FeatureInfo"];
            };
        }
    }
}
