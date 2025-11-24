namespace Fase07.Solucao.Domain
{
    // Capacidade 3: Apenas admins
    public interface IAdministrativo
    {
        void BanirUsuarioDoSistema(int idAlvo);
    }
}