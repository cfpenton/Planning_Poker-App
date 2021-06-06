# Planning_Poker-App
This is a .Net Core 3.1 web application. (postgresSQL, Swagger, SignalR)

Hello

These are the endpoints that can be used to test the APP:

http://localhost:port

/  ->homepage

/allVotes  ->dashboard votes

/swagger   ->swagger user interface

/swagger/v1/swagger.json   ->swagger .json

/api/user  ->users list .json

/api/vote  ->votes list .json

/api/letter  ->letters list .json

/api/userStory  ->user stories list .json

To change the database configuration it must be done from:

DefaultConnection of the appsettings.json file

To create the database you must use the commands:

dotnet ef migrations add InitialCreate

dotnet ef database update

To run the app, execute the commands: 

dotnet restore

dotnet build

dotnet run

For more information contact:

cfpenton@gmx.com

https://www.linkedin.com/in/carlos-felix-penton-martinez-645801121

thanks :)
