using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSaroExro
{
    partial class HisSaroExroUpdate : BusinessBase
    {
		private List<HIS_SARO_EXRO> beforeUpdateHisSaroExros = new List<HIS_SARO_EXRO>();
		
        internal HisSaroExroUpdate()
            : base()
        {

        }

        internal HisSaroExroUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SARO_EXRO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSaroExroCheck checker = new HisSaroExroCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SARO_EXRO raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisSaroExros.Add(raw);
					if (!DAOWorker.HisSaroExroDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSaroExro_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSaroExro that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SARO_EXRO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSaroExroCheck checker = new HisSaroExroCheck(param);
                List<HIS_SARO_EXRO> listRaw = new List<HIS_SARO_EXRO>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisSaroExros.AddRange(listRaw);
					if (!DAOWorker.HisSaroExroDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSaroExro_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSaroExro that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSaroExros))
            {
                if (!new HisSaroExroUpdate(param).UpdateList(this.beforeUpdateHisSaroExros))
                {
                    LogSystem.Warn("Rollback du lieu HisSaroExro that bai, can kiem tra lai." + LogUtil.TraceData("HisSaroExros", this.beforeUpdateHisSaroExros));
                }
            }
        }
    }
}
