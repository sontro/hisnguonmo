using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ContentSubclinical.ADO
{
    public class HisSereServADONumOrder : MOS.EFMODEL.DataModels.HIS_SERE_SERV
    {
        public long? NUM_ORDER { get; set; }
        

        public HisSereServADONumOrder()
        {
        }

        public HisSereServADONumOrder(HIS_SERE_SERV data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<HisSereServADONumOrder>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
