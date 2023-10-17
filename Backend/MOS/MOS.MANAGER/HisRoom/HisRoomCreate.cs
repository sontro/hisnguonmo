using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoom
{
    class HisRoomCreate : BusinessBase
    {
        private List<HIS_ROOM> recentHisRooms;

        internal HisRoomCreate()
            : base()
        {
            this.recentHisRooms = new List<HIS_ROOM>();
        }

        internal HisRoomCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {
            this.recentHisRooms = new List<HIS_ROOM>();
        }

        internal bool Create(HIS_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomCheck checker = new HisRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    result = DAOWorker.HisRoomDAO.Create(data);
                    if (result)
                    {
                        this.recentHisRooms.Add(data);
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

        internal bool CreateList(List<HIS_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomCheck checker = new HisRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisRoomDAO.CreateList(listData);
                    if (result)
                    {
                        this.recentHisRooms.AddRange(listData);
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
                if (!DAOWorker.HisRoomDAO.TruncateList(this.recentHisRooms))
                {
                    LogSystem.Warn("Rollback his_room that bai. kiem tra lai du lieu");
                }
            }
        }
    }
}
