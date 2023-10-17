using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DebateDiagnostic.ADO
{
    internal class MedicineTypeADO : MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE
    {
        public bool IsChecked { get; set; }
        public string MEDICINE_TYPE_NAME__UNSIGN { get; set; }

        public MedicineTypeADO(MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE item)
        {
            Mapper.CreateMap<MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE, MedicineTypeADO>();
            Mapper.Map<MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE, MedicineTypeADO>(item, this);
            this.MEDICINE_TYPE_NAME__UNSIGN = Inventec.Common.String.Convert.UnSignVNese(this.MEDICINE_TYPE_NAME);
        }
    }
}
