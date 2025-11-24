# Fase 7 - ISP na Prática (Segregação de Interfaces)

## 1. Visão Geral
O objetivo desta fase é aplicar o **Princípio da Segregação de Interfaces (ISP)** para corrigir uma violação de design onde uma interface "gorda" (onipotente) forçava classes a dependerem de métodos que não utilizavam.

Diferente das fases anteriores de persistência, este exercício foca na **arquitetura e coesão** dos contratos.

**Status da Fase:** Concluído :heavy_check_mark:

---

## 2. Análise do Problema (Antes)
**Local:** `src/fase-07-isp/Violacao/`

O sistema utilizava uma interface única chamada `IGestorUsuarioCompleto`. Isso gerava os seguintes problemas:
* **Acoplamento Desnecessário:** A classe `GestorPadrao` era obrigada a conhecer métodos de cobrança e banimento.
* **Poluição de Código:** Implementações cheias de `throw new NotImplementedException`.
* **Insegurança:** Um consumidor poderia chamar `CobrarAssinaturaMensal` em um usuário padrão, causando erro em tempo de execução (runtime).

---

## 3. A Solução (Depois)
**Local:** `src/fase-07-isp/Solucao/`

Aplicamos a segregação quebrando o contrato único em três capacidades coesas (Interfaces pequenas):

1.  **`IGeradorMensagem`:** Capacidade básica de saudação (Todos implementam).
2.  **`IFinanceiro`:** Capacidade de cobrar assinaturas (Apenas `GestorPremium` implementa).
3.  **`IAdministrativo`:** Capacidade de gestão/banimento (Apenas `GestorAdmin` implementa).

### Ganhos Obtidos
* **Segurança de Tipos:** O compilador agora impede que um `GestorPadrao` seja passado para um contexto financeiro. O erro é pego em tempo de compilação, não de execução.
* **Código Limpo:** Remoção total das exceções `NotImplementedException`.
* **Flexibilidade:** Módulos consumidores (ex: Faturamento) dependem apenas da interface `IFinanceiro`, aceitando qualquer classe que cumpra esse contrato específico, sem saber se é um Premium, um SuperUser ou um Bot de Pagamento.

---

## 4. Estrutura de Pastas

* **`Violacao/`**: Contém o código original (anti-pattern) para fins de comparação didática.
* **`Solucao/`**: Contém a refatoração final seguindo o SOLID.
* **`Program.cs`**: Demonstra o consumo da **Solução**, provando a segurança de tipos.

---

## 5. Como Executar

Para ver a demonstração da solução segura em funcionamento:

```bash
dotnet run --project src/fase-07-isp