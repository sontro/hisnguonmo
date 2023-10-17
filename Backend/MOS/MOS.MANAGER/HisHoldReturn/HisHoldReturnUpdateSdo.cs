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
using AutoMapper;
using Inventec.Common.ObjectChecker;

namespace MOS.MANAGER.HisHoldReturn
{
    partial class HisHoldReturnUpdateSdo : BusinessBase
    {
        private HisHoldReturnUpdate holdReturnUpdate;
        private HisHoreDhtyCreate horeDhtyCreate;
        private HisHoreDhtyTruncate horeDhtyTruncate;

        internal HisHoldReturnUpdateSdo()
            : base()
        {
            this.Init();
        }

        internal HisHoldReturnUpdateSdo(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.holdReturnUpdate = new HisHoldReturnUpdate(param);
            this.horeDhtyCreate = new HisHoreDhtyCreate(param);
            this.horeDhtyTruncate = new HisHoreDhtyTruncate(param);
        }

        internal bool Update(HisHoldReturnUpdateSDO data, ref HIS_HOLD_RETURN resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_HOLD_RETURN holdReturn = null;
                HIS_TREATMENT treatment = null;
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsValidData(data);
                valid = valid && checker.VerifyId(data.Id, ref holdReturn);
                valid = valid && treatChecker.VerifyId(data.TreatmentId, ref treatment);
                if (valid)
                {
                    this.ProcessHoldReturn(data, treatment.PATIENT_ID, workPlace, holdReturn);
                    this.ProcessHoreDhty(data, holdReturn);
                    resultData = holdReturn;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessHoreDhty(HisHoldReturnUpdateSDO data, HIS_HOLD_RETURN holdReturn)
        {
            List<HIS_HORE_DHTY> horeDhties = new HisHoreDhtyGet().GetByHoldReturnId(holdReturn.ID);

            List<HIS_HORE_DHTY> toInserts = data.DocHoldTypeIds
                .Where(o => horeDhties == null || !horeDhties.Exists(t => t.DOC_HOLD_TYPE_ID == o))
                .Select(o => new HIS_HORE_DHTY
                {
                    DOC_HOLD_TYPE_ID = o,
                    HOLD_RETURN_ID = holdReturn.ID
                }).ToList();

            List<HIS_HORE_DHTY> toDeletes = horeDhties != null ? horeDhties
                .Where(o => !data.DocHoldTypeIds.Contains(o.DOC_HOLD_TYPE_ID)).ToList() : null;

            if (IsNotNullOrEmpty(toInserts) && !this.horeDhtyCreate.CreateList(toInserts))
            {
                throw new Exception("Tao HIS_HORE_DHTY that bai");
            }

            if (IsNotNullOrEmpty(toDeletes) && !this.horeDhtyTruncate.TruncateList(toDeletes))
            {
                throw new Exception("Xoa HIS_HORE_DHTY that bai");
            }
        }

        private void ProcessHoldReturn(HisHoldReturnUpdateSDO data, long patientId, WorkPlaceSDO workPlace, HIS_HOLD_RETURN holdReturn)
        {
            Mapper.CreateMap<HIS_HOLD_RETURN, HIS_HOLD_RETURN>();
            HIS_HOLD_RETURN before = Mapper.Map<HIS_HOLD_RETURN>(holdReturn);

            holdReturn.PATIENT_ID = patientId;
            holdReturn.HOLD_ROOM_ID = data.WorkingRoomId;
            holdReturn.HOLD_TIME = data.HoldTime;
            holdReturn.HOLD_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            holdReturn.HOLD_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            holdReturn.RESPONSIBLE_ROOM_ID = data.WorkingRoomId;
            holdReturn.HEIN_CARD_NUMBER = data.HeinCardNumber;
            holdReturn.TREATMENT_ID = data.TreatmentId;

            //Chi update neu co su thay doi
            if (ValueChecker.IsPrimitiveDiff<HIS_HOLD_RETURN>(before, holdReturn)
                && !this.holdReturnUpdate.Update(holdReturn))
            {
                throw new Exception("Update HIS_HOLD_RETURN that bai");
            }
        }

        internal void Rollback()
        {
            this.holdReturnUpdate.RollbackData();
        }

        private bool IsValidData(HisHoldReturnUpdateSDO data)
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
