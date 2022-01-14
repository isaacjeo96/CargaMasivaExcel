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

	WHERE Usuario.Nombre LIKE '%' + 'Patricia' + '%' 
		AND Usuario.ApellidoPaterno LIKE '%' + 'Ocampo' + '%' 
		AND Usuario.ApellidoMaterno LIKE '%' + 'Cuevas' + '%'



SELECT Nombre,ApellidoPaterno,ApellidoMaterno,IdDireccion
FROM Usuario
 WHERE Nombre LIKE 'E'+'_%' 


