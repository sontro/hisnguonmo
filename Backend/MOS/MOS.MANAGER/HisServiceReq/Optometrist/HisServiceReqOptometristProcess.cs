using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Optometrist
{
    /// <summary>
    /// Xu ly do thi luc
    /// </summary>
    class HisServiceReqOptometristProcess : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqOptometristProcess()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqOptometristProcess(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(HisServiceReqOptometristSDO data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;

                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                HIS_SERVICE_REQ serviceReq = null;
                HIS_TREATMENT treatment = null;
                valid = valid && serviceReqChecker.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && serviceReqChecker.HasExecute(serviceReq);
                valid = valid && serviceReqChecker.IsNotFinished(serviceReq);
                valid = valid && treatChecker.VerifyId(serviceReq.TREATMENT_ID, ref treatment);
                valid = valid && treatChecker.IsUnpause(treatment);
                if (valid)
                {
                    serviceReq.IS_FIRST_OPTOMETRIST = data.IsFirstOptometrist ? (short?)Constant.IS_TRUE : null;
                    serviceReq.OPTOMETRIST_TIME = data.OptometristTime;
                    serviceReq.FORESIGHT_RIGHT_EYE = data.ForesightRightEye;
                    serviceReq.FORESIGHT_LEFT_EYE = data.ForesightLeftEye;
                    serviceReq.FORESIGHT_RIGHT_GLASS_HOLE = data.ForesightRightGlassHole;
                    serviceReq.FORESIGHT_LEFT_GLASS_HOLE = data.ForesightLeftGlassHole;
                    serviceReq.FORESIGHT_RIGHT_USING_GLASS = data.ForesightRightUsingGlass;
                    serviceReq.FORESIGHT_LEFT_USING_GLASS = data.ForesightLeftUsingGlass;
                    serviceReq.REFACTOMETRY_RIGHT_EYE = data.RefactometryRightEye;
                    serviceReq.REFACTOMETRY_LEFT_EYE = data.RefactometryLeftEye;
                    serviceReq.BEFORE_LIGHT_REFLECTION_RIGHT = data.BeforeLightReflectionRight;
                    serviceReq.BEFORE_LIGHT_REFLECTION_LEFT = data.BeforeLightReflectionLeft;
                    serviceReq.AFTER_LIGHT_REFLECTION_RIGHT = data.AfterLightReflectionRight;
                    serviceReq.AFTER_LIGHT_REFLECTION_LEFT = data.AfterLightReflectionLeft;
                    serviceReq.AJUSTABLE_GLASS_FORESIGHT = data.AjustableGlassForesight;
                    serviceReq.AJUSTABLE_GLASS_FORESIGHT_R = data.AjustableGlassForesightR;
                    serviceReq.AJUSTABLE_GLASS_FORESIGHT_L = data.AjustableGlassForesightL;
                    serviceReq.NEARSIGHT_GLASS_RIGHT_EYE = data.NearsightGlassRightEye;
                    serviceReq.NEARSIGHT_GLASS_LEFT_EYE = data.NearsightGlassLeftEye;
                    serviceReq.NEARSIGHT_GLASS_READING_DIST = data.NearsightGlassReadingDist;
                    serviceReq.NEARSIGHT_GLASS_PUPIL_DIST = data.NearsightGlassPupilDist;
                    serviceReq.REOPTOMETRIST_APPOINTMENT = data.ReoptometristAppointment;
                    serviceReq.FORESIGHT_USING_GLASS_DEGREE_R = data.ForesightUsingGlassDegreeR;
                    serviceReq.FORESIGHT_USING_GLASS_DEGREE_L = data.ForesightUsingGlassDegreeL;
                    serviceReq.EXECUTE_LOGINNAME = data.ExecuteLoginname;
                    serviceReq.EXECUTE_USERNAME = data.ExecuteUsername;

                    if (data.IsFinish)
                    {
                        serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                        serviceReq.FINISH_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    }

                    if (this.hisServiceReqUpdate.Update(serviceReq, false))
                    {
                        result = true;
                        resultData = serviceReq;
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
