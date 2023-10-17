using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServReha
{
    partial class HisSereServRehaUpdate : BusinessBase
    {
		private List<HIS_SERE_SERV_REHA> beforeUpdateHisSereServRehas = new List<HIS_SERE_SERV_REHA>();
		
        internal HisSereServRehaUpdate()
            : base()
        {

        }

        internal HisSereServRehaUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERE_SERV_REHA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServRehaCheck checker = new HisSereServRehaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERE_SERV_REHA raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
					this.beforeUpdateHisSereServRehas.Add(raw);
                    if (!DAOWorker.HisSereServRehaDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServReha_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServReha that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SERE_SERV_REHA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServRehaCheck checker = new HisSereServRehaCheck(param);
                List<HIS_SERE_SERV_REHA> listRaw = new List<HIS_SERE_SERV_REHA>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisSereServRehas.AddRange(listRaw);
                    if (!DAOWorker.HisSereServRehaDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServReha_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServReha that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSereServRehas))
            {
                if (!new HisSereServRehaUpdate(param).UpdateList(this.beforeUpdateHisSereServRehas))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServReha that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServRehas", this.beforeUpdateHisSereServRehas));
                }
            }
        }
    }
}
