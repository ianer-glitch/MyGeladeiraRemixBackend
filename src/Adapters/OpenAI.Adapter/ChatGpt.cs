using System.Data;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using OpenAI.Assistants;
using OpenAI.Chat;
using OpenAI.Models;
using OpenAI.Threads;
using Ports;
using Message = OpenAI.Threads.Message;

namespace OpenAI.Adapter;

public class ChatGpt : IChatGpt
{
    private readonly IConfiguration _configuration;
    private readonly OpenAIClient  _openAiClient;
    
    public ChatGpt(IConfiguration configuration)
    {
        _configuration = configuration;
        _openAiClient = new OpenAIClient( _configuration.GetSection("Open_API").GetSection("KEY").Value);
        
    }

    private async Task<AssistantResponse> CreateAssistant()
    {
        var openApiConf = _configuration.GetSection("Open_API");
        var instructions = openApiConf.GetSection("ASSISTANT_INSTRUCTIONS").Value;
        var name = openApiConf.GetSection("ASSISTANT_NAME").Value;

        
        var listAssistants = await _openAiClient.AssistantsEndpoint.ListAssistantsAsync();
        
        
        if (listAssistants.Items.Any())
        {
            return await _openAiClient.AssistantsEndpoint.RetrieveAssistantAsync(listAssistants.LastId);
        }
        
        return await _openAiClient.AssistantsEndpoint.CreateAssistantAsync(
            new CreateAssistantRequest(
                name: name,
                instructions:instructions,
                model: Model.GPT4oMini
            ));
        
    }

    public async Task<string> AskAssistant(string message)
    {
        try
        {
            if(string.IsNullOrEmpty(message))
                throw new ArgumentException("message is null or empty");

            var openApiConf = _configuration.GetSection("Open_API");
            var instructions = openApiConf.GetSection("ASSISTANT_INSTRUCTIONS").Value;
            
            var messages = new List<OpenAI.Chat.Message>
            {
                new Chat.Message(Role.System,instructions),
                new Chat.Message(Role.User,message),
            };

            var chatRequest = new ChatRequest(messages,model:Model.GPT4oMini);
            
            var response = await _openAiClient.ChatEndpoint.GetCompletionAsync(chatRequest);
            return response.FirstChoice;
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    
    public async Task<T> AskAssistant<T>(string message)
    {
        try
        {
            if(string.IsNullOrEmpty(message))
                throw new ArgumentException("message is null or empty");

            var openApiConf = _configuration.GetSection("Open_API");
            var instructions = openApiConf.GetSection("ASSISTANT_INSTRUCTIONS").Value;
            
            var messages = new List<OpenAI.Chat.Message>
            {
                new Chat.Message(Role.System,instructions),
                new Chat.Message(Role.User,message),
            };

            var chatRequest = new ChatRequest(messages,model:Model.GPT4oMini,responseFormat:ChatResponseFormat.JsonSchema);
            
            var response = await _openAiClient.ChatEndpoint.GetCompletionAsync(chatRequest);
            var r = response.FirstChoice.ToString().Replace("```json","").Replace("```","");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(r) ?? Activator.CreateInstance<T>();
            
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    
    
    
    
    
}