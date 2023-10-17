using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentBodyPart
{
    partial class HisAccidentBodyPartCreate : BusinessBase
    {
		private List<HIS_ACCIDENT_BODY_PART> recentHisAccidentBodyParts = new List<HIS_ACCIDENT_BODY_PART>();
		
        internal HisAccidentBodyPartCreate()
            : base()
        {

        }

        internal HisAccidentBodyPartCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACCIDENT_BODY_PART data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentBodyPartCheck checker = new HisAccidentBodyPartCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ACCIDENT_BODY_PART_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAccidentBodyPartDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentBodyPart_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentBodyPart that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAccidentBodyParts.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAccidentBodyParts))
            {
                if (!new HisAccidentBodyPartTruncate(param).TruncateList(this.recentHisAccidentBodyParts))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentBodyPart that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAccidentBodyParts", this.recentHisAccidentBodyParts));
                }
            }
        }
    }
}
