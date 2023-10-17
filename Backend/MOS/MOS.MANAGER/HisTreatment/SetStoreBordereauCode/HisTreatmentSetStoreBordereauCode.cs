using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.SetStoreBordereauCode
{
    class HisTreatmentSetStoreBordereauCode : BusinessBase
    {
        internal HisTreatmentSetStoreBordereauCode()
            : base()
        {

        }

        internal HisTreatmentSetStoreBordereauCode(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(StoreBordereauCodeSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.TreatmentId, ref raw);
                valid = valid && checker.IsLockHein(raw);
                valid = valid && checker.IsStoreBordereauCodeUsed(raw, data.StoreBordereauCode);

                if (valid)
                {
                    raw.STORE_BORDEREAU_CODE = data.StoreBordereauCode;

                    if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_TREATMENT SET STORE_BORDEREAU_CODE = :param1 WHERE ID = :param2", raw.STORE_BORDEREAU_CODE, raw.ID))
                    {
                        throw new Exception("Update HisTreatment that bai");
                    }
                    resultData = raw;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
