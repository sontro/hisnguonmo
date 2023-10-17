using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.ADO
{
    class SupplierADO : HIS_SUPPLIER
    {
        public string SUPPLIER_NAME_UNSIGN { get; set; }

        public SupplierADO(HIS_SUPPLIER data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<SupplierADO>(this, data);
            this.SUPPLIER_NAME_UNSIGN = Inventec.Common.String.Convert.UnSignVNese(data.SUPPLIER_NAME);
        }
    }
}
