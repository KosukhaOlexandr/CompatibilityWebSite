SELECT Desease.Name
FROM Desease INNER JOIN (Active_Substance INNER JOIN DeseasesActive_Substances 
ON Active_Substance.Id = DeseasesActive_Substances.Active_SubstanceId) ON Desease.Id = DeseasesActive_Substances.DeseaseId
WHERE ((Active_Substance.Id=as1));
