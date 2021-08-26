namespace Business.Helpers.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void Danger(string message);
    }
}