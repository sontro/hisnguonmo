using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HIS.Desktop.Plugins.HisImportServiceRoom.ADO
{
    public class ServiceRoomImportADO : V_HIS_SERVICE_ROOM
    {

        public string ERROR { get; set; }

        public ServiceRoomImportADO()
        {
        }

        public ServiceRoomImportADO(V_HIS_SERVICE_ROOM data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<ServiceRoomImportADO>(this, data);
        }
    }
}
