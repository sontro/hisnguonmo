using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicineType
{
    class HisMedicineTypeUtil
    {
        private const string PREFIX__MEDICINE_TYPE_CODE = "TH";
        private const string FORMAT_BASE = "00000";
        private static string SQL = "SELECT NVL(MAX(TO_NUMBER(SUBSTR(MEDICINE_TYPE_CODE,3,length(MEDICINE_TYPE_CODE)-2))), 0) FROM HIS_MEDICINE_TYPE WHERE  MEDICINE_TYPE_CODE like 'TH%' and REGEXP_LIKE(SUBSTR(MEDICINE_TYPE_CODE,3,length(MEDICINE_TYPE_CODE)-2), '^[[:digit:]]+$')";

        internal static bool GenerateMedicineTypeCode(HIS_MEDICINE_TYPE data)
        {
            if (data != null && String.IsNullOrWhiteSpace(data.MEDICINE_TYPE_CODE))
            {
                int max = DAOWorker.SqlDAO.GetSqlSingle<int>(SQL);
                max++;
                if (max.ToString().Length >= FORMAT_BASE.Length)
                {
                    data.MEDICINE_TYPE_CODE = String.Format("{0}{1}", PREFIX__MEDICINE_TYPE_CODE, max);
                }
                else
                {
                    data.MEDICINE_TYPE_CODE = String.Format("{0}{1}", PREFIX__MEDICINE_TYPE_CODE, max.ToString(FORMAT_BASE));
                }
            }
            return true;
        }
    }
}
