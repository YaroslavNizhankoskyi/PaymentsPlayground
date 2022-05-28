using Newtonsoft.Json;
using System.Reflection;

namespace PaymentsPlayground.Helpers
{
    public static class ResourceReader
    {
        public static string GetFromResources(string resourceName)
        {
            Assembly assem = Assembly.GetExecutingAssembly();

            using (Stream stream = assem.GetManifestResourceStream(assem.GetName().Name + "." + resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T Read<T>(string path)
        {
            string rulesData = ResourceReader.GetFromResources(path);

            return JsonConvert.DeserializeObject<T>(rulesData);
        }
    }
}
