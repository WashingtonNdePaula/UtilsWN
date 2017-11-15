using System.Collections.Generic;

namespace UtilsWN.Padrao
{
    public class Despesa
    {
        public string Data { get; set; }
        public string Loja { get; set; }
        public string Portador { get; set; }
        public string NumeroOperacao { get; set; }
        public string Caixa { get; set; }
        public string Cidade { get; set; }
        public string Descricao { get; set; }
        public string Credito { get; set; }
        public string Debito { get; set; }
        public string Valor { get; set; }
        public string Sinal { get; set; }
        public string Parcela { get; set; }
        public string Categoria { get; set; }
        public List<Campo> Personalizado { get; set; }
        public Despesa()
        {
            Personalizado = new List<Campo>();
        }
    }
}
