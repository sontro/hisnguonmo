using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineTypeRoom
{
    partial class HisMedicineTypeRoomCreate : BusinessBase
    {
		private List<HIS_MEDICINE_TYPE_ROOM> recentHisMedicineTypeRooms = new List<HIS_MEDICINE_TYPE_ROOM>();
		
        internal HisMedicineTypeRoomCreate()
            : base()
        {

        }

        internal HisMedicineTypeRoomCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_TYPE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeRoomCheck checker = new HisMedicineTypeRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMedicineTypeRoomDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineTypeRoom_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineTypeRoom that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicineTypeRooms.Add(data);
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
		
		internal bool CreateList(List<HIS_MEDICINE_TYPE_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineTypeRoomCheck checker = new HisMedicineTypeRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicineTypeRoomDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineTypeRoom_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineTypeRoom that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMedicineTypeRooms.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMedicineTypeRooms))
            {
                if (!DAOWorker.HisMedicineTypeRoomDAO.TruncateList(this.recentHisMedicineTypeRooms))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineTypeRoom that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMedicineTypeRooms", this.recentHisMedicineTypeRooms));
                }
				this.recentHisMedicineTypeRooms = null;
            }
        }
    }
}
