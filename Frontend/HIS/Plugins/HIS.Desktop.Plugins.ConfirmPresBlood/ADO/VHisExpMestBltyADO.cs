using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfirmPresBlood.ADO
{
    class VHisExpMestBltyADO : V_HIS_EXP_MEST_BLTY_REQ_1
    {
        public decimal Discount { get; set; }
        public long AmountReq { get; set; }
        public long ExpMestBltyId { get; set; }
        public long AvailableAmount { get; set; }

        public VHisExpMestBltyADO() { }

        public VHisExpMestBltyADO(V_HIS_EXP_MEST_BLTY_REQ_1 data)
        {
            try
            {
                if (data != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<V_HIS_EXP_MEST_BLTY_REQ_1>();
                    foreach (var item in pi)
                    {
                        item.SetValue(this, item.GetValue(data));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
