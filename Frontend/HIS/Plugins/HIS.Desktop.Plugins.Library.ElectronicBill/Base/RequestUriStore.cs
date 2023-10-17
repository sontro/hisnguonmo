using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Base
{
    public class RequestUriStore
    {
        internal const string MobifoneLogin = "/api/Account/Login";
        internal const string MobifoneGetDataReferencesByRefId = "/api/System/GetDataReferencesByRefId?refId=RF00059";
        internal const string MobifoneSaveListHoadon78 = "/api/Invoice68/SaveListHoadon78";
        internal const string MobifoneSignInvoiceCertFile68 = "/api/Invoice68/SignInvoiceCertFile68";
        internal const string MobifoneInHoadon = "/api/Invoice68/inHoadon?id={0}&type=PDF&inchuyendoi={1}";//api/Invoice68/inHoadon?id={hdon_id}&type=PDF&inchuyendoi=true
        internal const string MobifoneuploadCanceledInv = "/api/Invoice68/uploadCanceledInv?id={0}";//api/Invoice68/uploadCanceledInv?id={hdon_id}
        internal static string CombileUrl(params string[] data)
        {
            string result = "";
            List<string> pathUrl = new List<string>();
            for (int i = 0; i < data.Length; i++)
            {
                pathUrl.Add(data[i].Trim('/'));
            }

            result = string.Join("/", pathUrl);
            return result;
        }
    }
}
