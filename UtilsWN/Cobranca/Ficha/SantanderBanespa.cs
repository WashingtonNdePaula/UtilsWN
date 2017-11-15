using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsWN.Util;

namespace UtilsWN.Cobranca.Ficha
{ 
    public class SantanderBanespa : Compensacao
    {
        public enum TipoCobranca
        { 
            COBRANCA_SIMPLES_RAPIDA_COM_REGISTRO = 101,
            COBRANCA_SIMPLES_SEM_REGISTRO = 102,
            COBRANCA_PENHOR_RAPIDA_COM_REGISTRO = 201,
            COBRANCA_ANTIGA_BANESPA = 33
        }

        public enum NumeroBanco
        {
            SANTANDER = 3530,
            BANESPA = 337,
            MERIDIONAL = 86
        }

        private int codigoCedente;
        private TipoCobranca tipoCobranca;
        private int IOF;


        /// <summary>Ficha de Compensação Padrão - Santander Banespa</summary>
        /// <param name="numeroBanco">Número do Banco - Santander = 353, Banespa = 033, Meridional = 008</param>
        /// <param name="tipoCobranca">Modalidade da Carteira</param>
        /// <param name="codigoCedente">Número do Código do Cedente (PSK)</param>
        /// <param name="IOF">Seguradoras Informar IOF, Demais Informar 0</param>
        /// <param name="agencia">Número da Agência sem Dígito Verificador</param>
        /// <param name="nossoNumero">Nosso Número sem formatação</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="dataVencimento">Data de Vencimento</param>
        public SantanderBanespa(NumeroBanco numeroBanco, TipoCobranca tipoCobranca, int codigoCedente, int IOF, int agencia, long nossoNumero, DateTime? dataVencimento, decimal valorDocumento)
            : base(nossoNumero, dataVencimento, valorDocumento, agencia, "0", 0, "0", int.Parse(String.Format("{0:d4}", int.Parse(numeroBanco.GetHashCode().ToString().Substring(0, 3)))), String.Format("{0:d4}", int.Parse(numeroBanco.GetHashCode().ToString().Substring(3, 1))))
        {
            try
            {
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (codigoCedente == 0)
                    throw new Exception("É necessário informar o Código do Cedente para fazer a cobrança");
                if (agencia == 0)
                    throw new Exception("É necessário informar a Agência para fazer a cobrança");
                if (codigoCedente.ToString().Length > 7)
                    throw new Exception("O Código do Cedente só pode ter no máximo 7 caracteres");
                if (nossoNumero.ToString().Length > 12)
                    throw new Exception("O Nosso Número só pode ter no máximo 12 caracteres");

                this.IOF = IOF;
                this.codigoCedente = codigoCedente;
                this.tipoCobranca = tipoCobranca;
                if (tipoCobranca == TipoCobranca.COBRANCA_SIMPLES_SEM_REGISTRO)
                    Carteira = "CSR";
                else
                    Carteira = "ECR";
                
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

        /// <summary>Ficha de Compensação Padrão - Antigo Banespa</summary>
        /// <param name="codigoCedente">Código do Cedente com 11 Caracteres</param>
        /// <param name="nossoNumero">Nosso Número sem formatação</param>
        /// <param name="valorDocumento">Valor do Documento</param>
        /// <param name="dataVencimento">Data de Vencimento</param>
        public SantanderBanespa(int codigoCedente, long nossoNumero, DateTime? dataVencimento, decimal valorDocumento)
            : base(nossoNumero, dataVencimento, valorDocumento, 0, "0", 0, "0", 33, "7")
        {
            try
            {
                if (nossoNumero == 0)
                    throw new Exception("É necessário informar o Nosso Número para fazer a cobrança");
                if (codigoCedente == 0)
                    throw new Exception("É necessário informar o Código do Cedente para fazer a cobrança");
                if (codigoCedente.ToString().Length > 11)
                    throw new Exception("O Código do Cedente só pode ter no máximo 11 caracteres");
                if (nossoNumero.ToString().Length > 7)
                    throw new Exception("O Nosso Número só pode ter no máximo 7 caracteres");

                this.codigoCedente = codigoCedente;
                this.tipoCobranca = TipoCobranca.COBRANCA_ANTIGA_BANESPA;
                Carteira = "COB";
                LocalPagamento = "PAGÁVEL EM QUALQUER AGÊNCIA BANCÁRIA ATÉ O VENCIMENTO";
                Aceite = "N";
                EspecieDocumento = "RC-CI";
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
            if (tipoCobranca == TipoCobranca.COBRANCA_ANTIGA_BANESPA)
                AgenciaCodigoBeneficiario = codigoCedente.ToString().Insert(10, " ").Insert(5, " ").Insert(3, " ");
            else
                AgenciaCodigoBeneficiario = Agencia + " / " + codigoCedente;
        }

        /// <summary>Formatação do Campo "Nosso Número" na ficha de compensação</summary>
        protected override void formatarNossoNumero()
        {
            if (tipoCobranca == TipoCobranca.COBRANCA_ANTIGA_BANESPA)
            {
                string dignosso, nosso;
                nosso = String.Format("{0:d7}", NumeroIdentificacao);
                dignosso = Funcoes.NNBanespa(AgenciaCodigoBeneficiario.Substring(0, 3) + nosso).ToString();
                NossoNumero = AgenciaCodigoBeneficiario.Substring(0, 3) + " " + nosso + " " + dignosso;
            }
            else
            {
                string dignosso, nosso;
                nosso = String.Format("{0:d12}", NumeroIdentificacao);
                dignosso = Funcoes.Mod11(nosso, 2, 9) > 9 ?  "0" : Funcoes.Mod11(nosso, 2, 9).ToString();
                NossoNumero = nosso + dignosso;
            }
        }

        protected override void formatarCampoLivre()
        {
            if (tipoCobranca == TipoCobranca.COBRANCA_ANTIGA_BANESPA)
            {
                bool resp = true;
                string constante1;
                int dvBarra1, dvBarra2;

                constante1 = codigoCedente + String.Format("{0:d7}", NumeroIdentificacao) + String.Format("{0:d5}", tipoCobranca);
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
            else
                CampoLivre = "9" + String.Format("{0:d7}", codigoCedente) + NossoNumero.Substring(0, 13) + IOF + tipoCobranca;
        }
    }
}