using OpenAI_API;

namespace ChatGPT.ChatBotAPI.Extensions
{
    public static class ChatGPTExtensions
    {
        public static WebApplicationBuilder AddChatGpt(
          this WebApplicationBuilder builder,
           IConfiguration configuration
        ) {
            OpenAIAPI chat;

            var configurationAPIKey = configuration["ChatGpt:Key"];
            var environmentAPIKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            if(!string.IsNullOrEmpty(environmentAPIKey))
                chat = new OpenAIAPI(environmentAPIKey);
            else
                chat = new OpenAIAPI(configurationAPIKey);

            builder.Services.AddSingleton(chat);
            return builder;
        }
    }
}
