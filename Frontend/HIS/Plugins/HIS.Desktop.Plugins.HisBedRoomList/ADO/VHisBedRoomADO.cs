using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBedRoomList.ADO
{
    public class VHisBedRoomADO : V_HIS_BED_ROOM
    {
        public short? IsSurgery { get; set; }

        public VHisBedRoomADO(V_HIS_BED_ROOM data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<VHisBedRoomADO>(this, data);
                if (this.IS_SURGERY > 0)
                {
                    this.IsSurgery = this.IS_SURGERY;
                }
            }
        }
    }
}
