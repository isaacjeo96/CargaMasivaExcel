USE [PruebaDDL]
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetAll]    Script Date: 1/13/2022 10:47:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UsuarioGetAll]
@Nombre VARCHAR(50)
,@ApellidoPaterno VARCHAR(50)
,@ApellidoMaterno VARCHAR(50)
AS
SELECT Usuario.IdUsuario
      ,Usuario.Nombre
      ,Usuario.ApellidoPaterno
      ,Usuario.ApellidoMaterno
      ,Direccion.IdDireccion
	  ,Direccion.Calle AS Calle
	  ,Direccion.NumExterior AS NumExterior
	  ,Direccion.NumInterior AS NumInterior
	  ,Direccion.IdColonia 
	  ,Colonia.Nombre AS Colonia
	  ,Colonia.CodigoPostal AS CP
	  ,Colonia.IdMunicipio 
	  ,Municipio.Nombre AS Municipio
	  ,Municipio.IdEstado 
	  ,Estado.Nombre AS Estado
	  ,Estado.IdPais 
	  ,Pais.Nombre AS Pais
      ,Usuario.Imagen

  FROM Usuario

  INNER JOIN Direccion ON Usuario.IdDireccion = Direccion.IdDireccion
  INNER JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
  INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
  INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
  INNER JOIN Pais ON Estado.IdPais = Pais.IdPais

	WHERE Usuario.Nombre LIKE '%' + @Nombre + '%' 
		AND Usuario.ApellidoPaterno LIKE '%' + @ApellidoPaterno + '%' 
		AND Usuario.ApellidoMaterno LIKE '%' + @ApellidoMaterno + '%'


