using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAdr
{
    public class MedicineTypeADO : MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE
    {
        public string PACKAGE_NUMBER { get; set; }

        public MedicineTypeADO() { }

        public MedicineTypeADO(V_HIS_MEDICINE_TYPE dataType, List<HIS_MEDICINE> _medicines)
        {
            try
            {
                if (dataType != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<MedicineTypeADO>(this, dataType);
                    if (_medicines != null && _medicines.Count > 0)
                    {
                        var data = _medicines.FirstOrDefault(p => p.MEDICINE_TYPE_ID == dataType.ID);
                        this.PACKAGE_NUMBER = data != null ? data.PACKAGE_NUMBER : "";
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
