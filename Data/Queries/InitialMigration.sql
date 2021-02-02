CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Clientes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Clientes" PRIMARY KEY AUTOINCREMENT,
    "Name" VARCHAR(80) NOT NULL,
    "Telefone" CHAR(12) NULL,
    "CEP" CHAR(8) NOT NULL,
    "Estado" CHAR(2) NOT NULL,
    "Cidade" TEXT NOT NULL
);

CREATE TABLE "Produtos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Produtos" PRIMARY KEY AUTOINCREMENT,
    "CodigoDeBarras" VARCHAR(60) NULL,
    "Descricao" VARCHAR(60) NULL,
    "Valor" TEXT NOT NULL,
    "TipoProduto" TEXT NOT NULL,
    "Ativo" INTEGER NOT NULL
);

CREATE TABLE "Pedidos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Pedidos" PRIMARY KEY AUTOINCREMENT,
    "ClientId" INTEGER NOT NULL,
    "ClienteId" INTEGER NULL,
    "IniciadoEm" TEXT NOT NULL DEFAULT (getdate()),
    "FinalizadoEm" TEXT NOT NULL,
    "TipoFrete" TEXT NOT NULL,
    "Status" TEXT NOT NULL,
    "Observacao" VARCHAR(512) NULL,
    CONSTRAINT "FK_Pedidos_Clientes_ClienteId" FOREIGN KEY ("ClienteId") REFERENCES "Clientes" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "PedidoItens" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_PedidoItens" PRIMARY KEY AUTOINCREMENT,
    "PedidoId" INTEGER NOT NULL,
    "ProdutoId" INTEGER NOT NULL,
    "Quantidade" INTEGER NOT NULL DEFAULT 1,
    "Valor" TEXT NOT NULL,
    "Desconto" TEXT NOT NULL,
    CONSTRAINT "FK_PedidoItens_Pedidos_PedidoId" FOREIGN KEY ("PedidoId") REFERENCES "Pedidos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PedidoItens_Produtos_ProdutoId" FOREIGN KEY ("ProdutoId") REFERENCES "Produtos" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Clientes_Telefone" ON "Clientes" ("Telefone");

CREATE INDEX "IX_PedidoItens_PedidoId" ON "PedidoItens" ("PedidoId");

CREATE INDEX "IX_PedidoItens_ProdutoId" ON "PedidoItens" ("ProdutoId");

CREATE INDEX "IX_Pedidos_ClienteId" ON "Pedidos" ("ClienteId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210202201744_InitialMigration', '5.0.2');

COMMIT;

