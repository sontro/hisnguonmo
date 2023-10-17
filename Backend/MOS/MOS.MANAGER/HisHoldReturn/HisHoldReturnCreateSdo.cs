using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisHoreDhty;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;

namespace MOS.MANAGER.HisHoldReturn
{
    partial class HisHoldReturnCreateSdo : BusinessBase
    {
        private HisHoldReturnCreate holdReturnCreate;
        private HisHoreDhtyCreate horeDhtyCreate;

        internal HisHoldReturnCreateSdo()
            : base()
        {
            this.Init();
        }

        internal HisHoldReturnCreateSdo(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.holdReturnCreate = new HisHoldReturnCreate(param);
            this.horeDhtyCreate = new HisHoreDhtyCreate(param);
        }

        internal bool Create(HisHoldReturnCreateSDO data, ref HIS_HOLD_RETURN resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsValidData(data);
                valid = valid && treatChecker.VerifyId(data.TreatmentId, ref treatment);
                if (valid)
                {
                    HIS_HOLD_RETURN rs = null;
                    this.ProcessHoldReturn(data, treatment.PATIENT_ID, workPlace, ref rs);
                    this.ProcessHoreDhty(data, rs);
                    resultData = rs;
                    result = true;
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

        private void ProcessHoreDhty(HisHoldReturnCreateSDO data, HIS_HOLD_RETURN holdReturn)
        {
            List<HIS_HORE_DHTY> horeDhties = data.DocHoldTypeIds.Select(o => new HIS_HORE_DHTY
                {
                    DOC_HOLD_TYPE_ID = o,
                    HOLD_RETURN_ID = holdReturn.ID
                }).ToList();

            if (!this.horeDhtyCreate.CreateList(horeDhties))
            {
                throw new Exception("Tao HIS_HORE_DHTY that bai");
            }
        }

        private void ProcessHoldReturn(HisHoldReturnCreateSDO data, long patientId, WorkPlaceSDO workPlace, ref HIS_HOLD_RETURN resultData)
        {
            HIS_HOLD_RETURN holdReturn = new HIS_HOLD_RETURN();
            holdReturn.PATIENT_ID = patientId;
            holdReturn.HOLD_ROOM_ID = data.WorkingRoomId;
            holdReturn.HOLD_TIME = Inventec.Common.DateTime.Get.Now().Value;
            holdReturn.HOLD_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            holdReturn.HOLD_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            holdReturn.RESPONSIBLE_ROOM_ID = data.WorkingRoomId;
            holdReturn.HEIN_CARD_NUMBER = data.HeinCardNumber;
            holdReturn.TREATMENT_ID = data.TreatmentId;
            holdReturn.IS_HANDOVERING = null;

            if (!this.holdReturnCreate.Create(holdReturn))
            {
                throw new Exception("Tao HIS_HOLD_RETURN that bai");
            }
            resultData = holdReturn;
        }

        internal void Rollback()
        {
        }

        private bool IsValidData(HisHoldReturnCreateSDO data)
        {
            try
            {
                if (!IsNotNullOrEmpty(data.DocHoldTypeIds))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.DocHoldTypeIds null");
                    return false;
                }
                if (data.TreatmentId <= 0)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.TreatmentId null");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
