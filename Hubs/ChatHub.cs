using Azure.AI.TextAnalytics;
using Azure;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Reflection.Metadata;
using TestTask.Data;

namespace TestTask.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationContext _context;

        private readonly TextAnalyticsClient _client;

        public const string HubUrl = "/chat";

        public ChatHub(ApplicationContext context, TextAnalyticsClient client)
        {
            _context = context;
            
            _client = client;
        }

        public async Task Broadcast(string username, string text)
        {
            var message = new Message(username, text, string.Empty);

            if (username != "app")
            {
                Response<DocumentSentiment> response = _client.AnalyzeSentiment(text);
                DocumentSentiment docSentiment = response.Value;

                message.Sentiment = docSentiment.Sentiment.ToString();                

                _context.Messages.Add(message);
                _context.SaveChanges();
            }            

            await Clients.All.SendAsync("Broadcast", message);
        }
    }
}
