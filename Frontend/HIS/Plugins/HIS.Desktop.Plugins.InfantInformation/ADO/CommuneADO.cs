using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InfantInformation.ADO
{
    public class CommuneADO : V_SDA_COMMUNE
    {
        public string RENDERER_COMMUNE_NAME { get; set; }
        public CommuneADO(V_SDA_COMMUNE item)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<CommuneADO>(this, item);
                RENDERER_COMMUNE_NAME = string.Format("{0} {1}", INITIAL_NAME, COMMUNE_NAME);
            }
            catch (Exception  ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
