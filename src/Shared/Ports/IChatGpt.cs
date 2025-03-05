namespace Ports;

public interface IChatGpt
{
    public Task<string> AskAssistant(string message);
}