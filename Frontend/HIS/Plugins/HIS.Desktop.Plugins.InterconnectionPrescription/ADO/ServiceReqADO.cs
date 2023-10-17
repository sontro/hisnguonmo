using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InterconnectionPrescription.ADO
{
    public class ServiceReqADO : HIS_SERVICE_REQ
    {
        public string SERVICE_REQ_TYPE_NAME { get; set; }
        public ServiceReqADO()
        {
        }
        public ServiceReqADO(HIS_SERVICE_REQ data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ServiceReqADO>(this, data);
                    var serviceReqType = BackendDataWorker.Get<HIS_SERVICE_REQ_TYPE>().FirstOrDefault(o => o.ID == data.SERVICE_REQ_TYPE_ID);
                    this.SERVICE_REQ_TYPE_NAME = serviceReqType != null ? serviceReqType.SERVICE_REQ_TYPE_NAME : null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
