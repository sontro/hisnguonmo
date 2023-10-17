using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMachineServMaty.ADO
{
    public class MaterialTypeADO : MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE
    {
        public bool IsHightLight { get; set; }
        public decimal? EXPEND_AMOUNT { get; set; }
        public decimal? EXPEND_PRICE { get; set; }
        public long MACHINE_ID { get; set; }

        public MaterialTypeADO()
        { }

        public MaterialTypeADO(MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE _data)
        {
            try
            {
                if (_data != null)
                {

                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE>();

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
