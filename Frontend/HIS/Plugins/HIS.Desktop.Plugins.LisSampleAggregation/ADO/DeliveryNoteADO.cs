using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisSampleAggregation.ADO
{
    public class DeliveryNoteADO : LIS_DELIVERY_NOTE
    {
        public string RECEIVING_TIME_ForDisplay { get; set; }

        public DeliveryNoteADO()
        { }

        public DeliveryNoteADO(LIS_DELIVERY_NOTE data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<DeliveryNoteADO>(this, data);
                    if (data.RECEIVING_TIME != null)
                    {
                        this.RECEIVING_TIME_ForDisplay = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.RECEIVING_TIME ?? 0);
                    }

                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
