using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.HisExamServiceTemp.HisExamServiceTemp
{
    class HisExamServiceTempBehavior : BusinessBase, IHisExamServiceTemp
    {
        object[] entity;
        internal HisExamServiceTempBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisExamServiceTemp.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                ExamServiceTempADO examServiceTempADO = null;
                MOS.EFMODEL.DataModels.HIS_EXAM_SERVICE_TEMP data_ = new MOS.EFMODEL.DataModels.HIS_EXAM_SERVICE_TEMP();
                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is ExamServiceTempADO)
                            {
                                examServiceTempADO = (ExamServiceTempADO)entity[i];
                            }
                            if (entity[i] is MOS.EFMODEL.DataModels.HIS_EXAM_SERVICE_TEMP)
                            {
                                data_ = (MOS.EFMODEL.DataModels.HIS_EXAM_SERVICE_TEMP)entity[i];
                            }
                        }
                    }
                }

                return new frmHisExamServiceTemp(moduleData, examServiceTempADO,data_);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
