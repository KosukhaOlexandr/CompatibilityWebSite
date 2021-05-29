SELECT Active_Substance.Name
FROM Desease INNER JOIN (Active_Substance INNER JOIN DeseasesActive_Substances ON Active_Substance.Id = DeseasesActive_Substances.Active_SubstanceId) 
				ON Desease.Id = DeseasesActive_Substances.DeseaseId
WHERE ((Desease.Id=d1));
