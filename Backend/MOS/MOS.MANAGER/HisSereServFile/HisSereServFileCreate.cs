using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServFile
{
    class HisSereServFileCreate : BusinessBase
    {
        private HIS_SERE_SERV_FILE recentHisSereServFileDTO;
		private List<HIS_SERE_SERV_FILE> recentHisSereServFileDTOs;
		
        internal HisSereServFileCreate()
            : base()
        {

        }

        internal HisSereServFileCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_FILE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServFileCheck checker = new HisSereServFileCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSereServFileDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServFile_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServFile that bai." + LogUtil.TraceData("HisSereServFile", data));
                    }
                    this.recentHisSereServFileDTO = data;
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

        internal bool CreateList(List<HIS_SERE_SERV_FILE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServFileCheck checker = new HisSereServFileCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisSereServFileDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServFile_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisSereServFile that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.recentHisSereServFileDTOs = listData;
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
            if (this.recentHisSereServFileDTO != null)
            {
                if (!DAOWorker.HisSereServFileDAO.Truncate(this.recentHisSereServFileDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServFile that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServFileDTO", this.recentHisSereServFileDTO));
                }
            }
			
			if (this.recentHisSereServFileDTOs != null)
            {
                if (!DAOWorker.HisSereServFileDAO.TruncateList(this.recentHisSereServFileDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServFile that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServFileDTOs", this.recentHisSereServFileDTOs));
                }
            }
        }
    }
}
