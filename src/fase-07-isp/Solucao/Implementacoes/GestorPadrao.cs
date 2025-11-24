using Fase07.Solucao.Domain;

namespace Fase07.Solucao.Implementacoes
{
    // Limpo: Só implementa mensagem
    public class GestorPadrao : IGeradorMensagem
    {
        public string GerarBoasVindas(string nome) => $"Bem-vindo(a), {nome}.";
    }
}