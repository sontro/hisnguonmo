using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.Roche
{
    class RocheAstmConstants
    {
        public const string PROCESSING_ID = "P";//production
        public const string DELIMITER_DEF = "|\\^&";
        public const string IDENTIFIER__HEADER_RECORD = "H";
        public const string IDENTIFIER__PATIENT_RECORD = "P";
        public const string IDENTIFIER__ORDER_RECORD = "O";
        public const string IDENTIFIER__RESULT_RECORD = "R";
        public const string IDENTIFIER__TERMINATOR_RECORD = "L";
        public const string PRIORITY__ROUTINE = "R";
        public const string REPORT_TYPE__DELETE_SAMPLE = "X";
        public const string ACTION_CODE__ADD = "A";
        public const string ACTION_CODE__DELETE = "C";
        public const string ACTION_CODE__REPEAT = "G";
    }
}
