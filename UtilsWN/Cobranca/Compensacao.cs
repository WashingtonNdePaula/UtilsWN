using System;
using UtilsWN.Util;

namespace UtilsWN.Cobranca
{
    public abstract class Compensacao : Cobranca
    {
        public string Aceite { get; set; }
        public int Agencia { get; }
        public string AgenciaCodigoBeneficiario { get; protected set; }
        public int Banco { get; }
        public string Carteira { get; set; }
        public int Conta { get; }
        public DateTime DataDocumento { get; set; }
        public DateTime DataProcessamento { get; set; }
        public decimal Desconto { get; set; }
        public string DVAgencia { get;}
        public string DVConta { get;}
        public string DVBanco { get; }
        public string EspecieDocumento { get; set; }
        public string Mora_Multa_Juros { get; set; }
        public string NumeroDocumento { get; set; }
        public string OutrasDeducoes_Abatimento { get; set; }
        public string OutrosAcrescimos { get; set; }
        public string UsoBanco { get; set; }
        public string NossoNumero { get; protected set; }
        protected abstract void formatarNossoNumero();
        protected abstract void formatarAgenciaCodigoBeneficiario();
        protected abstract void formatarCampoLivre();

        public Compensacao(long numeroIdentificacao, DateTime dataVencimento, decimal valorDocumento, int agencia, string dvAgencia, int conta, string dvConta, int banco, string dvBanco)
            : base(numeroIdentificacao, dataVencimento, valorDocumento)
        {
            Agencia = agencia;
            DVAgencia = dvAgencia;
            Conta = conta;
            DVConta = dvConta;
            Banco = banco;
            DVBanco = dvBanco;
        }

        protected override void montarLinhaDigitavel()
        {
            string lindig;
            string digtav1;
            string digtav2;
            string digtav3;
            string digtov1;
            string digtov2;
            string digtov3;

            if (CodigoDeBarras.Trim().Length != 44)
                throw new Exception("Código de Barras inválido, deve conter 44 dígitos!!!");
            for (int x = 0; x < 44; x++)
            {
                if (!Funcoes.IsNumeric(CodigoDeBarras.Substring(x, 1)))
                    throw new Exception("Código de Barras deve conter somente números!!!");
            }

            lindig = CodigoDeBarras.Substring(0, 4) + CodigoDeBarras.Substring(19);
            digtav1 = lindig.Substring(0, 9);
            digtov1 = Funcoes.Mod10(digtav1).ToString();
            digtav2 = lindig.Substring(9, 10);
            digtov2 = Funcoes.Mod10(digtav2).ToString();
            digtav3 = lindig.Substring(19, 10);
            digtov3 = Funcoes.Mod10(digtav3).ToString();

            LinhaDigitavel = digtav1.Insert(5, ".") + digtov1 + "  " +
                   digtav2.Insert(5, ".") + digtov2 + "  " +
                   digtav3.Insert(5, ".") + digtov3 + "  " +
                   CodigoDeBarras.Substring(4, 15).Insert(1, "  ");
        }

        protected override void montarCodigoDeBarras()
        {
            string codbar;
            string fator;
            string mvalor;
            string digbar;

            if (CampoLivre.Trim().Length != 25)
                throw new Exception("Campo livre inválido, deve conter 25 dígitos!!!");
            for (int x = 0; x < 25; x++)
            {
                if (! Funcoes.IsNumeric(CampoLivre.Substring(x, 1)))
                    throw new Exception("Campo livre deve conter somente números!!!");
            }

            fator = String.Format("{0:d4}", DataVencimento.Subtract(new DateTime(1997, 10, 7)).Days);
            mvalor = String.Format("{0:D10}", int.Parse(String.Format("{0:n2}", ValorDocumento).ToString().Replace(",", "").Replace(".", "")));
            codbar = String.Format("{0:d3}", Banco) + "9" + fator + mvalor + CampoLivre;
            digbar = (Funcoes.Mod11(codbar, 2, 9) == 0 || Funcoes.Mod11(codbar, 2, 9) > 9) ? "1" : Funcoes.Mod11(codbar, 2, 9).ToString();
            codbar = codbar.Substring(0, 4) + digbar.ToString() + codbar.Substring(4);
            CodigoDeBarras = codbar;
            CodigoDeBarras2de5 = Funcoes.Cod_Bar(codbar);
        }
    }
}
