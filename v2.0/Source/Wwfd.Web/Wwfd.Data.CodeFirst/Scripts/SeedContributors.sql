INSERT [dbo].[Contributor] ([ContributorID], [FirstName], [LastName], [Email], [PasswordHash], [DateCreated], [IsActive]) VALUES (N'8ebd9a3c-3993-4f92-993b-261e61e040cf', N'Britton L.', N'Beckham', N'britton.beckham@gmail.com', N'All@t1once', GETDATE(), 1)
INSERT [dbo].[Contributor] ([ContributorID], [FirstName], [LastName], [Email], [PasswordHash], [DateCreated], [IsActive]) VALUES (N'ac687b9e-da17-4788-81cc-8e0ba5a283c5', N'Carolee', N'Beckham', N'carolee.beckham@gmail.com', N'iTake#1hotos', GETDATE(), 1)
INSERT [dbo].[Contributor] ([ContributorID], [FirstName], [LastName], [Email], [PasswordHash], [DateCreated], [IsActive]) VALUES (N'5092ae9f-6b57-47c9-9c09-1f2dc9d43230', N'Michael', N'Snow', N'snowdog87@gmail.com ', N'Thuan123', GETDATE(), 0)
INSERT [dbo].[Contributor] ([ContributorID], [FirstName], [LastName], [Email], [PasswordHash], [DateCreated], [IsActive]) VALUES (N'e12d7c38-84b1-490e-b967-72230dd4b9d9', N'Ted', N'Lee', N'ted@securitymetrics.com', N'1r0nw1ll', GETDATE(), 0)
INSERT [dbo].[Contributor] ([ContributorID], [FirstName], [LastName], [Email], [PasswordHash], [DateCreated], [IsActive]) VALUES (N'f7b4f9b7-8206-4082-9e2e-9f5b682b7efd', N'Heidi', N'Johnson', N'sleepingheidi@yahoo.com', N'c00per24', GETDATE(), 0)
INSERT [dbo].[Contributor] ([ContributorID], [FirstName], [LastName], [Email], [PasswordHash], [DateCreated], [IsActive]) VALUES (N'4d83d388-9873-479e-9c59-cee7d32fc87f', N'Matt', N'Bryson', N'mattjbryson@gmail.com', N'BearLake22', GETDATE(), 0)

--set all original contibutor roles
INSERT INTO ContributorRole
SELECT ContributorId, 1 FROM Contributor

--default software creator to admin
INSERT INTO ContributorRole
VALUES ('8EBD9A3C-3993-4F92-993B-261E61E040CF', 3)