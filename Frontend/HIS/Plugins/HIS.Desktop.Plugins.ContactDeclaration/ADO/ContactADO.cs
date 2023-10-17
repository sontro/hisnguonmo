using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Filter;
using Inventec.Core;
using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;

namespace HIS.Desktop.Plugins.ContactDeclaration.ADO
{
    class ContactPointADO : V_HIS_CONTACT_POINT
    {
        public string CODE_STR { get; set; }
        public long CONTACT_TIME { get; set; }
        public long CONTACT_ID { get; set; }
        public string FULL_NAME { get; set; }

        public ContactPointADO() { }

        public ContactPointADO(V_HIS_CONTACT_POINT data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ContactPointADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
