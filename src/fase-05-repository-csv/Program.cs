using System;
using Fase07.Solucao.Contratos;
using Fase07.Solucao.Implementacoes;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Fase 7: ISP na Prática ===\n");

        // 1. Polimorfismo Seguro: Todo mundo é IGeradorMensagem
        var usuarios = new IGeradorMensagem[]
        {
            new GestorPadrao(),
            new GestorPremium(),
            new GestorAdmin()
        };

        Console.WriteLine("--- Mensagens de Boas Vindas ---");
        foreach (var u in usuarios)
        {
            // O compilador garante que GerarBoasVindas existe para todos
            Console.WriteLine(u.GerarBoasVindas("UsuárioTeste"));
        }

        // 2. Capacidade Específica: Financeiro
        Console.WriteLine("\n--- Processo de Cobrança ---");
        
        IFinanceiro pagante = new GestorPremium();
        pagante.CobrarAssinaturaMensal();

        // ERRO DE COMPILAÇÃO (Segurança):
        // IFinanceiro invalido = new GestorPadrao(); // O compilador não deixa!
        // Isso evita o erro de runtime que acontecia na "Violação".

        // 3. Capacidade Específica: Admin
        Console.WriteLine("\n--- Área Administrativa ---");
        IAdministrativo admin = new GestorAdmin();
        admin.BanirUsuarioDoSistema(999);
    }
}