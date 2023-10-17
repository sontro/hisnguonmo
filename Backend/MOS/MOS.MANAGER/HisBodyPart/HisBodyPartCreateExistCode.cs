using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBodyPart
{
    partial class HisBodyPartCreate : BusinessBase
    {
		private List<HIS_BODY_PART> recentHisBodyParts = new List<HIS_BODY_PART>();
		
        internal HisBodyPartCreate()
            : base()
        {

        }

        internal HisBodyPartCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BODY_PART data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBodyPartCheck checker = new HisBodyPartCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BODY_PART_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBodyPartDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBodyPart_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBodyPart that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBodyParts.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBodyParts))
            {
                if (!DAOWorker.HisBodyPartDAO.TruncateList(this.recentHisBodyParts))
                {
                    LogSystem.Warn("Rollback du lieu HisBodyPart that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBodyParts", this.recentHisBodyParts));
                }
				this.recentHisBodyParts = null;
            }
        }
    }
}
