namespace Fase06.Domain
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string TipoAssinatura { get; set; } = "Padrao";

        // Construtor vazio necessário para o Deserializer do JSON
        public Usuario() { }

        public Usuario(int id, string nome, string tipoAssinatura)
        {
            Id = id;
            Nome = nome;
            TipoAssinatura = tipoAssinatura;
        }

        public override string ToString() => $"{Id} - {Nome} ({TipoAssinatura})";
    }
}