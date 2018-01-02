
using System;

namespace Remote4Support.Data
{
    public enum SessionTypes
    {
        Putty,
        Console
    }

    public abstract class SessionBase
    {
        public abstract SessionTypes SessionType { get; }
        public abstract String SessionName { get; set; }
        public abstract String SessionGroup { get; set; }
    }
}
