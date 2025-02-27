FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ApiCashBalance/*.csproj ApiDebit/
COPY ApiCashBalance.Application/*.csproj ApiDebit.Application/
COPY ApiCashBalance.Domain/*.csproj ApiDebit.Domain/
COPY ApiCashBalance.Infrastructure/*.csproj ApiDebit.Infrastructure/

COPY ApiCashBalance/*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish ApiCashBalance.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

CMD ["dotnet", "ApiCashBalance.dll"]
