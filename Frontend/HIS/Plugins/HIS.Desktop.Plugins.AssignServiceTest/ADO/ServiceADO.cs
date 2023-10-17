using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignServiceTest.ADO
{
    class ServiceADO : MOS.EFMODEL.DataModels.V_HIS_SERVICE
    {
        public string CONCRETE_ID__IN_SETY { get; set; }
        public string PARENT_ID__IN_SETY { get; set; }
        public string SERVICE_CODE_HIDDEN { get; set; }
        public string SERVICE_NAME_HIDDEN { get; set; }

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

                this.SERVICE_NAME_HIDDEN = convertToUnSign3(sereServ.TDL_SERVICE_NAME) + sereServ.TDL_SERVICE_NAME;
                this.SERVICE_CODE_HIDDEN = convertToUnSign3(sereServ.TDL_SERVICE_CODE) + sereServ.TDL_SERVICE_CODE;
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
                this.SERVICE_NAME_HIDDEN = convertToUnSign3(service.SERVICE_NAME) + service.SERVICE_NAME;
                this.SERVICE_CODE_HIDDEN = convertToUnSign3(service.SERVICE_CODE) + service.SERVICE_CODE;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public string convertToUnSign3(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
                return "";

            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
