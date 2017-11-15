
using System;
using UtilsWN.Util;

namespace UtilsWN.Cobranca.Ficha
{
    public sealed class BancoDoBrasil : Compensacao
    {
        public enum TipoDeCobranca
        {
            SEM_REGISTRO = 1,
            REGISTRADA = 2
        }
        private TipoDeCobranca tipoCobranca;
        public int Convenio { get; set; }

        /// <summary>Ficha de Compensação Padrão - Banco do Brasil</summary>
        /// <param name="tipoCobranca">Tipo de Cobrança - Carteira Sem Registro ou Carteira Registrada</param>
        /// <param name="convenio">Número do Convênio</param>
        /// <param name="agencia">Número da Agência sem Dígito Verificador</param>
        /// <param name="dvAgencia">Dígito Verificador da Agência</param>
        /// <param name="conta">Número da Conta sem Dígito Verificador</param>
        /// <param name="dvConta">Dígito Verificador da Conta</param>
        /// <param name="carteira">Número da Carteira</param>
        /// <param name="variacaoCarteira">Variação da Carteira</param>
        /// <param name="nossoNumero">Nosso Número</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="dataVencimento">Data de Vencimento</param>
        /// <remarks></remarks>
        public BancoDoBrasil(TipoDeCobranca tipoCobranca, int convenio, int agencia, string dvAgencia, int conta, string dvConta, int carteira, int variacaoCarteira, long nossoNumero, DateTime? vencimento, decimal valorDocumento)
            : base(nossoNumero, vencimento, valorDocumento, agencia, dvAgencia, conta, dvConta, 1, "9")
        {
            try
            {
                if (convenio == 0)
                    throw new Exception("É necessário o informar número do Convênio para fazer a cobrança");
                if (agencia == 0)
                    throw new Exception("É necessário o informar número da Agência para fazer a cobrança");
                if (conta == 0)
                    throw new Exception("É necessário o informar número da Conta para fazer a cobrança");
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (tipoCobranca == TipoDeCobranca.REGISTRADA)
                {
                    if (convenio.ToString().Length > 4)
                        throw new Exception("Para uma cobrança registrada o convênio deve ter no máximo 4 dígitos");
                    if (nossoNumero.ToString().Length > 7)
                        throw new Exception("Para uma cobrança registrada o Nosso Número deve ter no máximo 7 dígitos");
                }
                if (tipoCobranca == TipoDeCobranca.SEM_REGISTRO)
                {
                    if (convenio.ToString().Length > 7)
                        throw new Exception("Para uma cobrança sem registro o convênio deve ter no máximo 7 dígitos");
                    if (convenio.ToString().Length > 6)
                    {
                        if (nossoNumero.ToString().Length > 10)
                            throw new Exception("Para uma cobrança sem registro com convênio de 7 dígitos o Nosso Número deve ter no máximo 10 dígitos");
                    }
                    else
                        if (nossoNumero.ToString().Length > 17)
                        throw new Exception("Para uma cobrança sem registro com convênio de 6 dígitos o Nosso Número deve ter no máximo 17 dígitos");
                }
                Convenio = convenio;
                this.tipoCobranca = tipoCobranca;
                Carteira = String.Format("{0:d2}", carteira) + "-" + String.Format("{0:d3}", variacaoCarteira);
                LocalPagamento = "PAGÁVEL EM QUALQUER AGÊNCIA BANCÁRIA ATÉ O VENCIMENTO";
                Aceite = "N";
                EspecieDocumento = "COB";
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
            if (tipoCobranca == TipoDeCobranca.REGISTRADA)
            {
                string dignosso;
                string nosso;
                nosso = String.Format("{0:d4}", Convenio) + String.Format("{0:d7}", NumeroIdentificacao);
                dignosso = 11 - Funcoes.Mod11(nosso, 9, 2) == 10 ? "X" : (11 - Funcoes.Mod11(nosso, 9, 2)).ToString();
                NossoNumero = nosso + "-" + dignosso;
            }
            else {
                if (Convenio.ToString().Length > 6)
                    NossoNumero = String.Format("{0:d7}", Convenio) + String.Format("{0:d10}", NumeroIdentificacao);
                else
                    NossoNumero = String.Format("{0:d17}", NumeroIdentificacao);
            }
        }

        protected override void formatarAgenciaCodigoBeneficiario()
        {
            AgenciaCodigoBeneficiario = String.Format("{0:d4}", Agencia) + "-" + DVAgencia + " / " + String.Format("{0:d8}", Conta) + "-" + DVConta;
        }

        protected override void formatarCampoLivre()
        {
            string conv;
            string cart;

            if (tipoCobranca == TipoDeCobranca.SEM_REGISTRO)
            {
                if (Convenio.ToString().Length > 6)
                {
                    conv = String.Format("{0:d7}", Convenio);
                    cart = Carteira.Substring(0, 2);
                }
                else
                {
                    conv = String.Format("{0:d6}", Convenio);
                    cart = "21";
                }
                CampoLivre = conv + NossoNumero + cart;
            }
            else
                CampoLivre = NossoNumero.Substring(0, 11) + String.Format("{0:d4}", Agencia) + String.Format("{0:d8}", Conta) + Carteira.Substring(0, 2);
        }
    }
}
