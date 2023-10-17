using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using AutoMapper;

namespace HIS.Desktop.Plugins.Bordereau.ADO
{
    class TreatmentADO : V_HIS_TREATMENT
    {
        public string timeTreatment { get; set; }
        public string contentShow { get; set; }
        public TreatmentADO(MOS.EFMODEL.DataModels.V_HIS_TREATMENT item)
        {
            Mapper.CreateMap<MOS.EFMODEL.DataModels.V_HIS_TREATMENT, TreatmentADO>();
            Mapper.Map<MOS.EFMODEL.DataModels.V_HIS_TREATMENT, TreatmentADO>(item, this);

            this.timeTreatment = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(this.IN_TIME);
            contentShow = string.Format("{0} - {1}", item.TREATMENT_CODE, timeTreatment);
        }
    }
}
