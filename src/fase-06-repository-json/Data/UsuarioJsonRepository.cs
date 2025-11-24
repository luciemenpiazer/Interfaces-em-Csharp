using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Fase06.Domain;

namespace Fase06.Data
{
    public sealed class UsuarioJsonRepository : IRepository<Usuario, int>
    {
        private readonly string _path;
        
        // Opções de serialização (Padronização sugerida no PDF Lousa)
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true, // Deixa o JSON legível (bonito)
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Padrão JS (id, nome)
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public UsuarioJsonRepository(string path)
        {
            _path = path;
            // Garante que o arquivo exista com um array vazio válido "[]"
            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "[]");
            }
        }

        // --- Helpers Privados (Load/Save) ---

        private List<Usuario> Load()
        {
            if (!File.Exists(_path)) return new List<Usuario>();
            
            var json = File.ReadAllText(_path);
            if (string.IsNullOrWhiteSpace(json)) return new List<Usuario>();

            try
            {
                return JsonSerializer.Deserialize<List<Usuario>>(json, _options) ?? new List<Usuario>();
            }
            catch (JsonException)
            {
                // Se o JSON estiver corrompido, retorna lista vazia ou lança erro (decisão de design)
                return new List<Usuario>();
            }
        }

        private void Save(List<Usuario> list)
        {
            var json = JsonSerializer.Serialize(list, _options);
            File.WriteAllText(_path, json);
        }

        // --- Implementação do Contrato ---

        public Usuario Add(Usuario entity)
        {
            var list = Load();
            // Validação simples de unicidade
            if (list.Any(u => u.Id == entity.Id))
                throw new InvalidOperationException($"ID {entity.Id} já existe.");

            list.Add(entity);
            Save(list);
            return entity;
        }

        public IReadOnlyList<Usuario> ListAll() => Load();

        public Usuario? GetById(int id) => Load().FirstOrDefault(u => u.Id == id);

        public bool Update(Usuario entity)
        {
            var list = Load();
            var index = list.FindIndex(u => u.Id == entity.Id);

            if (index < 0) return false;

            list[index] = entity;
            Save(list);
            return true;
        }

        public bool Remove(int id)
        {
            var list = Load();
            var removedCount = list.RemoveAll(u => u.Id == id);

            if (removedCount > 0)
            {
                Save(list);
                return true;
            }
            return false;
        }
    }
}