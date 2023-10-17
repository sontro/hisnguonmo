using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRefectory
{
	partial class HisRefectoryUpdate : BusinessBase
	{
		private List<HIS_REFECTORY> beforeUpdateHisRefectorys = new List<HIS_REFECTORY>();
		
		internal HisRefectoryUpdate()
			: base()
		{

		}

		internal HisRefectoryUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

        internal bool Update(HisRefectorySDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    //backup du lieu de phuc vu rollback
                    Mapper.CreateMap<HIS_ROOM, HIS_ROOM>();
                    HIS_ROOM originalHisRoomDTO = Mapper.Map<HIS_ROOM>(data.HisRoom);

                    if (new HisRoomUpdate(param).Update(data.HisRoom))
                    {
                        data.HisRefectory.ROOM_ID = data.HisRoom.ID;
                        result = this.Update(data.HisRefectory);
                        if (!result)
                        {
                            if (!new HisRoomUpdate(param).Update(originalHisRoomDTO))
                            {
                                LogSystem.Warn("Rollback du lieu his_room that bai. Can kiem tra lai. " + LogUtil.TraceData(LogUtil.GetMemberName(() => originalHisRoomDTO), originalHisRoomDTO));
                            }
                        }
                    }
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

		internal bool Update(HIS_REFECTORY data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisRefectoryCheck checker = new HisRefectoryCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_REFECTORY raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				valid = valid && checker.ExistsCode(data.REFECTORY_CODE, data.ID);
				if (valid)
				{
					if (!DAOWorker.HisRefectoryDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRefectory_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisRefectory that bai." + LogUtil.TraceData("data", data));
					}
					
					this.beforeUpdateHisRefectorys.Add(raw);
					
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

		internal bool UpdateList(List<HIS_REFECTORY> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisRefectoryCheck checker = new HisRefectoryCheck(param);
				List<HIS_REFECTORY> listRaw = new List<HIS_REFECTORY>();
				List<long> listId = listData.Select(o => o.ID).ToList();
				valid = valid && checker.VerifyIds(listId, listRaw);
				valid = valid && checker.IsUnLock(listRaw);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
					valid = valid && checker.ExistsCode(data.REFECTORY_CODE, data.ID);
				}
				if (valid)
				{
					if (!DAOWorker.HisRefectoryDAO.UpdateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRefectory_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisRefectory that bai." + LogUtil.TraceData("listData", listData));
					}
					this.beforeUpdateHisRefectorys.AddRange(listRaw);
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
			if (IsNotNullOrEmpty(this.beforeUpdateHisRefectorys))
			{
				if (!DAOWorker.HisRefectoryDAO.UpdateList(this.beforeUpdateHisRefectorys))
				{
					LogSystem.Warn("Rollback du lieu HisRefectory that bai, can kiem tra lai." + LogUtil.TraceData("HisRefectorys", this.beforeUpdateHisRefectorys));
				}
				this.beforeUpdateHisRefectorys = null;
			}
		}
	}
}
