using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.UTILITY;
using Inventec.Fss.Utility;
using His.ExportXml.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MOS.MANAGER.HisTreatment.Xml
{
    public class HisTreatmentExportXML4210Create : BusinessBase
    {
        internal HisTreatmentExportXML4210Create()
            : base()
        {

        }

        internal HisTreatmentExportXML4210Create(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<long> ids, ref List<V_HIS_TREATMENT_1> resultData)
        {
            bool result = false;
            try
            {
                var treatments = new HisTreatmentGet().GetByIds(ids);
                LogSystem.Info(string.Format("XmlJob has {0} treatments:", treatments.Count.ToString()));

                if (treatments != null && treatments.Count > 0)
                {
                    foreach (var treatment in treatments)
                    {
                        new ExportXml().ExportXML4210(treatment.ID, treatment.BRANCH_ID);
                    }
                }
                HisTreatmentView1FilterQuery filter = new HisTreatmentView1FilterQuery();
                filter.IDs = ids;

                result = true;
                resultData = new HisTreatmentGet().GetView1(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
