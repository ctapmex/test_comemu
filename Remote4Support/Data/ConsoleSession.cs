
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

        [DisplayName("Session type")]
        [Description("This is the type of session.")]
        public override SessionTypes SessionType { get;}

        [XmlAttribute]
        [Browsable(false)]
        public override string SessionId { get; set;}

        [XmlAttribute]
        [DisplayName("Name")]
        [Description("This is the name that will be displayed in the connections tree.")]
        public override string SessionName { get; set; }

        [DisplayName("Panel")]
        [Description("Sets the panel in which the connection will open.")]
        [XmlAttribute]
        public override string SessionGroup { get; set; }

        [XmlAttribute]
        [DisplayName("External tool")]
        [Description("Select the external tool to be started.")]
        public string CommandLine { get; set; }
    }
}
