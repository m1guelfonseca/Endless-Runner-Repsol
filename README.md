# 🚗 Endless Runner Repsol

A Unity-based endless runner game developed as part of the **Tecnologias e Multimédia** subject, within the **Engenharia Informática** degree.

---

## 📖 About

Endless Runner Repsol is a 3D endless runner where the player controls a car, dodging obstacles and competing for the highest score. The game features car physics, explosion effects, sound design with engine and skid audio, and AI-controlled vehicles.

---

## 🛠️ Built With

- [Unity](https://unity.com/) — Game Engine
- C# — Scripting Language

---

## 🚀 Getting Started

### Prerequisites

- Unity **2022.3 LTS** or newer
- Git

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/m1guelfonseca/Endless-Runner-Repsol.git
   ```
2. Open the project in Unity Hub.
3. Press **Play** to run the game in the editor.

---

## 🎮 How to Play

The player controls a car that moves forward automatically. Dodge obstacles and other vehicles to survive as long as possible.

| Key | Action |
|-----|--------|
| `W` | Accelerate |
| `S` | Brake |
| `A` | Steer left |
| `D` | Steer right |
| `R` | Restart the game |

> If you collide with an obstacle or another car, your vehicle explodes and the game ends.

---

## 🌿 Branch Strategy

| Branch | Purpose |
|--------|---------|
| `main` | Stable, production-ready code |
| `develop` | Active development branch |
| `feature/*` | Individual features or fixes |

> ⚠️ **Never push directly to `main`.**

---

## 🔀 Contributing

### Workflow

1. Create a branch from `develop`:
   ```bash
   git checkout -b feature/your-feature-name develop
   ```
2. Make your changes and commit following the conventions below.
3. Push your branch:
   ```bash
   git push origin feature/your-feature-name
   ```
4. Create a Pull Request targeting `develop`. After review, merge into `develop`. Never push directly to `main`.

---

## ✍️ Commit Conventions

Commits must be written in **English**.

| Prefix | Usage |
|--------|-------|
| `feat:` | New feature |
| `fix:` | Bug fix |
| `refactor:` | Internal improvement |
| `docs:` | Documentation changes |
| `chore:` | Technical tasks / trivial changes |

**Examples:**
```
feat: add car skid sound effect
fix: resolve rigidbody null reference on explosion
refactor: improve steer velocity clamping logic
docs: update README with branch strategy
chore: remove unused using directives
```

---

## 👥 Authors

- **Gonçalo Sousa** — [@goncalojbsousa](https://github.com/goncalojbsousa)
- **Miguel Fonseca** — [@m1guelfonseca](https://github.com/m1guelfonseca)

---

## 📄 License

This project was developed for academic purposes as part of the **Tecnologias e Multimédia** subject, within the **Engenharia Informática** degree.
