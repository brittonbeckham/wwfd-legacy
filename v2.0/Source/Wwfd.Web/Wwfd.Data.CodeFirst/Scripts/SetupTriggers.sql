
/* Founder full name update */
CREATE TRIGGER FounderFullName 
   ON  dbo.Founder
   AFTER INSERT,UPDATE
AS 
BEGIN
	DECLARE @FullName varchar(60)
	
	SELECT
		@FullName = 
			CASE 
				WHEN Prefix IS NOT NULL THEN Prefix + ' '
				ELSE '' 
			END
			+ FirstName + 
			CASE 
				WHEN MiddleName IS NOT NULL THEN ' ' + MiddleName
				ELSE '' 
			END
			+ ' ' + LastName +
			CASE 
				WHEN Suffix IS NOT NULL THEN ' ' + Suffix
				ELSE '' 
			END

	FROM Inserted

	UPDATE Founder SET FullName = @FullName WHERE FounderID = (SELECT FounderId from inserted)
END