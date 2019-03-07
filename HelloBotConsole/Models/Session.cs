namespace HelloBotConsole.Models
{
    public class Session
    {
        public long SessionChatId {get; set; }
        public string SessionCurrentCommand {get; set; }
        public SessionStatus Status = SessionStatus.Undefined;

        public Session(long sessionChatId, string sessionCurrentCommand, SessionStatus sessionStatus)
        {
            this.SessionChatId = sessionChatId;
            this.SessionCurrentCommand = sessionCurrentCommand;
            this.Status = sessionStatus;
        }
        
    }

    public enum SessionStatus
    {
        Undefined = 0,
        Started = 1,
        InProgress = 2,
        Finished = 3,
    }
    
}