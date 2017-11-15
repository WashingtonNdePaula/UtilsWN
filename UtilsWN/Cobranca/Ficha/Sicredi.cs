using System;
using UtilsWN.Util;

namespace UtilsWN.Cobranca.Ficha
{
    public sealed class Sicredi : Compensacao
    {
        public enum TipoCobranca
        {
            COM_REGISTRO = 1,
            SEM_REGISTRO = 3
        }

        public enum TipoCarteira
        {
            SIMPLES = 1,
            CAUCIONADA = 2,
            DESCONTADA = 3
        }

        private TipoCobranca tipoCobranca;
        private TipoCarteira tipoCarteira;
        public int Posto { get; set; }

        /// <summary>Ficha de Compensação Padrão - Sicredi</summary>
        /// <param name="tipoCobranca">Tipo de Cobrança - Com Registro ou Sem Registro</param>
        /// <param name="tipoCarteira">Tipo de Carteira - Simples, Caucionada ou Descontada</param>
        /// <param name="agencia">Número da Agência</param>
        /// <param name="posto">Posto Beneficiário</param>
        /// <param name="conta">Código do Beneficiário</param>
        /// <param name="nossoNumero">Nosso Número</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="dataVencimento">Data de Vencimento</param>
        /// <remarks></remarks>
        public Sicredi(TipoCobranca tipoCobranca, TipoCarteira tipoCarteira, int agencia, int posto, int conta, long nossoNumero, DateTime vencimento, decimal valorDocumento)
            : base(nossoNumero, vencimento, valorDocumento, agencia, "0", conta, "0", 748, "X")
        {
            try
            {
                if (agencia == 0)
                    throw new Exception("É necessário o informar número da Agência para fazer a cobrança");
                if (posto == 0)
                    throw new Exception("É necessário o informar número do Posto Beneficiário para fazer a cobrança");
                if (conta == 0)
                    throw new Exception("É necessário o informar número do Código Beneficiário para fazer a cobrança");
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (agencia.ToString().Length > 4)
                    throw new Exception("O Número da Agência deve ter no máximo 4 dígitos");
                if (posto.ToString().Length > 2)
                    throw new Exception("O Posto Beneficiário deve ter no máximo 2 dígitos");
                if (posto.ToString().Length > 5)
                    throw new Exception("O Posto Beneficiário deve ter no máximo 5 dígitos");
                if (nossoNumero.ToString().Length > 9)
                    throw new Exception("O Nosso Número deve ter no máximo 8 dígitos");

                this.tipoCobranca = tipoCobranca;
                this.tipoCarteira = tipoCarteira;
                Carteira = tipoCarteira.GetHashCode().ToString();
                LocalPagamento = "PAGÁVEL PREFERENCIALMENTE NAS COOPERATIVAS DE CRÉDITO DO SICREDI";
                Aceite = "N";
                EspecieDocumento = "DM";
                Posto = posto;
                formatarAgenciaCodigoBeneficiario();
                formatarNossoNumero();
                formatarCampoLivre();
                montarCodigoDeBarras();
                montarLinhaDigitavel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override void formatarNossoNumero()
        {
                string nosso = String.Format("{0:d4}", Agencia) + String.Format("{0:d2}", Posto) + String.Format("{0:d5}", Conta) + String.Format("{0:d8}", NumeroIdentificacao);
                string dignosso = Funcoes.Mod11(nosso, 2, 9) > 9 ? "0" : (Funcoes.Mod11(nosso, 2, 9)).ToString();
                NossoNumero = String.Format("{0:d8}", NumeroIdentificacao).Insert(2,"/") + "-" + dignosso;
        }

        protected override void formatarAgenciaCodigoBeneficiario()
        {
            AgenciaCodigoBeneficiario = String.Format("{0:d4}", Agencia) + "." + String.Format("{0:d2}", Posto) + "." + String.Format("{0:d5}", Conta);
        }

        protected override void formatarCampoLivre()
        {
            int dvCampoLivre;

            CampoLivre = tipoCobranca.GetHashCode().ToString() +
                tipoCarteira.GetHashCode().ToString() +
                String.Format("{0:d4}", Agencia) +
                String.Format("{0:d2}", Posto) +
                String.Format("{0:d5}", Conta) +
                NossoNumero.Replace("/", "").Replace(".", "").Replace("-", "");
            CampoLivre +=  ValorDocumento > 0 ? "10" : "00";
            dvCampoLivre = Funcoes.Mod11(CampoLivre, 2, 9);
            CampoLivre += (dvCampoLivre == 0 || dvCampoLivre > 9) ? "1" : dvCampoLivre.ToString();
        }
    }
}
