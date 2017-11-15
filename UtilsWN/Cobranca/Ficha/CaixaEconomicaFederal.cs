using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsWN.Util;

namespace UtilsWN.Cobranca.Ficha
{
    class CaixaEconomicaFederal : Compensacao
    {

        public enum TipoCobranca
        {
            SIGCB = 1,
            SICOB = 2,
            GRCSU = 3,
        }

        public enum TipoCarteira
        {
            REGISTRADA = 1,
            SEM_REGISTRO = 2,
        }

        public enum TipoCarteiraSICOB
        {
            SEM_REGISTRO = 82,
            RAPIDA = 9
        }

        public enum TipoEntidade
        {
            SINDICATO = 1,
            FEDERACAO = 2,
            CONFEDERACAO = 3,
            MTE_CEES = 4
        }

        public enum EmissaoBoleto
        {
            BENEFICIARIO = 4
        }

        private TipoCobranca tipoCobranca;
        private TipoCarteira tipoCarteira;
        private TipoCarteiraSICOB tipoCarteiraSICOB;
        private EmissaoBoleto emissaoBoleto;
        private int CNAE;
        private int operacao;

        /// <summary>
        /// Ficha de Compensação Padrão - Caixa Econômica Federal - SIGCB
        /// </summary>
        /// <param name="carteira">Tipo de Carteira</param>
        /// <param name="agencia">Numero da Agência</param>
        /// <param name="codigoBeneficiario">Código do Beneficiário</param>
        /// <param name="dvCodigoBeneficiario">Dígito Verificador do Código do Beneficiário</param>
        /// <param name="nossoNumero">Nosso Número</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="dataVencimento">Vencimento</param>
        /// <remarks></remarks>
        public CaixaEconomicaFederal(TipoCarteira tipoCarteira, int agencia, int codigoBeneficiario, string dvCodigoBeneficiario, long nossoNumero, DateTime? dataVencimento, decimal valorDocumento)
            : base(nossoNumero, dataVencimento, valorDocumento, agencia, "0", codigoBeneficiario, dvCodigoBeneficiario, 104, "0")
        {
            try
            {
                if (agencia == 0)
                    throw new Exception("É necessário o informar número da Agência para fazer a cobrança");
                if (codigoBeneficiario == 0)
                    throw new Exception("É necessário o informar número do Código do Beneficiário para fazer a cobrança");
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (agencia.ToString().Length > 4)
                    throw new Exception("Número da Agência deve ter no máximo 4 dígitos");
                if (codigoBeneficiario.ToString().Length > 6)
                    throw new Exception("Código do Beneficiário deve ter no máximo 6 dígitos");
                if (dvCodigoBeneficiario.ToString().Length > 1)
                    throw new Exception("Dígito Verificador do Código do Beneficiário deve ter no máximo 1 dígito");
                if (nossoNumero.ToString().Length > 15)
                    throw new Exception("Para uma cobrança registrada o Nosso Número deve ter no máximo 15 dígitos");

                LocalPagamento = "PREFERENCIALMENTE NAS LOTÉRICAS ATÉ O VALOR LIMITE";
                EspecieDocumento = "COB";
                if (tipoCarteira == TipoCarteira.REGISTRADA)
                    Carteira = "RG";
                else if (tipoCarteira == TipoCarteira.SEM_REGISTRO)
                    Carteira = "SR";

                Aceite = "N";
                tipoCobranca = TipoCobranca.SIGCB;
                this.tipoCarteira = tipoCarteira;
                emissaoBoleto = EmissaoBoleto.BENEFICIARIO;
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


        /// <summary>
        /// Ficha de Compensação Padrão - Caixa Econômica Federal - SICOB
        /// </summary>
        /// <param name="carteira">Tipo de Carteira</param>
        /// <param name="agencia">Numero da Agência</param>
        /// <param name="codigoBeneficiario">Código do Beneficiário</param>
        /// <param name="nossoNumero">Nosso Número</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="dataVencimento">Vencimento</param>
        /// <remarks></remarks>
        public CaixaEconomicaFederal(TipoCarteiraSICOB tipoCarteiraSICOB, int agencia, int operacao, int codigoBeneficiario, string dvCodigoBeneficiario, long nossoNumero, DateTime? dataVencimento, decimal valorDocumento)
            : base(nossoNumero, dataVencimento, valorDocumento, 0, "0", codigoBeneficiario, dvCodigoBeneficiario, 104, "0")
        {
            try
            {
                if (agencia == 0)
                    throw new Exception("É necessário o informar número da Agência para fazer a cobrança");
                if (codigoBeneficiario == 0)
                    throw new Exception("É necessário o informar número do Código do Beneficiário para fazer a cobrança");
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (agencia.ToString().Length > 4)
                    throw new Exception("Número da Agência deve ter no máximo 4 dígitos");
                if (operacao.ToString().Length > 3)
                    throw new Exception("Número da Operação deve ter no máximo 3 dígitos");
                if (codigoBeneficiario.ToString().Length > 8)
                    throw new Exception("Código do Beneficiário deve ter no máximo 8 dígitos");
                if (dvCodigoBeneficiario.ToString().Length > 1)
                    throw new Exception("Dígito Verificador do Código do Beneficiário deve ter no máximo 1 dígito");
                if (nossoNumero.ToString().Length > 15)
                    throw new Exception("Para uma cobrança registrada o Nosso Número deve ter no máximo 15 dígitos");

                LocalPagamento = "PREFERENCIALMENTE NAS LOTÉRICAS ATÉ O VALOR LIMITE";
                EspecieDocumento = "COB";
                if (tipoCarteira == TipoCarteira.REGISTRADA)
                    Carteira = "RG";
                else if (tipoCarteira == TipoCarteira.SEM_REGISTRO)
                    Carteira = "SR";

                Aceite = "N";
                tipoCobranca = TipoCobranca.SIGCB;
                this.tipoCarteiraSICOB = tipoCarteiraSICOB;
                emissaoBoleto = EmissaoBoleto.BENEFICIARIO;
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


        /// <summary>
        /// Ficha de Compensação Padrão - Caixa Econômica Federal - GRCSU
        /// </summary>
        /// <param name="agencia">Número da Agência</param>
        /// <param name="codigoEntidade">Código da Entidade</param>
        /// <param name="tipoEntidade">Tipo da Entidade</param>
        /// <param name="CNAE">Classificação Nacional de Atividades Econômicas</param>
        /// <param name="nossoNumero">Nosso Número</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="dataVencimento">Vencimento</param>
        /// <remarks></remarks>
        public CaixaEconomicaFederal(int agencia, int codigoEntidade, TipoEntidade tipoEntidade, int CNAE, long nossoNumero, DateTime? dataVencimento, decimal valorDocumento)
            : base(nossoNumero, dataVencimento, valorDocumento, agencia, "0", codigoEntidade, "0", 104, "0")
        {
            try
            {
                if (codigoEntidade == 0)
                    throw new Exception("É necessário informar o número do Código do Beneficiário para fazer a cobrança");
                if (CNAE == 0)
                    throw new Exception("É necessário informar o CNAE");
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (codigoEntidade.ToString().Length > 5)
                    throw new Exception("O Código do Beneficiário deve ter no máximo 5 dígitos");
                if (CNAE.ToString().Length > 3)
                    throw new Exception("O CNAE deve ter no máximo 3 dígitos");
                if (nossoNumero.ToString().Length > 12)
                    throw new Exception("O Nosso Número deve ter no máximo 12 dígitos");

                this.CNAE = CNAE;
                tipoCobranca = TipoCobranca.GRCSU;
                EspecieDocumento = "GRCSU";
                Carteira = "SIND";
                LocalPagamento = "PREFERENCIALMENTE NAS LOTÉRICAS ATÉ O VALOR LIMITE";

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
            if (tipoCobranca == TipoCobranca.GRCSU)
                AgenciaCodigoBeneficiario = String.Format("{0:d4}", Agencia) + " / " + String.Format("{0:d8}", Conta) + "-" + DVConta;
            else if (tipoCobranca == TipoCobranca.SIGCB)
                AgenciaCodigoBeneficiario = String.Format("{0:d4}", Agencia) + " / " + String.Format("{0:d6}", Conta) + "-" + DVConta;
            else if (tipoCobranca == TipoCobranca.SICOB)
                AgenciaCodigoBeneficiario = String.Format("{0:d4}", Agencia) + " / " + String.Format("{0:d6}", Conta) + "-" + DVConta;
        }

        /// <summary>Formatação do Campo "Nosso Número" na ficha de compensação</summary>
        protected override void formatarNossoNumero()
        {
            if (tipoCobranca == TipoCobranca.SIGCB)
            {
                string nosso = tipoCarteira.GetHashCode().ToString() + emissaoBoleto.GetHashCode().ToString() + String.Format("{0:d15}", NumeroIdentificacao);
                string dignosso = Funcoes.Mod11(nosso, 2, 9) > 9 ? "0" : Funcoes.Mod11(nosso, 2, 9).ToString();
                NossoNumero = nosso + "-" + dignosso;
            }
            else if (tipoCobranca == TipoCobranca.SICOB)
            {
                string nosso = tipoCarteiraSICOB.GetHashCode().ToString();
                nosso += tipoCarteiraSICOB == TipoCarteiraSICOB.SEM_REGISTRO ? String.Format("{0:8}", NumeroIdentificacao) : String.Format("{0:9}", NumeroIdentificacao);
                string dignosso = Funcoes.Mod11(nosso, 2, 9) > 9 ? "0" : Funcoes.Mod11(nosso, 2, 9).ToString();
                NossoNumero = nosso + "-" + dignosso;
            }
            else
                NossoNumero = String.Format("{0:d12}", NumeroIdentificacao);
        }

        protected override void formatarCampoLivre()
        {
            if (tipoCobranca == TipoCobranca.GRCSU)
                CampoLivre = "97" + String.Format("{0:d5}", Agencia) + String.Format("{0:d8}", Conta) + String.Format("{0:d3}", CNAE).Substring(0, 1) + emissaoBoleto.GetHashCode().ToString() + "77" + NossoNumero + String.Format("{0:d3}", CNAE).Substring(1, 2);
            else if (tipoCobranca == TipoCobranca.SICOB)
                CampoLivre = NossoNumero.Substring(0,10) + String.Format("{0:d4}", Agencia) + String.Format("{0:d3}", operacao) + String.Format("{0:d8}", Conta);
            else
            {
                CampoLivre = String.Format("{0:d6}", Conta) + DVConta + NossoNumero.Substring(2, 3) + NossoNumero.Substring(0, 1) + NossoNumero.Substring(5, 3) + NossoNumero.Substring(1, 1) + NossoNumero.Substring(8);
                string dvCampoLivre = Funcoes.Mod11(CampoLivre, 2, 9) > 9 ? "0" : Funcoes.Mod11(CampoLivre, 2, 9).ToString();
                CampoLivre += dvCampoLivre;
            }
        }

    }
}
