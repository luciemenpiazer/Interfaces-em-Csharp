using Fase07.Solucao.Contratos;

namespace Fase07.Solucao.Implementacoes
{
    // Limpo: Só implementa o que usa
    public class GestorPadrao : IGeradorMensagem
    {
        public string GerarBoasVindas(string nome) => $"Bem-vindo(a), {nome}.";
    }
}