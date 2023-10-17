using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintBordereau.Base
{
    public class PrintOption
    {
        public enum Value
        {
            PRINT_NOW = 1,
            INIT_MENU = 2,
            PRINT_NOW_AND_INIT_MENU = 3,
            PRINT_NOW_AND_EMR_SIGN_NOW = 4,
            EMR_SIGN_NOW = 5,
            EMR_SIGN_AND_PRINT_PREVIEW = 6,
            SHOW_DIALOG = 7,
        }

        public enum PayType
        {
            ALL = 1,
            DEPOSIT = 2,
            NOT_DEPOSIT = 3,
            BILL = 4,
            NOT_BILL = 5,
            NOT_BILL_OR_DEPOSIT = 6
        }
    }
}
