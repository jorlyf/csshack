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
        private LocalPlayer LocalPlayer { get; set; }
        private List<Player> Players { get; set; }
        private ESP ESP { get; set; }

        public Hack(AppForm app)
        {
            Overlay = app;
        }
        public async void Run()
        {
            await Task.Run(() =>
            {
                GetPlayers();
                ESP = new ESP(LocalPlayer, Players);
                while (true)
                {
                    GetPlayers();
                    Overlay.Clear();
                    ESP.GetESPBoxes().ForEach(box =>
                    {
                        Overlay.DrawBox(box, ESP.EnemyColor);
                    });
                    Thread.Sleep(15);
                }
            });
        }
        private void GetPlayers()
        {
            LocalPlayer = new LocalPlayer(Memory.Read<uint>(Offsets.EntityList));

            Players = new List<Player>();
            for (uint i = 1; i < 32; i++)
            {
                uint playerPtr = Memory.Read<uint>(Offsets.EntityList + (i * Offsets.CSS_Player));
                if (playerPtr == 0) continue;
                Players.Add(new Player(playerPtr));
            }
        }
    }
}
