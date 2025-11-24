# Fase 07 - ISP na Prática (Segregação de Interfaces)

## 1. Visão Geral
O objetivo desta fase é aplicar o **Princípio da Segregação de Interfaces (ISP)**, o "I" do SOLID. O foco aqui não é persistência, mas sim corrigir uma falha de design onde uma interface "gorda" (God Interface) forçava classes a dependerem de métodos que não utilizavam.

[cite_start]A refatoração demonstra como transformar um contrato rígido e perigoso em contratos pequenos e coesos [cite: 121-122].

**Status da Fase:** Concluído :heavy_check_mark:

---

## 2. Análise do Problema (Antes)
**Local:** `src/fase-07-isp/Violacao/`

O sistema utilizava uma interface única chamada `IGestorUsuarioCompleto`. Isso gerava os seguintes problemas:

* [cite_start]**Violação do ISP:** A classe `GestorPadrao` era obrigada a implementar `CobrarAssinaturaMensal` e `BanirUsuarioDoSistema`, mesmo sem ter permissão para isso [cite: 123-125].
* **Poluição de Código:** O código estava repleto de `throw new NotImplementedException`, um "code smell" claro de abstração incorreta.
* **Insegurança (Runtime):** Se um consumidor chamasse o método de cobrança em um gestor padrão, o programa quebraria durante a execução.

---

## 3. A Solução (Depois)
**Local:** `src/fase-07-isp/Solucao/`

[cite_start]Aplicamos a segregação quebrando o contrato monolítico em três capacidades distintas (Interfaces de Capacidade) [cite: 145-146]:

1. **`IGeradorMensagem`** – Capacidade de gerar boas-vindas (Implementada por todos).
2. **`IFinanceiro`** – Capacidade de cobrar assinaturas (Exclusiva para `GestorPremium`).
3. **`IAdministrativo`** – Capacidade de banir usuários (Exclusiva para `GestorAdmin`).

### Ganhos Obtidos
* **Segurança de Tipos (Compile-time):** O compilador agora impede que um objeto `GestorPadrao` seja passado para um método que espera `IFinanceiro`. [cite_start]O erro é detectado antes mesmo de rodar [cite: 127].
* **Coesão:** Cada classe implementa apenas os contratos que fazem sentido para o seu contexto.
* **Flexibilidade:** Módulos consumidores dependem apenas da capacidade que precisam (ex: um módulo de marketing pede apenas `IGeradorMensagem`), desacoplando-se da implementação concreta.

---

## 4. Estrutura de Arquivos

Para fins didáticos, mantivemos o "antes" e o "depois" no mesmo projeto:

```text
src/fase-07-isp/
├── Violacao/                  # Código legado (Anti-pattern)
│   ├── IGestorUsuarioCompleto.cs
│   └── GestorPadrao.cs
├── Solucao/                   # Código refatorado (SOLID)
│   ├── Contratos/             # Interfaces segregadas (Magras)
│   └── Implementacoes/        # Classes coesas
├── Program.cs                 # Demonstração de consumo seguro
└── README.md                  # Documentação da fase
```

---

## 5. Como Executar

O `Program.cs` foi configurado para executar o cenário da Solução, demonstrando a segurança de tipos:

```bash
dotnet run --project src/fase-07-isp
```

### Evidência de Saída (Console)

```plaintext
Banco vazio. Criando dados iniciais...

--- Usuários Ativos ---
1 - Luciemen (Premium)
2 - Joao (Padrao)
3 - Admin (SuperUser)

Usuário 2 atualizado para: Joao Silva
```

---

## 6. Checklist de Qualidade

- [x] **Remoção de Exceções:** Nenhuma classe da pasta `Solucao` lança `NotImplementedException` por obrigatoriedade de interface.
- [x] **Interfaces Coesas:** Cada interface definida em `Solucao/Contratos` possui apenas métodos estritamente relacionados (uma única responsabilidade/capacidade).
- [x] **Composição:** Classes complexas (`GestorAdmin`) implementam múltiplas interfaces, enquanto classes simples (`GestorPadrao`) implementam apenas uma.
