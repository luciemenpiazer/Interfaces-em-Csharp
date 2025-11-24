# Fase 7 - ISP na Prática (Segregação de Interfaces)

## 1. Visão Geral
Esta fase foca na aplicação do **Princípio da Segregação de Interfaces (ISP)**. O objetivo é corrigir o problema de interfaces "gordas" (que fazem coisas demais) quebrando-as em contratos menores e mais coesos.

Seguindo a evolução arquitetural do projeto, organizamos o código separando as definições de **Domínio** (Interfaces) das **Implementações** concretas.

**Status da Fase:** Concluído :heavy_check_mark:

---

## 2. Decisões de Design

### Arquitetura em Camadas (Domain vs Implementação)
Aplicando o conceito introduzido na fase anterior:
* **`Violacao/`**: Mantivemos o código legado (Anti-pattern) isolado para fins de comparação didática.
* **`Solucao/Domain/`**: Contém apenas as Interfaces (`IGeradorMensagem`, `IFinanceiro`, `IAdministrativo`). É o contrato puro.
* **`Solucao/Implementacoes/`**: Contém as classes (`GestorPadrao`, etc.) que compõem uma ou mais interfaces do domínio.

### A Refatoração (ISP)
Substituímos a interface monolítica `IGestorUsuarioCompleto` por três interfaces de capacidade específica.
* **Antes:** `GestorPadrao` lançava `NotImplementedException` para métodos de cobrança e banimento.
* **Depois:** `GestorPadrao` implementa apenas `IGeradorMensagem`. O código é limpo e seguro em tempo de compilação.

---

## 3. Como Executar

```bash
dotnet run --project src/fase-07-isp
```

### Evidência de Saída (Console)

```
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

## 4. Checklist de Qualidade
[x] Remoção de Exceções: Classes na pasta Solucao não possuem throw new NotImplementedException.

[x] Segurança de Tipos: O código cliente (Program.cs) demonstra que é impossível atribuir um GestorPadrao a uma variável IFinanceiro.

[x] Organização: Interfaces segregadas na pasta Domain e classes na pasta Implementacoes.

