using System.Drawing;

namespace ElmaDöngüsü
{
    public class ElmaKarti : TemelKart
    {
        private ElmaEvresi _evre;
        public ElmaEvresi Evre
        {
            get { return _evre; }
            set { _evre = value; }
        }

        public ElmaKarti(int x, int y, ElmaEvresi evre) : base(x, y)
        {
            this._evre = evre;
        }

        public override ElmaEvresi EvreGetir()
        {
            return _evre;
        }

        public override Image GorselGetir()
        {
            switch (_evre)
            {
                case ElmaEvresi.Cicek: return Properties.Resources.img_1_cicek;
                case ElmaEvresi.Ham: return Properties.Resources.img_2_ham;
                case ElmaEvresi.Olgun: return Properties.Resources.img_3_olgun;
                case ElmaEvresi.Isirilmis: return Properties.Resources.img_4_isirilmis;
                case ElmaEvresi.Kocan: return Properties.Resources.img_5_kocan;
                case ElmaEvresi.Curuk: return Properties.Resources.img_6_curuk;
                default: return null;
            }
        }
    }
}