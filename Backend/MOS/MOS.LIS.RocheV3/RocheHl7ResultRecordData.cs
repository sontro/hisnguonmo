using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    public class RocheHl7ResultRecordData
    {
        private const string IDENTIFIER__RESULT_RECORD = "R";

        public string TestIndexCode { get; set; }
        public string Value { get; set; }
        public string UnitSymbol { get; set; }
        public string MachineCode { get; set; }

        public RocheHl7ResultRecordData()
        {
        }
    }
}
