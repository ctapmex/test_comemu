
using System.Xml.Serialization;

namespace Remote4Support.Data
{
    public class PuttySession: SessionBase
    {
        public PuttySession()
        {
            SessionType = SessionTypes.Putty;
        }

        public override SessionTypes SessionType { get;}
        [XmlAttribute]
        public override string SessionId { get; set; }
        [XmlAttribute]
        public override string SessionName { get; set; }
        [XmlAttribute]
        public override string SessionGroup { get; set; }
    }
}
