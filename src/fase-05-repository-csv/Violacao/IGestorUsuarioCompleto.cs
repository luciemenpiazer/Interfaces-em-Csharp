using System;

namespace Fase07.Violacao
{
    // VIOLAÇÃO DO ISP: Interface "gorda" que força comportamentos indesejados
    public interface IGestorUsuarioCompleto
    {
        string GerarBoasVindas(string nome);
        void CobrarAssinaturaMensal();      // Nem todo usuário paga
        void BanirUsuarioDoSistema(int id); // Nem todo usuário é admin
    }
}