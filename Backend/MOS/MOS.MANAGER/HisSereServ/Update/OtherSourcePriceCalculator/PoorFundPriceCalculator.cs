using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ
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

		private string provinceCode;

		public PoorFundPriceCalculator(string provinceCode)
		{
			this.provinceCode = provinceCode;
		}

		/// <summary>
		/// The hop le la the co do dai 9 ki tu hoac 12 ki tu
		/// Neu the 12 ki tu thi phai ket thuc la "VCN" --> sua lai, ko can check ket thuc la "VCN"
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
		private static bool IsPoorMan(string heinCardNumber, string hnCode)
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
		public void Run(List<HIS_SERE_SERV> sereServs, string hnCode, string heinCardNumber)
		{
			if (sereServs != null && sereServs.Count > 0
                && PoorFundPriceCalculator.IsPoorMan(heinCardNumber, hnCode)
                && HisPoorFundCFG.IS_APPLIED)
			{
				//Lay ma tinh lay ra tu so the BHYT
				string cardProvinceCode = heinCardNumber.Substring(3, 2);

				//Chi xu ly voi cac the co tien to duoc quy dinh va cung tinh voi chi nhanh dang chay
				if (cardProvinceCode == this.provinceCode && (heinCardNumber.StartsWith(CN_PREFIX) || heinCardNumber.StartsWith(GD_PREFIX)))
				{
					decimal ratio = heinCardNumber.StartsWith(CN_PREFIX) ? CN_PREFIX_RATIO : GD_PREFIX_RATIO;
					
					foreach (HIS_SERE_SERV s in sereServs)
					{
						//Quy chi thanh toan khi BHYT ko thanh toan 100%
						if (s.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && s.HEIN_RATIO < 1)
						{
							//neu dau ma the GD va ket thuc la VCN thi kiem tra co thuoc ds dich vu duoc thanh toan khong
							decimal price = s.HEIN_LIMIT_PRICE.HasValue ? s.HEIN_LIMIT_PRICE.Value : s.PRICE;
							if (heinCardNumber.StartsWith(GD_PREFIX))
							{
								s.OTHER_SOURCE_PRICE = price * ratio;
							}
							else if (heinCardNumber.StartsWith(CN_PREFIX))
							{
								s.OTHER_SOURCE_PRICE = price * ratio;
							}
						}
					}
				}
			}
		}
	}
}
