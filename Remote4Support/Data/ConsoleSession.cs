
using System.ComponentModel;
using System.Xml.Serialization;

namespace Remote4Support.Data
{
    public class ConsoleSession: SessionBase
    {
        public ConsoleSession()
        {
            SessionType = SessionTypes.Console;
        }

        public override SessionTypes SessionType { get;}
        [XmlAttribute]
        public override string SessionId { get; set;}
        [XmlAttribute]
        [DisplayName("Session Name")]
        [Description("This is the name of the session.")]
        public override string SessionName { get; set; }
        [XmlAttribute]
        public override string SessionGroup { get; set; }
        [XmlAttribute]
        public string CommandLine { get; set; }
    }
}
