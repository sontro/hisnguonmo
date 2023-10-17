using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediStockImty;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisMediStockImtyCFG
    {
        private static List<HIS_MEDI_STOCK_IMTY> data;
        public static List<HIS_MEDI_STOCK_IMTY> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisMediStockImtyGet().Get(new HisMediStockImtyFilterQuery());
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
            var tmp = new HisMediStockImtyGet().Get(new HisMediStockImtyFilterQuery());
            data = tmp;
        }
    }
}
