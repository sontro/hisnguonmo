using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.VaccinationExam.ADO
{
    public class VaccExamResultADO : HIS_VACC_EXAM_RESULT
    {
        public string Note { get; set; }

        public VaccExamResultADO()
        {
        }

        public VaccExamResultADO(HIS_VACC_EXAM_RESULT data)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<VaccExamResultADO>(this, data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
