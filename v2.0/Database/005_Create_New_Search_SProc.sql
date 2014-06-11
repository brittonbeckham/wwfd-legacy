EXEC sp_rename 'SearchQuotes', 'SearchQuotesOld'
GO

CREATE PROCEDURE SearchQuotes 
	(
		@SearchText varchar(100)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
	Select * from Quotes where contains(QuoteText, @SearchText)
END
GO
