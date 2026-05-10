
using System.Net.ServerSentEvents;
using System.Threading.Channels;
using be.Data;
using Microsoft.AspNetCore.Mvc;

namespace be.Endpoints;

public class GameEvent
{

}

public static class Game
{
    public static async Task<IResult> Events(
        ChannelReader<GameEvent> reader,
        [FromHeader(Name = "Last-Event-ID")] string? lastEventId,
        CancellationToken cancellationToken
    )
    {
        async IAsyncEnumerable<SseItem<GameEvent>> StreamEvents()
        {
            await foreach (var ev in reader.ReadAllAsync(cancellationToken))
            {
                var sseItem = new SseItem<GameEvent>(ev);
                yield return sseItem;
            }
        }

        return TypedResults.ServerSentEvents(StreamEvents());
    }
}