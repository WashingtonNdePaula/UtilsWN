using System;
using System.Collections.Generic;
using UtilsWN.Padrao;

namespace UtilsWN.Cobranca
{ 
    public abstract class Cobranca
    {
        public Pessoa Beneficiario { get; set; }
        /// <summary>
        /// Numero de Identificação, Número para baixa de pagamento (Nosso Número sem formatação)
        /// </summary>
        public long NumeroIdentificacao { get; }
        public DateTime DataVencimento { get; }
        public decimal ValorDocumento { get; }
        public decimal Valor { get; set; }
        public decimal Quantidade { get; set; }
        public string EspecieMoeda { get; set; }
        public string LocalPagamento { get; set; }
        public List<String> InstrucaoPagamento { get; set; }
        public string CodigoDeBarras { get; protected set; }
        public string CodigoDeBarras2de5 { get; protected set; }
        public string LinhaDigitavel { get; protected set; }
        public string CampoLivre { get; protected set; }
        protected abstract void montarLinhaDigitavel();
        protected abstract void montarCodigoDeBarras();
        public Cobranca(long numeroIdentificacao, DateTime dataVencimento, decimal valorDocumento)
        {
            NumeroIdentificacao = numeroIdentificacao;
            DataVencimento = dataVencimento;
            ValorDocumento = valorDocumento;
            InstrucaoPagamento = new List<String>();
        }

    }
}
