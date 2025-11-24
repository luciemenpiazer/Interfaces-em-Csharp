using System;
using Fase07.Solucao.Contratos;

namespace Fase07.Solucao.Implementacoes
{
    // Combina capacidades: Mensagem + Financeiro
    public class GestorPremium : IGeradorMensagem, IFinanceiro
    {
        public string GerarBoasVindas(string nome) 
            => $"Parabéns, {nome}, acesso Premium!";

        public void CobrarAssinaturaMensal()
        {
            Console.WriteLine("Cobrança efetuada com sucesso (via cartão).");
        }
    }
}