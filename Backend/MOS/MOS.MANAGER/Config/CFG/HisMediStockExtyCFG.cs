using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediStockExty;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisMediStockExtyCFG
    {
        private static List<HIS_MEDI_STOCK_EXTY> data;
        public static List<HIS_MEDI_STOCK_EXTY> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisMediStockExtyGet().Get(new HisMediStockExtyFilterQuery());
                }
                return data;
            }
        }

        public static void Reset()
        {
            data = null;
        }

        public static void Reload()
        {
            var tmp = new HisMediStockExtyGet().Get(new HisMediStockExtyFilterQuery());
            data = tmp;
        }
    }
}
