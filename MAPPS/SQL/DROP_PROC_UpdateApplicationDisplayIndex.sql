IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateApplicationDisplayIndex]') AND type in (N'P'))
BEGIN
	DROP PROCEDURE [dbo].[usp_UpdateApplicationDisplayIndex]
END