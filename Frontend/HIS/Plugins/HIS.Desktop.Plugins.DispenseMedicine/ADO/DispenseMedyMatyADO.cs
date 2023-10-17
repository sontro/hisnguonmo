using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DispenseMedicine.ADO
{
    public class DispenseMedyMatyADO
    {
        public decimal? OldAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal? CFGAmount { get; set; }
        public long PreparationMediMatyTypeId { get; set; }
        public string PreparationMediMatyTypeName { get; set; }
        public long ServiceTypeId { get; set; }
        public string ServiceUnitName { get; set; }
        public bool IsNotAvaliable { get; set; }
        public decimal? ProductAmount { get; set; }

        public DispenseMedyMatyADO(DispenseMedyMatyADO r)
        {
            try
            {
                System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<DispenseMedyMatyADO>();
                foreach (var item in pi)
                {
                    item.SetValue(this, (item.GetValue(r)));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public DispenseMedyMatyADO()
        { 
        }
    }
}
