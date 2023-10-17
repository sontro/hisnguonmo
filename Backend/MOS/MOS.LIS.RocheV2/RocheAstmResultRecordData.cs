using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV2
{
    public class RocheAstmResultRecordData
    {
        private const string IDENTIFIER__RESULT_RECORD = "R";

        public string TestIndexCode { get; set; }
        public string Value { get; set; }
        public string UnitSymbol { get; set; }
        public string MachineCode { get; set; }

        public RocheAstmResultRecordData()
        {
        }

        public static RocheAstmResultRecordData FromString(string str)
        {
            RocheAstmResultRecordData index = null;
            string indexStr = str.StartsWith(IDENTIFIER__RESULT_RECORD) ? str : null;
            if (!string.IsNullOrWhiteSpace(indexStr))
            {
                index = new RocheAstmResultRecordData();
                string[] indexContent = indexStr.Split('|');
                if (indexContent != null && indexContent.Length >= 5)
                {
                    //du lieu chi so co dang: ^^^500110^^^^900
                    //trong do 500110 la ma chi so XN
                    string indexCodeContentStr = indexContent[2];
                    do
                    {
                        indexCodeContentStr = indexCodeContentStr.Replace("^^", "^");
                    }
                    while (indexCodeContentStr.Contains("^^"));
                    string[] indexCodeContents = indexCodeContentStr.Split('^');

                    index.TestIndexCode = indexCodeContents[1];                    
                    index.Value = indexContent[3];
                    index.UnitSymbol = indexContent[4];
                    if (indexContent.Length >= 14)
                    {
                        index.MachineCode = indexContent[13];
                    }
                }
            }
            return index;
        }
    }
}
