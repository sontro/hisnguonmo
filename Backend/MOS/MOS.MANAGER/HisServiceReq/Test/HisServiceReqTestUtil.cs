using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test
{
    class HisServiceReqTestUtil
    {
        internal static void AddTestData(HIS_SERVICE_REQ serviceReq)
        {
            if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
            {
                //Gan phong lay mau dua vao phong chi dinh
                if (HisRoomSaroCFG.DATA != null)
                {
                    HIS_ROOM_SARO roomSaro = HisRoomSaroCFG.DATA
                        .Where(o => o.ROOM_ID == serviceReq.REQUEST_ROOM_ID).OrderByDescending(o => o.MODIFY_TIME).FirstOrDefault();
                    if (roomSaro != null)
                    {
                        serviceReq.SAMPLE_ROOM_ID = roomSaro.SAMPLE_ROOM_ID;
                    }
                }
            }
        }
    }
}
