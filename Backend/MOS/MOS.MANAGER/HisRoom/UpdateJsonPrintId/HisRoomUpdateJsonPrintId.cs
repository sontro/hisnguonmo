using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRoom
{
    class HisRoomUpdateJsonPrintId : BusinessBase
    {
        private HisRoomUpdate hisRoomUpdate;

        internal HisRoomUpdateJsonPrintId()
            : base()
        {
            this.Init();
        }
        
        internal HisRoomUpdateJsonPrintId(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisRoomUpdate = new HisRoomUpdate(param);
        }

        internal bool Run(HisRoomSDO data, ref HIS_ROOM resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_ROOM room = null;
                HisRoomCheck checker = new HisRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.RoomId, ref room);
                if (valid)
                {
                    room.JSON_PRINT_ID = data.JsonPrintId;
                    if (!hisRoomUpdate.Update(room))
                    {
                        throw new Exception("Cap nhat thong tin JSON_PRINT_ID trong HIS_ROOM that bai, " + LogUtil.TraceData("dataUpdate: ", room));
                    }
                    result = true;
                    resultData = room;
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
            this.hisRoomUpdate.Rollback();
        }
    }
}
