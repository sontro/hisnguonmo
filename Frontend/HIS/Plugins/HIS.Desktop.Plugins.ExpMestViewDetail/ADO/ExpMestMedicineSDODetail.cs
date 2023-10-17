using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestViewDetail.ADO
{
    public class ExpMestMedicineSDODetail : V_HIS_EXP_MEST_MEDICINE
    {
        public decimal? TotalAmount { get; set; }

        public ExpMestMedicineSDODetail(V_HIS_EXP_MEST_MEDICINE _data)
        {
            try
            {
                if (_data != null)
                {

                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<V_HIS_EXP_MEST_MEDICINE>();

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
    }
}
