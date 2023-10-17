using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Base
{
    public class ProviderType
    {
        public const string VNPT = "VNPT";
        public const string VIETSENS = "VIETSENS";
        public const string BKAV = "BKAV";
        public const string VIETTEL = "VIETEL";
        public const string CongThuong = "MOIT";
        public const string SoftDream = "SODR";
        public const string MISA = "MISA";
        public const string safecert = "SAFECERT";
        public const string CTO = "CTO_PROXY";
        public const string BACH_MAI = "BACH_MAI";
        public const string MOBIFONE = "MOBIFONE";
        //thêm đối tác cần add thêm vào type
        public static List<string> TYPE
        {
            get
            {
                return new List<string>() { VNPT, VIETSENS, BKAV, VIETTEL, CongThuong, SoftDream, MISA, safecert, CTO, BACH_MAI, MOBIFONE };
            }
        }

        //1 - 0%, 2 - 5%, 3 - 10%, 4 - khong chiu thue, 5 - khong ke khai thue, 6 - khac
        internal const int tax_0 = 1;
        internal const int tax_5 = 2;
        internal const int tax_10 = 3;
        internal const int tax_KCT = 4;
        internal const int tax_KKKT = 5;
        internal const int tax_K = 6;
    }
}
