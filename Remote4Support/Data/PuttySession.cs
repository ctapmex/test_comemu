
namespace Remote4Support.Data
{
    public class PuttySession: SessionBase
    {
        public PuttySession()
        {
            SessionType = SessionTypes.Putty;
        }

        public override SessionTypes SessionType { get; }
        public override string SessionName { get; set; }
        public override string SessionGroup { get; set; }
    }
}
