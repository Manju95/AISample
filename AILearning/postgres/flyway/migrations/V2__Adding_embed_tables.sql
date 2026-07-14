CREATE TABLE documents
(
    id UUID PRIMARY KEY,
    file_name TEXT NOT NULL,
    uploaded_at TIMESTAMPTZ NOT NULL,
    total_chunks INT NOT NULL
);

CREATE EXTENSION IF NOT EXISTS vector;

CREATE TABLE document_chunks
(
    id UUID PRIMARY KEY,
    document_id UUID NOT NULL REFERENCES documents(id),
    chunk_number INT NOT NULL,
    content TEXT NOT NULL
);

CREATE TABLE chunk_embeddings
(
    chunk_id UUID PRIMARY KEY REFERENCES document_chunks(id),
    embedding VECTOR(768),
    embedding_model TEXT NOT NULL,
    created_at TIMESTAMPTZ NOT NULL
);

CREATE INDEX idx_chunk_embeddings ON chunk_embeddings
    USING hnsw (embedding vector_cosine_ops);