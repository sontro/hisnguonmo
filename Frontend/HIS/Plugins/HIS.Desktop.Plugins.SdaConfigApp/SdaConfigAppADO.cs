
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SdaConfigApp
{
    public class SdaConfigAppADO : SDA_CONFIG_APP 
    {
        public int Action { get; set; }
        public long? ID_GRID { get; set; }
        public SdaConfigAppADO(int actionType, SDA_CONFIG_APP data)
        {
            this.Action = actionType;
            Inventec.Common.Mapper.DataObjectMapper.Map<SdaConfigAppADO>(this, data);

        }
        public SdaConfigAppADO(SDA_CONFIG_APP data)
            : this(GlobalVariables.ActionView, data)
        {
        }
    }
}
