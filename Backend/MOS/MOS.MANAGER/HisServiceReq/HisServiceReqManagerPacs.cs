using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Pacs.Start;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisServiceReq.Test.Microbiology;
using MOS.MANAGER.HisServiceReq.Pacs.PacsUpdateResult;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        /// <summary>
        /// Xu ly y/c cap nhat trang thai cua chi dinh khi ben PACS xu ly
        /// </summary>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<bool> PacsStart(string accessionNumber)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(accessionNumber);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceReqPacsStart(param).Run(accessionNumber);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        /// <summary>
        /// Xu ly y/c cap nhat huy trang thai cua chi dinh khi ben PACS xu ly
        /// </summary>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<bool> PacsUnstart(string accessionNumber)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(accessionNumber);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceReqPacsUnstart(param).Run(accessionNumber);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> UpdateResult(HisPacsResultTDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new PacsServiceReqUpdateResult(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        public PacsHl7TDO UpdateResultHl7(PacsHl7TDO data)
        {
            PacsHl7TDO result = new PacsHl7TDO();

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    result = new PacsServiceReqUpdateResultByHl7(param).Run(data);
                }
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
