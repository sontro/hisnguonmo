using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public partial class HisTreatmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisTreatmentInvoiceInfoTDO> GetInvoiceInfo(string treatmentCode)
        {
            ApiResultObject<HisTreatmentInvoiceInfoTDO> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisTreatmentInvoiceInfoTDO resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentGet(param).GetInvoiceInfo(treatmentCode);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> SendToOldSystem(long id, bool isOldPatient)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool rs = new HisTreatmentSendToOldSystem(param).Run(id, isOldPatient);
                result = this.PackResult(rs, rs);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> TransferXml(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool rs = new HisTreatmentTransferXml(param).Run(ids);
                result = this.PackResult(rs, rs);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
