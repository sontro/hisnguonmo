using Inventec.Core;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsBackKhoa;
using MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsSancy;
using MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsVietsens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs
{
    class PacsFactory
    {
        public static IPacsProcessor GetProcessor(CommonParam param)
        {
            if (PacsCFG.PACS_INTEGRATE_OPTION == (int)PacsIntegrateOption.PACS_VIETSENS)
            {
                return new PacsVietsensProcessor(param);
            }
            else if (PacsCFG.PACS_INTEGRATE_OPTION == (int)PacsIntegrateOption.PACS_SANCY)
            {
                return new PacsSancyProcessor(param);
            }
            else if (PacsCFG.PACS_INTEGRATE_OPTION == (int)PacsIntegrateOption.PACS_BACH_KHOA)
            {
                return new PacsBackKhoaProcessor(param);
            }
            return null;
        }

        public static IPacsReadProcessor GetReadProcessor(CommonParam param)
        {
            if (PacsCFG.PACS_INTEGRATE_OPTION == (int)PacsIntegrateOption.PACS_SANCY)
            {
                return new PacsSancyReadProcessor(param);
            }
            else if (PacsCFG.PACS_INTEGRATE_OPTION == (int)PacsIntegrateOption.PACS_BACH_KHOA)
            {
                return new PacsBackKhoaReadProcessor(param);
            }
            return null;
        }
    }
}
