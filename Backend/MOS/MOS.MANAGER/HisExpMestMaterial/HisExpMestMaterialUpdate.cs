using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMaterial
{
    partial class HisExpMestMaterialUpdate : BusinessBase
    {
        private List<HIS_EXP_MEST_MATERIAL> beforeUpdateHisExpMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();

        internal HisExpMestMaterialUpdate()
            : base()
        {

        }

        internal HisExpMestMaterialUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXP_MEST_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestMaterialCheck checker = new HisExpMestMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXP_MEST_MATERIAL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    if (!data.SERE_SERV_PARENT_ID.HasValue)
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }

                    this.beforeUpdateHisExpMestMaterials.Add(raw);
                    if (!DAOWorker.HisExpMestMaterialDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestMaterial that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_EXP_MEST_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMaterialCheck checker = new HisExpMestMaterialCheck(param);
                List<HIS_EXP_MEST_MATERIAL> listRaw = new List<HIS_EXP_MEST_MATERIAL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
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
                    this.beforeUpdateHisExpMestMaterials.AddRange(listRaw);
                    if (!DAOWorker.HisExpMestMaterialDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestMaterial that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_EXP_MEST_MATERIAL> listData, List<HIS_EXP_MEST_MATERIAL> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMaterialCheck checker = new HisExpMestMaterialCheck(param);
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
                    this.beforeUpdateHisExpMestMaterials.AddRange(befores);
                    if (!DAOWorker.HisExpMestMaterialDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestMaterial that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpMestMaterials))
            {
                if (!DAOWorker.HisExpMestMaterialDAO.UpdateList(this.beforeUpdateHisExpMestMaterials))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestMaterial that bai, can kiem tra lai." + LogUtil.TraceData("HisExpMestMaterials", this.beforeUpdateHisExpMestMaterials));
                }
            }
        }
    }
}
