using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InfantInformation.ADO
{
    public class DistrictADO : V_SDA_DISTRICT
    {
        public string RENDERER_DISTRICT_NAME { get; set; }
        public DistrictADO(V_SDA_DISTRICT item)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<DistrictADO>(this, item);
                RENDERER_DISTRICT_NAME = string.Format("{0} {1}", INITIAL_NAME, DISTRICT_NAME);
            }
            catch (Exception  ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
