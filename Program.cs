using MongoDB.Driver;
 
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
// Root-Endpunkt anpassen
app.MapGet("/", () => "Minimal API Version 1.0");
// Check-Endpunkt hinzufügen
app.MapGet("/check", () =>
{
    try
    {
        // Verbindung zur MongoDB herstellen
        var client = new MongoClient("mongodb://gbs:geheim@localhost:27017");
        var databaseList = client.ListDatabaseNames().ToList();
        // Erfolgreiche Antwort mit den Datenbanknamen
        return Results.Ok(new
        {
            Message = "Zugriff auf MongoDB ok. Vorhandene DBs: " + string.Join(",", databaseList);
            Databases = databaseList
        });
    }
    catch (Exception ex)
    {
        // Fehlerhafte Antwort mit der Fehlermeldung
        return Results.Problem($"Fehler beim Zugriff auf MongoDB: {ex.Message}");
    }
});
app.Run();