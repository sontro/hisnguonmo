using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedType
{
    partial class HisBedTypeCreate : BusinessBase
    {
		private List<HIS_BED_TYPE> recentHisBedTypes = new List<HIS_BED_TYPE>();
		
        internal HisBedTypeCreate()
            : base()
        {

        }

        internal HisBedTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BED_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedTypeCheck checker = new HisBedTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BED_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBedTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBedType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBedTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBedTypes))
            {
                if (!new HisBedTypeTruncate(param).TruncateList(this.recentHisBedTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBedType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBedTypes", this.recentHisBedTypes));
                }
            }
        }
    }
}
