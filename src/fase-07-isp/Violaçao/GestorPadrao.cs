using System;

namespace Fase07.Violacao
{
    public class GestorPadrao : IGestorUsuarioCompleto
    {
        public string GerarBoasVindas(string nome) => $"Bem-vindo(a), {nome}.";

        // O Problema: Obrigado a implementar o que não deve
        public void CobrarAssinaturaMensal()
        {
            throw new NotImplementedException("ERRO: Usuário padrão não paga.");
        }

        public void BanirUsuarioDoSistema(int id)
        {
            throw new NotImplementedException("ERRO: Usuário padrão não é admin.");
        }
    }
}