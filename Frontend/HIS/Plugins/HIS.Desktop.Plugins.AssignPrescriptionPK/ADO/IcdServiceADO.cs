using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.ADO
{
    public class IcdServiceADO : HIS_ICD_SERVICE
    {
        public decimal? AMOUNT { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }

        public IcdServiceADO() { }

        public IcdServiceADO(HIS_ICD_SERVICE data, MediMatyTypeADO MediMaty) 
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<IcdServiceADO>(this, data);
                }
                if (MediMaty != null)
                {
                    this.SERVICE_CODE = MediMaty.MEDICINE_TYPE_CODE;
                    this.SERVICE_NAME = MediMaty.MEDICINE_TYPE_NAME;
                    this.AMOUNT = MediMaty.AMOUNT;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
