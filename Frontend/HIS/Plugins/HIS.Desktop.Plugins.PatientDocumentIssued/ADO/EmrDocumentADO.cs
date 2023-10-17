using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EMR.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.PatientDocumentIssued.ADO
{
    class EmrDocumentADO : V_EMR_DOCUMENT
    {
        public string CONCRETE_ID__IN_SETY { get; set; }
        public string PARENT_ID__IN_SETY { get; set; }
        public string NOTE_ADO { get; set; }
        public string AMOUNT_SER { get; set; }

        public EmrDocumentADO() { }

        public EmrDocumentADO(V_EMR_DOCUMENT data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<EmrDocumentADO>(this, data);
                    this.PARENT_ID__IN_SETY = data.TREATMENT_CODE;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
