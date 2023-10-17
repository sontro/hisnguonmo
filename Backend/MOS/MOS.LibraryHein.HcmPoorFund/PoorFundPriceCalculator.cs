using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LibraryHein.HcmPoorFund
{
    /// <summary>
    /// 1. Với đầu thẻ CN (thẻ 95%) --> UB sẽ trả 5% còn lại cho bệnh nhân
    /// 2. Với đầu thẻ GD (thẻ 80%), nếu có chuỗi thông tin hộ nghèo (trên thẻ có ghi), thì phần mềm sẽ cho nhập chuỗi thông tin này để nhận diện, khi này sẽ có 2 trường hợp:
    /// 2.1. Chuỗi thông tin có dạng XXXXXXXX (9 ký tự) --> UB sẽ trả 15% viện phí, bệnh nhân vẫn trả 5%
    /// 2.2. Chuỗi thông tin có dạng XXXXXXXXVCN (12 ký tự) --> UB sẽ trả 15% viện phí neu BN co tham gia 1 số dịch vụ nhất định --> hệ thống có thẻ cấu hình để khai báo các mã dịch vụ này
    /// </summary>
    public class PoorFundPriceCalculator
    {
        private const decimal CN_PREFIX_RATIO = 0.05m;
        private const decimal GD_PREFIX_RATIO = 0.15m;
        private const string CN_PREFIX = "CN";
        private const string GD_PREFIX = "GD";
        private const string VCN_POST_FIX = "VCN";

        private long patientTypeId;//doi tuong thanh toan duoc ap dung
        private string provinceCode;
        private List<long> vcnAcceptServiceIds;

        public PoorFundPriceCalculator(string provinceCode, List<long> vcnAcceptServiceIds, long patientTypeId)
        {
            this.provinceCode = provinceCode;
            this.vcnAcceptServiceIds = vcnAcceptServiceIds;
            this.patientTypeId = patientTypeId;
        }

        /// <summary>
        /// The hop le la the co do dai 9 ki tu hoac 12 ki tu
        /// Neu the 12 ki tu thi phai ket thuc la "VCN"
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static bool IsValidCode(string hnCode)
        {
            if (hnCode != null && !string.IsNullOrWhiteSpace(hnCode.Trim()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kiem tra xem co phai thuoc dien doi tuong ho ngheo hay khong. Dam bao 1 trong 2 dieu kien:
        /// - So the BHYT co dau the la CN
        /// - So the BHYT co dau the la GD va ma ho ngheo la hop le
        /// </summary>
        /// <param name="heinCardNumber"></param>
        /// <param name="hnCode"></param>
        /// <returns></returns>
        public static bool IsPoorMan(string heinCardNumber, string hnCode)
        {
            if (!string.IsNullOrWhiteSpace(heinCardNumber))
            {
                if (heinCardNumber.StartsWith(CN_PREFIX))
                {
                    return true;
                }
                if (heinCardNumber.StartsWith(GD_PREFIX) && !string.IsNullOrWhiteSpace(hnCode) && PoorFundPriceCalculator.IsValidCode(hnCode))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Lay so tien chi tra theo chinh sach cua Quy ho ngheo cua HCM
        /// </summary>
        /// <param name="sereServs"></param>
        /// <param name="services"></param>
        /// <param name="patientAlterBhyt"></param>
        /// <returns></returns>
        public decimal? GetPaidAmount(List<HIS_SERE_SERV> sereServs, string hnCode, string heinCardNumber)
        {
            decimal? result = null;
            if (sereServs != null && sereServs.Count > 0 && PoorFundPriceCalculator.IsPoorMan(heinCardNumber, hnCode))
            {
                //Lay ma tinh lay ra tu so the BHYT
                string cardProvinceCode = heinCardNumber.Substring(3, 2);
                //Chi xu ly voi cac the co tien to duoc quy dinh va thuoc HCM
                if (cardProvinceCode == this.provinceCode && (heinCardNumber.StartsWith(CN_PREFIX) || heinCardNumber.StartsWith(GD_PREFIX)))
                {
                    decimal ratio = heinCardNumber.StartsWith(CN_PREFIX) ? CN_PREFIX_RATIO : GD_PREFIX_RATIO;
                    bool isExistVcnService = sereServs.Any(o => vcnAcceptServiceIds != null && vcnAcceptServiceIds.Contains(o.SERVICE_ID));

                    result = 0;
                    foreach (HIS_SERE_SERV s in sereServs)
                    {
                        //Quy chi thanh toan khi BHYT ko thanh toan 100%
                        if (s.PATIENT_TYPE_ID == this.patientTypeId && s.HEIN_RATIO < 1)
                        {
                            //neu dau ma the GD va ket thuc la VCN thi kiem tra co thuoc ds dich vu duoc thanh toan khong
                            if ((heinCardNumber.StartsWith(GD_PREFIX) && hnCode.EndsWith(VCN_POST_FIX) && isExistVcnService))
                            {
                                result += (s.VIR_TOTAL_HEIN_PRICE / s.HEIN_RATIO) * ratio;
                            }
                            else if (heinCardNumber.StartsWith(CN_PREFIX) || (heinCardNumber.StartsWith(GD_PREFIX) && !hnCode.EndsWith(VCN_POST_FIX)))
                            {
                                result += (s.VIR_TOTAL_HEIN_PRICE / s.HEIN_RATIO) * ratio;
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
