# masstransit_routing

## Prerequisites
- .NET 9.0 SDK
- Docker and Docker Compose

## Running RabbitMQ with Docker
1. Open terminal in the `Masstransit` folder.
2. Start RabbitMQ in background:
   ```bash
   docker-compose up -d
   ```
3. Verify the management UI at http://localhost:15672 (guest/guest).

## Running the application
1. Navigate to the project folder:
   ```bash
   cd Masstransit/Masstransit
   ```
2. Run the app:
   ```bash
   dotnet run
   ```
3. You should see in console:
   - Publisher logs: published messages with routing keys.
   - Consumer1 and Consumer2 logs: each receives only its routing key messages.
