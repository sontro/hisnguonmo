using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InvoiceCreateForTreatment
{
    class InvoiceCreateForTreatmentConfig
    {
        private const string Key__InvoiceTypeCreate = "HIS.Desktop.Plugins.InvoiceTypeCreate";

        /// <summary>
        /// Cấu hình chế độ tạo hóa đơn điện tử, chữ ký điện tử
        ///- Đặt 1: Chỉ tạo hóa đơn điện tử trên hệ thống của vnpt, không tạo trên hệ thống HIS
        ///- Mặc định tạo giao dịch trên hệ thống HIS, tự tạo hóa đơn + ký điện tử trên hóa đơn lưu trên hệ thống HIS
        /// </summary>
        private static string invoiceTypeCreate;
        public static string InvoiceTypeCreate
        {
            get
            {
                if (invoiceTypeCreate == null)
                {
                    invoiceTypeCreate = GetValue(Key__InvoiceTypeCreate);
                }
                return invoiceTypeCreate;
            }
            set
            {
                invoiceTypeCreate = value;
            }
        }

        private static string GetValue(string code)
        {
            string result = null;
            try
            {
                return Inventec.Common.LocalStorage.SdaConfig.SdaConfigs.Get<string>(code);
            }
            catch (Exception ex)
            {
                LogSystem.Debug(ex);
                result = null;
            }
            return result;
        }
    }
}
