USE [practicaweb]
GO
/****** Object:  Table [dbo].[Parametros]    Script Date: 11/07/2024 04:59:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parametros](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Parametro] [varchar](255) NOT NULL,
	[Valor] [varchar](255) NOT NULL,
	[Estatus] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registros]    Script Date: 11/07/2024 04:59:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registros](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehiculoId] [int] NULL,
	[FechaHoraEntrada] [datetime] NULL,
	[FechaHoraSalida] [datetime] NULL,
	[CostoPorHora] [decimal](18, 2) NULL,
	[CostoTotal] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 11/07/2024 04:59:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Contrasena] [varchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehiculos]    Script Date: 11/07/2024 04:59:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehiculos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroDePlaca] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Registros]  WITH CHECK ADD FOREIGN KEY([VehiculoId])
REFERENCES [dbo].[Vehiculos] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[RegistroEntradaVehiculo]    Script Date: 11/07/2024 04:59:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistroEntradaVehiculo]
    @NumeroDePlaca VARCHAR(50),
    @FechaHoraEntrada DATETIME,
    @CostoPorHora DECIMAL(18, 2),
    @VehiculoId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si el vehículo ya existe
        IF EXISTS (SELECT 1 FROM Vehiculos WHERE NumeroDePlaca = @NumeroDePlaca)
        BEGIN
            -- Obtener el ID del vehículo existente
            SELECT @VehiculoId = Id FROM Vehiculos WHERE NumeroDePlaca = @NumeroDePlaca;
        END
        ELSE
        BEGIN
            -- Insertar en la tabla Vehiculos si no existe
            INSERT INTO Vehiculos (NumeroDePlaca)
            VALUES (@NumeroDePlaca);

            -- Obtener el último ID insertado
            SET @VehiculoId = SCOPE_IDENTITY(); 
        END

        -- Insertar en la tabla Registros
        INSERT INTO Registros (VehiculoId, FechaHoraEntrada, CostoPorHora)
        VALUES (@VehiculoId, @FechaHoraEntrada, @CostoPorHora);

        -- Confirmar transacción
        COMMIT;

    END TRY
    BEGIN CATCH
        -- Revertir transacción en caso de error
        IF @@TRANCOUNT > 0
            ROLLBACK;
        -- Lanzar el error para manejo externo
        THROW;
    END CATCH
END;
GO
