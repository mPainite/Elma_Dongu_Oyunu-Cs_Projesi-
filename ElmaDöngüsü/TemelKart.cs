using System.Drawing;

namespace ElmaDöngüsü
{
    public abstract class TemelKart
    {
        public int X { get; set; }
        public int Y { get; set; }

        public TemelKart(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        // Alt sınıflar bunları doldurmak zorunda
        public abstract ElmaEvresi EvreGetir();
        public abstract Image GorselGetir();
    }
}