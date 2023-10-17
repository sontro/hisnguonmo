using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMixedMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInfusion
{
    partial class HisInfusionUpdate : BusinessBase
    {
        private List<HIS_INFUSION> beforeUpdateHisInfusions = new List<HIS_INFUSION>();
        private HisMixedMedicineCreate mixedMedicineCreate;
        private HisMixedMedicineTruncate mixedMedicineTruncate;
        internal HisInfusionUpdate()
            : base()
        {
            this.Init();
        }

        internal HisInfusionUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.mixedMedicineCreate = new HisMixedMedicineCreate(param);
            this.mixedMedicineTruncate = new HisMixedMedicineTruncate(param);
        }

        internal bool Update(HisInfusionSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInfusionCheck checker = new HisInfusionCheck(param);
                valid = valid && checker.VerifyRequireField(data.HisInfusion);
                valid = valid && checker.ValidateData(data.HisInfusion);
                HIS_INFUSION raw = null;
                valid = valid && checker.VerifyId(data.HisInfusion.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisInfusions.Add(raw);

                    //Khong cap nhat trang thai
                    data.HisInfusion.EMR_DOCUMENT_STT_ID = raw.EMR_DOCUMENT_STT_ID;

                    if (!DAOWorker.HisInfusionDAO.Update(data.HisInfusion))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInfusion_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInfusion that bai." + LogUtil.TraceData("data", data));
                    }
                    this.ProcessUpdateMixedMedicine(data.HisInfusion.ID, data.HisMixedMedicines);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessUpdateMixedMedicine(long infusionId, List<HIS_MIXED_MEDICINE> newMixedMedicines)
        {
            var oldMixedMedicines = new HisMixedMedicineGet().GetByInfusionId(infusionId);
            if (IsNotNullOrEmpty(oldMixedMedicines))
            {
                if (IsNotNullOrEmpty(oldMixedMedicines) && !this.mixedMedicineTruncate.TruncateList(oldMixedMedicines))
                {
                    throw new Exception("Xoa danh sach mix medicines cu khi update that bai");
                }
            }

            if (IsNotNullOrEmpty(newMixedMedicines))
            {
                newMixedMedicines.ForEach(o => o.INFUSION_ID = infusionId);

                if (!this.mixedMedicineCreate.CreateList(newMixedMedicines))
                {
                    throw new Exception("Tao danh sach mix medicines theo infusion that bai");
                }
            }
        }

        internal bool UpdateList(List<HIS_INFUSION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInfusionCheck checker = new HisInfusionCheck(param);
                List<HIS_INFUSION> listRaw = new List<HIS_INFUSION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisInfusions = listRaw;
                    if (!DAOWorker.HisInfusionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInfusion_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInfusion that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            this.mixedMedicineCreate.RollbackData();
            if (IsNotNullOrEmpty(this.beforeUpdateHisInfusions))
            {
                if (!new HisInfusionUpdate(param).UpdateList(this.beforeUpdateHisInfusions))
                {
                    LogSystem.Warn("Rollback du lieu HisInfusion that bai, can kiem tra lai." + LogUtil.TraceData("HisInfusions", this.beforeUpdateHisInfusions));
                }
            }
        }
    }
}
