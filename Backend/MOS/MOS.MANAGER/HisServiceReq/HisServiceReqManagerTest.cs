using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisServiceReq.Test.Microbiology;
using MOS.MANAGER.HisServiceReq.CheckSurgSimultaneily;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<HisTestServiceReqTDO>> GetTdo(long createTimeFrom, long createTimeTo, bool? isSpecimen, string roomTypeCode, string kskContractCode, string executeDepartmentCode, bool? hasContract)
        {
            ApiResultObject<List<HisTestServiceReqTDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisTestServiceReqTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqTestGet(param).GetTdo(createTimeFrom, createTimeTo, isSpecimen, roomTypeCode, kskContractCode, executeDepartmentCode, hasContract);
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
        public ApiResultObject<List<HisTestServiceReqTDO>> GetTdo(long createTimeFrom, long createTimeTo, bool? isSpecimen, string roomTypeCode)
        {
            ApiResultObject<List<HisTestServiceReqTDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisTestServiceReqTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqTestGet(param).GetTdo(createTimeFrom, createTimeTo, isSpecimen, roomTypeCode);
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
        public ApiResultObject<HisTestServiceReqTDO> GetTdo(string serviceReqCode, bool? isSpecimen)
        {
            ApiResultObject<HisTestServiceReqTDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisTestServiceReqTDO resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqTestGet(param).GetTdo(serviceReqCode, isSpecimen);
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
        public ApiResultObject<List<HisTestServiceReqTDO>> GetTdoByTurnCode(string turnCode, bool? isSpecimen)
        {
            ApiResultObject<List<HisTestServiceReqTDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisTestServiceReqTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqTestGet(param).GetTdoByTurnCode(turnCode, isSpecimen);
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
        public ApiResultObject<List<HisTestServiceReqTDO>> GetTdoByTreatmentCode(string treatmentCode, bool? isSpecimen)
        {
            ApiResultObject<List<HisTestServiceReqTDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisTestServiceReqTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqTestGet(param).GetTdoByTreatmentCode(treatmentCode, isSpecimen);
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
        public ApiResultObject<bool> RequestOrder(long serviceReqId)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceReqRequestOrder(param).Run(serviceReqId);
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
        /// Xu ly y/c cap nhat ket qua xet nghiem tu phan mem cua Labconn
        /// </summary>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<bool> UpdateResult(HisTestResultTDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceReqTestUpdate(param).UpdateResult(data);
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
        /// Xu ly y/c cap nhat ket qua xet nghiem tu phan mem cua roche
        /// </summary>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<bool> UpdateResult(string data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceReqTestUpdate(param).UpdateResult(data);
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
        public ApiResultObject<bool> UpdateSpecimen(HisTestServiceReqTDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceReqTestUpdate(param).UpdateSpecimen(data);
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
        /// Xu ly y/c cap nhat ket qua vi sinh tu phan mem cua Labconn
        /// Tra ket qua Soi Cay
        /// </summary>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<bool> CultureResult(CultureResultTDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceReqCultureResult(param).Run(data);
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
        /// Xu ly y/c cap nhat ket qua vi sinh tu phan mem cua Labconn
        /// Tra ket qua Khang Sinh Do
        /// </summary>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<bool> AntibioticMapResult(AntibioticMapResultTDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceReqAntibioticMapResult(param).Run(data);
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
        /// chi cap nhat thong tin TEST_SAMPLE_TYPE_ID
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<HIS_SERVICE_REQ> UpdateSampleType(HIS_SERVICE_REQ data)
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
                    resultData = new HisServiceReqTestUpdateSampleType(param).Update(data, ref resultData);
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
        public ApiResultObject<List<TestResultI3DrugsTDO>> GetResultForI3Drugs(TestResultI3DrugsFilter data)
        {
            ApiResultObject<List<TestResultI3DrugsTDO>> result = new ApiResultObject<List<TestResultI3DrugsTDO>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                List<TestResultI3DrugsTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqTestGet(param).GetResultForI3Drugs(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

         /// <summary>
         /// Xu ly y/c cap nhat barcode tu phan mem cua Labconn
         /// </summary>
         /// <returns></returns>
         [Logger]
         public ApiResultObject<bool> UpdateBarcode(HisTestUpdateBarcodeTDO data)
         {
             ApiResultObject<bool> result = new ApiResultObject<bool>(false);

             try
             {
                 bool valid = true;
                 valid = valid && IsNotNull(data);
                 bool resultData = false;
                 if (valid)
                 {
                     resultData = new HisServiceReqTestUpdate(param).UpdateBarcode(data);
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
         public ApiResultObject<HisTestDetailTDO> GetDetailBySearchCode(string searchCode)
         {
             ApiResultObject<HisTestDetailTDO> result = null;

             try
             {
                 bool valid = true;
                 valid = valid && IsNotNull(param);
                 HisTestDetailTDO resultData = null;
                 if (valid)
                 {
                     resultData = new HisServiceReqTestGet(param).GetDetailBySearchCode(searchCode);
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
         public ApiResultObject<bool> CheckSurgSimultaneily(HisSurgServiceReqUpdateListSDO data)
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
                     resultData = new CheckSurgSimultaneilyProcessor(param).Run(data);
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
         public ApiResultObject<bool> ConfirmNoExcute(HisTestConfirmNoExcuteTDO data)
         {
             ApiResultObject<bool> result = new ApiResultObject<bool>(false);

             try
             {
                 bool valid = true;
                 valid = valid && IsNotNull(data);
                 bool resultData = false;
                 if (valid)
                 {
                     resultData = new HisServiceReqTestUpdate(param).ConfirmNoExcute(data);
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
