using HelloBotConsole.Interfaces;

namespace HelloBotConsole.Models
{
    public class Session
    {
        public long SessionChatId { get; set; }
        public string SessionCurrentCommand { get; set; }
        public SessionStatus Status = SessionStatus.Undefined;
        public ICommand CommandSessionHandler;

        public Session(ICommand commandSessionHandler, long sessionChatId, string sessionCurrentCommand,
            SessionStatus sessionStatus)
        {
            this.CommandSessionHandler = commandSessionHandler;
            this.SessionChatId = sessionChatId;
            this.SessionCurrentCommand = sessionCurrentCommand;
            this.Status = sessionStatus;
        }
        public Session(Session session)
        {
            this.CommandSessionHandler = session.CommandSessionHandler;
            this.SessionChatId = session.SessionChatId;
            this.SessionCurrentCommand = session.SessionCurrentCommand;
            this.Status = session.Status;
        }

        public bool isEqualTo(Session _session)
        {
            return (this.SessionCurrentCommand == _session.SessionCurrentCommand 
                    && this.SessionChatId == _session.SessionChatId 
                    && this.Status == _session.Status);
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