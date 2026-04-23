# 🚗 Endless Runner Repsol

Jogo desenvolvido no âmbito da unidade curricular de **Tecnologias e Multimédia**, do curso de **Engenharia Informática** — **IPVC / ESTG**.

---

## 👥 Elementos do Grupo

| Nome | Número de Aluno |
|------|----------------|
| Gonçalo Sousa | 29726 |
| Miguel Fonseca | 29725 |

---

## 🛠️ Versão do Unity

Este projeto foi desenvolvido com a versão **Unity 6000.3.9f1**.

---

## 📖 Descrição do Jogo

**Endless Runner Repsol** é um jogo 3D de endless runner no qual o jogador controla um carro numa estrada infinita, tendo de desviar de obstáculos e de veículos controlados por IA, acumular distância percorrida e gerir o combustível disponível.

### Funcionalidades Implementadas

- **Movimento e física do carro** - controlo de aceleração, travagem e direção.
- **Câmara Cinemachine** - câmara de follow com deteção de colisão para evitar que atravesse a geometria da cena.
- **Nível endless aleatório** - geração procedural de secções de estrada, com reciclagem de segmentos para manter o desempenho.
- **Mundo curvo** - efeito visual de curvatura do terreno à distância.
- **Árvores aleatorizadas** - objetos de cenário ao longe gerados e posicionados aleatoriamente para aumentar a sensação de variedade.
- **Veículos NPC com IA** - carros adversários que surgem na estrada e circulam de forma autónoma, servindo de obstáculos dinâmicos.
- **Explosão do carro** - efeito de explosão ativado em colisão, que desencadeia o fim do jogo.
- **Iluminação e pós-processamento** - configuração de luzes e pipeline de pós-processamento para melhorar a qualidade visual.
- **Efeitos sonoros** - sons de motor, derrapagem e outros eventos de jogo (formatos MP3 e WAV).
- **Interface** - HUD que exibe em tempo real a distância acumulada pelo jogador e combustível restante.
- **Sistema de combustível** - o carro consome combustível ao longo do tempo; o jogador pode recolher galões de gasolina espalhados pela pista para reabastecer.
- **Seletor de carro** - menu principal com seleção de veículo antes de iniciar a partida.
- **Ecrã de Game Over** - UI dedicado apresentado após a colisão, com informação da distância percorrida e opção de reinício.

---

## 🎮 Jogabilidade

### Objetivo

Percorrer a maior distância possível sem colidir com obstáculos ou veículos NPC, e sem ficar sem combustível.

### Controlos

| Tecla | Ação |
|-------|------|
| `W` | Acelerar |
| `S` | Travar |
| `A` | Virar à esquerda |
| `D` | Virar à direita |
| `R` | Reiniciar o jogo |

### Regras

- O jogador controla a direção e velocidade.
- Colidir com um obstáculo ou veículo NPC provoca uma explosão e termina o jogo.
- O nível de combustível diminui com o tempo; recolher galões de gasolina repõe o combustível.
- Ficar sem combustível termina o jogo.
- A distância percorrida é registada e apresentada no ecrã de Game Over.

---

## 🚀 Como Abrir o Projeto

### Pré-requisitos

- [Unity Hub](https://unity.com/download) instalado.
- Unity versão **6000.3.9f1** instalada através do Unity Hub.

### Passos

1. Clonar o repositório:
   ```bash
   git clone https://github.com/m1guelfonseca/Endless-Runner-Repsol.git
   ```
2. Abrir o **Unity Hub**.
3. Clicar em **Add** → **Add project from disk** e selecionar a pasta clonada.
4. Garantir que a versão do editor selecionada é a **6000.3.9f1**.
5. Abrir o projeto e, no painel **Project**, navegar até `Assets/Scenes` e abrir a cena `Main menu` .
6. Clicar em **Play** para correr o jogo no editor.

---

## 🎨 Assets Multimédia

### Texturas

| Formato | Utilização | Justificação |
|---------|-----------|--------------|
| PNG | Texturas de carros, cenário e UI | Formato sem perdas, com suporte a transparência (canal alpha); ideal para elementos de interface e objetos com detalhes finos |

### Sons

| Formato | Utilização | Justificação |
|---------|-----------|--------------|
| MP3 | Música de fundo / sons de motor | Compressão com perdas aceitável para faixas longas; reduz o tamanho do projeto sem impacto percetível na qualidade |
| WAV | Efeitos sonoros curtos (derrapagem, explosão, recolha de combustível) | Sem compressão, latência mínima; adequado para sons de curta duração onde a qualidade e a sincronia são críticas |

---

## ⚠️ Observações e Limitações Conhecidas

Não foram identificadas lacunas relevantes.

---

## 🌿 Estratégia de Branches

| Branch | Propósito |
|--------|----------|
| `main` | Código estável, pronto para entrega |
| `develop` | Branch de desenvolvimento ativo |
| `feature/*` | Funcionalidades ou correções individuais |

> ⚠️ **Nunca fazer push diretamente para `main`.**

---

## ✍️ Convenções de Commits

Os commits são escritos em **inglês**.

| Prefixo | Utilização |
|---------|-----------|
| `feat:` | Nova funcionalidade |
| `fix:` | Correção de bug |
| `refactor:` | Melhoria interna |
| `docs:` | Alterações à documentação |
| `chore:` | Tarefas técnicas / alterações triviais |

---

## 📄 Contexto Académico

Projeto desenvolvido para fins académicos no âmbito da unidade curricular de **Tecnologias e Multimédia**, do curso de **Engenharia Informática** — **IPVC / ESTG**.
