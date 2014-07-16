CREATE FULLTEXT CATALOG FullTextSearchCatalog
CREATE FULLTEXT INDEX ON dbo.Quote 
(   
    QuoteText,   
    Keywords   
) 
KEY INDEX [PK_dbo.Quote] ON FullTextSearchCatalog