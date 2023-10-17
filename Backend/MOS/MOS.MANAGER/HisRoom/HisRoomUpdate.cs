using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRoom
{
    class HisRoomUpdate : BusinessBase
    {
        private List<HIS_ROOM> recentHisRooms;
        internal HisRoomUpdate()
            : base()
        {
            this.recentHisRooms = new List<HIS_ROOM>();
        }

        internal HisRoomUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.recentHisRooms = new List<HIS_ROOM>();
        }

        internal bool Update(HIS_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_ROOM raw = null;
                HisRoomCheck checker = new HisRoomCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisRoomDAO.Update(data);
                    if (result)
                    {
                        this.recentHisRooms.Add(raw);
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

        internal bool UpdateList(List<HIS_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_ROOM> listRaw = new List<HIS_ROOM>();
                valid = IsNotNullOrEmpty(listData);
                HisRoomCheck checker = new HisRoomCheck(param);
                valid = valid && checker.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisRoomDAO.UpdateList(listData);
                    if (result)
                    {
                        this.recentHisRooms.AddRange(listRaw);
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

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.recentHisRooms))
            {
                if (!DAOWorker.HisRoomDAO.UpdateList(this.recentHisRooms))
                {
                    LogSystem.Warn("Rollback HisRoom that bai. Kiem tra lai du lieu");
                }
            }
        }
    }
}
