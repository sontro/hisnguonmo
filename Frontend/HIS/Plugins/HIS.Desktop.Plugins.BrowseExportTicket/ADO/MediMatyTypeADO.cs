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

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.ADO
{
    public class MediMatyTypeADO : HisMedicineTypeInStockSDO
    {
        public string ADDITION_INFO { get; set; }
        public decimal YCD_AMOUNT { get; set; } 

        public MediMatyTypeADO()
        {
        }

        public MediMatyTypeADO(HisMedicineTypeInStockSDO _data)
        {
            try
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

            catch (Exception)
            {

            }
        }

        public MediMatyTypeADO(HisMaterialTypeInStockSDO _data)
        {
            try
            {
                if (_data != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<HisMaterialTypeInStockSDO>();

                    foreach (var item in pi)
                    {
                        item.SetValue(this, (item.GetValue(_data)));
                    }

                    this.MedicineTypeCode = _data.MaterialTypeCode;
                    this.MedicineTypeName = _data.MaterialTypeName;
                }
            }

            catch (Exception)
            {

            }
        }
    }
}