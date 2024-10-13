namespace API.Shared.Core.Interfaces.Responses
{
    public class Notification : INotification
    {
        public Notification(string name, string message, int tasks = 1)
        {
            Name = name;
            Message = message;
            Tasks = (short)tasks;
        }

        public string Name { get; }

        public string Message { get; }

        public short Tasks { get; set; }
    }
}
