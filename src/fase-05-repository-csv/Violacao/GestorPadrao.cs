using System;

namespace Fase07.Violacao
{
    // Esta classe sofre porque é obrigada a implementar métodos que não usa
    public class GestorPadrao : IGestorUsuarioCompleto
    {
        public string GerarBoasVindas(string nome) 
            => $"Bem-vindo(a), {nome}.";

        // Code Smell: NotImplementedException
        public void CobrarAssinaturaMensal()
        {
            throw new NotImplementedException("Usuário padrão não possui cobrança.");
        }

        public void BanirUsuarioDoSistema(int id)
        {
            throw new NotImplementedException("Usuário padrão não pode banir.");
        }
    }
}