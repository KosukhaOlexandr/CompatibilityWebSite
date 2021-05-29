SELECT Active_Substance.Name
FROM Active_Substance
WHERE (SELECT Count(Compatibility.Id) 
FROM Compatibility
WHERE ((Compatibility.First_Active_Substance=Active_Substance.Id) AND (Compatibility.Compatibility_Status_Id=s1))
OR (((Compatibility.Compatibility_Status_Id)=s1) AND ((Compatibility.Second_Active_Substance)=Active_Substance.Id))) >=N1;