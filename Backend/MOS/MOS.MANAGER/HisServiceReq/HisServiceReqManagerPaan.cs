using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Common.Update.Paan;
using MOS.MANAGER.HisServiceReq.Paan;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisServiceReqResultSDO> PaanCreate(HisPaanServiceReqSDO data)
        {
            ApiResultObject<HisServiceReqResultSDO> result = new ApiResultObject<HisServiceReqResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisServiceReqResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqPaanCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> PaanTakeSample(long data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdatePaanSample(param).Take(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> PaanCancelSample(long data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdatePaanSample(param).Cancel(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> PaanUpdateBlock(PaanBlockSDO data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdatePaanBlock(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> PaanUpdateIntructionNote(PaanIntructionNoteSDO data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdatePaanIntructionNote(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_SERVICE_REQ>> PaanUpdateListBlock(List<PaanBlockSDO> data)
        {
            ApiResultObject<List<HIS_SERVICE_REQ>> result = new ApiResultObject<List<HIS_SERVICE_REQ>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(data);
                List<HIS_SERVICE_REQ> resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdateListPaanBlock(param).Run(data, ref resultData);
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
