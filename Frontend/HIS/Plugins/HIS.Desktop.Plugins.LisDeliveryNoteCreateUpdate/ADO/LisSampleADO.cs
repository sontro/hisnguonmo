using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisDeliveryNoteCreateUpdate.ADO
{
    public class LisSampleADO : LIS_SAMPLE
    {
        public LisSampleADO()
        {

        }

        public LisSampleADO(ImportLisDeliveryNoteADO data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<LIS_SAMPLE>(this, data);
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
