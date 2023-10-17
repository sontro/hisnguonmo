using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AggrMobaImpMests.ADO
{
    public class MediMateTypeADO
    {
        public long MEDICINE_ID { get; set; }
        public long MEDI_STOCK_ID { get; set; }
        public long MEDICINE_TYPE_ID { get; set; }
        public string MEDICINE_TYPE_CODE { get; set; }
        public string MEDICINE_TYPE_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }

        public bool IsMedicine { get; set; }
        public decimal AMOUNT { get; set; }


        public MediMateTypeADO() { }

        public MediMateTypeADO(V_HIS_IMP_MEST_MEDICINE medicine)
        {
            try
            {
                if (medicine != null)
                {
                    this.IsMedicine = true;
                    this.AMOUNT = medicine.AMOUNT;
                    this.MEDICINE_ID = medicine.MEDICINE_ID;
                    this.MEDI_STOCK_ID = medicine.MEDI_STOCK_ID;
                    this.MEDICINE_TYPE_ID = medicine.MEDICINE_TYPE_ID;
                    this.MEDICINE_TYPE_CODE = medicine.MEDICINE_TYPE_CODE;
                    this.MEDICINE_TYPE_NAME = medicine.MEDICINE_TYPE_NAME;
                    this.SERVICE_UNIT_NAME = medicine.SERVICE_UNIT_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public MediMateTypeADO(V_HIS_IMP_MEST_MATERIAL material)
        {
            try
            {
                if (material != null)
                {
                    this.IsMedicine = false;
                    this.AMOUNT = material.AMOUNT;
                    this.MEDICINE_ID = material.MATERIAL_ID;
                    this.MEDI_STOCK_ID = material.MEDI_STOCK_ID;
                    this.MEDICINE_TYPE_ID = material.MATERIAL_TYPE_ID;
                    this.MEDICINE_TYPE_CODE = material.MATERIAL_TYPE_CODE;
                    this.MEDICINE_TYPE_NAME = material.MATERIAL_TYPE_NAME;
                    this.SERVICE_UNIT_NAME = material.SERVICE_UNIT_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
