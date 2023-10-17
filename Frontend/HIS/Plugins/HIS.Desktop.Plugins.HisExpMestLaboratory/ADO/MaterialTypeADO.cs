using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExpMestLaboratory.ADO
{
    class MaterialTypeADO : V_HIS_MATERIAL_TYPE
    {
        public decimal AMOUNT { get; set; }
        public decimal AVAILABLE_AMOUNT { get; set; }

        public MaterialTypeADO(V_HIS_MATERIAL_TYPE data, decimal amount)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<MaterialTypeADO>(this, data);
            }

            this.AMOUNT = amount;
        }

        public MaterialTypeADO(MOS.SDO.HisMaterialTypeInStockSDO item, decimal amount)
        {
            if (item != null)
            {
                this.AMOUNT = amount;
                this.AVAILABLE_AMOUNT = item.AvailableAmount ?? 0;
                this.CONCENTRA = item.Concentra;
                this.IS_ACTIVE = item.IsActive;
                this.LAST_EXPIRED_DATE = item.LastExpiredDate;
                this.LAST_EXP_PRICE = item.LastExpPrice;
                this.LAST_EXP_VAT_RATIO = item.LastExpVatRatio;
                this.MANUFACTURER_CODE = item.ManufacturerCode;
                this.MANUFACTURER_NAME = item.ManufacturerName;
                this.MATERIAL_TYPE_CODE = item.MaterialTypeCode;
                this.HEIN_SERVICE_BHYT_NAME = item.MaterialTypeHeinName;
                this.MATERIAL_TYPE_MAP_ID = item.MaterialTypeMapId;
                this.MATERIAL_TYPE_NAME = item.MaterialTypeName;
                //this.NATIONAL_CODE = item.NationalCode;
                this.NATIONAL_NAME = item.NationalName;
                this.NUM_ORDER = item.NumOrder;
                this.PARENT_ID = item.ParentId;
                this.SERVICE_ID = item.ServiceId;
                this.SERVICE_UNIT_CODE = item.ServiceUnitCode;
                this.SERVICE_UNIT_ID = item.ServiceUnitId ?? 0;
                this.SERVICE_UNIT_NAME = item.ServiceUnitName;
                this.ID = item.Id;
                //this.SERVICE_UNIT_SYMBOL = item.ServiceUnitSymbol;
            }
        }
    }
}
