using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HIS.Desktop.ApiConsumer
{
    public class TokenCodeStore
    {
        public static LocalDataStoreSlot SlotTokenCode = Thread.AllocateNamedDataSlot("HIS.Desktop.ApiConsumer.TokenCode");
        public static string TokenCode
        {
            get
            {
                return (Thread.GetData(SlotTokenCode) ?? "").ToString();
            }
        }
    }
}
