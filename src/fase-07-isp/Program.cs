using System;
using Fase07.Solucao.Domain;
using Fase07.Solucao.Implementacoes;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Fase 7: ISP na Prática ===\n");

        // 1. Polimorfismo Seguro (Todos geram mensagens)
        // Isso simula o "Módulo de Marketing" descrito no texto
        var usuarios = new IGeradorMensagem[]
        {
            new GestorPadrao(),
            new GestorPremium(),
            new GestorAdmin()
        };

        Console.WriteLine("--- 1. Mensagens de Boas Vindas ---");
        foreach (var u in usuarios)
        {
            Console.WriteLine(u.GerarBoasVindas("Visitante"));
        }

        // 2. Capacidade Específica: Financeiro
        // Isso simula o "Módulo de Faturamento"
        Console.WriteLine("\n--- 2. Processo de Cobrança ---");
        
        IFinanceiro pagante = new GestorPremium();
        pagante.CobrarAssinaturaMensal();

        // O compilador PROTEGE o código abaixo. Se descomentar, não compila!
        // IFinanceiro invalido = new GestorPadrao(); 
        // Console.WriteLine("Isso nem compila, garantindo segurança.");

        // 3. Capacidade Específica: Admin
        Console.WriteLine("\n--- 3. Área Administrativa ---");
        IAdministrativo admin = new GestorAdmin();
        admin.BanirUsuarioDoSistema(99);
    }
}