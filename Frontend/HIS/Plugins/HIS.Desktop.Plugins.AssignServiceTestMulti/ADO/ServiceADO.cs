using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignServiceTestMulti.ADO
{
    class ServiceADO : MOS.EFMODEL.DataModels.V_HIS_SERVICE
    {
        public string CONCRETE_ID__IN_SETY { get; set; }
        public string PARENT_ID__IN_SETY { get; set; }

        public ServiceADO()
        {

        }
        public ServiceADO(MOS.EFMODEL.DataModels.V_HIS_SERE_SERV sereServ)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ServiceADO>(this, sereServ);
                this.ID = sereServ.SERVICE_ID;

                //this.CONCRETE_ID__IN_SETY = (sereServ.SERVICE_TYPE_ID + "." + sereServ.SERVICE_ID);
                //this.PARENT_ID__IN_SETY = (sereServ.SERVICE_TYPE_ID + "." + sereServ.PARENT_ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public ServiceADO(MOS.EFMODEL.DataModels.V_HIS_SERVICE service)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ServiceADO>(this, service);

                this.CONCRETE_ID__IN_SETY = (service.SERVICE_TYPE_ID + "." + (service.ID));
                this.PARENT_ID__IN_SETY = (service.SERVICE_TYPE_ID + "." + (service.PARENT_ID));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
