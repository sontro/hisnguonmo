using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFilmSize
{
    partial class HisFilmSizeUpdate : BusinessBase
    {
		private List<HIS_FILM_SIZE> beforeUpdateHisFilmSizes = new List<HIS_FILM_SIZE>();
		
        internal HisFilmSizeUpdate()
            : base()
        {

        }

        internal HisFilmSizeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_FILM_SIZE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFilmSizeCheck checker = new HisFilmSizeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_FILM_SIZE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.FILM_SIZE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisFilmSizeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFilmSize_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFilmSize that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisFilmSizes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_FILM_SIZE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFilmSizeCheck checker = new HisFilmSizeCheck(param);
                List<HIS_FILM_SIZE> listRaw = new List<HIS_FILM_SIZE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.FILM_SIZE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisFilmSizeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFilmSize_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFilmSize that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisFilmSizes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisFilmSizes))
            {
                if (!DAOWorker.HisFilmSizeDAO.UpdateList(this.beforeUpdateHisFilmSizes))
                {
                    LogSystem.Warn("Rollback du lieu HisFilmSize that bai, can kiem tra lai." + LogUtil.TraceData("HisFilmSizes", this.beforeUpdateHisFilmSizes));
                }
				this.beforeUpdateHisFilmSizes = null;
            }
        }
    }
}
