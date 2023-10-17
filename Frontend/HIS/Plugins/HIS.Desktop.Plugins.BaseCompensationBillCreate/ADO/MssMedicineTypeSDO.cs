using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationBillCreate.ADO
{
    public class MssMedicineTypeSDO : MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE
    {
        public MssMedicineTypeSDO()
        {

        }
        public MssMedicineTypeSDO(List<V_HIS_EXP_MEST_MEDICINE> datas)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<MssMedicineTypeSDO>(this, datas[0]);
                this.IS_MEDICINE = true;
                this.ID = datas[0].MEDICINE_TYPE_ID;
                this.MEDICINE_USE_FORM_ID = datas[0].MEDICINE_USE_FORM_ID;
                this.MEDICINE_USE_FORM_CODE = datas[0].MEDICINE_USE_FORM_CODE;
                this.MEDICINE_USE_FORM_NAME = datas[0].MEDICINE_USE_FORM_NAME;
                this.TUTORIAL = datas[0].TUTORIAL;
                this.AMOUNT = datas.Sum(p => p.AMOUNT);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public MssMedicineTypeSDO(List<V_HIS_EXP_MEST_MATERIAL> datas)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<MssMedicineTypeSDO>(this, datas[0]);
                this.ID = datas[0].MATERIAL_TYPE_ID;
                this.MEDICINE_TYPE_CODE = datas[0].MATERIAL_TYPE_CODE;
                this.MEDICINE_TYPE_NAME = datas[0].MATERIAL_TYPE_NAME;
                this.ID = datas[0].MATERIAL_TYPE_ID;
                this.IS_MEDICINE = false;
                this.MEDICINE_USE_FORM_NAME = "";
                this.TUTORIAL = "";
                this.AMOUNT = datas.Sum(p => p.AMOUNT);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public bool IS_MEDICINE { get; set; } // kiểm tra có phải là thuốc không
        public long? MEDICINE_ID { get; set; }
        public long? MATERIAL_ID { get; set; }
        public long? MATERIAL_TYPE_ID { get; set; }
        public double IdRow { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal AMOUNT_CHECK { get; set; }
        public decimal? PRICE { get; set; }
        public decimal IMP_PRICE { get; set; }
        public decimal? VAT { get; set; }
        public decimal? DISCOUNT { get; set; }
        public decimal? DISCOUNT_RATIO { get; set; }
        public bool IsAvailableInStock { get; set; } // kiểm tra thuốc có trong kho không

        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeAmount { get; set; }
        public string ErrorMessageAmount { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypePrice { get; set; }
        public string ErrorMessage { get; set; }

    }
}
