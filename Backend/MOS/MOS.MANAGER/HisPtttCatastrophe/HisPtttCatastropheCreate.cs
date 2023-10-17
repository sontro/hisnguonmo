using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttCatastrophe
{
    partial class HisPtttCatastropheCreate : BusinessBase
    {
		private HIS_PTTT_CATASTROPHE recentHisPtttCatastropheDTO;
		
        internal HisPtttCatastropheCreate()
            : base()
        {

        }

        internal HisPtttCatastropheCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PTTT_CATASTROPHE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttCatastropheCheck checker = new HisPtttCatastropheCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PTTT_CATASTROPHE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPtttCatastropheDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttCatastrophe_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttCatastrophe that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPtttCatastropheDTO = data;
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
            if (this.recentHisPtttCatastropheDTO != null)
            {
                if (!new HisPtttCatastropheTruncate(param).Truncate(this.recentHisPtttCatastropheDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttCatastrophe that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttCatastropheDTO", this.recentHisPtttCatastropheDTO));
                }
            }
        }
    }
}
