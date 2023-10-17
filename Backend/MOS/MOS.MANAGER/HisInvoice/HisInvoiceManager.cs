using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInvoice
{
    public partial class HisInvoiceManager : BusinessBase
    {
        public HisInvoiceManager()
            : base()
        {

        }

        public HisInvoiceManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_INVOICE>> Get(HisInvoiceFilterQuery filter)
        {
            ApiResultObject<List<HIS_INVOICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_INVOICE> resultData = null;
                if (valid)
                {
                    resultData = new HisInvoiceGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_INVOICE>> GetView(HisInvoiceViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_INVOICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_INVOICE> resultData = null;
                if (valid)
                {
                    resultData = new HisInvoiceGet(param).GetView(filter);
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
        public ApiResultObject<V_HIS_INVOICE> Create(HisInvoiceSDO data)
        {
            ApiResultObject<V_HIS_INVOICE> result = new ApiResultObject<V_HIS_INVOICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_INVOICE resultData = null;
                if (valid)
                {
                    new HisInvoiceCreate(param).Create(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_INVOICE> Cancel(HIS_INVOICE data)
        {
            ApiResultObject<HIS_INVOICE> result = new ApiResultObject<HIS_INVOICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE resultData = null;
                if (valid && new HisInvoiceUpdate(param).Cancel(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_INVOICE> UpdateInfo(HisInvoiceUpdateInfoSDO data)
        {
            ApiResultObject<HIS_INVOICE> result = new ApiResultObject<HIS_INVOICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE resultData = null;
                if (valid)
                {
                    new HisInvoiceUpdateInfo(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_INVOICE> ChangeLock(HIS_INVOICE data)
        {
            ApiResultObject<HIS_INVOICE> result = new ApiResultObject<HIS_INVOICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE resultData = null;
                if (valid && new HisInvoiceLock(param).ChangeLock(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
