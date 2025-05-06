using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StopBulliesUserService.Domain.Entities;

namespace StopBulliesUserService.Database;
internal sealed class ApplicationDbContextInitializer(ApplicationDbContext context)
{
    public async Task TrySeedAsync()
    {
        await using FileStream stream = File.OpenRead("../IdentityDataImporter.json");
        using var reader = new JsonTextReader(new StreamReader(stream));

        JArray readerFile = await JArray.LoadAsync(reader);
        bool existData = await context.Status.AnyAsync();
        if (!existData)
        {
            foreach (JToken statusData in readerFile[0]["status"]!)
            {
                var statusRecord = new Status
                {
                    //Id = (int)statusData["Id"]!,
                    Name = statusData["Name"]!.ToString().Trim(),
                    Entity = statusData["Entity"]!.ToString().Trim(),
                    Order = (int)statusData["Order"]!,
                    IsEnabled = (bool)statusData["IsEnabled"]!,
                    Description = statusData["Description"]!.ToString().Trim()
                };
                _ = context.Status.Add(statusRecord);
            }
        }
        if (!existData)
        {
            _ = await context.SaveChangesAsync();
        }
    }
}


