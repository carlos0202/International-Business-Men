CREATE TABLE [dbo].[Rates]
(
    [From] VARCHAR(10) NOT NULL, 
    [To] VARCHAR(10) NOT NULL, 
    [Rate] DECIMAL(11, 6) NOT NULL, 
    CONSTRAINT [CK_Rates_Id_PK] PRIMARY KEY ([From], [To])
)
