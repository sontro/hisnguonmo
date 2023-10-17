using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class ImpMestTypeADO : MOS.EFMODEL.DataModels.HIS_IMP_MEST_TYPE
    {
        public ImpMestTypeADO() { }
        public ImpMestTypeADO(MOS.EFMODEL.DataModels.HIS_IMP_MEST_TYPE data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ImpMestTypeADO>(this, data);
            }
        }
        public long? IMP_MEST_TYPE_ID_USER { get; set; }
        public bool checkImpMestType { get; set; }
        public bool radioImpMestType { get; set; }
        public bool isKeyChoose { get; set; }
    }
}
