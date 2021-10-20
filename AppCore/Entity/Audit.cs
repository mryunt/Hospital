using System;

namespace AppCore.Entity
{
    public abstract class Audit
    {
        public DateTime CDate { get; set; } = DateTime.Now;
        public DateTime ?MDate { get; set; }
    }
}
