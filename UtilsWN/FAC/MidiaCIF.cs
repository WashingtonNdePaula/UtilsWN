using System;
using System.IO;

namespace UtilsWN.FAC
{
    public static class MidiaCIF
    {
        public static void header(string diretorio, string nomeArquivo, int codigoDR, int codigoAdm, long numeroCartao, int numeroLote, int codigoUnidade, int cepOrigem, long numeroContrato)
        {
            try
            {
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                StreamWriter arquivo = new StreamWriter(diretorio + nomeArquivo);

                arquivo.WriteLine("1" + codigoDR.ToString().Trim().PadLeft(2, '0') +
                                  codigoAdm.ToString().Trim().PadLeft(8, '0') +
                                  numeroCartao.ToString().Trim().PadLeft(12, '0') +
                                  numeroLote.ToString().Trim().PadLeft(5, '0') +
                                  codigoUnidade.ToString().Trim().PadLeft(8, '0') +
                                  cepOrigem.ToString().Trim().PadLeft(8, '0') +
                                  numeroContrato.ToString().Trim().PadLeft(10, '0'));
                arquivo.Flush();
                arquivo.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void detalhe(string diretorio, string nomeArquivo, string idObjeto, double pesoObjeto, int cepObjeto, string codigoCategoria)
        {
            try
            {
                StreamWriter arquivo = new StreamWriter(diretorio + nomeArquivo, true);
                arquivo.WriteLine("2" + idObjeto.ToString().Trim().PadLeft(11, '0') +
                                  String.Format("{0:0.00}", pesoObjeto).Trim().Replace(".", "").Replace(",", "").PadLeft(6, '0') +
                                  cepObjeto.ToString().Trim().PadLeft(8, '0') +
                                  codigoCategoria);
                arquivo.Flush();
                arquivo.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void trailer(string diretorio, string nomeArquivo, int quantidade, double peso)
        {
            try
            {
                StreamWriter arquivo = new StreamWriter(diretorio + nomeArquivo, true);
                arquivo.WriteLine("4" + quantidade.ToString().Trim().PadLeft(7, '0') +
                                  String.Format("{0:0.00}", peso).Trim().Replace(".", "").Replace(",", "").PadLeft(10, '0'));
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
