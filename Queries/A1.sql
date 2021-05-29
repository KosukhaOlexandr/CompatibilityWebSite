SELECT Desease1.Name
From Desease AS Desease1
WHERE (NOT EXISTS
	(SELECT Active_Substance.Id 
	FROM Active_Substance INNER JOIN (DeseasesActive_Substances INNER JOIN Desease ON Desease.Id = DeseasesActive_Substances.DeseaseId) ON Active_Substance.Id = DeseasesActive_Substances.Active_SubstanceId
	WHERE Desease.Id = X1 AND Active_Substance.Id
	NOT IN
	(SELECT Active_Substance.Id
	FROM Active_Substance INNER JOIN (DeseasesActive_Substances INNER JOIN Desease ON Desease.Id = DeseasesActive_Substances.DeseaseId) ON Active_Substance.Id = DeseasesActive_Substances.Active_SubstanceId
	WHERE Desease.Id = Desease1.Id)))
	AND
	(NOT EXISTS
	(SELECT Active_Substance.Id
	FROM Active_Substance INNER JOIN (DeseasesActive_Substances INNER JOIN Desease ON Desease.Id = DeseasesActive_Substances.DeseaseId) ON Active_Substance.Id = DeseasesActive_Substances.Active_SubstanceId
	WHERE Desease.Id = Desease1.Id AND Active_Substance.Id
	NOT IN  
	(SELECT Active_Substance.Id 
	FROM Active_Substance INNER JOIN (DeseasesActive_Substances INNER JOIN Desease ON Desease.Id = DeseasesActive_Substances.DeseaseId) ON Active_Substance.Id = DeseasesActive_Substances.Active_SubstanceId
	WHERE Desease.Id = X1)))
