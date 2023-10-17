using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Bed;
using MOS.MANAGER.HisServiceReq.Exam;
using MOS.MANAGER.HisServiceReq.Exam.Add;
using MOS.MANAGER.HisServiceReq.Exam.Change;
using MOS.MANAGER.HisServiceReq.Exam.ChangeMain;
using MOS.MANAGER.HisServiceReq.Exam.Process;
using MOS.MANAGER.HisServiceReq.Exam.Register.Dkk;
using MOS.MANAGER.HisServiceReq.Exam.Register.Kiosk;
using MOS.MANAGER.HisServiceReq.Exam.Register.Receptionist;
using MOS.MANAGER.HisServiceReq.Paan;
using MOS.MANAGER.HisServiceReq.Surg;
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
        public ApiResultObject<V_HIS_SERVICE_REQ> ExamAddition(HisServiceReqExamAdditionSDO data)
        {
            ApiResultObject<V_HIS_SERVICE_REQ> result = new ApiResultObject<V_HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                V_HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqExamAddition(param).Run(data, ref resultData);
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
        public ApiResultObject<HisServiceReqResultSDO> ExamChange(HisServiceReqExamChangeSDO data)
        {
            ApiResultObject<HisServiceReqResultSDO> result = new ApiResultObject<HisServiceReqResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisServiceReqResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqExamChange(param).Run(data, ref resultData);
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
        /// Dang ky tiep don tu man hinh "Tiep don 1", "Tiep don 2" (tiep don boi nhan vien tiep don)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<HisServiceReqExamRegisterResultSDO> ExamRegister(HisServiceReqExamRegisterSDO data)
        {
            ApiResultObject<HisServiceReqExamRegisterResultSDO> result = new ApiResultObject<HisServiceReqExamRegisterResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisServiceReqExamRegisterResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqExamRegisterReceptionist(param).Run(data, ref resultData);
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
        /// Dang ky tiep don tren app HSSK (ket noi thong qua backend DKK)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<HisServiceReqExamRegisterResultSDO> ExamRegisterDkk(HisExamRegisterDkkSDO data)
        {
            ApiResultObject<HisServiceReqExamRegisterResultSDO> result = new ApiResultObject<HisServiceReqExamRegisterResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisServiceReqExamRegisterResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqExamRegisterDkk(param).Run(data, ref resultData);
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
        /// Dang ky qua kiosk cua Vietsens
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<HisServiceReqExamRegisterResultSDO> ExamRegisterKiosk(HisExamRegisterKioskSDO data)
        {
            ApiResultObject<HisServiceReqExamRegisterResultSDO> result = new ApiResultObject<HisServiceReqExamRegisterResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisServiceReqExamRegisterResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqExamRegisterKiosk(param).Run(data, ref resultData);
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
        /// Dang ky qua kiosk cua doi tac
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Logger]
        public ApiResultObject<HisServiceReqExamRegisterResultSDO> ExamRegisterKiosk(HisRegisterKioskSDO data)
        {
            ApiResultObject<HisServiceReqExamRegisterResultSDO> result = new ApiResultObject<HisServiceReqExamRegisterResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisServiceReqExamRegisterResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqExamRegisterPartnerKiosk(param).Run(data, ref resultData);
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
        public ApiResultObject<HisServiceReqExamUpdateResultSDO> ExamUpdate(HisServiceReqExamUpdateSDO data)
        {
            ApiResultObject<HisServiceReqExamUpdateResultSDO> result = new ApiResultObject<HisServiceReqExamUpdateResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisServiceReqExamUpdateResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqExamProcess(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ> ChangeMain(long serviceReqId)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqExamChangeMain(param).Run(serviceReqId, ref resultData);
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
