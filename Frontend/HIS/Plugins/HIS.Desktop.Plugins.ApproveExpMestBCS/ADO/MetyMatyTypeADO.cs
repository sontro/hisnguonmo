using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApproveExpMestBCS.ADO
{
    public class MetyMatyTypeADO : HisMedicineTypeInStockSDO
    {
        public string ADDITION_INFO { get; set; }
        public decimal YCD_AMOUNT { get; set; }

        public MetyMatyTypeADO()
        {
        }

        public MetyMatyTypeADO(HisMedicineTypeInStockSDO _data)
        {
            if (_data != null)
            {
                System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<HisMedicineTypeInStockSDO>();

                foreach (var item in pi)
                {
                    item.SetValue(this, (item.GetValue(_data)));
                }
            }
        }

        public MetyMatyTypeADO(HisMaterialTypeInStockSDO _data)
        {
            if (_data != null)
            {
                this.AvailableAmount = _data.AvailableAmount;
                this.Concentra = _data.Concentra;
                this.ExportedTotalAmount = _data.ExportedTotalAmount;
                this.Id = _data.Id;
                this.ImpStockAvailableAmount = _data.ImpStockAvailableAmount;
                this.ImpStockTotalAmount = _data.ImpStockTotalAmount;
                this.IsActive = _data.IsActive;
                this.IsBusiness = _data.IsBusiness;
                this.IsGoodsRestrict = _data.IsGoodsRestrict;
                this.IsLeaf = _data.IsLeaf;
                this.ManufacturerCode = _data.ManufacturerCode;
                this.ManufacturerName = _data.ManufacturerName;
                this.MedicineTypeCode = _data.MaterialTypeCode;
                this.MedicineTypeName = _data.MaterialTypeName;
                this.MediStockCode = _data.MediStockCode;
                this.MediStockId = _data.MediStockId;
                this.MediStockName = _data.MediStockName;
                this.NationalCode = _data.NationalCode;
                this.NationalName = _data.NationalName;
                this.NumOrder = _data.NumOrder;
                this.ParentId = _data.ParentId;
                this.ServiceId = _data.ServiceId;
                this.ServiceUnitCode = _data.ServiceUnitCode;
                this.ServiceUnitId = _data.ServiceUnitId;
                this.ServiceUnitName = _data.ServiceUnitName;
                this.ServiceUnitSymbol = _data.ServiceUnitSymbol;
                this.TotalAmount = _data.TotalAmount;                
            }
        }
    }
}
