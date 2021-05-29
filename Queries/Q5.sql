SELECT Medicine1.Name
FROM Medicine AS Medicine1
WHERE ((((SELECT COUNT (Active_Substance.Id)
FROM Medicine INNER JOIN (MedicineActive_Substances INNER JOIN Active_Substance 
ON Active_Substance.Id = MedicineActive_Substances.Active_SubstanceId) ON Medicine.Id = MedicineActive_Substances.MedicineId
WHERE Medicine.Id = Medicine1.Id))>=N1));
