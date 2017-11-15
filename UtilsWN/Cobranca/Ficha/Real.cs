using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsWN.Util;

namespace UtilsWN.Cobranca.Ficha
{
    public class Real : Compensacao
    {
        public enum NumeroBanco
        {
            REAL_275 = 2755,
            REAL_356 = 3565
        }

        /// <summary>Ficha de Compensação Padrão - Banco Real ABN AMRO</summary>
        /// <param name="agencia">Número da Agência sem Dígito Verificador</param>
        /// <param name="conta">Número da Conta sem Dígito Verificador</param>
        /// <param name="nossoNumero">Nosso Número sem formatação</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="dataVencimento">Data de Vencimento</param>
        public Real(NumeroBanco numeroBanco, int agencia, int conta, long nossoNumero, decimal valorDocumento, DateTime dataVencimento)
            : base(nossoNumero, dataVencimento, valorDocumento, agencia, "0", conta, "0", int.Parse(String.Format("{0:d4}", int.Parse(numeroBanco.GetHashCode().ToString().Substring(0, 3)))), String.Format("{0:d4}", int.Parse(numeroBanco.GetHashCode().ToString().Substring(3, 1))))
        {
            try
            {
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (agencia == 0)
                    throw new Exception("É necessário informar a Agência para fazer a cobrança");
                if (conta == 0)
                    throw new Exception("É necessário informar a Conta para fazer a cobrança");
                if (agencia.ToString().Length > 4)
                    throw new Exception("A Agência só pode ter no máximo 4 caracteres");
                if (nossoNumero.ToString().Length > 13)
                    throw new Exception("A Modalidade só pode ter no máximo 13 caracteres");
                if (conta.ToString().Length > 7)
                    throw new Exception("A Conta só pode ter no máximo 7 caracteres");

                Carteira = "20";
                LocalPagamento = "PAGÁVEL EM QUALQUER AGÊNCIA BANCÁRIA ATÉ O VENCIMENTO";
                Aceite = "N";
                EspecieDocumento = "RC";
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
            int dv = Funcoes.Mod10(NumeroIdentificacao.ToString() + String.Format("{0:d4}", Agencia) + String.Format("{0:d7}", Conta));
            AgenciaCodigoBeneficiario = Agencia + " / " + Conta + "-" + dv;
        }

        /// <summary>Formatação do Campo "Nosso Número" na ficha de compensação</summary>
        protected override void formatarNossoNumero()
        {
            NossoNumero = String.Format("{0:d13}", NumeroIdentificacao);
        }

        protected override void formatarCampoLivre()
        {
            CampoLivre = String.Format("{0:d4}", Agencia) + String.Format("{0:d7}", Conta) + Funcoes.Right(AgenciaCodigoBeneficiario, 1) + String.Format("{0:d13}", NumeroIdentificacao);
        }
    }
}
