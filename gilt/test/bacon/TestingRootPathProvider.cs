using System.IO;
using Nancy;

namespace gilt.test
{
    /// <summary>
    /// TinyIOC Class that informs nancy where the real Views and Assets are located
    /// </summary>
    public class TestingRootPathProvider : IRootPathProvider
    {
        private static readonly string RootPath;

        static TestingRootPathProvider()
        {
            var directoryName = Path.GetDirectoryName(typeof(TestBootStrapper).Assembly.CodeBase);
            if (directoryName != null)
            {
                var assemblyPath = directoryName.Replace(@"file:\", string.Empty);

                RootPath = Path.Combine(assemblyPath, "..", "..", "..", "..", "..", "gilt", "bacon");
            }
        }

        public string GetRootPath()
        {
            return RootPath;
        }
    }
}