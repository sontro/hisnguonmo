using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServFile
{
	class HisSereServFileUpdate : BusinessBase
	{
        private HIS_SERE_SERV_FILE beforeUpdateHisSereServFileDTO;
		private List<HIS_SERE_SERV_FILE> beforeUpdateHisSereServFileDTOs;

		internal HisSereServFileUpdate()
			: base()
		{

		}

		internal HisSereServFileUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_SERE_SERV_FILE data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HIS_SERE_SERV_FILE raw = null;
				HisSereServFileCheck checker = new HisSereServFileCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
					this.beforeUpdateHisSereServFileDTO = raw;
					if (!DAOWorker.HisSereServFileDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServFile_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisSereServFile that bai." + LogUtil.TraceData("data", data));
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

		internal bool UpdateList(List<HIS_SERE_SERV_FILE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServFileCheck checker = new HisSereServFileCheck(param);
                List<HIS_SERE_SERV_FILE> listRaw = new List<HIS_SERE_SERV_FILE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisSereServFileDTOs = listRaw;
					if (!DAOWorker.HisSereServFileDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServFile_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServFile that bai." + LogUtil.TraceData("listData", listData));
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
            if (this.beforeUpdateHisSereServFileDTO != null)
            {
                if (!new HisSereServFileUpdate(param).Update(this.beforeUpdateHisSereServFileDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServFile that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServFileDTO", this.beforeUpdateHisSereServFileDTO));
                }
            }
			
			if (this.beforeUpdateHisSereServFileDTOs != null)
            {
                if (!new HisSereServFileUpdate(param).UpdateList(this.beforeUpdateHisSereServFileDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServFile that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServFileDTOs", this.beforeUpdateHisSereServFileDTOs));
                }
            }
        }
	}
}
