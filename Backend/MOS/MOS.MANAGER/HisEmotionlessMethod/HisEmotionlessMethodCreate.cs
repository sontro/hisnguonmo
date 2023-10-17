using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmotionlessMethod
{
    partial class HisEmotionlessMethodCreate : BusinessBase
    {
		private HIS_EMOTIONLESS_METHOD recentHisEmotionlessMethodDTO;
		
        internal HisEmotionlessMethodCreate()
            : base()
        {

        }

        internal HisEmotionlessMethodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EMOTIONLESS_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmotionlessMethodCheck checker = new HisEmotionlessMethodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EMOTIONLESS_METHOD_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisEmotionlessMethodDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmotionlessMethod_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmotionlessMethod that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEmotionlessMethodDTO = data;
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
            if (this.recentHisEmotionlessMethodDTO != null)
            {
                if (!new HisEmotionlessMethodTruncate(param).Truncate(this.recentHisEmotionlessMethodDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisEmotionlessMethod that bai, can kiem tra lai." + LogUtil.TraceData("HisEmotionlessMethodDTO", this.recentHisEmotionlessMethodDTO));
                }
            }
        }
    }
}
