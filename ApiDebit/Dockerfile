FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ApiDebit/*.csproj ApiDebit/
COPY ApiDebit.Application/*.csproj ApiDebit.Application/
COPY ApiDebit.Domain/*.csproj ApiDebit.Domain/
COPY ApiDebit.Infrastructure/*.csproj ApiDebit.Infrastructure/

WORKDIR /app/ApiDebit
RUN dotnet restore

COPY . .
RUN dotnet publish ApiDebit.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

CMD ["dotnet", "ApiDebit.dll"]
