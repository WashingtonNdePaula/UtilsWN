using System;

namespace UtilsWN.Util
{
    public static class Funcoes
    {

        public static string Right(string texto, int tamanho)
        {
            // se comprimento é maior que "tamanho" redefine "tamanho"
            tamanho = (texto.Length < tamanho ? texto.Length : tamanho);
            return texto.Substring(texto.Length - tamanho);
        }

        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        public static string EditaDois(string valor)
        {
            return Double.Parse(valor.Trim().Insert(valor.Trim().Length - 2, ",")).ToString("C").Replace("R$", "");
        }

        public static string EditaDois(double valor)
        {
            return valor.ToString("C").Replace("R$", "");
        }
        public static string EditaDois(decimal valor)
        {
            return valor.ToString("C").Replace("R$", "");
        }
        public static string SomenteNumeros(string cep)
        {
            string numeros = "";
            int tamanho = cep.Length;
            for (int i = 0; i < tamanho; i++)
            {
                if (IsNumeric(cep.Substring(i, 1)))
                {
                    numeros += cep.Substring(i, 1);
                }
            }
            return numeros;
        }

        /// <summary>Cálculo Módulo 10</summary>
        /// <param name="codigo">Sequência numérica para cálculo</param>
        /// <returns>Retorna um valor Inteiro</returns>
        public static int Mod10(string codigo)
        {
            int i, soma, somad, peso;
            peso = 2;
            soma = 0;
            somad = 0;
            i = codigo.Length;
            while (i > 0)
            {
                somad = int.Parse(codigo.Substring(i - 1, 1)) * peso;
                i = i - 1;
                if (somad > 9)
                    somad = int.Parse(somad.ToString().Trim().Substring(0, 1)) +
                            int.Parse(somad.ToString().Trim().Substring(1, 1));
                soma = soma + somad;
                peso = peso + 1;
                if (peso > 2) peso = 1;
            }
            i = soma.ToString().Trim().Length;
            if (int.Parse(soma.ToString().Trim().Substring(i - 1, 1)) != 0)
                return 10 - int.Parse(soma.ToString().Trim().Substring(i - 1, 1));
            else
                return 0;
        }

        /// <summary>Cálculo Módulo 11 - Peso Inicial e Peso Final</summary>
        /// <param name="codigo">Sequência numérica para cálculo</param>
        /// <param name="pesoInicial">Peso Inicial</param>
        /// <param name="pesoFinal">Peso Final</param>
        /// <returns>Retorna um valor Inteiro</returns>
        public static int Mod11(string codigo, int pesoInicial, int pesoFinal)
        {
            int i, soma, peso;
            peso = pesoInicial;
            soma = 0;

            i = codigo.Length;
            if (pesoInicial > pesoFinal)
            {
                while (i > 0)
                {
                    soma = int.Parse(soma + codigo.Substring(i - 1, 1)) * pesoInicial;
                    pesoInicial -= 1;
                    if (pesoInicial < pesoFinal) pesoInicial = peso;
                    i = i - 1;
                }
            }
            if (pesoInicial < pesoFinal)
            {
                while (i > 0)
                {
                    soma = soma + int.Parse(codigo.Substring(i - 1, 1)) * pesoInicial;
                    pesoInicial += 1;
                    if (pesoInicial > pesoFinal) pesoInicial = peso;
                    i = i - 1;
                }
            }
            return 11 - (soma % 11);
        }


        /// <summary>Função para Codificar o código de barras</summary>
        /// <param name="strNumero">Sequência numérica a ser codificada</param>
        /// <returns>Retorna um String</returns>
        public static string Cod_Bar(string strNumero)
        {
            int l, tamanho;
            string codbarra, compxerox, compxerox1, compxerox2;
            string[] codxerox = { "00110", "10001", "01001", "11000", "00101", "10100", "01100", "00011", "10010", "01010" };

            tamanho = strNumero.Length;
            if (tamanho % 2 != 0)
            {
                throw new Exception("Código Inválido, Quantidade de números precisam ser múltiplos de 2 - Função 2 de 5 intercalado");
            }
            else if (tamanho == 0)
            {
                throw new Exception("Código de Barras em Branco - Função 2 de 5 intercalado");
            }
            codbarra = "<";
            l = 0;

            for (int i = 0; i < tamanho / 2; i++)
            {
                int numx = int.Parse(strNumero.Substring(l, 1));
                l = l + 1;
                int numy = int.Parse(strNumero.Substring(l, 1));
                l = l + 1;
                compxerox1 = codxerox[numx];
                compxerox2 = codxerox[numy];
                compxerox = "";
                for (int w = 0; w < 5; w++)
                {
                    compxerox += compxerox1.Substring(w, 1);
                    compxerox += compxerox2.Substring(w, 1);
                }
                int y = 0;
                for (int x = 0; x < 5; x++)
                {
                    string binxer = compxerox.Substring(y, 2);
                    switch (binxer)
                    {
                        case "00":
                            codbarra += "n";
                            break;
                        case "01":
                            codbarra += "N";
                            break;
                        case "10":
                            codbarra += "w";
                            break;
                        case "11":
                            codbarra += "W";
                            break;
                    }
                    y += 2;
                }
            }
            codbarra = codbarra + ">";
            return codbarra;
        }

        public static int NNBanespa(string cnumero)
        {
            string cvetor;
            int nsoma, nconta;
            cvetor = "7319731973";
            nsoma = 0;
            for (nconta = 1;nconta <= cvetor.Length; nconta++)
            {
                nsoma += int.Parse(cvetor.Substring(nconta - 1, 1)) * int.Parse(cnumero.Substring(nconta - 1, 1));
            }
            return nsoma % 10 > 0 ? 10 - (nsoma % 10) : 0;
        }

        public static int NNNossaCaixa(string cnumero)
        {
            string cvetor;
            int nsoma, nconta;
            cvetor = "31973197319731319731973";
            nsoma = 0;
            for (nconta = 1; nconta <= cvetor.Length; nconta++)
            {
                nsoma += int.Parse(cvetor.Substring(nconta - 1, 1)) * int.Parse(cnumero.Substring(nconta - 1, 1));
            }
            return nsoma % 10 > 0 ? 10 - (nsoma % 10) : 0;
        }

        public static int NNBankBoston(string cnumero)
        {
            string cvetor;
            int nsoma, nconta;
            cvetor = "98765432";
            nsoma = 0;
            for (nconta = 1; nconta <= cvetor.Length; nconta++)
            {
                nsoma += int.Parse(cvetor.Substring(nconta - 1, 1)) * int.Parse(cnumero.Substring(nconta - 1, 1));
            }
            nsoma = nsoma * 10;
            return nsoma % 11 == 10 ? 0 : nsoma % 11;
        }

        public static string RetiraAcentos(string texto)
        {
            var acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û", "'", "/" };
            var semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", " ", " " };
            for (int i = 0; i < acentos.Length; i++)
            {
                texto = texto.Replace(acentos[i], semAcento[i]);
            }
            return texto;
        }

        /// <summary>
        /// Funcão usada para a construção do código de barras - PostNet
        /// </summary>
        /// <param name="cep">CEP - somente números</param>
        /// <returns>CEP + Dígito Verificador</returns>
        /// <remarks></remarks>

        public static string PostNet(string cep)
        {
            string dvCEP = "";
            if (cep.Length != 8)
            {
                throw new Exception("CEP Inválido!!!!");
            }
            int soma = 0;
            for (int i = 0; i < 8; i++)
            {
                soma = soma + int.Parse(cep.Substring(i, 1));
            }
            dvCEP = (soma.ToString().Trim().Substring(soma.ToString().Trim().Length - 1) == "0" ? 0 : 10 - int.Parse(soma.ToString().Trim().Substring(soma.ToString().Trim().Length - 1))).ToString();
            return "*" + cep + dvCEP + "*";
        }
    }
}
