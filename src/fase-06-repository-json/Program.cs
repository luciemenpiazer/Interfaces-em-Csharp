using System;
using System.IO;
using Fase06.Domain;
using Fase06.Data;

class Program
{
    static void Main()
    {
        // Define o caminho do arquivo JSON na raiz de execução
        var path = Path.Combine(AppContext.BaseDirectory, "banco_usuarios.json");
        Console.WriteLine($"Persistência JSON em: {path}\n");

        // 1. COMPOSIÇÃO: Trocamos CsvRepository por JsonRepository
        // O tipo da variável continua sendo a interface IRepository
        IRepository<Usuario, int> repo = new UsuarioJsonRepository(path);

        // 2. Carga Inicial (Seed)
        if (repo.ListAll().Count == 0)
        {
            Console.WriteLine("Banco vazio. Criando dados iniciais...");
            repo.Add(new Usuario(1, "Luciemen", "Premium"));
            repo.Add(new Usuario(2, "Joao", "Padrao"));
            repo.Add(new Usuario(3, "Admin", "SuperUser"));
        }
        else
        {
            Console.WriteLine("Dados carregados do disco (JSON).");
        }

        // 3. Consumo (Idêntico às fases anteriores)
        Console.WriteLine("\n--- Usuários Ativos ---");
        foreach (var u in repo.ListAll())
        {
            Console.WriteLine(u);
        }

        // Teste de Update
        var user = repo.GetById(2);
        if (user != null)
        {
            user.Nome = "Joao Silva";
            repo.Update(user);
            Console.WriteLine($"\nUsuário 2 atualizado para: {user.Nome}");
        }
    }
}