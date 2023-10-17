using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFileType
{
    partial class HisFileTypeCreate : BusinessBase
    {
		private List<HIS_FILE_TYPE> recentHisFileTypes = new List<HIS_FILE_TYPE>();
		
        internal HisFileTypeCreate()
            : base()
        {

        }

        internal HisFileTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_FILE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFileTypeCheck checker = new HisFileTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.FILE_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisFileTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFileType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisFileType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisFileTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisFileTypes))
            {
                if (!DAOWorker.HisFileTypeDAO.TruncateList(this.recentHisFileTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisFileType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisFileTypes", this.recentHisFileTypes));
                }
				this.recentHisFileTypes = null;
            }
        }
    }
}
