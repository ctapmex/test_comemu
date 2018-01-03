
using System.ComponentModel;
using System.Xml.Serialization;

namespace Remote4Support.Data
{
    public class PuttySession: SessionBase
    {
        public PuttySession()
        {
            SessionType = SessionTypes.Putty;
        }

        [DisplayName("Session type")]
        [Description("This is the type of session.")]
        public override SessionTypes SessionType { get;}

        [XmlAttribute]
        [Browsable(false)]
        public override string SessionId { get; set; }

        [XmlAttribute]
        [DisplayName("Name")]
        [Description("This is the name that will be displayed in the connections tree.")]
        public override string SessionName { get; set; }

        [XmlAttribute]
        [DisplayName("Panel")]
        [Description("Sets the panel in which the connection will open.")]
        public override string SessionGroup { get; set; }
    }
}
