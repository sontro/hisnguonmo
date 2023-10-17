using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeRoom
{
    partial class HisPatientTypeRoomCreate : BusinessBase
    {
		private List<HIS_PATIENT_TYPE_ROOM> recentHisPatientTypeRooms = new List<HIS_PATIENT_TYPE_ROOM>();
		
        internal HisPatientTypeRoomCreate()
            : base()
        {

        }

        internal HisPatientTypeRoomCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PATIENT_TYPE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeRoomCheck checker = new HisPatientTypeRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.CheckExists(data);
                if (valid)
                {
					if (!DAOWorker.HisPatientTypeRoomDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientTypeRoom_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPatientTypeRoom that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPatientTypeRooms.Add(data);
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
		
		internal bool CreateList(List<HIS_PATIENT_TYPE_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeRoomCheck checker = new HisPatientTypeRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.CheckExists(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPatientTypeRoomDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientTypeRoom_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPatientTypeRoom that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPatientTypeRooms.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPatientTypeRooms))
            {
                if (!DAOWorker.HisPatientTypeRoomDAO.TruncateList(this.recentHisPatientTypeRooms))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientTypeRoom that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPatientTypeRooms", this.recentHisPatientTypeRooms));
                }
				this.recentHisPatientTypeRooms = null;
            }
        }
    }
}
