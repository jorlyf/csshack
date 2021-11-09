using csshack.UtilsNS;

namespace csshack.StructsNS
{
    internal struct Fov
    {
        public Fov(double x, double y, float deviation)
        {
            X = Trigonometry.RationalizeAngle(x);
            Y = Trigonometry.RationalizeAngle(y);
            Deviation = (float)Trigonometry.RationalizeAngle(deviation);
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double XLeft { get => X + Deviation; }
        public double XRight { get => X - Deviation; }
        public float Deviation { get; set; }
    }
}
