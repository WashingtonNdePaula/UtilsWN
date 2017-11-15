namespace UtilsWN.FAC
{
    public class FaixaCEP
    {
        public string Estado { get; set; }
        public int CEPInicial { get; set; }
        public int CEPFinal { get; set; }
        public FaixaCEP(string estado, int cepInicial, int cepFinal)
        {
            Estado = estado;
            CEPInicial = cepInicial;
            CEPFinal = CEPFinal;
        }
    }
}
