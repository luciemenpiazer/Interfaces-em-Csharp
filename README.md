# Tarefa por Fases - Interfaces em C#

Este repositório documenta a evolução de um projeto, fase por fase, aplicando conceitos de design de software em C#.

## 🎯 Objetivo Geral
Consolidar a jornada procedural → OO → interfaces e repository, com foco em **design consciente** (ISP, baixo acoplamento, testabilidade).

## 👥 Equipe
| Nome | RA |
| --- | --- |
| Leticia Moro | 2602008 |
| Luciemen Piazer | 2652609 |

---

## 🗺️ Sumário de Fases
Abaixo estão os links para a documentação e para o código-fonte de cada etapa:

* [Fase 00: Aquecimento Conceitual](#fase-00) | 📂 [Código Fonte](./src/fase-00-aquecimento/)
* [Fase 01: Procedural](#fase-01) | 📂 [Código Fonte](./src/fase-01-procedural/)
* [Fase 02: OO Sem Interface](#fase-02) | 📂 [Código Fonte](./src/fase-02-oo-sem-interface/)
* [Fase 03: OO Com Interface](#fase-03) | 📂 [Código Fonte](./src/fase-03-com-interfaces/)
* [Fase 04: Repository InMemory](#fase-04) | 📂 [Código Fonte](./src/fase-04-repository-inmemory/)
* [Fase 04: Repository csv](#fase-05) | 📂 [Código Fonte](./src/fase-05-repository-csv/)
* [Fase 04: Repository json](#fase-06) | 📂 [Código Fonte](./src/fase-05-repository-json/)

---

### <a id="fase-00"></a> 🔥 Fase 00: Aquecimento Conceitual
**Foco:** Entender contratos de capacidade sem código.

#### 💡 Decisões de Design
* Definição de cenários do cotidiano onde o objetivo é fixo mas as "peças" variam.
* **Cenário 1 (Pagamento):** O contrato é "Pagar", as implementações são "Pix", "Cartão" ou "Boleto".
* **Cenário 2 (Entrega):** O contrato é "Entregar Relatório", as peças são "PDF por E-mail" ou "CSV no Disco".

---

### <a id="fase-01"></a> 🔨 Fase 01: Procedural
**Foco:** Implementação de lógica centralizada com `switch`.

#### 💡 Decisões de Design
* Uso de `Switch Expressions` (C# 8) para mapear tipos de usuário.
* Identificação do **acoplamento**: toda a regra de negócio reside em um único método estático.

#### ✅ Checklist de Qualidade
* [x] Código funcional.
* [x] Identificação clara de onde o princípio OCP (Aberto/Fechado) é violado.

---

### <a id="fase-02"></a> 🧱 Fase 02: OO Sem Interface
**Foco:** Substituição de condicional por polimorfismo (Herança).

#### 💡 Decisões de Design
* **Remoção de Condicionais:** O `switch` foi removido; a lógica agora pertence a classes concretas (`PremiumGerador`, `AdminGerador`).
* **Herança:** Uso de classe base abstrata `GeradorMensagem` para definir o contrato.
* **Rigidez:** Percebemos que o cliente ainda precisa dar `new ClasseConcreta()`, mantendo acoplamento.

#### ✅ Checklist de Qualidade
* [x] Switch removido da regra de negócio.
* [x] Alta coesão (cada classe faz apenas uma coisa).
* [x] Classes folhas marcadas como `sealed`.

#### 📸 Evidências de Testes
Abaixo, o log de execução comprovando o comportamento polimórfico. Observe que não passamos mais uma string de "tipo", mas sim instanciamos classes diferentes que respondem ao mesmo método `.Gerar()`:

```text
--- Testando Orientação a Objetos (Fase 02) ---
[Premium] Parabéns, Luciemen, seu acesso Premium foi ativado!
[Admin]   Olá, Admin Leticia. Pronta para gerenciar o sistema?
[VIP]     Seja muito bem-vindo(a), Ana. Sua experiência VIP começa agora!
[Padrão]  Bem-vindo(a), Visitante.
```

---

### <a id="fase-03"></a> 🔌 Fase 03: OO Com interface
**Foco:** Desacoplamento total usando Contratos (`interface`) e Injeção de Dependência.

#### 💡 Decisões de Design
* **Inversão de Controle:** O `Notificador` não cria mais instâncias (`new`), ele as recebe no construtor.
* **Testabilidade:** Criação de um `GeradorDuble` (Stub) para validar o fluxo sem depender de lógica de negócio real.
* **Contrato:** A interface `IGeradorMensagem` define o "o quê", enquanto as classes definem o "como".

#### ✅ Checklist de Qualidade
* [x] Cliente (`Notificador`) não depende de classes concretas.
* [x] Possibilidade de trocar a implementação sem alterar o cliente.
* [x] Teste com dublê executado com sucesso.

#### 📸 Evidências de Testes
```text
--- Fase 03: Interfaces e Injeção de Dependência ---
[Notificação]: Parabéns, Luciemen, seu acesso Premium foi ativado!
[Notificação]: Olá, Admin Leticia. Sistema pronto.
```

---

---

### <a id="fase-04"></a> 💾 Fase 04: Repository InMemory
**Foco:** Centralização do acesso a dados usando o padrão **Repository** para desacoplar o domínio da persistência.

#### 💡 Decisões de Design
* [cite_start]**Padrão Repository:** Criação de um contrato genérico `IRepository<T, TId>` [cite: 507-512]. Isso permite trocar a persistência (Memória → CSV → SQL) sem quebrar a regra de negócio.
* [cite_start]**Persistência em Memória:** Implementação técnica usando `Dictionary<TId, T>` para simular um banco de dados com acesso rápido O(1) [cite: 513-519].
* **Camada de Serviço:** Introdução do `UsuarioService` para orquestrar as chamadas. [cite_start]O `Program.cs` (Cliente) conversa com o Service, e o Service conversa com o Repository, garantindo a inversão de dependência [cite: 621-622].
* [cite_start]**Proteção de Estado:** O método `ListAll()` retorna `IReadOnlyList`, impedindo que consumidores modifiquem a coleção interna do repositório inadvertidamente[cite: 32].

#### ✅ Checklist de Qualidade
* [x] Contrato `IRepository` não expõe detalhes de implementação (como `List` ou `Dictionary`).
* [x] Cliente não acessa dados diretamente, apenas via métodos do repositório.
* [cite_start][x] Testes de unidade cobrem inserção e busca sem depender de disco/IO [cite: 567-568].

#### 📸 Evidências de Testes
Log de execução mostrando o fluxo completo: Composição (Program) → Serviço → Repositório (Salva) → Serviço → Interface (Notifica).

```text
=== Fase 04: Repository InMemory ===

--- Cadastrando via Service ---
[Repository] Usuário 1 salvo em memória.
[Repository] Usuário 2 salvo em memória.

--- Notificando Usuários ---
[Premium] Olá PREMIUM Luciemen, confira ofertas exclusivas!
[Padrão]  Olá João, assine o premium hoje!

Total de usuários ativos: 2
```

---

### <a id="fase-05"></a> 📄 Fase 05: Repository CSV
[cite_start]**Foco:** Evolução do armazenamento para **persistência em disco** (arquivo CSV), mantendo o desacoplamento via contrato `IRepository` [cite: 102-104].

#### 💡 Decisões de Design
* [cite_start]**Persistência Física:** Implementação de `UsuarioCsvRepository` lendo e escrevendo em `banco_usuarios.csv` com separador ponto e vírgula (`;`), garantindo que os dados sobrevivam ao reinício da aplicação[cite: 105, 109].
* [cite_start]**Estratégia de Escrita:** Uso de `AppendAllText` para inserções rápidas (`Add`) e reescrita total do arquivo para operações de edição (`Update/Remove`), superando a volatilidade da memória RAM [cite: 148-154].
* [cite_start]**Resiliência de I/O:** Verificação defensiva que cria o arquivo automaticamente caso ele não exista na primeira execução, evitando exceções de `FileNotFound` [cite: 144-145].
* [cite_start]**Transparência no Cliente:** A troca de `InMemory` para `Csv` ocorre apenas na inicialização (`Program.cs`), sem alterar nenhuma linha de código do consumidor ou do serviço [cite: 309-311].

#### ✅ Checklist de Qualidade
* [cite_start][x] Contrato `IRepository` mantido estritamente igual à fase anterior[cite: 123].
* [x] Dados persistem corretamente após fechar e abrir o programa.
* [x] Sistema trata arquivo inexistente sem travar (criação automática).
* [cite_start][x] Cliente desconhece se está usando memória ou arquivo (inversão de dependência)[cite: 43].

#### 📸 Evidências de Testes
```text
=== Fase 7: ISP na Prática ===

--- Mensagens de Boas Vindas ---
Bem-vindo(a), UsuárioTeste.
Parabéns, UsuárioTeste, acesso Premium!
Olá Admin UsuárioTeste.

--- Processo de Cobrança ---
Cobrança efetuada com sucesso (via cartão).

--- Área Administrativa ---
USUÁRIO 999 BANIDO DO SISTEMA.
```

---

### <a id="fase-06"></a> 📦 Fase 06: Repository JSON
**Foco:** Persistência estruturada com serialização, preservação de tipos e separação arquitetural entre **Domínio** e **Infraestrutura**.

#### 💡 Decisões de Design
* **Arquitetura em Camadas:** Separação física do código em pastas `Domain` (Contratos e Entidades Puras) e `Data` (Implementação do Repositório), isolando a regra de negócio de dependências de biblioteca externa.
* [cite_start]**Serialização Padronizada:** Uso de `System.Text.Json` com `JsonSerializerOptions` configurado para **camelCase** e **identação**, garantindo interoperabilidade e legibilidade humana no arquivo gerado [cite: 352-355].
* [cite_start]**Preservação de Tipos:** Diferente do CSV, o JSON mantém a tipagem original dos dados (números são tratados como números), eliminando a necessidade de *parsing* manual frágil [cite: 1247-1251].
* [cite_start]**Resiliência de Arquivo:** O repositório garante a existência de um array vazio `[]` válido caso o arquivo não exista, prevenindo erros de deserialização na primeira execução.

#### ✅ Checklist de Qualidade
* [x] Entidade `Usuario` (Domain) não possui referências a `System.Text.Json` (Pura).
* [x] Arquivo `banco_usuarios.json` é gerado com formatação legível (WriteIndented).
* [x] Troca de implementação no `Program.cs` feita sem alterar nenhuma linha da lógica de negócio.
* [x] Tratamento correto para arquivo inexistente ou vazio.

#### 📸 Evidências de Testes
```json
[
  { "id": 1, "nome": "Luciemen", "tipoAssinatura": "Premium" },
  { "id": 2, "nome": "Joao Silva", "tipoAssinatura": "Padrao" },
  { "id": 3, "nome": "Admin", "tipoAssinatura": "SuperUser" }
]
```

---

### <a id="fase-07"></a> 🧩 Fase 07: ISP na Prática
**Foco:** Refatoração de arquitetura aplicando o **Princípio da Segregação de Interfaces (ISP)** para eliminar contratos "gordos" e acoplamento desnecessário.

#### 💡 Decisões de Design
* [cite_start]**Segregação por Capacidade:** O contrato monolítico `IGestorUsuarioCompleto` foi fatiado em 3 interfaces coesas no **Domínio**: `IGeradorMensagem`, `IFinanceiro` e `IAdministrativo`[cite: 121, 371].
* **Evolução Arquitetural:** O código da solução foi organizado em pastas `Domain` (para os contratos) e `Implementacoes` (para as classes concretas), separando claramente as abstrações das concretizações.
* **Segurança de Tipos:** A refatoração moveu a detecção de erros do *runtime* (exceções) para o *compile-time*. Agora é impossível passar um `GestorPadrao` para um método que exige `IFinanceiro`.
* [cite_start]**Comparativo Didático:** O projeto mantém a pasta `Violacao` (código legado) isolada da `Solucao`, permitindo a comparação direta entre o anti-pattern e a boa prática[cite: 41, 125].

#### ✅ Checklist de Qualidade
* [x] Eliminação total de `throw new NotImplementedException` nas classes da solução.
* [x] Classes dependem apenas das interfaces que realmente utilizam (Coesão).
* [x] Consumo polimórfico seguro demonstrado no `Program.cs`.

#### 📸 Evidências de Testes
```text
=== Fase 7: ISP na Prática ===

--- 1. Mensagens de Boas Vindas ---
Bem-vindo(a), Visitante.
Parabéns, Visitante, acesso Premium!
Olá Admin Visitante.

--- 2. Processo de Cobrança ---
Cobrança efetuada com sucesso.

--- 3. Área Administrativa ---
Usuário 99 banido pelo Administrador.
```

---