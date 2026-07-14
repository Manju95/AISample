.PHONY: help up down db migrate api logs clean rebuild

# Load .env for use in make targets
include .env
export

help: ## Show this help
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | \
		awk 'BEGIN {FS = ":.*?## "}; {printf "  \033[36m%-15s\033[0m %s\n", $$1, $$2}'

up: ## Start all services (db + migrations + api)
	docker compose up --build -d

down: ## Stop all services
	docker compose down

db: ## Start only Postgres
	docker compose up postgres -d

migrate: ## Run Flyway migrations against running Postgres
	docker compose run --rm flyway \
		-url=jdbc:postgresql://postgres:5432/$(POSTGRES_DB) \
		-user=$(POSTGRES_USER) \
		-password=$(POSTGRES_PASSWORD) \
		-locations=filesystem:/flyway/sql \
		migrate

migrate-info: ## Show migration status
	docker compose run --rm flyway \
		-url=jdbc:postgresql://postgres:5432/$(POSTGRES_DB) \
		-user=$(POSTGRES_USER) \
		-password=$(POSTGRES_PASSWORD) \
		-locations=filesystem:/flyway/sql \
		info

api: ## Start only the API (assumes DB is running)
	docker compose up api --build -d

clean: ## Stop and remove all containers + volumes
	docker compose down -v --remove-orphans

rebuild: ## Full teardown and fresh start
	$(MAKE) clean
	$(MAKE) up

psql: ## Open a psql shell inside Postgres container
	docker exec -it my-api-postgres psql -U $(POSTGRES_USER) -d $(POSTGRES_DB)