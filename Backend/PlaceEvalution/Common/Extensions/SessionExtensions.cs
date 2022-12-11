using PlaceEvolution.API.API.Models;
using System.Text.Json;

namespace PlaceEvolution.API.Common.Extensions;

public static class SessionExtensions
{
    public static void SetData(this ISession session, SessionData value)
    {
        session.SetString("User", JsonSerializer.Serialize(value));
    }

    public static SessionData? GetData(this ISession session)
    {
        var value = session.GetString("User");
        return value is null ? null : JsonSerializer.Deserialize<SessionData>(value);
    }
}