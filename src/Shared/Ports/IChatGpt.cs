namespace Ports;

public interface IChatGpt
{
    public Task<string> AskAssistant(string message);

    public  Task<T> AskAssistant<T>(string message);
}