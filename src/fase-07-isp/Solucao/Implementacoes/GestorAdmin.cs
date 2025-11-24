using System;
using Fase07.Solucao.Domain;

namespace Fase07.Solucao.Implementacoes
{
    // Compõe: Mensagem + Administrativo
    public class GestorAdmin : IGeradorMensagem, IAdministrativo
    {
        public string GerarBoasVindas(string nome) => $"Olá Admin {nome}.";

        public void BanirUsuarioDoSistema(int idAlvo)
        {
            Console.WriteLine($"Usuário {idAlvo} banido pelo Administrador.");
        }
    }
}