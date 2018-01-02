
namespace Remote4Support.Data
{
    public class ConsoleSession: SessionBase
    {
        public ConsoleSession()
        {
            SessionType = SessionTypes.Console;
        }

        public override SessionTypes SessionType { get; }
        public override string SessionName { get; set; }
        public override string SessionGroup { get; set; }
    }
}
