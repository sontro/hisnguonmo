using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMedicine
{
    partial class HisExpMestMedicineUpdate : BusinessBase
    {
        private List<HIS_EXP_MEST_MEDICINE> beforeUpdateHisExpMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();

        internal HisExpMestMedicineUpdate()
            : base()
        {

        }

        internal HisExpMestMedicineUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXP_MEST_MEDICINE data, HIS_EXP_MEST_MEDICINE before)
        {
            return this.UpdateList(new List<HIS_EXP_MEST_MEDICINE>() { data }, new List<HIS_EXP_MEST_MEDICINE>() { before });
        }

        internal bool UpdateList(List<HIS_EXP_MEST_MEDICINE> listData, List<HIS_EXP_MEST_MEDICINE> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMedicineCheck checker = new HisExpMestMedicineCheck(param);
                valid = valid && checker.IsUnLock(befores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);

                    //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    if (!data.SERE_SERV_PARENT_ID.HasValue)
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }
                }
                if (valid)
                {
                    this.beforeUpdateHisExpMestMedicines.AddRange(befores);
                    if (!DAOWorker.HisExpMestMedicineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestMedicine that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_EXP_MEST_MEDICINE> listData)
        {
            List<long> ids = listData != null ? listData.Select(o => o.ID).ToList() : null;
            List<HIS_EXP_MEST_MEDICINE> listRaw = new HisExpMestMedicineGet().GetByIds(ids);
            return this.UpdateList(listData, listRaw);
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpMestMedicines))
            {
                if (!DAOWorker.HisExpMestMedicineDAO.UpdateList(this.beforeUpdateHisExpMestMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestMedicine that bai, can kiem tra lai." + LogUtil.TraceData("HisExpMestMedicines", this.beforeUpdateHisExpMestMedicines));
                }
            }
        }
    }
}
