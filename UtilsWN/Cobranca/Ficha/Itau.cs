using System;
using UtilsWN.Util;

namespace UtilsWN.Cobranca.Ficha
{
    public class Itau : Compensacao
    {

        public enum TipoCarteira
        {
            NORMAL = 1,
            ESPECIAL = 2
        }

        private TipoCarteira tipoCarteira;
        private int codigoCliente;

        /// <summary>Ficha de Compensação Padrão - Itaú</summary>
        /// <param name="tipoCobranca">Modalidade da Carteira</param>
        /// <param name="carteira">Número da Carteira</param>
        /// <param name="codigoCliente">Código do Cliente para as carteiras especiais, senão informar 0</param>
        /// <param name="agencia">Número da Agência sem Dígito Verificador</param>
        /// <param name="conta">Número da Conta sem Dígito Verificador</param>
        /// <param name="nossoNumero">Nosso Número sem formatação</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="dataVencimento">Data de Vencimento</param>
        public Itau(TipoCarteira tipoCarteira, int carteira, int codigoCliente, int agencia, int conta, long nossoNumero, decimal valorDocumento, DateTime dataVencimento)
            : base(nossoNumero, dataVencimento, valorDocumento, agencia, "0", conta, "0", 341, "7")
        {
            try
            {
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (agencia == 0)
                    throw new Exception("É necessário informar a Agência para fazer a cobrança");
                if (carteira == 0)
                    throw new Exception("É necessário informar a Carteira para fazer a cobrança");
                if (agencia.ToString().Length > 4)
                    throw new Exception("A Agência só pode ter no máximo 4 caracteres");
                if (tipoCarteira == TipoCarteira.ESPECIAL)
                {
                    if (codigoCliente == 0)
                        throw new Exception("É necessário informar o Código do Cliente para fazer a cobrança");
                    if (codigoCliente.ToString().Length > 5)
                        throw new Exception("O Código do Cliente só pode ter no máximo 5 caracteres");
                    if (nossoNumero.ToString().Length > 15)
                        throw new Exception("O Código do Cliente só pode ter no máximo 15 caracteres");
                }
                else
                {
                    if (conta == 0)
                        throw new Exception("É necessário informar a Conta para fazer a cobrança");
                    if (conta.ToString().Length > 5)
                        throw new Exception("A Conta só pode ter no máximo 5 caracteres");
                    if (nossoNumero.ToString().Length > 8)
                        throw new Exception("O Código do Cliente só pode ter no máximo 8 caracteres");
                }
                this.codigoCliente = codigoCliente;
                this.tipoCarteira = tipoCarteira;
                Carteira = String.Format("{0:d3}", carteira);
                LocalPagamento = "Até o vencimento pagável preferencialmente no Itaú, após pagável em qualquer banco";
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
            int dv;
            if (tipoCarteira == TipoCarteira.NORMAL)
            {
                dv = Funcoes.Mod10(String.Format("{0:d4}", Agencia) + String.Format("{0:d5}", Conta));
                AgenciaCodigoBeneficiario = String.Format("{0:d4}", Agencia) + " / " + String.Format("{0:d5}", Conta) + "-" + dv;
            }
            else
            {
                dv = Funcoes.Mod10(String.Format("{0:d4}", Agencia) + String.Format("{0:d5}", codigoCliente));
                AgenciaCodigoBeneficiario = String.Format("{0:d4}", Agencia) + " / " + String.Format("{0:d5}", codigoCliente) + "-" + dv;
            }
        }

        /// <summary>Formatação do Campo "Nosso Número" na ficha de compensação</summary>
        protected override void formatarNossoNumero()
        {
            if (tipoCarteira == TipoCarteira.NORMAL)
            {
                string nosso, dignosso;
                nosso = String.Format("{0:d4}", Agencia) + String.Format("{0:d5}", Conta) + String.Format("{0:d3}", int.Parse(Carteira)) + String.Format("{0:d8}", NumeroIdentificacao);
                dignosso = Funcoes.Mod10(nosso).ToString();
                NossoNumero = String.Format("{0:d3}", int.Parse(Carteira)) + "/" + String.Format("{0:d8}", NumeroIdentificacao) + "-" + dignosso;
            }
            else
            {
                string dignosso, digseu, nosso, seu;
                nosso = String.Format("{0:d15}", NumeroIdentificacao).Substring(0, 8);
                dignosso = Funcoes.Mod10(String.Format("{0:d4}", Agencia) + String.Format("{0:d5}", codigoCliente) + String.Format("{0:d3}", int.Parse(Carteira)) + nosso).ToString();
                NossoNumero = String.Format("{0:d3}", int.Parse(Carteira)) + "/" + nosso + "-" + dignosso;
                seu = String.Format("{0:d15}", NumeroIdentificacao).Substring(8, 7);
                digseu = Funcoes.Mod10(seu).ToString();
                NumeroDocumento = seu + "-" + digseu;
            }
        }

        protected override void formatarCampoLivre()
        {
            if (tipoCarteira == TipoCarteira.NORMAL)
                CampoLivre = String.Format("{0:d3}", int.Parse(Carteira)) + String.Format("{0:d8}", NumeroIdentificacao) + Funcoes.Right(NossoNumero, 1) + String.Format("{0:d4}", Agencia) + String.Format("{0:d5}", Conta) + Funcoes.Right(AgenciaCodigoBeneficiario, 1) + "000";
            else
            {
                int dac;
                dac = Funcoes.Mod10(String.Format("{0:d3}", int.Parse(Carteira)) + NossoNumero.Substring(4, 8) + NumeroDocumento.Substring(0, 7) + String.Format("{0:d5}", codigoCliente));
                CampoLivre = String.Format("{0:d3}", int.Parse(Carteira)) + NossoNumero.Substring(4, 8) + NumeroDocumento.Substring(0, 7) + String.Format("{0:d5}", codigoCliente) + dac.ToString() + "0";
            }
        }
    }
}
