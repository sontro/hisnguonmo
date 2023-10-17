using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.Common.Truncate;
using MOS.MANAGER.HisServiceReq.Common.Update.SereServ;
using MOS.MANAGER.HisServiceReq.Exam;
using MOS.MANAGER.HisServiceReq.Update.ChangeListRoom;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.MANAGER.HisServiceReq.UpdateCommonInfo;
using MOS.MANAGER.HisServiceReq.UpdateSampleInfo;
using MOS.MANAGER.HisServiceReq.UpdateIsNotRequireFee;
using MOS.SDO;
using MOS.TDO;
using System.Linq;
using System;
using System.Collections.Generic;
using MOS.MANAGER.HisServiceReq.Common.Update;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq.Prescription.TemporaryPres;


namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        public HisServiceReqManager()
            : base()
        {

        }

        public HisServiceReqManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERVICE_REQ>> Get(HisServiceReqFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_REQ>> result = null;

            try
            {

                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_REQ> resultData = null;
                if (valid)
                {
                    filter.IS_RESTRICTED_KSK = HisKskContractCFG.RESTRICTED_ACCESSING;
                    resultData = new HisServiceReqGet(param).Get(filter);
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
        public ApiResultObject<List<string>> GetAttachAssignPrint(long treatemendId)
        {
            ApiResultObject<List<string>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(treatemendId);
                List<string> resultData = new List<string>();
                if (valid)
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.TREATMENT_ID = treatemendId;
                    filter.IS_NO_EXECUTE = false;
                    filter.HAS_ATTACH_ASSIGN_PRINT_TYPE_CODE = true;
                    var listServiceReqs = new HisServiceReqGet(param).Get(filter);
                    foreach (var service in listServiceReqs)
                    {
                        var listCodes = service.ATTACH_ASSIGN_PRINT_TYPE_CODE.Split(',').ToList();
                        if (IsNotNullOrEmpty(listCodes))
                        {
                            resultData.AddRange(listCodes);
                        }
                    }
                    if (IsNotNullOrEmpty(resultData))
                        resultData = resultData.Distinct().ToList();
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
        public ApiResultObject<List<V_HIS_SERVICE_REQ>> GetView(HisServiceReqViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_REQ>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqGet(param).GetView(filter);
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
        public ApiResultObject<HIS_SERVICE_REQ> ChangeRoom(HIS_SERVICE_REQ data)
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
                    new HisServiceReqUpdateChangeRoom(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_SERVICE_REQ>> UpdateIsNotRequireFee(List<long> serviceReqIds)
        {
            ApiResultObject<List<HIS_SERVICE_REQ>> result = new ApiResultObject<List<HIS_SERVICE_REQ>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_SERVICE_REQ> resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdateIsNotRequireFee(param).Run(serviceReqIds, ref resultData);
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
        public ApiResultObject<bool> ChangeRoom(ChangeRoomSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool resultData = false;
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    resultData = new HisServiceReqUpdateChangeListRoom(param).Run(data);
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
        public ApiResultObject<HIS_SERVICE_REQ> Update(HIS_SERVICE_REQ data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_SERVICE_REQ resultData = null;
                if (valid && new HisServiceReqUpdateCommonInfo(param).UpdateCommonInfo(data))
                {
                    resultData = data;
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
        public ApiResultObject<List<HIS_SERVICE_REQ>> UpdateSentErx(List<HIS_SERVICE_REQ> data)
        {
            ApiResultObject<List<HIS_SERVICE_REQ>> result = new ApiResultObject<List<HIS_SERVICE_REQ>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_SERVICE_REQ> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceReqUpdateSentErx(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> UpdateCommonInfo(HIS_SERVICE_REQ data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_SERVICE_REQ resultData = null;
                if (valid && new HisServiceReqUpdateCommonInfo(param).UpdateCommonInfo(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_SERVICE_REQ> UpdateSampleInfo(ServiceReqSampleInfoSDO sdo)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool isSuccess = false;
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    isSuccess = new HisServiceReqUpdateSampleInfo(param).Run(sdo, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERVICE_REQ> UpdateJsonForm(HIS_SERVICE_REQ data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ resultData = null;
                if (valid && new HisServiceReqUpdateJsonForm(param).Run(data))
                {
                    resultData = data;
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
        public ApiResultObject<HisServiceReqUpdateResultSDO> UpdateSereServ(HisServiceReqUpdateSDO data)
        {
            ApiResultObject<HisServiceReqUpdateResultSDO> result = new ApiResultObject<HisServiceReqUpdateResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisServiceReqUpdateResultSDO resultData = null;
                ServiceReqDetailSDO sdo = null;
                if (valid)
                {
                    new HisServiceReqUpdateSereServ(param).Update(data, ref resultData, ref sdo);
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
        public ApiResultObject<HIS_SERVICE_REQ> UpdateJsonPrintId(HIS_SERVICE_REQ data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdate(param).UpdateJsonPrintId(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> Start(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdateStart(param).Start(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> StartWithTime(HIS_SERVICE_REQ data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceReqUpdateStart(param).StartWithTime(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> Start(string code)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdateStart(param).Start(code, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> Unstart(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdateStart(param).Unstart(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> Call(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdate(param).Call(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> Finish(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdateFinish(param).Finish(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> FinishWithTime(HIS_SERVICE_REQ data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdateFinish(param).FinishWithTime(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> Unfinish(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdateUnfinish(param).Run(id, ref resultData);
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
        public ApiResultObject<bool> SendToOldSystem(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool rs = new HisServiceReqSendToOldSystem(param).Run(id);
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
        public ApiResultObject<bool> Delete(HisServiceReqSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    result = this.PackSingleResult(new HisServiceReqTruncate(param).Truncate(data));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HisServiceReqTDO>> GetResultForEmr(HisServiceReqResultForEmrFilter filter)
        {
            ApiResultObject<List<HisServiceReqTDO>> result = null;

            try
            {

                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisServiceReqTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqGet(param).GetResultForEmr(filter);
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
        public ApiResultObject<PrescriptionTDO> GetPrescriptionByCode(string ServiceReqCode)
        {
            ApiResultObject<PrescriptionTDO> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                PrescriptionTDO resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqGet(param).GetPrescriptionByCode(ServiceReqCode);
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
        public ApiResultObject<HIS_SERVICE_REQ> UpdateTemporaryPres(TemporaryServiceReqSDO sdo)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool isSuccess = false;
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    isSuccess = new HisServiceReqUpdateTemporaryPres(param).Run(sdo, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERVICE_REQ> UpdateToTemporaryPres(HIS_SERVICE_REQ data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqUpdate(param).UpdateToTemporaryPres(data, ref resultData);
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
    }
}
