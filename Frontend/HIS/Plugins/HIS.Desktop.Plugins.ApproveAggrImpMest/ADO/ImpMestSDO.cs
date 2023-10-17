using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApproveAggrImpMest.ADO
{
    public class ImpMestSDO : V_HIS_IMP_MEST_2
    {
        public bool IsHighLight { get; set; }

        public ImpMestSDO()
        { }

        public ImpMestSDO(V_HIS_IMP_MEST_2 _data)
        {
            try
            {
                if (_data != null)
                {

                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<V_HIS_IMP_MEST_2>();

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
