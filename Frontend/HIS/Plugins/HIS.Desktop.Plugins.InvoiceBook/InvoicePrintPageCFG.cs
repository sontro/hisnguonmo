using HIS.Desktop.LocalStorage.ConfigApplication;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InvoiceBook
{
    public class InvoicePrintPageCFG
    {
        private const string CONFIG_KEY__SO_BAN_GHI_TRANG_DAU_TIEN_HIEN_THI_IN_HOA_DON = "CONFIG_KEY__SO_BAN_GHI_TRANG_DAU_TIEN_HIEN_THI_IN_HOA_DON";
        private const string CONFIG_KEY__SO_BAN_GHI_TRANG_TIEP_THEO_HIEN_THI_IN_HOA_DON = "CONFIG_KEY__SO_BAN_GHI_TRANG_TIEP_THEO_HIEN_THI_IN_HOA_DON";

        public static long SoBanGhiTrangDauTien { get; set; }
        public static long SoBanGhiTrangTiepTheo { get; set; }

        public static void LoadConfig()
        {
            try
            {
                SoBanGhiTrangDauTien = ConfigApplicationWorker.Get<long>(CONFIG_KEY__SO_BAN_GHI_TRANG_DAU_TIEN_HIEN_THI_IN_HOA_DON);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            try
            {
                SoBanGhiTrangTiepTheo = ConfigApplicationWorker.Get<long>(CONFIG_KEY__SO_BAN_GHI_TRANG_TIEP_THEO_HIEN_THI_IN_HOA_DON);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

        }
    }
}
