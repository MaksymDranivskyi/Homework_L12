using Atata.Configuration.Json;

namespace PhpTravels.UITests
{
    public class AppConfig : JsonConfig<AppConfig>
    {
        public string AdminEmail { get; set; }

        public string AdminPassword { get; set; }
    }
}
