using HID.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterVaccination.ADO
{
    class PersonADO : HID_PERSON
    {
        internal PersonADO() { }
        internal PersonADO(HID_PERSON p) 
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<PersonADO>(this, p);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
