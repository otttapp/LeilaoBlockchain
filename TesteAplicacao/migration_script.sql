IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250613164335_mssql.onprem_migration_894'
)
BEGIN
    CREATE TABLE [produto] (
        [produto_id] bigint NOT NULL IDENTITY,
        [nome] nvarchar(100) NOT NULL,
        [ativo] bit NOT NULL,
        [descricao] nvarchar(255) NULL,
        [data_compra] datetime2 NULL,
        [datahora_insercao] datetime2 NULL,
        [valor] decimal(18,2) NOT NULL,
        [raridade] bigint NOT NULL,
        CONSTRAINT [PK_produto] PRIMARY KEY ([produto_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250613164335_mssql.onprem_migration_894'
)
BEGIN
    CREATE TABLE [usuario] (
        [usuario_id] bigint NOT NULL IDENTITY,
        [nome] nvarchar(100) NOT NULL,
        [email] nvarchar(100) NULL,
        [telefone] nvarchar(15) NULL,
        [ativo] bit NOT NULL DEFAULT CAST(1 AS bit),
        [datahora_insercao] datetime2 NULL DEFAULT (CURRENT_TIMESTAMP),
        [datahora_desativacao] datetime2 NULL,
        [senha_hash] varbinary(64) NOT NULL,
        [senha_salt] varbinary(64) NOT NULL,
        CONSTRAINT [PK_usuario] PRIMARY KEY ([usuario_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250613164335_mssql.onprem_migration_894'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250613164335_mssql.onprem_migration_894', N'9.0.5');
END;

COMMIT;
GO

