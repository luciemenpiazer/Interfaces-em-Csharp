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
Arquivo de banco: .../bin/Debug/net8.0/banco_usuarios.csv

Carregando usuários do arquivo... (Persistência Confirmada)

--- Lista de Usuários ---
1 - Luciemen (Premium)
2 - Joao (Padrao)
```

---