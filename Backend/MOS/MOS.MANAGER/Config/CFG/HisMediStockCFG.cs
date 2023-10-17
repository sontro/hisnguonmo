using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediStock;
using MOS.UTILITY;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MOS.MANAGER.Config
{
    public class HisMediStockCFG
    {
        public enum ExportOption
        {
            IMP_TIME = 1,//uu tien theo thoi gian nhap truoc
            EXPIRED_DATE = 2,//uu tien theo han su dung
        }

        //Cac cau hinh quan ly thuoc le cua kho duoc (ko tinh tu truc)
        public enum OddManagementOption
        {
            //Khong quan ly phan le (phan le coi nhu la hao phi) ==> ko tao ra phieu xuat bu le
            NO_MANAGEMENT = 1,

            //Co quan ly phan le
            //- Trong truong hop linh thuoc thi tao phan bu` se chuyen ve kho le,
            //- Trong truong hop tra thuoc thi phan le se chuyen ve kho le
            MANAGEMENT = 2,

            /// <summary>
            /// Co quan ly phan le. nhung so luong bu le se duoc chia deu cho cac benh nhan duoc ke thuoc/vat tu day
            /// </summary>
            MANAGEMENT_ALLOCATE = 3,

            /// <summary>
            /// Co quan ly phan le. nhung so luong bu le se gan cho benh nhan cuoi cung duoc ke thuoc/vat tu day
            /// </summary>
            MANAGEMENT_PATIENT = 4
        }

        private const string EXPORT_OPTION_CFG = "MOS.HIS_MEDI_STOCK.EXPORT_OPTION";
        private const string ODD_MEDICINE_MANAGEMENT_OPTION_CFG = "MOS.HIS_MEDI_STOCK.ODD_MEDICINE_MANAGEMENT_OPTION";
        private const string ODD_MATERIAL_MANAGEMENT_OPTION_CFG = "MOS.HIS_MEDI_STOCK.ODD_MATERIAL_MANAGEMENT_OPTION";
        private const string IS_USE_BASE_AMOUNT_CABINET_CFG = "MOS.HIS_MEDI_STOCK.CABINET.IS_USE_BASE_AMOUNT";
        //Ko ke thuoc/vat tu het han su dung
        private const string DONT_PRES_EXPIRED_ITEM_CFG = "MOS.HIS_MEDI_STOCK.DONT_PRES_EXPIRED_ITEM";
        private const string IS_AUTO_STOCK_TRANSFER_CFG = "MOS.HIS_MEDI_STOCK.IS_AUTO_TRANSFER";
        private const string MEDI_STOCK_PERIOD_AUTO_CREATE = "MOS.HIS_MEDI_STOCK_PERIOD.AUTO_CREATE_OPTION";

        private static int exportOption;
        public static int EXPORT_OPTION
        {
            get
            {
                if (exportOption == 0)
                {
                    exportOption = ConfigUtil.GetIntConfig(EXPORT_OPTION_CFG);
                }
                return exportOption;
            }
        }

        private static OddManagementOption oddMedicineManagementOption;
        public static OddManagementOption ODD_MEDICINE_MANAGEMENT_OPTION
        {
            get
            {
                if (oddMedicineManagementOption == 0)
                {
                    oddMedicineManagementOption = (OddManagementOption) ConfigUtil.GetIntConfig(ODD_MEDICINE_MANAGEMENT_OPTION_CFG);
                }
                return oddMedicineManagementOption;
            }
        }

        private static OddManagementOption oddMaterialManagementOption;
        public static OddManagementOption ODD_MATERIAL_MANAGEMENT_OPTION
        {
            get
            {
                if (oddMaterialManagementOption == 0)
                {
                    oddMaterialManagementOption = (OddManagementOption) ConfigUtil.GetIntConfig(ODD_MATERIAL_MANAGEMENT_OPTION_CFG);
                }
                return oddMaterialManagementOption;
            }
        }

        private static bool? dontPresExpiredItem;
        public static bool DONT_PRES_EXPIRED_ITEM
        {
            get
            {
                if (!dontPresExpiredItem.HasValue)
                {
                    dontPresExpiredItem = ConfigUtil.GetStrConfig(DONT_PRES_EXPIRED_ITEM_CFG) == "1";
                }
                return dontPresExpiredItem.Value;
            }
        }

        private static bool? isUseBaseAmountCabinet;
        public static bool IS_USE_BASE_AMOUNT_CABINET
        {
            get
            {
                if (!isUseBaseAmountCabinet.HasValue)
                {
                    isUseBaseAmountCabinet = ConfigUtil.GetStrConfig(IS_USE_BASE_AMOUNT_CABINET_CFG) == "1";
                }
                return isUseBaseAmountCabinet.Value;
            }
        }

        private static bool? isAutoStockTransfer;
        public static bool IS_AUTO_STOCK_TRANSFER
        {
            get
            {
                if (!isAutoStockTransfer.HasValue)
                {
                    isAutoStockTransfer = ConfigUtil.GetStrConfig(IS_AUTO_STOCK_TRANSFER_CFG) == "1";
                }
                return isAutoStockTransfer.Value;
            }
        }

        private static bool? IsMediStockPeriodAutoCreate;
        public static bool IS_MEDI_STOCK_PERIOD_AUTO_CREATE
        {
            get
            {
                if (!IsMediStockPeriodAutoCreate.HasValue)
                {
                    IsMediStockPeriodAutoCreate = ConfigUtil.GetStrConfig(MEDI_STOCK_PERIOD_AUTO_CREATE) == "1";
                }
                return IsMediStockPeriodAutoCreate.Value;
            }
        }

        private static List<V_HIS_MEDI_STOCK> data;
        public static List<V_HIS_MEDI_STOCK> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisMediStockGet().GetView(new HisMediStockViewFilterQuery());
                }
                return data;
            }
        }
        private static List<V_HIS_MEDI_STOCK> isExpend;
        public static List<V_HIS_MEDI_STOCK> IS_EXPEND_VIEW
        {
            get
            {
                if (isExpend == null)
                {
                    isExpend = DATA != null ? DATA.Where(o => o.IS_EXPEND == Constant.IS_TRUE).ToList() : null;
                }
                return isExpend;
            }
        }

        public static void Reload()
        {
            var tmp = new HisMediStockGet().GetView(new HisMediStockViewFilterQuery());
            exportOption = ConfigUtil.GetIntConfig(EXPORT_OPTION_CFG);
            oddMedicineManagementOption = (OddManagementOption) ConfigUtil.GetIntConfig(ODD_MEDICINE_MANAGEMENT_OPTION_CFG);
            oddMaterialManagementOption = (OddManagementOption) ConfigUtil.GetIntConfig(ODD_MATERIAL_MANAGEMENT_OPTION_CFG);
            isUseBaseAmountCabinet = ConfigUtil.GetStrConfig(IS_USE_BASE_AMOUNT_CABINET_CFG) == "1";
            dontPresExpiredItem = ConfigUtil.GetStrConfig(DONT_PRES_EXPIRED_ITEM_CFG) == "1";
            isAutoStockTransfer = ConfigUtil.GetStrConfig(IS_AUTO_STOCK_TRANSFER_CFG) == "1";
            isExpend = DATA != null ? DATA.Where(o => o.IS_EXPEND == Constant.IS_TRUE).ToList() : null;

            data = tmp;
        }
    }
}
