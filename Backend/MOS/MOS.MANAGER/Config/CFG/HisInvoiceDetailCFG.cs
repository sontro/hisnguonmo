using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisInvoiceDetailCFG
    {
        /// <summary>
        /// Các tùy chọn gom nhóm dữ liệu khi tạo dữ liệu chi tiết hóa đơn
        /// </summary>
        public enum GroupOption
        {
            /// <summary>
            /// Gom nhóm toàn bộ các dữ liệu
            /// </summary>
            ALL = 1,

            /// <summary>
            /// Gom nhóm theo loại dịch vụ
            /// </summary>
            BY_TYPE = 2,

            /// <summary>
            /// Không gom nhóm
            /// </summary>
            NONE = 0
        }

        private const string GROUP_OTPION_CFG = "MOS.INVOICE_DETAIL.GROUP_OPTION";
        private const string GOODS_GROUP_NAME__BHYT_CFG = "MOS.INVOICE_DETAIL.GOODS_GROUP_NAME_BHYT";
        private const string GOODS_GROUP_NAME__NON_BHYT_CFG = "MOS.INVOICE_DETAIL.GOODS_GROUP_NAME_NON_BHYT";
        private const string GOODS_GROUP_CURRENCY_CFG = "MOS.INVOICE_DETAIL.GOODS_GROUP_CURRENCY";
        private const string GOODS_GROUP_NAME_CFG = "MOS.INVOICE_DETAIL.GOODS_GROUP_NAME";
        private const string GOODS_GROUP_UNIT_CFG = "MOS.INVOICE_DETAIL.GOODS_GROUP_UNIT";
        private const string HEIN_DESCRIPTION_CFG = "MOS.INVOICE_DETAIL.HEIN_DESCRIPTION";

        private static GroupOption groupOption;
        public static GroupOption GROUP_OPTION
        {
            get
            {
                if (groupOption == 0)
                {
                    try
                    {
                        groupOption = (GroupOption)ConfigUtil.GetIntConfig(GROUP_OTPION_CFG);
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Error(ex);
                    }
                }
                return groupOption;
            }
        }

        private static string invoiceDetailGoodsGroupName;
        public static string GOODS_GROUP_NAME
        {
            get
            {
                if (invoiceDetailGoodsGroupName == null)
                {
                    invoiceDetailGoodsGroupName = ConfigUtil.GetStrConfig(GOODS_GROUP_NAME_CFG);
                }
                return invoiceDetailGoodsGroupName;
            }
        }

        private static string invoiceDetailGoodsGroupNameBhyt;
        public static string GOODS_GROUP_NAME__BHYT
        {
            get
            {
                if (invoiceDetailGoodsGroupNameBhyt == null)
                {
                    invoiceDetailGoodsGroupNameBhyt = ConfigUtil.GetStrConfig(GOODS_GROUP_NAME__BHYT_CFG);
                }
                return invoiceDetailGoodsGroupNameBhyt;
            }
        }

        private static string invoiceDetailGoodsGroupNameNonBhyt;
        public static string GOODS_GROUP_NAME__NON_BHYT
        {
            get
            {
                if (invoiceDetailGoodsGroupNameNonBhyt == null)
                {
                    invoiceDetailGoodsGroupNameNonBhyt = ConfigUtil.GetStrConfig(GOODS_GROUP_NAME__NON_BHYT_CFG);
                }
                return invoiceDetailGoodsGroupNameNonBhyt;
            }
        }

        private static string invoiceDetailGoodsGroupUnit;
        public static string GOODS_GROUP_UNIT
        {
            get
            {
                if (invoiceDetailGoodsGroupUnit == null)
                {
                    invoiceDetailGoodsGroupUnit = ConfigUtil.GetStrConfig(GOODS_GROUP_UNIT_CFG);
                }
                return invoiceDetailGoodsGroupUnit;
            }
        }

        private static string goodsGroupCurrency;
        public static string GOODS_GROUP_CURRENCY
        {
            get
            {
                if (goodsGroupCurrency == null)
                {
                    goodsGroupCurrency = ConfigUtil.GetStrConfig(GOODS_GROUP_CURRENCY_CFG);
                }
                return goodsGroupCurrency;
            }
        }

        private static string invoiceDetailHeinDescription;
        public static string HEIN_DESCRIPTION
        {
            get
            {
                if (invoiceDetailHeinDescription == null)
                {
                    invoiceDetailHeinDescription = ConfigUtil.GetStrConfig(HEIN_DESCRIPTION_CFG);
                }
                return invoiceDetailHeinDescription;
            }
        }

        public static void Reload()
        {
            invoiceDetailGoodsGroupName = ConfigUtil.GetStrConfig(GOODS_GROUP_NAME_CFG);
            invoiceDetailGoodsGroupUnit = ConfigUtil.GetStrConfig(GOODS_GROUP_UNIT_CFG);
            invoiceDetailHeinDescription = ConfigUtil.GetStrConfig(HEIN_DESCRIPTION_CFG);
            invoiceDetailGoodsGroupNameBhyt = ConfigUtil.GetStrConfig(GOODS_GROUP_NAME__BHYT_CFG);
            invoiceDetailGoodsGroupNameNonBhyt = ConfigUtil.GetStrConfig(GOODS_GROUP_NAME__NON_BHYT_CFG);
            goodsGroupCurrency = ConfigUtil.GetStrConfig(GOODS_GROUP_CURRENCY_CFG);

            try
            {
                groupOption = (GroupOption)ConfigUtil.GetIntConfig(GROUP_OTPION_CFG);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
