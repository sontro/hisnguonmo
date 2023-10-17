using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Kiosk
{
    /// <summary>
    /// Dang ky kham tren cay kiosk
    /// </summary>
    class HisServiceReqExamRegisterPartnerKiosk : BusinessBase
    {
        private HisServiceReqExamRegister hisServiceReqExamRegister;

        internal HisServiceReqExamRegisterPartnerKiosk()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqExamRegisterPartnerKiosk(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqExamRegister = new HisServiceReqExamRegister(param);

        }

        /// <summary>
        /// Dang ky kham tren cay kiosk
        /// </summary>
        /// <param name="tdo"></param>
        /// <param name="resultData"></param>
        /// <returns></returns>
        internal bool Run(HisRegisterKioskSDO sdo, ref HisServiceReqExamRegisterResultSDO resultData)
        {
            bool result = false;
            try
            {
                HisServiceReqExamRegisterSDO registerSdo = new DataPreparer(param).ToRegisterKioskSdo(sdo);
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workPlace = null;
                result = this.hisServiceReqExamRegister.Create(registerSdo, false, ref resultData, ref treatment, ref workPlace);
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
