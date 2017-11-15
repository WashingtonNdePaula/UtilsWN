using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Linq;

namespace UtilsWN.Util
{
    public static class PDFWN
    {
        private const string diretorioImagem = @"E:\FreeLancer\imglib\";
        public static void escreverFrase(PdfContentByte cb, float x, float y, float xx, float yy, float espacamento, int alinhamento, Single tabulacao, float rotacao, string texto, BaseColor cor)
        {
            var frase = new Phrase();
            var pedaco = new Chunk();

            string[] linha = texto.Split('#');
            for (int i = 0; i < linha.Count(); i++)
            {
                string[] pedacos = linha[i].Split('|');
                var fonte = new Font();
                fonte = FontFactory.GetFont(pedacos[0], BaseFont.CP1252, BaseFont.EMBEDDED, Convert.ToSingle(pedacos[1]), Convert.ToInt32(pedacos[2]), cor);
                pedaco = new Chunk(pedacos[3], fonte);
                frase.Add(pedaco);
            }
            if (rotacao == 0)
            {
                var ct = new ColumnText(cb);
                ct.SetIndent(tabulacao, true);
                ct.SetSimpleColumn(frase, Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y + espacamento), Utilities.MillimetersToPoints(xx), Utilities.MillimetersToPoints(yy), Utilities.MillimetersToPoints(espacamento), alinhamento);
                ct.Go();
            }
            else
            {
                ColumnText.ShowTextAligned(cb, alinhamento, frase, Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y), rotacao);
            }

        }

        public static void escreverFrase(PdfContentByte cb, float x, float y, float xx, float yy, float espacamento, int alinhamento, Single tabulacao, float rotacao, string texto, BaseColor cor, bool embutirfonte)
        {
            var frase = new Phrase();
            var pedaco = new Chunk();

            string[] linha = texto.Split('#');
            for (int i = 0; i < linha.Count(); i++)
            {
                string[] pedacos = linha[i].Split('|');
                var fonte = new Font();
                fonte = FontFactory.GetFont(pedacos[0], BaseFont.CP1252, embutirfonte, Convert.ToSingle(pedacos[1]), Convert.ToInt32(pedacos[2]), cor);
                pedaco = new Chunk(pedacos[3], fonte);
                frase.Add(pedaco);
            }
            if (rotacao == 0)
            {
                var ct = new ColumnText(cb);
                ct.SetIndent(tabulacao, true);
                ct.SetSimpleColumn(frase, Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y + espacamento), Utilities.MillimetersToPoints(xx), Utilities.MillimetersToPoints(yy), Utilities.MillimetersToPoints(espacamento), alinhamento);
                ct.Go();
            }
            else
            {
                ColumnText.ShowTextAligned(cb, alinhamento, frase, Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y), rotacao);
            }
        }

        public static void criarBox(PdfContentByte cb, float x, float y, float largura, float altura, BaseColor corBorda, BaseColor corFundo)
        {
            cb.Rectangle(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y - altura), Utilities.MillimetersToPoints(largura), Utilities.MillimetersToPoints(altura));
            cb.SetColorFill(corFundo);
            cb.SetColorStroke(corBorda);
            cb.FillStroke();
            cb.SetColorFill(BaseColor.BLACK);
            cb.SetColorStroke(BaseColor.BLACK);
            cb.FillStroke();
        }

        public static void criarBoxArredondado(PdfContentByte cb, float x, float y, float largura, float altura, BaseColor corBorda, BaseColor corFundo, Single arredondado)
        {
            cb.RoundRectangle(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y - altura), Utilities.MillimetersToPoints(largura), Utilities.MillimetersToPoints(altura), arredondado);
            cb.SetColorFill(corFundo);
            cb.SetColorStroke(corBorda);
            cb.FillStroke();
            cb.SetColorFill(BaseColor.BLACK);
            cb.SetColorStroke(BaseColor.BLACK);
            cb.FillStroke();
        }

        public static void criarLinha(PdfContentByte cb, float x, float y, float largura, BaseColor cor)
        {
            cb.MoveTo(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y));
            cb.LineTo(Utilities.MillimetersToPoints(x) + Utilities.MillimetersToPoints(largura), Utilities.MillimetersToPoints(297 - y));
            cb.SetColorStroke(cor);
            cb.Stroke();
            cb.SetColorFill(BaseColor.BLACK);
            cb.Fill();
        }

        public static void criarLinha(PdfContentByte cb, float x, float y, float largura, float larguraPontilhado, float espacoPontilhado, BaseColor cor)
        {
            if (larguraPontilhado != 0)
            {
                float div = largura / (larguraPontilhado + espacoPontilhado);
                for (float i = 0; i < div; i++)
                {
                    cb.MoveTo(Utilities.MillimetersToPoints(x + i * (larguraPontilhado + espacoPontilhado)), Utilities.MillimetersToPoints(297 - y));
                    cb.LineTo(Utilities.MillimetersToPoints(x + i * (larguraPontilhado + espacoPontilhado)) + Utilities.MillimetersToPoints(larguraPontilhado), Utilities.MillimetersToPoints(297 - y));
                }
            }
            else
            {
                cb.MoveTo(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y));
                cb.LineTo(Utilities.MillimetersToPoints(x) + Utilities.MillimetersToPoints(largura), Utilities.MillimetersToPoints(297 - y));
            }
            cb.SetColorStroke(cor);
            cb.Stroke();
            cb.SetColorFill(BaseColor.BLACK);
            cb.Fill();
        }

        public static void criarColuna(PdfContentByte cb, float x, float y, float altura, BaseColor cor)
        {
            cb.MoveTo(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y) - Utilities.MillimetersToPoints(altura));
            cb.LineTo(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y));
            cb.SetColorStroke(cor);
            cb.Stroke();
            cb.SetColorFill(BaseColor.BLACK);
            cb.Fill();
        }

        public static void criarColuna(PdfContentByte cb, float x, float y, float altura, float alturaPontilhado, float espacoPontilhado, BaseColor cor)
        {
            if (alturaPontilhado != 0)
            {
                float div = altura / (alturaPontilhado + espacoPontilhado);
                for (float i = 0; i < div; i++)
                {
                    cb.MoveTo(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y - i * (alturaPontilhado + espacoPontilhado) - alturaPontilhado));
                    cb.LineTo(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y - i * (alturaPontilhado + espacoPontilhado)));
                }
            }
            else
            {
                cb.MoveTo(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y) - Utilities.MillimetersToPoints(altura));
                cb.LineTo(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y));
            }
            cb.SetColorStroke(cor);
            cb.Stroke();
            cb.SetColorFill(BaseColor.BLACK);
            cb.Fill();
        }

        public static void inserirImagem(PdfContentByte cb, Image imagem, float x, float y, int alinhamento, float porcentagem, float rotacao)
        {
            imagem.ScalePercent(porcentagem);
            imagem.RotationDegrees = rotacao;
            if (rotacao == 180)
            {
                imagem.SetAbsolutePosition(Utilities.MillimetersToPoints(x) - imagem.Width, Utilities.MillimetersToPoints(297 - y) - imagem.Height);
            }
            else
            {
                imagem.SetAbsolutePosition(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y));
            }
            imagem.Alignment = alinhamento;
            cb.AddImage(imagem);
        }

        public static void inserirImagem(PdfContentByte cb, string imagem, float x, float y, int alinhamento, float porcentagem)
        {
            var img = Image.GetInstance(diretorioImagem + imagem);
            img.ScalePercent(porcentagem);
            img.SetAbsolutePosition(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y));
            img.Alignment = alinhamento;
            cb.AddImage(img);
        }

        public static void inserirImagem(PdfContentByte cb, string imagem, float x, float y, int alinhamento, float largura, float altura)
        {
            var img = Image.GetInstance(diretorioImagem + imagem);
            img.SetAbsolutePosition(Utilities.MillimetersToPoints(x), Utilities.MillimetersToPoints(297 - y));
            img.ScaleToFit(Utilities.MillimetersToPoints(largura), Utilities.MillimetersToPoints(altura));
            img.Alignment = alinhamento;
            cb.AddImage(img);
        }


        public static void criarFichaCompensacao(PdfContentByte cb, float x, float y, int numerobanco)
        {
            BaseColor cinzaescuro = new BaseColor(160, 160, 160);
            string logobanco = "logo_ficha_" + numerobanco.ToString().PadLeft(3, '0') + ".gif";
            inserirImagem(cb, logobanco, x + 20, y + 203.8F, 0, 45, 6);

            // Ficha Compensação
            criarBox(cb, x + 147, y + 297 - 93, 48, 9, cinzaescuro, cinzaescuro);
            criarBox(cb, x + 15, y + 297 - 93, 180, 0.2F, BaseColor.BLACK, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 91, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Local de Pagamento", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 91, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Vencimento", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 84, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 82.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Beneficiário", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 82.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Agência / Código do Beneficiário", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 78, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Data do Documento", BaseColor.BLACK);
            criarColuna(cb, x + 44, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 45, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Número do Documento", BaseColor.BLACK);
            criarColuna(cb, x + 94, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 95, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Espécie Documento", BaseColor.BLACK);
            criarColuna(cb, x + 116, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 117, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Aceite", BaseColor.BLACK);
            criarColuna(cb, x + 126, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 127, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Data Processamento", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Nosso Número", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 72, 48, 6, cinzaescuro, cinzaescuro);
            criarLinha(cb, x + 15, y + 297 - 72, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Uso do Banco", BaseColor.BLACK);
            criarColuna(cb, x + 34, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 35, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Carteira", BaseColor.BLACK);
            criarColuna(cb, x + 65, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 66, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Esp. Moeda", BaseColor.BLACK);
            criarColuna(cb, x + 79, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 80, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Qauntidade", BaseColor.BLACK);
            criarColuna(cb, x + 108, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 109, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Valor", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( = ) Valor do Documento", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 66, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 64.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Instruções: (Texto de responsabilidade exclusiva do Beneficiário", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 64.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( - ) Desconto", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 60, 48, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 58.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( - ) Abatimento/Outras Deduções", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 54, 48, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 52.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( + ) Mora / Multa", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 48, 48, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 46.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( + ) Outros Acréscimos", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 42, 48, 6, cinzaescuro, cinzaescuro);
            criarLinha(cb, x + 147, y + 297 - 42, 48, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 40.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( = ) Valor Cobrado)", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 93, 0.2F, 57, BaseColor.BLACK, BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 36, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 34.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Pagador", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 20, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 20.7F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Sacador/Avalista:", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 20.7F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Código da Baixa", BaseColor.BLACK);
            escreverFrase(cb, x + 137, y + 297 - 18.1F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Autenticação Mecânica", BaseColor.BLACK);
            escreverFrase(cb, x + 163, y + 297 - 17.6F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|6|1|FICHA DE COMPENSAÇÃO", BaseColor.BLACK);
            
        }

        public static void criarFichaCompensacao176(PdfContentByte cb, float x, float y, int numerobanco)
        {
            BaseColor cinzaescuro = new BaseColor(160, 160, 160);
            string logobanco = "logo_ficha_" + numerobanco.ToString().PadLeft(3, '0') + ".gif";
            inserirImagem(cb, logobanco, x + 20, y + 203.8F, 0, 45, 6);

            // Ficha Compensação
            criarBox(cb, x + 147, y + 297 - 93, 44, 9, cinzaescuro, cinzaescuro);
            criarBox(cb, x + 15, y + 297 - 93, 176, 0.2F, BaseColor.BLACK, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 91, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Local de Pagamento", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 91, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Vencimento", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 84, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 82.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Beneficiário", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 82.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Agência / Código do Beneficiário", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 78, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Data do Documento", BaseColor.BLACK);
            criarColuna(cb, x + 44, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 45, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Número do Documento", BaseColor.BLACK);
            criarColuna(cb, x + 94, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 95, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Espécie Documento", BaseColor.BLACK);
            criarColuna(cb, x + 116, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 117, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Aceite", BaseColor.BLACK);
            criarColuna(cb, x + 126, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 127, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Data Processamento", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Nosso Número", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 72, 44, 6, cinzaescuro, cinzaescuro);
            criarLinha(cb, x + 15, y + 297 - 72, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Uso do Banco", BaseColor.BLACK);
            criarColuna(cb, x + 34, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 35, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Carteira", BaseColor.BLACK);
            criarColuna(cb, x + 65, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 66, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Esp. Moeda", BaseColor.BLACK);
            criarColuna(cb, x + 79, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 80, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Qauntidade", BaseColor.BLACK);
            criarColuna(cb, x + 108, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 109, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Valor", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( = ) Valor do Documento", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 66, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 64.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Instruções: (Texto de responsabilidade exclusiva do Beneficiário", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 64.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( - ) Desconto", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 60, 44, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 58.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( - ) Abatimento/Outras Deduções", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 54, 44, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 52.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( + ) Mora / Multa", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 48, 44, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 46.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( + ) Outros Acréscimos", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 42, 44, 6, cinzaescuro, cinzaescuro);
            criarLinha(cb, x + 147, y + 297 - 42, 44, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 40.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( = ) Valor Cobrado)", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 93, 0.2F, 57, BaseColor.BLACK, BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 36, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 34.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Pagador", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 20, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 20.7F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Sacador/Avalista:", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 20.7F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Código da Baixa", BaseColor.BLACK);
            escreverFrase(cb, x + 137, y + 297 - 18.1F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Autenticação Mecânica", BaseColor.BLACK);
            escreverFrase(cb, x + 163, y + 297 - 17.6F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|6|1|FICHA DE COMPENSAÇÃO", BaseColor.BLACK);
        }

        public static void criarFichaCompensacaoFatura(PdfContentByte cb, float x, float y, int numerobanco)
        {
            BaseColor cinzaescuro = new BaseColor(160, 160, 160);
            string logobanco = "logo_ficha_" + numerobanco.ToString().PadLeft(3, '0') + ".gif";
            inserirImagem(cb, logobanco, x + 20, y + 203.8F, 0, 45, 6);

            // Ficha Compensação
            criarBox(cb, x + 147, y + 297 - 93, 48, 9, cinzaescuro, cinzaescuro);
            criarBox(cb, x + 15, y + 297 - 93, 180, 0.2F, BaseColor.BLACK, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 91, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Local de Pagamento", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 91, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Vencimento", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 84, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 82.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Beneficiário", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 82.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Agência / Código do Beneficiário", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 78, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Data do Documento", BaseColor.BLACK);
            criarColuna(cb, x + 44, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 45, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Número do Documento", BaseColor.BLACK);
            criarColuna(cb, x + 94, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 95, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Espécie Documento", BaseColor.BLACK);
            criarColuna(cb, x + 116, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 117, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Aceite", BaseColor.BLACK);
            criarColuna(cb, x + 126, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 127, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Data Processamento", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Nosso Número", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 72, 48, 6, cinzaescuro, cinzaescuro);
            criarLinha(cb, x + 15, y + 297 - 72, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Uso do Banco", BaseColor.BLACK);
            criarColuna(cb, x + 34, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 35, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Carteira", BaseColor.BLACK);
            criarColuna(cb, x + 65, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 66, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Esp. Moeda", BaseColor.BLACK);
            criarColuna(cb, x + 79, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 80, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Qauntidade", BaseColor.BLACK);
            criarColuna(cb, x + 108, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 109, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Valor", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( = ) Total desta Fatura", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 66, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 64.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Instruções: (Texto de responsabilidade exclusiva do Beneficiário", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 64.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( = ) Valor da Parcela", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 60, 48, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 58.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( - ) Desconto/Abatimento/Outras Deduções", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 54, 48, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 52.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( + ) Mora / Multa", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 48, 48, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 46.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( + ) Outros Acréscimos", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 42, 48, 6, cinzaescuro, cinzaescuro);
            criarLinha(cb, x + 147, y + 297 - 42, 48, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 40.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( = ) Valor Cobrado)", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 93, 0.2F, 57, BaseColor.BLACK, BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 36, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 34.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Pagador", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 20, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 20.7F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Sacador/Avalista:", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 20.7F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Código da Baixa", BaseColor.BLACK);
            escreverFrase(cb, x + 137, y + 297 - 18.1F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Autenticação Mecânica", BaseColor.BLACK);
            escreverFrase(cb, x + 163, y + 297 - 17.6F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|6|1|FICHA DE COMPENSAÇÃO", BaseColor.BLACK);

        }

        public static void criarFichaCompensacaoFatura176(PdfContentByte cb, float x, float y, int numerobanco)
        {
            BaseColor cinzaescuro = new BaseColor(160, 160, 160);
            string logobanco = "logo_ficha_" + numerobanco.ToString().PadLeft(3, '0') + ".gif";
            inserirImagem(cb, logobanco, x + 20, y + 203.8F, 0, 45, 6);

            // Ficha Compensação
            criarBox(cb, x + 147, y + 297 - 93, 44, 9, cinzaescuro, cinzaescuro);
            criarBox(cb, x + 15, y + 297 - 93, 176, 0.2F, BaseColor.BLACK, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 91, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Local de Pagamento", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 91, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Vencimento", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 84, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 82.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Beneficiário", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 82.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Agência / Código do Beneficiário", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 78, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Data do Documento", BaseColor.BLACK);
            criarColuna(cb, x + 44, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 45, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Número do Documento", BaseColor.BLACK);
            criarColuna(cb, x + 94, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 95, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Espécie Documento", BaseColor.BLACK);
            criarColuna(cb, x + 116, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 117, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Aceite", BaseColor.BLACK);
            criarColuna(cb, x + 126, y + 297 - 78, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 127, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Data Processamento", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 76.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Nosso Número", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 72, 44, 6, cinzaescuro, cinzaescuro);
            criarLinha(cb, x + 15, y + 297 - 72, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Uso do Banco", BaseColor.BLACK);
            criarColuna(cb, x + 34, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 35, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Carteira", BaseColor.BLACK);
            criarColuna(cb, x + 65, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 66, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Esp. Moeda", BaseColor.BLACK);
            criarColuna(cb, x + 79, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 80, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Qauntidade", BaseColor.BLACK);
            criarColuna(cb, x + 108, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 109, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Valor", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 70.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( = ) Valor do Documento", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 66, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 64.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Instruções: (Texto de responsabilidade exclusiva do Beneficiário", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 64.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( = ) Valor da Parcela", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 60, 44, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 58.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( - ) Desconto/Abatimento/Outras Deduções", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 54, 44, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 52.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( + ) Mora / Multa", BaseColor.BLACK);
            criarLinha(cb, x + 147, y + 297 - 48, 44, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 46.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( + ) Outros Acréscimos", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 42, 44, 6, cinzaescuro, cinzaescuro);
            criarLinha(cb, x + 147, y + 297 - 42, 44, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 40.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|( = ) Valor Cobrado)", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 93, 0.2F, 57, BaseColor.BLACK, BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 36, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 34.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Pagador", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 20, 176, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 20.7F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Sacador/Avalista:", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 20.7F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Código da Baixa", BaseColor.BLACK);
            escreverFrase(cb, x + 137, y + 297 - 18.1F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Autenticação Mecânica", BaseColor.BLACK);
            escreverFrase(cb, x + 163, y + 297 - 17.6F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|6|1|FICHA DE COMPENSAÇÃO", BaseColor.BLACK);
        }

        public static void criarReciboPagador(PdfContentByte cb, float x, float y)
        {
            BaseColor cinzaescuro = new BaseColor(160, 160, 160);

            // Recibo Pagador
            escreverFrase(cb, x + 160, y + 297 - 130, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|10|1|Recibo do Pagador", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 128, 48, 6, cinzaescuro, cinzaescuro);
            criarBox(cb, x + 15, y + 297 - 128, 180, 0.2F, BaseColor.BLACK, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 126.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Vencimento", BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 126.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Beneficiário", BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 122, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 120.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Nosso Número", BaseColor.BLACK);
            criarColuna(cb, x + 54, y + 297 - 122, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 55, y + 297 - 120.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Número do Documento", BaseColor.BLACK);
            criarColuna(cb, x + 94, y + 297 - 122, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 95, y + 297 - 120.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Espécie Documento", BaseColor.BLACK);
            criarColuna(cb, x + 116, y + 297 - 122, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 117, y + 297 - 120.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Aceite", BaseColor.BLACK);
            criarColuna(cb, x + 126, y + 297 - 122, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 127, y + 297 - 120.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Data Processamento", BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 120.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Agência / Código Beneficiário", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 116, 48, 6, cinzaescuro, cinzaescuro);
            criarLinha(cb, x + 15, y + 297 - 116, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 16, y + 297 - 114.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Pagador", BaseColor.BLACK);
            criarColuna(cb, x + 34, y + 297 - 72, 6, BaseColor.BLACK);
            escreverFrase(cb, x + 148, y + 297 - 114.2F, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Valor do Documento", BaseColor.BLACK);
            criarBox(cb, x + 147, y + 297 - 128, 0.2F, 18, BaseColor.BLACK, BaseColor.BLACK);
            criarLinha(cb, x + 15, y + 297 - 110, 180, BaseColor.BLACK);
            escreverFrase(cb, x + 160, y + 297 - 108, x + 195, 1, 4, Element.ALIGN_LEFT, 0, 0, "Arial|5|1|Autenticação Mecânica", BaseColor.BLACK);
        }
    }
}
