using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceType
{
    class HisServiceTypeUpdate : BusinessBase
    {
        internal HisServiceTypeUpdate()
            : base()
        {

        }

        internal HisServiceTypeUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceTypeCheck checker = new HisServiceTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.SERVICE_TYPE_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisServiceTypeDAO.Update(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool UpdateList(List<HIS_SERVICE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceTypeCheck checker = new HisServiceTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.SERVICE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceTypeDAO.UpdateList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool UpdateSdo(ServiceTypeUpdateSDO data, ref HIS_SERVICE_TYPE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceTypeCheck checker = new HisServiceTypeCheck(param);
                HIS_SERVICE_TYPE raw = null;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.ServiceTypeId);
                valid = valid && checker.VerifyId(data.ServiceTypeId, ref raw);
                valid = valid && IsGreaterThanZero(raw.ID);
                valid = valid && checker.IsUnLock(raw.ID);
                valid = valid && checker.ExistsCode(raw.SERVICE_TYPE_CODE, raw.ID);
                if (valid)
                {
                    raw.NUM_ORDER = data.NumOrder;
                    if (data.IsAutoSplitReq.HasValue && data.IsAutoSplitReq.Value)
                    {
                        raw.IS_AUTO_SPLIT_REQ = UTILITY.Constant.IS_TRUE;
                    }
                    else
                    {
                        raw.IS_AUTO_SPLIT_REQ = null;
                    }
                    if (data.IsNotDisplayAssign)
                    {
                        raw.IS_NOT_DISPLAY_ASSIGN = UTILITY.Constant.IS_TRUE;
                    }
                    else
                    {
                        raw.IS_NOT_DISPLAY_ASSIGN = null;
                    }
                    if (data.IsSplitReqBySampleType == true)
                    {
                        raw.IS_SPLIT_REQ_BY_SAMPLE_TYPE = UTILITY.Constant.IS_TRUE;
                    }
                    else
                    {
                        raw.IS_SPLIT_REQ_BY_SAMPLE_TYPE = null;
                    }

                    if (data.IsRequiredSampleType == true)
                    {
                        raw.IS_REQUIRED_SAMPLE_TYPE = UTILITY.Constant.IS_TRUE;
                    }
                    else
                    {
                        raw.IS_REQUIRED_SAMPLE_TYPE = null;
                    }
                    result = DAOWorker.HisServiceTypeDAO.Update(raw);
                    if (result)
                    {
                        resultData = raw;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
