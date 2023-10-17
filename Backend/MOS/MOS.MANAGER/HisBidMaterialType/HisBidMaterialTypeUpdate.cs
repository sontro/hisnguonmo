using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBidMaterialType
{
    partial class HisBidMaterialTypeUpdate : BusinessBase
    {
        private List<HIS_BID_MATERIAL_TYPE> beforeUpdateHisBidMaterialTypes = new List<HIS_BID_MATERIAL_TYPE>();

        internal HisBidMaterialTypeUpdate()
            : base()
        {

        }

        internal HisBidMaterialTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BID_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidMaterialTypeCheck checker = new HisBidMaterialTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BID_MATERIAL_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisBidMaterialTypes.Add(raw);
                    if (!DAOWorker.HisBidMaterialTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidMaterialType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBidMaterialType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BID_MATERIAL_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidMaterialTypeCheck checker = new HisBidMaterialTypeCheck(param);
                List<HIS_BID_MATERIAL_TYPE> listRaw = new List<HIS_BID_MATERIAL_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBidMaterialTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidMaterialType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBidMaterialType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisBidMaterialTypes.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_BID_MATERIAL_TYPE> listData, List<HIS_BID_MATERIAL_TYPE> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidMaterialTypeCheck checker = new HisBidMaterialTypeCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBidMaterialTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidMaterialType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBidMaterialType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisBidMaterialTypes.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBidMaterialTypes))
            {
                if (!new HisBidMaterialTypeUpdate(param).UpdateList(this.beforeUpdateHisBidMaterialTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBidMaterialType that bai, can kiem tra lai." + LogUtil.TraceData("HisBidMaterialTypes", this.beforeUpdateHisBidMaterialTypes));
                }
            }
        }
    }
}
