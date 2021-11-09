using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using csshack.FeaturesNS;
using csshack.MemoryNS;
using csshack.StructsNS;

namespace csshack
{
    internal class Hack
    {
        private AppForm Overlay { get; set; }
        private LocalPlayer LocalPlayer { get => new LocalPlayer(Memory.Read<uint>(Offsets.EntityList)); }
        private List<Player> Players
        {
            get
            {
                List<Player> players = new List<Player>();
                for (uint i = 1; i < 32; i++)
                {
                    uint playerPtr = Memory.Read<uint>(Offsets.EntityList + (i * Offsets.CSS_Player));
                    if (playerPtr == 0) continue;
                    players.Add(new Player(playerPtr));
                }
                return players;
            }
        }
        private ESP ESP { get; set; }

        public Hack(AppForm app)
        {
            Overlay = app;
            ESP = new ESP(LocalPlayer, Players);
        }
        public async void Run()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Overlay.Clear();
                    ESP.ESPBoxes.ForEach(box =>
                    {
                        Overlay.DrawBox(box, ESP.EnemyColor);
                    });
                    Thread.Sleep(15);
                }
            });
        }
    }
}
