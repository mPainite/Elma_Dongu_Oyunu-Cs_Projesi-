using System.Drawing;

namespace ElmaDöngüsü
{
    public enum ElmaEvresi
    {
        Cicek = 1,      
        Ham = 2,        
        Olgun = 3,      
        Isirilmis = 4,  
        Kocan = 5,      
        Curuk = 6       
    }
    public interface IKart
    {
        Image GorselGetir();
        ElmaEvresi EvreGetir();
    }
}