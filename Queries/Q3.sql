SELECT Medicine.Name
FROM Medicine INNER JOIN (Active_Substance INNER JOIN MedicineActive_Substances ON 
Active_Substance.Id = MedicineActive_Substances.Active_SubstanceId) ON Medicine.Id = MedicineActive_Substances.MedicineId
WHERE (Active_Substance.Id=as1);