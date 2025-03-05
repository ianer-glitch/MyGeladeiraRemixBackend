using System.Data;
using Microsoft.Extensions.Configuration;
using OpenAI.Assistants;
using OpenAI.Models;
using OpenAI.Threads;
using Ports;

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
        var name = openApiConf.GetSection("ASSISTANT_NAME").Value;
        var instructions = openApiConf.GetSection("ASSISTANT_INSTRUCTIONS").Value;
        
        var assistant = await _openAiClient.AssistantsEndpoint.CreateAssistantAsync(
            new CreateAssistantRequest(
                name: name,
                instructions:instructions,
                model: Model.GPT4oMini
            ));
        
        

        return assistant;
    }

    public async Task<string> AskAssistant(string message)
    {
        try
        {
            if(string.IsNullOrEmpty(message))
                throw new ArgumentException("message is null or empty");

            var assistant = await CreateAssistant();
            
            var requestMessages = new List<Message> { message };
            
            var threadRequest = new CreateThreadRequest(requestMessages);
            var run =  await assistant.CreateThreadAndRunAsync(threadRequest); 
            var thread = await _openAiClient.ThreadsEndpoint.RetrieveThreadAsync(run.ThreadId);
            
            var messages = await thread.ListMessagesAsync();
            return messages.Items.FirstOrDefault()?.PrintContent() ?? string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    
}