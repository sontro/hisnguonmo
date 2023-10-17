using HIS.Desktop.LocalStorage.BackendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisHoldReturn.ADO
{
    internal class HoldReturnADO : MOS.EFMODEL.DataModels.V_HIS_HOLD_RETURN
    {
        public string RETURN_ROOM_NAME { get; set; }
        public string HOLD_ROOM_NAME { get; set; }
        public string RESPONSIBLE_ROOM_NAME { get; set; }

        internal HoldReturnADO() { }

        internal HoldReturnADO(MOS.EFMODEL.DataModels.V_HIS_HOLD_RETURN data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<HoldReturnADO>(this, data);
                    if (this.RESPONSIBLE_ROOM_ID > 0)
                    {
                        var resRoom = BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_ROOM>().Where(o => o.ID == this.RESPONSIBLE_ROOM_ID).FirstOrDefault();
                        this.RESPONSIBLE_ROOM_NAME = resRoom != null ? resRoom.ROOM_NAME : "";
                    }

                    if (this.RETURN_ROOM_ID > 0)
                    {
                        var resRoom = BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_ROOM>().Where(o => o.ID == this.RETURN_ROOM_ID).FirstOrDefault();
                        this.RETURN_ROOM_NAME = resRoom != null ? resRoom.ROOM_NAME : "";
                    }

                    if (this.HOLD_ROOM_ID > 0)
                    {
                        var resRoom = BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_ROOM>().Where(o => o.ID == this.HOLD_ROOM_ID).FirstOrDefault();
                        this.HOLD_ROOM_NAME = resRoom != null ? resRoom.ROOM_NAME : "";
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
