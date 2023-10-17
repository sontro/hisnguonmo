using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfirmPresBlood.ADO
{
    public class BloodVolumeADO : HIS_BLOOD_VOLUME
    {
        public string Blood_Volume_Str { get; set; }
        //public long Id { get; set; }
        public BloodVolumeADO()
        {
        }

        public BloodVolumeADO(HIS_BLOOD_VOLUME blood)
        {
            try
            {
                if (blood != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<BloodVolumeADO>(this, blood);
                    this.Blood_Volume_Str = blood.VOLUME.ToString();
                    this.ID = blood.ID;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }
    }
}
