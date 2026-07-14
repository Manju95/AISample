# AILearning — Modular RAG Platform with .NET 8

> A production-style Retrieval-Augmented Generation (RAG) backend built with .NET 8, designed from the ground up to be **LLM-agnostic**, **provider-swappable**, and **SOLID-compliant**.

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?logo=docker)](https://www.docker.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-pgvector-336791?logo=postgresql)](https://www.postgresql.org/)
[![Ollama](https://img.shields.io/badge/Ollama-Local%20LLM-000000)](https://ollama.com/)

---

## 🧭 Overview

This project is a full RAG (Retrieval-Augmented Generation) pipeline — document upload, chunking, embedding, vector search, and chat — implemented as a clean, feature-sliced .NET 8 API. It currently integrates with **Ollama** for local LLM inference, but the architecture was deliberately built so that **any LLM provider can be swapped in with minimal code changes** — no rewrites of business logic, no leaking provider-specific types across the codebase.

It's part personal learning project, part reusable RAG scaffold for future AI-powered applications.

---

## 🏗️ Architecture

The project follows **Vertical Slice Architecture** instead of a traditional layered (Controller → Service → Repository) structure. Each feature owns its own controllers, models, services, and repositories — reducing coupling between unrelated features and making the codebase easier to navigate and extend.

```
Features/
├── Chat/            # Conversation orchestration & LLM providers
├── Chunking/         # Splits extracted text into embeddable chunks
├── DataExtractor/     # Extracts raw text/content from uploaded files
├── Embedding/        # Generates & stores vector embeddings (EF Core)
├── PromptBuilder/     # Constructs context-aware prompts for the LLM
├── UploadDocument/    # Handles document ingestion
└── VectorSearch/      # Similarity search over embedded documents (Dapper)
```

### Design Principles

- **SOLID throughout** — each service has a single responsibility, depends on abstractions, and is open for extension without modification.
- **Provider-agnostic LLM layer** — the `Chat/Providers` abstraction means swapping Ollama for OpenAI, Anthropic, or any other provider requires implementing one interface, not touching downstream code.
- **CQRS-flavored data access** — **EF Core** handles writes and structured storage (embedding documents), while **Dapper** is used for the read-heavy, performance-critical RAG vector search queries.
- **Database-first migrations** — schema is version-controlled and applied via **Flyway**, keeping SQL changes explicit and auditable rather than hidden behind ORM migrations.

---

## ⚙️ Tech Stack

| Layer | Technology |
|---|---|
| API | .NET 8 (ASP.NET Core Web API) |
| Database | PostgreSQL + pgvector |
| ORM (writes) | Entity Framework Core |
| Data Access (reads/RAG) | Dapper |
| Migrations | Flyway |
| LLM Runtime | Ollama (pluggable) |
| Containerization | Docker + Docker Compose |
| Tooling | Makefile for common workflows |

---

## 📂 Project Structure

```
AILearning/
├── Configuration/
├── Context/                  # DbContext & EF Core setup
├── Extensions/                # Service collection extensions (DI wiring)
├── Features/                  # Vertical slices (see architecture above)
├── postgres/
│   ├── flyway/migrations/     # Versioned SQL migration scripts
│   └── postgres-init/         # Extension bootstrap (e.g., pgvector)
├── Program.cs
├── Dockerfile
├── compose.yaml
├── Makefile
└── AILearning.sln
```

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker & Docker Compose](https://www.docker.com/)
- [Ollama](https://ollama.com/) running locally (or accessible remotely)

### Run with Docker Compose

```bash
docker compose up --build
```

This spins up:
- The .NET 8 API
- PostgreSQL with the pgvector extension enabled
- Flyway migrations applied automatically on startup

### Using the Makefile

Common commands are wrapped in the `Makefile` for convenience:

```bash
make up        # Start all services
make down      # Stop all services
make migrate   # Run Flyway migrations
make logs      # Tail service logs
```

---

## 🔄 RAG Pipeline

1. **Upload** — a document is submitted via the `UploadDocument` feature.
2. **Extract** — `DataExtractor` pulls raw text/content from the file.
3. **Chunk** — `Chunking` splits the text into retrieval-friendly segments.
4. **Embed** — `Embedding` generates vector embeddings and persists them via EF Core.
5. **Search** — `VectorSearch` performs similarity search over stored embeddings using Dapper for performance.
6. **Prompt** — `PromptBuilder` assembles the retrieved context into a structured prompt.
7. **Chat** — the `Chat` feature sends the prompt to the configured LLM provider (Ollama by default) and returns the response.

---

## 🔌 Swapping LLM Providers

The `Chat/Providers` folder abstracts LLM interaction behind a common interface. To integrate a new provider:

1. Implement the provider interface for the new LLM.
2. Register it in `ServiceCollectionExtension.cs`.
3. No other feature needs to change.

This was a core design goal: **the rest of the application should never need to know which LLM is answering the question.**

---

## 🗺️ Roadmap

- [ ] Support additional LLM providers (OpenAI, Anthropic, Azure OpenAI)
- [ ] Streaming chat responses
- [ ] Hybrid search (keyword + vector)
- [ ] Authentication & multi-tenant support
- [ ] Observability (structured logging, tracing)

---

## 📝 License

This project is for personal learning and portfolio purposes. Feel free to explore, fork, and adapt.

---

*Built with .NET 8 while exploring RAG architectures, vector databases, and clean backend design.*