using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVareVart
{
    partial class HisVareVartCreate : BusinessBase
    {
		private List<HIS_VARE_VART> recentHisVareVarts = new List<HIS_VARE_VART>();
		
        internal HisVareVartCreate()
            : base()
        {

        }

        internal HisVareVartCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VARE_VART data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVareVartCheck checker = new HisVareVartCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVareVartDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVareVart_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVareVart that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVareVarts.Add(data);
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
		
		internal bool CreateList(List<HIS_VARE_VART> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVareVartCheck checker = new HisVareVartCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVareVartDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVareVart_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVareVart that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVareVarts.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVareVarts))
            {
                if (!DAOWorker.HisVareVartDAO.TruncateList(this.recentHisVareVarts))
                {
                    LogSystem.Warn("Rollback du lieu HisVareVart that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVareVarts", this.recentHisVareVarts));
                }
				this.recentHisVareVarts = null;
            }
        }
    }
}
