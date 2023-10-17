using Inventec.Common.Logging;
using MOS.MANAGER.HisTreatment.Xml;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Xml
{
    class ExportXmlFhirSurehisJob: IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info(String.Format("Begin ExportXmlFhirSurehisJob. Thread: {0}; Time= {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                HisTreatmentExportXmlFhirSurehis.Run();
                LogSystem.Info(String.Format("End ExportXmlFhirSurehisJob. Thread: {0}; Time= {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}