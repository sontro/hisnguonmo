using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update
{
    class HisTreatmentUpdateXml130Info: BusinessBase
    {
        internal HisTreatmentUpdateXml130Info(CommonParam param)
            : base(param)
        {

        }
        internal bool UpdateXml130Info(HisTreatmentXmlResultSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && this.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.TreatmentId, ref treatment);
                if (valid)
                {
                    if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_TREATMENT SET XML130_RESULT = :param1, XML130_DESC = :param2, XML130_CHECK_CODE = :param3 WHERE ID = :param4", data.XmlResult, data.Description, data.CheckCode, treatment.ID))
                    {
                        throw new Exception("Update XML130_RESULT,XML130_DESC,XML130_CHECK_CODE cho HIS_TREATMENT that bai");
                    }
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

        private bool VerifyRequireField(HisTreatmentXmlResultSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
