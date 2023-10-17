using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidMaterialType
{
    partial class HisBidMaterialTypeCreate : BusinessBase
    {
		private List<HIS_BID_MATERIAL_TYPE> recentHisBidMaterialTypes = new List<HIS_BID_MATERIAL_TYPE>();
		
        internal HisBidMaterialTypeCreate()
            : base()
        {

        }

        internal HisBidMaterialTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BID_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidMaterialTypeCheck checker = new HisBidMaterialTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BID_MATERIAL_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBidMaterialTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidMaterialType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBidMaterialType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBidMaterialTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBidMaterialTypes))
            {
                if (!new HisBidMaterialTypeTruncate(param).TruncateList(this.recentHisBidMaterialTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBidMaterialType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBidMaterialTypes", this.recentHisBidMaterialTypes));
                }
            }
        }
    }
}
