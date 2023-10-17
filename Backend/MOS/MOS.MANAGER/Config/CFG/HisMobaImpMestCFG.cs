

namespace MOS.MANAGER.Config
{
    class HisMobaImpMestCFG
    {
        private const string NOT_ALLOW_FOR_CABINET_CFG = "MOS.HIS_MOBA_IMP_MEST.NOT_ALLOW_FOR_CABINET";
        private const string MOBA_INTO_MEDI_STOCK_EXPORT_CFG = "MOS.HIS_MOBA_IMP_MEST.CABINET.MOBA_INTO_MEDI_STOCK_EXPORT";

        private static bool? notAllowForCabinet;
        public static bool NOT_ALLOW_FOR_CABINET
        {
            get
            {
                if (!notAllowForCabinet.HasValue)
                {
                    notAllowForCabinet = ConfigUtil.GetIntConfig(NOT_ALLOW_FOR_CABINET_CFG) == 1;
                }
                return notAllowForCabinet.Value;
            }
        }

        private static bool? mobaIntoMediStockExport__Cabinet;
        public static bool MOBA_INTO_MEDI_STOCK_EXPORT__CABINET
        {
            get
            {
                if (!mobaIntoMediStockExport__Cabinet.HasValue)
                {
                    mobaIntoMediStockExport__Cabinet = ConfigUtil.GetIntConfig(MOBA_INTO_MEDI_STOCK_EXPORT_CFG) == 1;
                }
                return mobaIntoMediStockExport__Cabinet.Value;
            }
        }

        public static void Reload()
        {
            notAllowForCabinet = ConfigUtil.GetIntConfig(NOT_ALLOW_FOR_CABINET_CFG) == 1;
            mobaIntoMediStockExport__Cabinet = ConfigUtil.GetIntConfig(MOBA_INTO_MEDI_STOCK_EXPORT_CFG) == 1;
        }
    }
}
