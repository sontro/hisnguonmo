using COS.SDO;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.CosCard;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatient.UpdateCard
{
    class HisPatientUpdateCardCheck : BusinessBase
    {
        internal HisPatientUpdateCardCheck()
            : base()
        {

        }

        internal HisPatientUpdateCardCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool VerifyCosCard(string cardCode, ref CardOwnerSDO cardSdo)
        {
            bool valid = true;
            try
            {
                CardOwnerSDO cardOwnerSDO = new CosCardGet().GetByServiceCode(cardCode);
                if (cardOwnerSDO == null)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCard_KhongLayDuocThongTinTheYTe, cardCode);
                    return false;
                }
                cardSdo = cardOwnerSDO;

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifySameInfo(HIS_PATIENT patient, CardOwnerSDO cardSdo)
        {
            bool valid = true;
            try
            {
                if (patient.VIR_PATIENT_NAME.ToLower().Trim() != cardSdo.People.VIR_PEOPLE_NAME.ToLower().Trim())
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCard_KhongTrungThongTinVoiChuThe, patient.VIR_PATIENT_NAME, cardSdo.CardCode);
                    return false;
                }

                if (patient.GENDER_ID != cardSdo.People.GENDER_ID)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCard_KhongTrungThongTinVoiChuThe, patient.VIR_PATIENT_NAME, cardSdo.CardCode);
                    return false;
                }

                if (patient.DOB != cardSdo.People.DOB)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCard_KhongTrungThongTinVoiChuThe, patient.VIR_PATIENT_NAME, cardSdo.CardCode);
                    return false;
                }

                if ((patient.IS_HAS_NOT_DAY_DOB == Constant.IS_TRUE
                    && cardSdo.People.IS_HAS_NOT_DAY_DOB != Constant.IS_TRUE)
                    || (patient.IS_HAS_NOT_DAY_DOB != Constant.IS_TRUE
                    && cardSdo.People.IS_HAS_NOT_DAY_DOB == Constant.IS_TRUE))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCard_KhongTrungThongTinVoiChuThe, patient.VIR_PATIENT_NAME, cardSdo.CardCode);
                    return false;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

    }
}
