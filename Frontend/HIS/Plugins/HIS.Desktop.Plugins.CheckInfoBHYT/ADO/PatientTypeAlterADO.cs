using HIS.Desktop.Plugins.Library.CheckHeinGOV;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CheckInfoBHYT.ADO
{
    public class PatientTypeAlterADO : V_HIS_PATIENT_TYPE_ALTER
    {
        public ResultDataADO ResultDataADO { get; set; }

        public PatientTypeAlterADO()
        { }

        public PatientTypeAlterADO(V_HIS_PATIENT_TYPE_ALTER data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<PatientTypeAlterADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
