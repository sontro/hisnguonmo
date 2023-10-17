using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportPtttMethod.ADO
{
    public class PtttMethodImportADO : HIS_PTTT_METHOD
    {
        public string PTTT_GROUP_CODE { get; set; }
        public string PTTT_GROUP_NAME { get; set; }

        public string ERROR { get; set; }

        public PtttMethodImportADO()
        {
        }

        public PtttMethodImportADO(HIS_PTTT_METHOD data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<PtttMethodImportADO>(this, data);
        }
    }
}
