using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Aggr;
using MOS.MANAGER.HisExpMest.Aggr.Approve;
using MOS.MANAGER.HisExpMest.Aggr.Create;
using MOS.MANAGER.HisExpMest.Aggr.Delete;
using MOS.MANAGER.HisExpMest.Aggr.Export;
using MOS.MANAGER.HisExpMest.Aggr.Remove;
using MOS.MANAGER.HisExpMest.Aggr.Unapprove;
using MOS.MANAGER.HisExpMest.Aggr.Unexport;
using MOS.MANAGER.HisExpMest.PharmacyCashier;
using MOS.MANAGER.HisExpMest.PharmacyCashier.ExpCancel;
using MOS.MANAGER.HisExpMest.PharmacyCashier.ExpInvoice;
using MOS.MANAGER.HisExpMest.PharmacyCashier.Get;
using MOS.MANAGER.HisExpMest.PharmacyCashier.Pay;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest
{
    public partial class HisExpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<bool> PharmacyCashierExpCancel(PharmacyCashierExpCancelSDO sdo)
        {
            ApiResultObject<bool> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new PharmacyCashierExpCancel(param).Run(sdo);
                }
                result = this.PackResult(resultData, resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<DHisTransExpSDO>> PharmacyCashierGet(DHisTransExpFilter filter)
        {
            ApiResultObject<List<DHisTransExpSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<DHisTransExpSDO> resultData = null;
                if (valid)
                {
                    resultData = new PharmacyCashierGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<PharmacyCashierResultSDO> PharmacyCashierPay(PharmacyCashierSDO data)
        {
            ApiResultObject<PharmacyCashierResultSDO> result = new ApiResultObject<PharmacyCashierResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                PharmacyCashierResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new PharmacyCashierPay(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TRANSACTION> PharmacyCashierExpInvoice(PharmacyCashierExpInvoiceSDO data)
        {
            ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSACTION resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new PharmacyCashierExpInvoice(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
