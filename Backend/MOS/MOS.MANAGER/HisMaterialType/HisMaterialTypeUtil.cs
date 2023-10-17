using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMaterialType
{
    class HisMaterialTypeUtil
    {
        private const string PREFIX__MATERIAL_TYPE_CODE = "VT";
        private const string FORMAT_BASE = "00000";
        private static string SQL = "SELECT NVL(MAX(TO_NUMBER(SUBSTR(MATERIAL_TYPE_CODE,3,length(MATERIAL_TYPE_CODE)-2))), 0) FROM HIS_MATERIAL_TYPE WHERE  MATERIAL_TYPE_CODE like 'VT%' and REGEXP_LIKE(SUBSTR(MATERIAL_TYPE_CODE,3,length(MATERIAL_TYPE_CODE)-2), '^[[:digit:]]+$')";

        internal static bool GenerateMaterialTypeCode(HIS_MATERIAL_TYPE data)
        {
            if (data != null && String.IsNullOrWhiteSpace(data.MATERIAL_TYPE_CODE))
            {
                int max = DAOWorker.SqlDAO.GetSqlSingle<int>(SQL);
                max++;
                if (max.ToString().Length >= FORMAT_BASE.Length)
                {
                    data.MATERIAL_TYPE_CODE = String.Format("{0}{1}", PREFIX__MATERIAL_TYPE_CODE, max);
                }
                else
                {
                    data.MATERIAL_TYPE_CODE = String.Format("{0}{1}", PREFIX__MATERIAL_TYPE_CODE, max.ToString(FORMAT_BASE));
                }
            }
            return true;
        }
    }
}
