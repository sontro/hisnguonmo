using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Linq;
using System.Collections.Generic;
using Inventec.Core;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisPatient;

namespace MOS.MANAGER.HisSereServTein
{
    class HisSereServTeinUpdate : BusinessBase
    {
        List<HIS_SERE_SERV_TEIN> recentSereServTeins = new List<HIS_SERE_SERV_TEIN>();
        internal HisSereServTeinUpdate()
            : base()
        {

        }

        internal HisSereServTeinUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERE_SERV_TEIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServTeinCheck checker = new HisSereServTeinCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    HIS_SERE_SERV sereServ = new HisSereServGet().GetById(data.SERE_SERV_ID);
                    HIS_PATIENT patient = sereServ != null ? new HisPatientGet().GetById(sereServ.TDL_PATIENT_ID.Value) : null;
                    HisSereServTeinDecorator.Decorator(data, patient.DOB, patient.GENDER_ID);
                    result = DAOWorker.HisSereServTeinDAO.Update(data);
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

        internal bool UpdateList(List<HIS_SERE_SERV_TEIN> listData, long dob, long genderId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServTeinCheck checker = new HisSereServTeinCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    foreach (HIS_SERE_SERV_TEIN t in listData)
                    {
                        HisSereServTeinDecorator.Decorator(t, dob, genderId);
                    }

                    result = DAOWorker.HisSereServTeinDAO.UpdateList(listData);
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

        internal bool UpdateList(List<HIS_SERE_SERV_TEIN> listData, List<HIS_SERE_SERV_TEIN> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServTeinCheck checker = new HisSereServTeinCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                valid = valid && checker.IsUnLock(listBefore);
                if (valid)
                {
                    if (!DAOWorker.HisSereServTeinDAO.UpdateList(listData))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisSereServTemp_CapNhatThatBai);
                        throw new Exception("cap nhat HIS_SERE_SERV_TEIN that bai");
                    }
                    this.recentSereServTeins.AddRange(listBefore);
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentSereServTeins))
            {
                if (!DAOWorker.HisSereServTeinDAO.UpdateList(this.recentSereServTeins))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServTein that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServTeins", this.recentSereServTeins));
                }
            }
        }
    }
}
