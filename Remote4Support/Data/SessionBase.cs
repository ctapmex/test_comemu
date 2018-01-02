
using System;
using System.Xml.Serialization;

namespace Remote4Support.Data
{
    public enum SessionTypes
    {
        Putty,
        Console
    }

    public abstract class SessionBase: IComparable
    {
        public abstract SessionTypes SessionType { get; }
        [XmlAttribute]
        public abstract string SessionId { get; set; }
        [XmlAttribute]
        public abstract string SessionName { get; set; }
        [XmlAttribute]
        public abstract string SessionGroup { get; set; }

        public int CompareTo(object obj)
        {
            SessionBase s = (SessionBase) obj;
            return s == null ? 1 : String.Compare(SessionId, s.SessionId, StringComparison.Ordinal);
        }
    }
}
