FROM microsoft/aspnet:1.0.0-rc1-update1
COPY src/ /app/
WORKDIR /app
ENV ENV=development
RUN ["dnu", "restore"]
ENTRYPOINT ["dnx", "-p", "project.json", "web"]
