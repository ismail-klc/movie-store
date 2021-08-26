using System;

namespace Business.Helpers.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Danger(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }

        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
        }
    }
}