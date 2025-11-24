namespace Fase07.Violacao
{
    // VIOLAÇÃO DO ISP: Uma interface fazendo coisas demais (God Interface)
    public interface IGestorUsuarioCompleto
    {
        string GerarBoasVindas(string nome);
        void CobrarAssinaturaMensal();      // Nem todos usam
        void BanirUsuarioDoSistema(int id); // Nem todos usam
    }
}