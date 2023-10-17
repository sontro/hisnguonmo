using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomTypeModule
{
    partial class HisRoomTypeModuleCreate : BusinessBase
    {
		private List<HIS_ROOM_TYPE_MODULE> recentHisRoomTypeModules = new List<HIS_ROOM_TYPE_MODULE>();
		
        internal HisRoomTypeModuleCreate()
            : base()
        {

        }

        internal HisRoomTypeModuleCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ROOM_TYPE_MODULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomTypeModuleCheck checker = new HisRoomTypeModuleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ROOM_TYPE_MODULE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRoomTypeModuleDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomTypeModule_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRoomTypeModule that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRoomTypeModules.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRoomTypeModules))
            {
                if (!new HisRoomTypeModuleTruncate(param).TruncateList(this.recentHisRoomTypeModules))
                {
                    LogSystem.Warn("Rollback du lieu HisRoomTypeModule that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRoomTypeModules", this.recentHisRoomTypeModules));
                }
            }
        }
    }
}
