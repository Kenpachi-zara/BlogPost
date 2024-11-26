# BlogPost

# set up
1- for Database tables required for the project and identity framework can be done two ways:
  - Delete Migrations folder and run  `dotnet ef add <migration-name> --context ApplicationContext` then `dotnet ef database update --context ApplicationContext`
  - second way is just run  `dotnet ef database update --context ApplicationContext` without deleting migrations folder, but it contains uneccessary steps that i used for testing purposes.
  - connection string in appsettings.json `Server=DESKTOP-S6E7FOF\\SQLEXPRESS;Initial Catalog=<database>;User ID=<username>;Password=<password>;Trust Server Certificate=true`

2- I'm using VS Code IDE for this project, but it can done through Visual Studio, however to run this use `dotnet run` I use dotnet CLI for any needed operations, this will host on localhost:5000


3- aExecute the following procedure within Database, I'm using SQL SERVER, or you can add it to `Up(),Down()` methods within any migration 
```
CREATE PROCEDURE RELATED_TOPICS
(
    @PostID nvarchar(255) = NULL
)
AS
BEGIN
    SET NOCOUNT ON

		SELECT *,
		CASE
			WHEN (SELECT COUNT(*) from TopicsPosts tp Where tp.PostId = @PostID AND tp.TopicId = t.TopicId) > 0  THEN 1
			ELSE 0
		END AS IsRelated
		from Topic t;
END
GO
```
It basically returns all Records of Topic table, with an extra boolean column, true when it's related to a record within Post table based on PostId, false otherwise. 

### Notes:
- in `BlogPost.csrpoj` I've used specific dependencies to avoid security vulnerabilities.
- In some parts of the code, I've left things as they are becuase I'd like to discuss it with the Reviewer, such as warnings , or request validation.
- I've used [https://github.com/madskristensen/Miniblog.Core](https://github.com/xoofx/markdig) for markdown, the marking explanation is in their repo.
- Authentication/Authorization using Asp.net Identity (cookies), And I've also looked into Identity JWT bearer token.

