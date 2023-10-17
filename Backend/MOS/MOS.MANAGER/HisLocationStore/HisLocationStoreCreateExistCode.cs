using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLocationStore
{
    partial class HisLocationStoreCreate : BusinessBase
    {
		private List<HIS_LOCATION_STORE> recentHisLocationStores = new List<HIS_LOCATION_STORE>();
		
        internal HisLocationStoreCreate()
            : base()
        {

        }

        internal HisLocationStoreCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_LOCATION_STORE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisLocationStoreCheck checker = new HisLocationStoreCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.LOCATION_STORE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisLocationStoreDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisLocationStore_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisLocationStore that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisLocationStores.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisLocationStores))
            {
                if (!DAOWorker.HisLocationStoreDAO.TruncateList(this.recentHisLocationStores))
                {
                    LogSystem.Warn("Rollback du lieu HisLocationStore that bai, can kiem tra lai." + LogUtil.TraceData("recentHisLocationStores", this.recentHisLocationStores));
                }
				this.recentHisLocationStores = null;
            }
        }
    }
}
