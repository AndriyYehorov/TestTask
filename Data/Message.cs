namespace TestTask.Data
{
    public class Message
    {      
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }

        public string Sentiment { get; set; }

        public Message(string username, string text, string sentiment)
        {
            Username = username;
            Text = text;
            Sentiment = sentiment;
        }

    }
}