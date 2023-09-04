USE SQLBOOKSDB;
GO

--******Titles******
ALTER PROCEDURE dbo.usp_GetAllTitles AS
BEGIN
	BEGIN TRY
		SELECT ISBN, Title, Year_Published, PubID, [Description],
			   Notes, [Subject], Comments
		FROM dbo.Titles ;
	END TRY

	BEGIN CATCH
	 SELECT ERROR_MESSAGE();
	END CATCH;
END;
GO

ALTER PROCEDURE dbo.usp_GetTitlesByValue 
(
	@Title NVARCHAR(255),
	@ISBN NVARCHAR(20),
	@YearPublished SMALLINT,
	@PubID INT
)AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;

		SELECT Title, Year_Published, ISBN, PubID, [Description],
			   Notes, [Subject], Comments
		FROM dbo.Titles
		WHERE Title LIKE ('%' + @Title + '%') OR
			  ISBN LIKE ('%' + @ISBN + '%') OR
			  Year_Published = @YearPublished OR
			  PubID = @PubID;
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE();
	END CATCH;
	RETURN 0;
END;
GO

ALTER PROCEDURE dbo.usp_GetTitle
(
	@ISBN NVARCHAR(20)
)AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;

		SELECT Title, Year_Published, ISBN, PubID, [Description],
			   Notes, [Subject], Comments
		FROM dbo.Titles
		WHERE ISBN = @ISBN;
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE();
	END CATCH
	RETURN 0;
END;
GO

ALTER PROCEDURE dbo.usp_AddTitle
(
	@Title NVARCHAR(255),
	@Year_Published SMALLINT,
	@ISBN NVARCHAR(20),
	@PubID INT,
	@Description NVARCHAR(50) = '',
	@Notes NVARCHAR(50) = '',
	@Subject NVARCHAR(50) = '',
	@Comments NVARCHAR(MAX) = ''
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO dbo.Titles (
				Title,
				Year_Published,
				ISBN,
				PubID,
				[Description],
				Notes,
				[Subject],
				Comments
			)
			VALUES (
				@Title,
				@Year_Published,
				@ISBN,
				@PubID,
				@Description,
				@Notes,
				@Subject,
				@Comments
			);
		COMMIT TRAN;
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN;
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH
	RETURN 0;
END
GO

ALTER PROCEDURE dbo.usp_UpdateTitle
(
	@Title NVARCHAR(255),
	@Year_Published SMALLINT,
	@ISBN NVARCHAR(20),
	@PubID INT,
	@Description NVARCHAR(50),
	@Notes NVARCHAR(50),
	@Subject NVARCHAR(50),
	@Comments NVARCHAR(MAX)
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			UPDATE dbo.Titles
			SET Title = @Title,
				Year_Published = @Year_Published,
				PubID = @PubID,
				[Description] = @Description,
				Notes = @Notes,
				[Subject] = @Subject,
				Comments = @Comments
			WHERE ISBN = @ISBN;
		COMMIT TRAN;
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH
END;
GO

ALTER PROCEDURE dbo.usp_DeleteTitle
(
	@ISBN NVARCHAR(20)
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			DELETE FROM dbo.Titles
			WHERE ISBN = @ISBN;
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN;
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
	RETURN 0;
END;
GO


--******Authors****************************************************************************
ALTER PROCEDURE dbo.usp_GetAuthors AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT Au_ID, Author, Year_Born
		FROM dbo.Authors;
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE();
	END CATCH;
END;
GO

ALTER PROCEDURE dbo.usp_AddAuthor (
	@Author NVARCHAR(50),
	@Year_Born SMALLINT
)AS
BEGIN
	
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			INSERT INTO dbo.Authors (Author, Year_Born)
			VALUES (@Author, @Year_Born);

		COMMIT TRAN;
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN;
		SELECT ERROR_MESSAGE();
	END CATCH;
END;
GO

ALTER PROCEDURE dbo.usp_UpdateAuthor (
	@Au_ID INT,
	@Author NVARCHAR(50),
	@Year_Born SMALLINT
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			UPDATE dbo.Authors
			SET Author = @Author,
			    Year_Born = @Year_Born
			WHERE Au_ID = @Au_ID;
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE();
	END CATCH;
END;
GO

ALTER PROCEDURE dbo.usp_DeleteAuthor (
	@Au_ID INT
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			DELETE FROM dbo.Authors
			WHERE Au_ID = @Au_ID;
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE();
	END CATCH;
END;
GO

ALTER PROCEDURE dbo.usp_GetAuthor (
	@Au_ID INT
)AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;

		SELECT Au_ID, Author, Year_Born
		FROM dbo.Authors
		WHERE Au_ID = @Au_ID;
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE();
	END CATCH;
END;
GO

ALTER PROCEDURE dbo.usp_GetAuthorsPerTitle
(
	@ISBN NVARCHAR(20)
)AS
BEGIN
	BEGIN TRY
		;WITH CTE AS (
			SELECT Au_ID
			FROM dbo.Title_Author
			WHERE ISBN = @ISBN
		)
		SELECT Au_ID, Author, Year_Born
		FROM Authors as au
		WHERE au.Au_ID IN (SELECT Au_ID FROM CTE);
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
END;
GO

ALTER PROCEDURE dbo.usp_GetMaxID AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;

		SELECT MAX(Au_ID) AS MaxID
		FROM dbo.Authors;
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE();
	END CATCH;
END;
GO
--*********************************************************************
--Publishers
ALTER PROCEDURE dbo.usp_GetAllPublishers AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;

		SELECT PubID, Name, Company_Name, Address, City, State, Zip, Telephone, Fax, Comments
		FROM Publishers WITH (NOLOCK);
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH
	RETURN 0;
END;
GO

ALTER PROCEDURE dbo.usp_GetPublisherByName
(
	@Name NVARCHAR(50)
)AS
BEGIN
	BEGIN TRY
		SELECT PubID, Name, Company_Name, Address, City, State, Zip, Telephone, Fax, Comments
		FROM Publishers WITH (NOLOCK)
		WHERE Name LIKE @Name + '%';
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
	RETURN 0;
END;
GO

ALTER PROCEDURE dbo.usp_GetPublisher
(
	@PubID INT
)AS
BEGIN
	BEGIN TRY
		SELECT PubID, Name, Company_Name, Address, City, State, Zip, Telephone, Fax, Comments
		FROM Publishers WITH (NOLOCK)
		WHERE PubID = @PubID;
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
	RETURN 0;
END;
GO

ALTER PROCEDURE dbo.usp_AddPublisher
(
	@Name NVARCHAR(50),
	@CompanyName NVARCHAR(255),
	@Address NVARCHAR(50),
	@City NVARCHAR(20),
	@State NVARCHAR(10),
	@Zip NVARCHAR(15),
	@Telephone NVARCHAR(15),
	@Fax NVARCHAR(15),
	@Comments NVARCHAR(MAX)
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO dbo.Publishers (Name, Company_Name, Address, City, State, Zip, Telephone, Fax, Comments)
			VALUES
			(
				@Name,
				@CompanyName,
				@Address,
				@City,
				@State,
				@Zip,
				@Telephone,
				@Fax,
				@Comments
			);
		COMMIT TRAN;
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN;
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
	RETURN 0;
END;
GO

ALTER PROCEDURE dbo.usp_UpdatePublisher
(
	@PubID INT,
	@Name NVARCHAR(50),
	@CompanyName NVARCHAR(255),
	@Address NVARCHAR(50),
	@City NVARCHAR(20),
	@State NVARCHAR(10),
	@Zip NVARCHAR(15),
	@Telephone NVARCHAR(15),
	@Fax NVARCHAR(15),
	@Comments NVARCHAR(MAX)
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			UPDATE dbo.Publishers
			SET
				Name = @Name,
				Company_Name = @CompanyName,
				Address = @Address,
				City = @City,
				State = @State,
				Zip = @Zip,
				Telephone = @Telephone,
				Fax = @Fax,
				Comments = @Comments
			WHERE PubID = @PubID;
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN;
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
	RETURN 0;
END;
GO

ALTER PROCEDURE dbo.usp_DeletePublisher
(
	@PubID INT
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			DELETE FROM dbo.Publishers
			WHERE PubID = @PubID;
		COMMIT TRAN;
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN;
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
	RETURN 0;
END;
GO
--**************Title_Author Table**************************************
ALTER PROCEDURE dbo.usp_AddTitleAuthor
(
	@ISBN NVARCHAR(20),
	@Au_ID INT
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			INSERT INTO dbo.Title_Author (ISBN, Au_ID)
			VALUES (@ISBN, @Au_ID);
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
END;
GO

ALTER PROCEDURE dbo.usp_DeleteTitleAuthor
(
	@ISBN NVARCHAR(20),
	@Au_ID INT
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			DELETE FROM dbo.Title_Author
			WHERE ISBN = @ISBN AND Au_ID = @Au_ID;
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN;
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
END;
GO

ALTER PROCEDURE dbo.usp_DeleteTitleAuthorByISBN
(
	@ISBN NVARCHAR(20)
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			DELETE FROM dbo.Title_Author
			WHERE ISBN = @ISBN
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN;
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
END;