IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateTabDisplayIndex]') AND type in (N'P'))
BEGIN
	DROP PROCEDURE [dbo].[usp_UpdateTabDisplayIndex]
END