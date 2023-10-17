using EMR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqResultView.ADO
{
    public class EmrVersionADO : EMR_VERSION
    {
        public string DOCUMENT_CODE_ForOrder { get; set; }

        public EmrVersionADO()
        {
        }

        public EmrVersionADO(EMR_VERSION data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<EmrVersionADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
