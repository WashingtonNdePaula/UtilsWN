using System;
using System.IO;

namespace UtilsWN.Util
{
    public static class ArquivoSaida
    {
        public static void Escrever(string diretorio, string nomeArquivo, string linha)
        {
            try
            {
                StreamWriter arquivo;
                if (! Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                if (File.Exists(diretorio + nomeArquivo))
                    arquivo = new StreamWriter(diretorio + nomeArquivo, true);
                else
                    arquivo = new StreamWriter(diretorio + nomeArquivo);

                arquivo.WriteLine(linha);
                arquivo.Flush();
                arquivo.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
