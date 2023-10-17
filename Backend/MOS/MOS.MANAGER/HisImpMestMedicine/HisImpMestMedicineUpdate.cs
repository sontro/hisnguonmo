using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestMedicine
{
    class HisImpMestMedicineUpdate : BusinessBase
    {
        private List<HIS_IMP_MEST_MEDICINE> beforeUpdateHisImpMestMedicines = new List<HIS_IMP_MEST_MEDICINE>();

        internal HisImpMestMedicineUpdate()
            : base()
        {

        }

        internal HisImpMestMedicineUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_MEST_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestMedicineCheck checker = new HisImpMestMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_IMP_MEST_MEDICINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisImpMestMedicineDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestMedicine that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisImpMestMedicines.Add(raw);
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

        internal bool Update(HIS_IMP_MEST_MEDICINE data, HIS_IMP_MEST_MEDICINE before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestMedicineCheck checker = new HisImpMestMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisImpMestMedicineDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestMedicine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisImpMestMedicines.Add(before);
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

        internal bool UpdateList(List<HIS_IMP_MEST_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestMedicineCheck checker = new HisImpMestMedicineCheck(param);
                List<HIS_IMP_MEST_MEDICINE> listRaw = new List<HIS_IMP_MEST_MEDICINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestMedicineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisImpMestMedicines.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_IMP_MEST_MEDICINE> listData, List<HIS_IMP_MEST_MEDICINE> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                valid = valid && IsNotNullOrEmpty(listBefore);
                HisImpMestMedicineCheck checker = new HisImpMestMedicineCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestMedicineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisImpMestMedicines.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpMestMedicines))
            {
                if (!new HisImpMestMedicineUpdate(param).UpdateList(this.beforeUpdateHisImpMestMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestMedicine that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMestMedicines", this.beforeUpdateHisImpMestMedicines));
                }
            }
        }
    }
}
