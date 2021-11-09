using System.Numerics;
using csshack.MemoryNS;

namespace csshack.StructsNS
{
    internal class Player
    {
        public uint Ptr;
        public int Health { get => Memory.Read<int>(Ptr + Offsets.Health); }
        public int Team { get => Memory.Read<int>(Ptr + Offsets.Team); } // 1 - Spectator, 2 - T, 3 - CT
        public Vector3 Position
        {
            get => new Vector3
            {
                X = Memory.Read<float>(Ptr + Offsets.Position),
                Y = Memory.Read<float>(Ptr + Offsets.Position + 4),
                Z = Memory.Read<float>(Ptr + Offsets.Position + 8)
            };
        }

        public Player(uint ptr)
        {
            Ptr = ptr;
        }
    }

    internal class LocalPlayer : Player
    {
        public Fov Fov
        {
            get => new Fov(Memory.Read<float>(Ptr + Offsets.HorizontalAngle), Memory.Read<float>(Ptr + Offsets.VerticalAngle), 60);
        }
        public LocalPlayer(uint ptr) : base(ptr) { }
    }
}
