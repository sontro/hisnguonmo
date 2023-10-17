using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ.Discount;
using MOS.MANAGER.HisSereServ.Truncate;
using MOS.MANAGER.HisSereServ.Update.AcceptNoExecute;
using MOS.MANAGER.HisSereServ.Update.PayslipInfo;
using MOS.MANAGER.HisSereServ.Update.BedAmount;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using MOS.MANAGER.HisSereServ.ConfirmNoExcute;
using MOS.MANAGER.HisSereServ.DeleteConfirmNoExcute;
using MOS.MANAGER.HisSereServ.UpdateMachine;

namespace MOS.MANAGER.HisSereServ
{
    public partial class HisSereServManager : BusinessBase
    {
        public HisSereServManager()
            : base()
        {

        }

        public HisSereServManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERE_SERV>> Get(HisSereServFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SERE_SERV>> GetView(HisSereServViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServGet(param).GetView(filter);
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
        public ApiResultObject<List<HIS_SERE_SERV>> GetSereServBhytOutpatientExam(HisSereServBhytOutpatientExamFilter filter)
        {
            ApiResultObject<List<HIS_SERE_SERV>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServGet(param).GetSereServBhytOutpatientExam(filter);
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
        public ApiResultObject<HIS_SERE_SERV> SwapService(SwapServiceSDO data)
        {
            ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV resultData = null;
                if (valid)
                {
                    new HisSereServSwapService(param).SwapService(data, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV> UpdateDiscount(HIS_SERE_SERV data)
        {
            ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV resultData = null;
                if (valid)
                {
                    new HisSereServUpdateDiscount(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV> AcceptNoExecute(HisSereServAcceptNoExecuteSDO data)
        {
            ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV resultData = null;
                if (valid)
                {
                    new HisSereServAcceptNoExecute(param).Accept(data, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV> UnacceptNoExecute(HisSereServAcceptNoExecuteSDO data)
        {
            ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV resultData = null;
                if (valid)
                {
                    new HisSereServAcceptNoExecute(param).Unaccept(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> AcceptNoExecuteByServiceReq(HisServiceReqAcceptNoExecuteSDO data)
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
                    new HisSereServAcceptNoExecuteByServiceReq(param).Accept(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> UnacceptNoExecuteByServiceReq(HisServiceReqAcceptNoExecuteSDO data)
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
                    new HisSereServAcceptNoExecuteByServiceReq(param).Unaccept(data, ref resultData);
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
        public ApiResultObject<HisSereServDiscountSDO> UpdateDiscountList(HisSereServDiscountSDO data)
        {
            ApiResultObject<HisSereServDiscountSDO> result = new ApiResultObject<HisSereServDiscountSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisSereServDiscountSDO resultData = null;
                if (valid)
                {
                    new HisSereServUpdateDiscount(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV> UpdateResult(HIS_SERE_SERV data)
        {
            ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV resultData = null;
                if (valid)
                {
                    new HisSereServUpdate(param).UpdateResult(data, ref resultData);
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
        public ApiResultObject<List<HIS_SERE_SERV>> UpdatePayslipInfo(HisSereServPayslipSDO data)
        {
            ApiResultObject<List<HIS_SERE_SERV>> result = new ApiResultObject<List<HIS_SERE_SERV>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERE_SERV> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServUpdatePayslipInfo(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_SERE_SERV>> UpdateEveryPayslipInfo(HisSereServPayslipSDO data)
        {
            ApiResultObject<List<HIS_SERE_SERV>> result = new ApiResultObject<List<HIS_SERE_SERV>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERE_SERV> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServUpdateEachPayslipInfo(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_SERE_SERV>> UpdateNoExecute(HisSereServNoExecuteSDO data)
        {
            ApiResultObject<List<HIS_SERE_SERV>> result = new ApiResultObject<List<HIS_SERE_SERV>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_SERE_SERV> resultData = null;
                if (valid)
                {
                    new HisSereServUpdateNoExecute(param).Run(data, ref resultData);
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
        public ApiResultObject<List<V_HIS_SERE_SERV>> UpdatePriceAndHeinInfoForAll(long treamentId)
        {
            ApiResultObject<List<V_HIS_SERE_SERV>> result = new ApiResultObject<List<V_HIS_SERE_SERV>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_SERE_SERV> resultData = null;
                if (valid)
                {
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(treamentId);
                    if (new HisSereServUpdateHein(param, treatment, true).UpdateDb())
                    {
                        resultData = new HisSereServGet().GetViewByTreatmentId(treamentId);
                    }
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

        public ApiResultObject<HisSereServWithFileSDO> Update(HIS_SERE_SERV data, List<FileHolder> files)
        {
            ApiResultObject<HisSereServWithFileSDO> result = new ApiResultObject<HisSereServWithFileSDO>(null);


            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisSereServWithFileSDO resultData = null;
                if (valid)
                {
                    new HisSereServUpdateFile(param).Update(data, files, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV> UpdateJsonPrintId(HIS_SERE_SERV data)
        {
            ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV resultData = null;
                if (valid)
                {
                    new HisSereServUpdate(param).UpdateJsonPrintId(data, ref resultData);
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
        public ApiResultObject<bool> ExamDelete(long data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisSereServExamTruncate(param).Run(data);
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
        public ApiResultObject<HIS_SERE_SERV> UpdateBedAmount(UpdateBedAmountSDO data)
        {
            ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServUpdateBedAmount(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV> UpdateBedTempAmount(UpdateBedAmountSDO data)
        {
            ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServUpdateBedTempAmount(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV> ConfirmNoExcute(HisSereServConfirmNoExcuteSDO data)
        {
            ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServConfirmNoExcute(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV> DeleteConfirmNoExcute(HisSereServDeleteConfirmNoExcuteSDO data)
        {
            ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServDeleteConfirmNoExcute(param).Run(data, ref resultData);
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
        public ApiResultObject<bool> UpdateMachine(List<HisSereServUpdateMachineSDO> data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisSereServUpdateMachine(param).Run(data);
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
