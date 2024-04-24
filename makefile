watch:
	dotnet watch  --no-hot-reload --project api

build:
	dotnet build

run:
	dotnet run
	

# migration-add:
#     dotnet ef migrations add init -o Data/Migrations --project api
