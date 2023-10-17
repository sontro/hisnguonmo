using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfigAppUser.ADO
{
    public class HisConfigAppUserADO : SDA_CONFIG_APP
    {
        public string VALUE { get; set; }
        public string LOGINNAME { get; set; }

        public HisConfigAppUserADO(SDA_CONFIG_APP data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<HisConfigAppUserADO>(this, data);
            }
        }
    }
}
