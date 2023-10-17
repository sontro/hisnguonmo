using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterial
{
    partial class HisMaterialUpdate : BusinessBase
    {
        private List<HIS_MATERIAL> beforeUpdateHisMaterialDTOs = new List<HIS_MATERIAL>();

        internal HisMaterialUpdate()
            : base()
        {

        }

        internal HisMaterialUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MATERIAL raw = null;
                HisMaterialCheck checker = new HisMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckChangePrice(data, raw);
                if (valid)
                {
                    if (!DAOWorker.HisMaterialDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterial that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisMaterialDTOs.Add(raw);
                    HisMaterialLog.Run(data, raw, LibraryEventLog.EventLog.Enum.HisMaterial_SuaThongTinLoVatTu);
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

        internal bool Update(HIS_MATERIAL data, HIS_MATERIAL before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialCheck checker = new HisMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                valid = valid && checker.CheckChangePrice(data, before);
                if (valid)
                {
                    if (!DAOWorker.HisMaterialDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterial that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisMaterialDTOs.Add(before);
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

        internal bool UpdateList(List<HIS_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialCheck checker = new HisMaterialCheck(param);
                List<HIS_MATERIAL> listRaw = new List<HIS_MATERIAL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMaterialDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisMaterialDTOs = listRaw;
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

        internal bool UpdateList(List<HIS_MATERIAL> listData, List<HIS_MATERIAL> listBefores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialCheck checker = new HisMaterialCheck(param);
                valid = valid && checker.IsUnLock(listBefores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMaterialDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisMaterialDTOs = listBefores;
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
            if (this.beforeUpdateHisMaterialDTOs != null)
            {
                if (!new HisMaterialUpdate(param).UpdateList(this.beforeUpdateHisMaterialDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMaterial that bai, can kiem tra lai." + LogUtil.TraceData("HisMaterialDTOs", this.beforeUpdateHisMaterialDTOs));
                }
            }
        }
    }
}
