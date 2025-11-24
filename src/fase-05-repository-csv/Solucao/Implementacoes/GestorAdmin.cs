using System;
using Fase07.Solucao.Contratos;

namespace Fase07.Solucao.Implementacoes
{
    // Combina capacidades: Mensagem + Administrativo
    public class GestorAdmin : IGeradorMensagem, IAdministrativo
    {
        public string GerarBoasVindas(string nome) => $"Olá Admin {nome}.";

        public void BanirUsuarioDoSistema(int idAlvo)
        {
            Console.WriteLine($"USUÁRIO {idAlvo} BANIDO DO SISTEMA.");
        }
    }
}