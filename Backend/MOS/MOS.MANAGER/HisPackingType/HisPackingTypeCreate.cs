using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackingType
{
    partial class HisPackingTypeCreate : BusinessBase
    {
		private HIS_PACKING_TYPE recentHisPackingTypeDTO;
		
        internal HisPackingTypeCreate()
            : base()
        {

        }

        internal HisPackingTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PACKING_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPackingTypeCheck checker = new HisPackingTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PACKING_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPackingTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackingType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPackingType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPackingTypeDTO = data;
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
            if (this.recentHisPackingTypeDTO != null)
            {
                if (!new HisPackingTypeTruncate(param).Truncate(this.recentHisPackingTypeDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisPackingType that bai, can kiem tra lai." + LogUtil.TraceData("HisPackingType", this.recentHisPackingTypeDTO));
                }
            }
        }
    }
}
