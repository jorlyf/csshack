using csshack.StructsNS;

namespace csshack
{
    internal static class Settings
    {
        public static Enums.Switch ESPBox = Enums.Switch.On;

        public static void SetDefault()
        {
            ESPBox = Enums.Switch.Off;
        }

    }
}
