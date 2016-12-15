using Newtonsoft.Json;

namespace Mijika
{
    internal static class Tools
    {
        /// <summary>
        /// Set Json default settings.
        /// </summary>
        public static void SetJsonDefaultSettings()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                                                {
                                                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                                                };
        }
    }
}
