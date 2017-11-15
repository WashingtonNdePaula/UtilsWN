using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsWN.Util;

namespace UtilsWN.Cobranca.Ficha
{
    public class NossaCaixa : Compensacao
    {
        private int modalidade;

        /// <summary>Ficha de Compensação Padrão - Nossa Caixa Nosso Banco</summary>
        /// <param name="agencia">Número da Agência sem Dígito Verificador</param>
        /// <param name="modalidade">Modalidade da Conta</param>
        /// <param name="conta">Número da Conta sem Dígito Verificador</param>
        /// <param name="dvConta">Dígito Verificador da Conta</param>
        /// <param name="nossoNumero">Nosso Número sem formatação</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="vencimento">Data de Vencimento</param>
        public NossaCaixa(int agencia, int modalidade, int conta, string dvConta, long nossoNumero, DateTime? dataVencimento, decimal valorDocumento)
            : base(nossoNumero, dataVencimento, valorDocumento, agencia, "0", conta, dvConta, 151, "1")
        {
            try
            {
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (agencia == 0)
                    throw new Exception("É necessário informar a Agência para fazer a cobrança");
                if (modalidade == 0)
                    throw new Exception("É necessário informar a Modalidade para fazer a cobrança");
                if (conta == 0)
                    throw new Exception("É necessário informar a Conta para fazer a cobrança");
                if (agencia.ToString().Length > 4)
                    throw new Exception("A Agência só pode ter no máximo 4 caracteres");
                if (modalidade.ToString().Length > 2)
                    throw new Exception("A Modalidade só pode ter no máximo 2 caracteres");
                if (conta.ToString().Length > 6)
                    throw new Exception("A Conta só pode ter no máximo 6 caracteres");

                this.modalidade = modalidade;
                if (modalidade == 4)
                {
                    Carteira = "CIDENT";
                    if (nossoNumero.ToString().Length > 7)
                        throw new Exception("O Nosso Número só pode ter no máximo 7 caracteres");
                    else
                        Carteira = "CDIR";
                    if (nossoNumero.ToString().Length > 8)
                        throw new Exception("O Nosso Número só pode ter no máximo 8 caracteres");
                }
                LocalPagamento = "PAGUE PREFERENCIALMENTO NO BANCO NOSSA CAIXA S.A.";
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
            AgenciaCodigoBeneficiario = String.Format("{0:d4}", Agencia) + " " + String.Format("{0:d2}", modalidade) + " " + String.Format("{0:d7}", Conta) + " " + DVConta;
        }

        /// <summary>Formatação do Campo "Nosso Número" na ficha de compensação</summary>
        protected override void formatarNossoNumero()
        {
            string dignosso, nosso;
            if (modalidade == 4)
                nosso = "99" + String.Format("{0:d7}", NumeroIdentificacao);
            else
                nosso = "9" + String.Format("{0:d8}", NumeroIdentificacao);
            dignosso = Funcoes.NNNossaCaixa(String.Format("{0:d4}", Agencia) + String.Format("{0:d2}", modalidade) + String.Format("{0:d7}", Conta) + DVConta + nosso).ToString();
            NossoNumero = nosso + " " + dignosso;
        }

        protected override void formatarCampoLivre()
        {
            bool resp = true;
            string constante1;
            int dvBarra1, dvBarra2;

            constante1 = "9" + NossoNumero.Substring(1, 8) + String.Format("{0:d4}", Agencia) + Funcoes.Right(modalidade.ToString(), 1) + String.Format("{0:d6}", Conta) + "151";
            dvBarra1 = Funcoes.Mod10(constante1);
            dvBarra2 = Funcoes.Mod11(constante1 + dvBarra1, 2, 7);
            while (resp)
            {
                if (dvBarra2 < 10)
                    break;
                if (dvBarra2 == 11)
                {
                    dvBarra2 = 0;
                    break;
                }
                else if (dvBarra2 == 10)
                {
                    if (dvBarra1 == 9)
                    {
                        dvBarra1 = 0;
                        dvBarra2 = Funcoes.Mod11(constante1 + dvBarra1.ToString().Trim(), 2, 7);
                    }
                    else
                    {
                        dvBarra1 = dvBarra1 + 1;
                        dvBarra2 = Funcoes.Mod11(constante1 + dvBarra1.ToString().Trim(), 2, 7);
                    }
                }
            }
            CampoLivre = constante1 + dvBarra1 + dvBarra2;
        }
    }
}
