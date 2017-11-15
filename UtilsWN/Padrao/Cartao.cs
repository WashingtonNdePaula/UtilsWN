using System.Collections.Generic;

namespace UtilsWN.Padrao
{
    public class Cartao
    {
        public string Numero { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public List<Despesa> Despesa { get; set; }
        public List<Despesa> LancamentoFuturo { get; set; }
        public string valor { get; set; }
        public Cartao()
        {
            Despesa = new List<Despesa>();
            LancamentoFuturo = new List<Despesa>();
        }
    }
}
