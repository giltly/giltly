using Nancy;

namespace gilt.ham.modules
{
    public sealed class IndexModule : NancyModule
    {
        public IndexModule()
            : base()
        {
            Get[HamRoutes.INDEX] = parameters =>
            {
                return View["Index"];
            };
            Get[@"/images/{filename}"] = parameters =>
            {
               return Response.AsImage(string.Format("images/{0}", (string)parameters.filename));
            };
            Get[@"/images/features/{filename}"] = parameters =>
            {
                return Response.AsImage(string.Format("images/features/{0}", (string)parameters.filename));
            };
        }
    }
}
