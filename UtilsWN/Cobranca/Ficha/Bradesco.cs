using System;
using UtilsWN.Util;

namespace UtilsWN.Cobranca.Ficha
{
    public class Bradesco : Compensacao
    {
        /// <summary>Ficha de Compensação Padrão - Bradesco</summary>
        /// <param name="carteira">Número da Carteira de Cobrança</param>
        /// <param name="agencia">Número da Agência sem Dígito Verificador</param>
        /// <param name="dvAgencia" >Dígito Verificador da Agência</param>
        /// <param name="conta">Número da Conta sem Dígito Verificador</param>
        /// <param name="dvConta">Dígito Verificador da Conta</param>
        /// <param name="nossoNumero">Nosso Número sem formatação</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="dataVencimento">Data de Vencimento</param>
        public Bradesco(int carteira, int agencia, string dvAgencia, int conta, string dvConta, long nossoNumero, DateTime? dataVencimento, decimal valorDocumento)
            : base(nossoNumero, dataVencimento, valorDocumento, agencia, dvAgencia, conta, dvConta, 237, "2")
        {
            try
            {
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (carteira == 0)
                    throw new Exception("É necessário informar a Carteira para fazer a cobrança");
                if (agencia == 0)
                    throw new Exception("É necessário informar a Agência para fazer a cobrança");
                if (conta == 0)
                    throw new Exception("É necessário informar a Conta para fazer a cobrança");
                if (carteira.ToString().Length > 2)
                    throw new Exception("A Carteira só pode ter no máximo 2 caracteres");
                if (agencia.ToString().Length > 4)
                    throw new Exception("A Agência só pode ter no máximo 4 caracteres");
                if (conta.ToString().Length > 7)
                    throw new Exception("A Conta só pode ter no máximo 7 caracteres");
                if (nossoNumero.ToString().Length > 11)
                    throw new Exception("O Nosso Número só pode ter no máximo 11 caracteres");

                Carteira = String.Format("{0:d2}", carteira);
                LocalPagamento = "PAGÁVEL EM QUALQUER AGÊNCIA BANCÁRIA ATÉ O VENCIMENTO";
                Aceite = "N";
                EspecieDocumento = "DM";
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

        /// <summary>Formatação do Campo "Agência/Código do Cedente" na ficha de compensação</summary>
        protected override void formatarAgenciaCodigoBeneficiario()
        {
            AgenciaCodigoBeneficiario = String.Format("{0:d4}", Agencia) + "-" + DVAgencia + " / " + String.Format("{0:d7}", Conta) + "-" + DVConta;
        }

        /// <summary>Formatação do Campo "Nosso Número" na ficha de compensação</summary>
        protected override void formatarNossoNumero()
        {
            string dignosso, nosso;
            nosso = Carteira + String.Format("{0:d11}", NumeroIdentificacao);
            dignosso = Funcoes.Mod11(nosso, 2, 7).ToString();
            if (dignosso == "11")
                dignosso = "0";
            else if (dignosso == "10")
                dignosso = "P";
            NossoNumero = nosso.Insert(2, "/") + "-" + dignosso;
        }

        protected override void formatarCampoLivre()
        {
            CampoLivre = String.Format("{0:d4}", Agencia) + Carteira + String.Format("{0:d11}", NumeroIdentificacao) + String.Format("{0:d7}", Conta) + "0";
        }
    }
}
