using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.Library.PrintBordereau.Base;

namespace HIS.Desktop.Plugins.Bordereau.ADO
{
    internal class PayTypeADO
    {
        public string Option { get; set; }
        public PrintOption.PayType Value { get; set; }
        internal PayTypeADO()
        {
        }
        internal List<PayTypeADO> PayTypeADOs
        {
            get
            {
                List<PayTypeADO> result = new List<PayTypeADO>();
                result.Add(new PayTypeADO() { Option = "Tất cả", Value = PrintOption.PayType.ALL });
                result.Add(new PayTypeADO() { Option = "Chưa tạm ứng dịch vụ", Value = PrintOption.PayType.NOT_DEPOSIT });
                result.Add(new PayTypeADO() { Option = "Đã tạm ứng dịch vụ", Value = PrintOption.PayType.DEPOSIT });
                result.Add(new PayTypeADO() { Option = "Chưa thanh toán", Value = PrintOption.PayType.NOT_BILL });
                result.Add(new PayTypeADO() { Option = "Đã thanh toán", Value = PrintOption.PayType.BILL });
                return result;
            }
        }
    }
}
