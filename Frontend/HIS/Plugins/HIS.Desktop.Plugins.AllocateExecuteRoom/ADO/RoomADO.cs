using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
namespace HIS.Desktop.Plugins.AllocateExecuteRoom.ADO
{
    class RoomADO : L_HIS_ROOM_COUNTER
    {
        public bool IsChecked { get; set; }
        public string PHU_TRACH_Str { get; set; }
        public long TOTAL_SERVICE_REQ_Str { get; set; }
        public long TOTAL_NEW_SERVICE_REQ_Str { get; set; }
        public long TOTAL_TODAY_SERVICE_REQ_Str { get; set; }
        public RoomADO()
            : base()
        {
            IsChecked = false;
        }

        public RoomADO(V_HIS_USER_ROOM userRoom)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<RoomADO>(this, userRoom);
            this.ID = userRoom.ROOM_ID;
            this.IsChecked = false;
        }

        public RoomADO(L_HIS_ROOM_COUNTER userRoom)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<RoomADO>(this, userRoom);
            this.IsChecked = false;
            if (!string.IsNullOrEmpty(userRoom.RESPONSIBLE_LOGINNAME) && !string.IsNullOrEmpty(userRoom.RESPONSIBLE_USERNAME))
            {
                this.PHU_TRACH_Str = userRoom.RESPONSIBLE_LOGINNAME + " - " + userRoom.RESPONSIBLE_USERNAME;
            }
            if (!string.IsNullOrEmpty(userRoom.RESPONSIBLE_LOGINNAME) && string.IsNullOrEmpty(userRoom.RESPONSIBLE_USERNAME))
            {
                this.PHU_TRACH_Str = userRoom.RESPONSIBLE_LOGINNAME;
            }
            if (string.IsNullOrEmpty(userRoom.RESPONSIBLE_LOGINNAME) && !string.IsNullOrEmpty(userRoom.RESPONSIBLE_USERNAME))
            {
                this.PHU_TRACH_Str = userRoom.RESPONSIBLE_USERNAME;
            }
            
            this.TOTAL_NEW_SERVICE_REQ_Str = (long)(userRoom.TOTAL_NEW_SERVICE_REQ ??0);
            
            
            this.TOTAL_TODAY_SERVICE_REQ_Str = (long)(userRoom.TOTAL_TODAY_SERVICE_REQ ?? 0);


            this.TOTAL_SERVICE_REQ_Str = (long)((userRoom.TOTAL_TODAY_SERVICE_REQ ?? 0) - (userRoom.TOTAL_NEW_SERVICE_REQ ?? 0)); 
        }
    }
}
