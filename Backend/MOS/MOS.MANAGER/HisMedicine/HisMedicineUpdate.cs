using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicine
{
    class HisMedicineUpdate : BusinessBase
    {
        private HIS_MEDICINE beforeUpdateHisMedicineDTO;
        private List<HIS_MEDICINE> beforeUpdateHisMedicineDTOs = new List<HIS_MEDICINE>();

        internal HisMedicineUpdate()
            : base()
        {

        }

        internal HisMedicineUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDICINE raw = null;
                HisMedicineCheck checker = new HisMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckChangePrice(data, raw);
                if (valid)
                {
                    if (!DAOWorker.HisMedicineDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisMedicineDTOs.Add(raw);
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

        internal bool Update(HIS_MEDICINE data, HIS_MEDICINE before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDICINE raw = null;
                HisMedicineCheck checker = new HisMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisMedicineDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisMedicineDTO = raw;
                    HisMedicineLog.Run(data, raw, LibraryEventLog.EventLog.Enum.HisMedicine_Sua);
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

        internal bool UpdateList(List<HIS_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineCheck checker = new HisMedicineCheck(param);
                List<HIS_MEDICINE> listRaw = new List<HIS_MEDICINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisMedicineDTOs = listRaw;
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

        internal bool UpdateList(List<HIS_MEDICINE> listData, List<HIS_MEDICINE> listBefores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineCheck checker = new HisMedicineCheck(param);
                valid = valid && checker.IsUnLock(listBefores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisMedicineDTOs = listBefores;
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
            if (this.beforeUpdateHisMedicineDTO != null)
            {
                if (!DAOWorker.HisMedicineDAO.Update(this.beforeUpdateHisMedicineDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicine that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineDTO", this.beforeUpdateHisMedicineDTO));
                }
            }

            if (this.beforeUpdateHisMedicineDTOs != null)
            {
                if (!DAOWorker.HisMedicineDAO.UpdateList(this.beforeUpdateHisMedicineDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicine that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineDTOs", this.beforeUpdateHisMedicineDTOs));
                }
            }
        }
    }
}
