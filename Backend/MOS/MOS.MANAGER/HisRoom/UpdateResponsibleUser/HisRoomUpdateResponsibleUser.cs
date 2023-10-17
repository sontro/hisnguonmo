using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRoom.UpdateResponsibleUser
{
    class HisRoomUpdateResponsibleUser : BusinessBase
    {
        private List<HIS_ROOM> recentHisRooms;

        internal HisRoomUpdateResponsibleUser()
            : base()
        {
            this.recentHisRooms = new List<HIS_ROOM>();
        }

        internal HisRoomUpdateResponsibleUser(CommonParam param)
            : base(param)
        {
            this.recentHisRooms = new List<HIS_ROOM>();
        }

        internal bool Run(List<UpdateResponsibleUserSDO> data, ref List<HIS_ROOM> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                
                List<HIS_ROOM> rooms = null;
                HisRoomUpdateResponsibleUserCheck checker = new HisRoomUpdateResponsibleUserCheck(param);

                valid = valid && checker.IsValid(data, ref rooms);

                if (valid)
                {
                    List<HIS_ROOM> toUpdates = new List<HIS_ROOM>();
                    foreach (HIS_ROOM r in rooms)
                    {
                        UpdateResponsibleUserSDO sdo = data.Where(o => o.RoomId == r.ID).FirstOrDefault();

                        if (r.RESPONSIBLE_LOGINNAME != sdo.ResponsibleLoginName || r.RESPONSIBLE_USERNAME != sdo.ResponsibleUserName)
                        {
                            r.RESPONSIBLE_LOGINNAME = sdo.ResponsibleLoginName;
                            r.RESPONSIBLE_USERNAME = sdo.ResponsibleUserName;
                            toUpdates.Add(r);
                        }
                    }

                    if (!IsNotNullOrEmpty(toUpdates) || DAOWorker.HisRoomDAO.UpdateList(toUpdates))
                    {
                        resultData = rooms;
                        result = true;
                        this.UpdateCache(toUpdates);
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

        /// <summary>
        /// Update lai cache cua he thong
        /// </summary>
        /// <param name="updates"></param>
        private void UpdateCache(List<HIS_ROOM> updates)
        {
            if (IsNotNullOrEmpty(updates))
            {
                foreach (HIS_ROOM r in updates)
                {
                    V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == r.ID).FirstOrDefault();
                    room.RESPONSIBLE_LOGINNAME = r.RESPONSIBLE_LOGINNAME;
                    room.RESPONSIBLE_USERNAME = r.RESPONSIBLE_USERNAME;
                }
            }
        }
    }
}
