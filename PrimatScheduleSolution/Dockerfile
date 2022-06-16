FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
COPY *.sln .
COPY ["PrimatSchedule/PrimatScheduleBot.csproj", "PrimatSchedule/"]
RUN dotnet restore "PrimatSchedule/PrimatScheduleBot.csproj"
COPY . .
WORKDIR "/source/PrimatSchedule"
RUN dotnet publish "PrimatScheduleBot.csproj" -c release -o /app --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "PrimatScheduleBot.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet PrimatScheduleBot.dll