using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineType.ADO
{
    public class MedicineTypeADO : V_HIS_MEDICINE_TYPE
    {
        public bool IsAddictive { get; set; }
        public bool IsAntibiotic { get; set; }
        public bool IsNeurological { get; set; }
        public bool IsStopImp { get; set; }
        public bool IsFood { get; set; }
        public bool IsCPNG { get; set; }
        public bool IsAutoExpend { get; set; }
        public bool? IsMustPrepare { get; set; }

        public MedicineTypeADO(V_HIS_MEDICINE_TYPE _data)
        {
            try
            {
                if (_data != null)
                {

                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<V_HIS_MEDICINE_TYPE>();

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
