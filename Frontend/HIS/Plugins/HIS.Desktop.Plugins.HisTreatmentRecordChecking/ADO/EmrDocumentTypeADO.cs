using EMR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTreatmentRecordChecking.ADO
{
	internal class EmrDocumentTypeADO : EMR_DOCUMENT_TYPE
	{
		public bool IsHasDocument { get; set; }
		public EmrDocumentTypeADO(EMR_DOCUMENT_TYPE data)
		{
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<EMR_DOCUMENT_TYPE>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

		public EmrDocumentTypeADO()
		{
		}
	}
}
