namespace UtilsWN.Padrao
{
    public class Campo
    {
        public string Nome { get; set; }
        public string Conteudo { get; set; }
        public Campo(string nome, string conteudo)
        {
            Nome = nome;
            Conteudo = conteudo;
        }
    }
}
