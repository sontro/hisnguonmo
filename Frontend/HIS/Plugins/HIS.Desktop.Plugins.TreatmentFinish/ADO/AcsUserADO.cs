using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish.ADO
{
    public class AcsUserADO : ACS.EFMODEL.DataModels.ACS_USER
    {
        public string DEPARTMENT_NAME { get; set; }

        public long? DOB { get; set; }
        public string DOB_STR { get; set; }
        public string DIPLOMA { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public long? DEPARTMENT_ID { get; set; }

        public AcsUserADO() { }

        public AcsUserADO(ACS.EFMODEL.DataModels.ACS_USER data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<AcsUserADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
