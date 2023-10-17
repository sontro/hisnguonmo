using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.OtherImpMestUpdate.ADO
{
    class VHisServiceADO
    {
        public HIS_MEDICINE HisMedicine { get; set; }
        public List<HIS_MEDICINE_PATY> HisMedicinePatys { get; set; }

        public HIS_MATERIAL HisMaterial { get; set; }
        public List<HIS_MATERIAL_PATY> HisMaterialPatys { get; set; }

        public bool IsMedicine { get; set; }
        public List<VHisServicePatyADO> VHisServicePatys { get; set; }

        public long SERVICE_ID { get; set; }
        public long SERVICE_TYPE_ID { get; set; }

        public long MEDI_MATE_ID { get; set; }
        public string MEDI_MATE_CODE { get; set; }
        public string MEDI_MATE_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string PACKAGE_NUMBER { get; set; }

        public decimal IMP_AMOUNT { get; set; }
        public decimal IMP_PRICE { get; set; }
        public decimal IMP_VAT_RATIO { get; set; }
        public decimal ImpVatRatio { get; set; }

        //public decimal? BidImpPrice { get; set; }
        //public decimal? BidImpVatRatio { get; set; }

        public long? EXPIRED_DATE { get; set; }

        public VHisServiceADO(V_HIS_MEDICINE_TYPE data)
        {
            try
            {
                this.IsMedicine = true;
                if (data != null)
                {
                    this.MEDI_MATE_ID = data.ID;
                    this.MEDI_MATE_CODE = data.MEDICINE_TYPE_CODE;
                    this.MEDI_MATE_NAME = data.MEDICINE_TYPE_NAME;
                    this.SERVICE_UNIT_NAME = data.SERVICE_UNIT_NAME;
                    this.IMP_PRICE = data.IMP_PRICE ?? 0;
                    this.IMP_VAT_RATIO = data.IMP_VAT_RATIO ?? 0;
                    this.ImpVatRatio = this.IMP_VAT_RATIO * 100;
                    this.SERVICE_ID = data.SERVICE_ID;
                    this.SERVICE_TYPE_ID = data.SERVICE_TYPE_ID;
                    this.HisMedicine = new HIS_MEDICINE();
                    this.HisMedicine.MEDICINE_TYPE_ID = data.ID;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public VHisServiceADO(V_HIS_MATERIAL_TYPE data)
        {
            try
            {
                this.IsMedicine = false;
                if (data != null)
                {
                    this.MEDI_MATE_ID = data.ID;
                    this.MEDI_MATE_CODE = data.MATERIAL_TYPE_CODE;
                    this.MEDI_MATE_NAME = data.MATERIAL_TYPE_NAME;
                    this.SERVICE_UNIT_NAME = data.SERVICE_UNIT_NAME;
                    this.IMP_PRICE = data.IMP_PRICE ?? 0;
                    this.IMP_VAT_RATIO = data.IMP_VAT_RATIO ?? 0;
                    this.ImpVatRatio = this.IMP_VAT_RATIO * 100;
                    this.SERVICE_ID = data.SERVICE_ID;
                    this.SERVICE_TYPE_ID = data.SERVICE_TYPE_ID;
                    this.HisMaterial = new HIS_MATERIAL();
                    this.HisMaterial.MATERIAL_TYPE_ID = data.ID;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
