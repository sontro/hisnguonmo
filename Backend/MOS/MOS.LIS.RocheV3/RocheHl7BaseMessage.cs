using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    public enum Hl7MessageType
    {
        ORDER,
        SAMPLE_SEEN,
        RESULT,
        ANTIBIOTIC_RESULT
    }

    public abstract class RocheHl7BaseMessage
    {
        public Hl7MessageType Type { get; set; }

        public RocheHl7BaseMessage()
        {
        }

        public abstract string ToMessage();
        public abstract void FromMessage(string message);
    }
}
