using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestMaterial
{
    partial class HisImpMestMaterialUpdate : BusinessBase
    {
        private List<HIS_IMP_MEST_MATERIAL> beforeUpdateHisImpMestMaterials = new List<HIS_IMP_MEST_MATERIAL>();

        internal HisImpMestMaterialUpdate()
            : base()
        {

        }

        internal HisImpMestMaterialUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_MEST_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestMaterialCheck checker = new HisImpMestMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_IMP_MEST_MATERIAL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisImpMestMaterialDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestMaterial that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisImpMestMaterials.Add(raw);
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

        internal bool UpdateList(List<HIS_IMP_MEST_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestMaterialCheck checker = new HisImpMestMaterialCheck(param);
                List<HIS_IMP_MEST_MATERIAL> listRaw = new List<HIS_IMP_MEST_MATERIAL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestMaterialDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisImpMestMaterials.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_IMP_MEST_MATERIAL> listData, List<HIS_IMP_MEST_MATERIAL> listBefores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestMaterialCheck checker = new HisImpMestMaterialCheck(param);
                valid = valid && checker.IsUnLock(listBefores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestMaterialDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisImpMestMaterials.AddRange(listBefores);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpMestMaterials))
            {
                if (!new HisImpMestMaterialUpdate(param).UpdateList(this.beforeUpdateHisImpMestMaterials))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestMaterial that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMestMaterials", this.beforeUpdateHisImpMestMaterials));
                }
            }
        }
    }
}
