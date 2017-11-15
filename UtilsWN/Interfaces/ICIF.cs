using UtilsWN.Padrao;
using UtilsWN.FAC;

namespace UtilsWN.Interfaces
{
    public interface ICIF
    {
        CIF CIF { get; set; }
        Pessoa Destinatario { get; set; }
        Pessoa Remetente { get; set; }
    }
}
