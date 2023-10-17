using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RadixChangeCabinet.ADO
{
    public class MediMatyTypeADO
    {
        public decimal? AMOUNT { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public string MEDICINE_TYPE_CODE__UNSIGN { get; set; }
        public string MEDICINE_TYPE_NAME__UNSIGN { get; set; }
        public string ACTIVE_INGR_BHYT_NAME__UNSIGN { get; set; }
        public bool IsMedicine { get; set; }
        public string MEDICINE_TYPE_CODE { get; set; }
        public string MEDICINE_TYPE_NAME { get; set; }
        public string CONCENTRA { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string MANUFACTURER_NAME { get; set; }
        public string REGISTER_NUMBER { get; set; }
        public string NATIONAL_NAME { get; set; }
        public string ACTIVE_INGR_BHYT_CODE { get; set; }
        public string ACTIVE_INGR_BHYT_NAME { get; set; }
        public long SERVICE_ID { get; set; }
        public long ID { get; set; }

        public MediMatyTypeADO()
        {

        }

        public MediMatyTypeADO(HisMedicineTypeInStockSDO data)
        {
            try
            {
                if (data != null)
                {
                    this.IsMedicine = true;
                    this.SERVICE_ID = data.ServiceId;
                    this.ID = data.Id;
                    this.MEDICINE_TYPE_NAME = data.MedicineTypeName;
                    this.MEDICINE_TYPE_CODE = data.MedicineTypeCode;
                    this.AMOUNT = data.AvailableAmount;
                    this.CONCENTRA = data.Concentra;
                    this.SERVICE_UNIT_NAME = data.ServiceUnitName;
                    this.MANUFACTURER_NAME = data.ManufacturerName;
                    this.REGISTER_NUMBER = data.RegisterNumber;
                    this.NATIONAL_NAME = data.NationalName;
                    this.ACTIVE_INGR_BHYT_CODE = data.ActiveIngrBhytCode;
                    this.ACTIVE_INGR_BHYT_NAME = data.ActiveIngrBhytName;
                    this.MEDICINE_TYPE_CODE__UNSIGN = StringUtil.convertToUnSign3(this.MEDICINE_TYPE_CODE) + this.MEDICINE_TYPE_CODE;
                    this.MEDICINE_TYPE_NAME__UNSIGN = StringUtil.convertToUnSign3(this.MEDICINE_TYPE_NAME) + this.MEDICINE_TYPE_NAME;
                    this.ACTIVE_INGR_BHYT_NAME__UNSIGN = StringUtil.convertToUnSign3(this.ACTIVE_INGR_BHYT_NAME) + this.ACTIVE_INGR_BHYT_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public MediMatyTypeADO(HisMaterialTypeInStockSDO data)
        {
            try
            {
                if (data != null)
                {
                    this.SERVICE_ID = data.ServiceId;
                    this.ID = data.Id;
                    this.IsMedicine = false;
                    this.MEDICINE_TYPE_NAME = data.MaterialTypeName;
                    this.MEDICINE_TYPE_CODE = data.MaterialTypeCode;
                    this.AMOUNT = data.AvailableAmount;
                    this.CONCENTRA = data.Concentra;
                    this.SERVICE_UNIT_NAME = data.ServiceUnitName;
                    this.MANUFACTURER_NAME = data.ManufacturerName;
                    this.NATIONAL_NAME = data.NationalName;
                    this.MEDICINE_TYPE_CODE__UNSIGN = StringUtil.convertToUnSign3(this.MEDICINE_TYPE_CODE) + this.MEDICINE_TYPE_CODE;
                    this.MEDICINE_TYPE_NAME__UNSIGN = StringUtil.convertToUnSign3(this.MEDICINE_TYPE_NAME) + this.MEDICINE_TYPE_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
