SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tamesh Sivaguru
-- Create date: 20/10/2013
-- Description:	Retrieve all Symbols from an authenticated users portfolio.
-- =============================================
CREATE PROCEDURE Portfolio.SelectSymbolsByUser
	@UserName NVARCHAR(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT S.SecurityId, S.Symbol 
	FROM Portfolio.[User] U
    INNER JOIN Portfolio.Portfolio P ON U.UserId = P.UserId
    INNER JOIN Portfolio.Portfolio_Security PS ON P.PortfolioId = PS.PortfolioId
    INNER JOIN Portfolio.Security S ON PS.SecurityId = S.SecurityId
END
GO