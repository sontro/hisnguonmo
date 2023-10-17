using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineLine
{
    partial class HisMedicineLineCreate : BusinessBase
    {
		private HIS_MEDICINE_LINE recentHisMedicineLineDTO;
		
        internal HisMedicineLineCreate()
            : base()
        {

        }

        internal HisMedicineLineCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_LINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineLineCheck checker = new HisMedicineLineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDICINE_LINE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMedicineLineDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineLine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineLine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicineLineDTO = data;
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
            if (this.recentHisMedicineLineDTO != null)
            {
                if (!new HisMedicineLineTruncate(param).Truncate(this.recentHisMedicineLineDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineLine that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineLine", this.recentHisMedicineLineDTO));
                }
            }
        }
    }
}
