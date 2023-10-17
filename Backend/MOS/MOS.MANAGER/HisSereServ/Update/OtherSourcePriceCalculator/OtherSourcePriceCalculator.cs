using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.Config.CFG;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ
{
	public class OtherSourcePriceCalculator
	{
        public OtherSourcePriceCalculator()
		{
		}

		/// <summary>
		/// Neu 1 thuoc/dich vu duoc danh dau la quy thanh toan khac chi tra thi
        /// quy se ho tro tra toan bo chi phi BN phai tra (khac voi quy HIV la chi tra phan BN dong chi tra)
		/// </summary>
		/// <param name="sereServs"></param>
		/// <param name="services"></param>
		/// <param name="patientAlterBhyt"></param>
		/// <returns></returns>
		public void Run(List<HIS_SERE_SERV> sereServs)
		{
			if (sereServs != null && sereServs.Count > 0)
			{
                foreach (HIS_SERE_SERV s in sereServs)
                {
                    HIS_OTHER_PAY_SOURCE otherPaySource = HisOtherPaySourceCFG.DATA != null && s.OTHER_PAY_SOURCE_ID.HasValue ? HisOtherPaySourceCFG.DATA.Where(o => o.ID == s.OTHER_PAY_SOURCE_ID.Value).FirstOrDefault() : null;
                    if (otherPaySource != null)
                    {
                        //Neu nguon luon chi tra 100% ke ca trong truong hop BHYT
                        if (otherPaySource.IS_PAID_ALL == Constant.IS_TRUE)
                        {
                            if (otherPaySource.IS_NOT_PAID_DIFF != Constant.IS_TRUE)
                            {
                                s.OTHER_SOURCE_PRICE = SereServVirtualColumn.VIR_PRICE(s);
                                s.HEIN_RATIO = null;
                                s.HEIN_PRICE = null;
                                s.PATIENT_PRICE_BHYT = null;
                            }
                            else
                            {
                                //Neu co gia tran thi lay gia tran (trong truong hop BHYT co khai bao gia tran)
                                if (s.HEIN_LIMIT_PRICE.HasValue)
                                {
                                    s.OTHER_SOURCE_PRICE = s.HEIN_LIMIT_PRICE;
                                }
                                //Neu co gia doi tuong thanh toan va gia cua doi tuong phu thu
                                //thi lay theo gia cua doi tuong thanh toan (trong truong hop chon doi tuong phu thu)
                                else if (SereServVirtualColumn.VIR_LIMIT_PRICE(s).HasValue)
                                {
                                    s.OTHER_SOURCE_PRICE = SereServVirtualColumn.VIR_LIMIT_PRICE(s);
                                }
                                else
                                {
                                    s.OTHER_SOURCE_PRICE = SereServVirtualColumn.VIR_PRICE(s);
                                }
                                s.HEIN_RATIO = null;
                                s.HEIN_PRICE = null;
                                s.PATIENT_PRICE_BHYT = null;
                            }
                        }
                        //Nguoc lai chi tra so tien BN phai chi tra
                        else
                        {
                            
                            //Neu check "ko tra chenh lech"
                            if (otherPaySource.IS_NOT_PAID_DIFF == Constant.IS_TRUE)
                            {
                                if (s.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                {
                                    s.OTHER_SOURCE_PRICE = SereServVirtualColumn.VIR_PATIENT_PRICE_BHYT_NO_OTHER_SOURCE(s);
                                }
                                //Neu co gia doi tuong thanh toan va gia cua doi tuong phu thu
                                //thi lay theo gia cua doi tuong thanh toan (trong truong hop chon doi tuong phu thu)
                                else if (SereServVirtualColumn.VIR_LIMIT_PRICE(s).HasValue)
                                {
                                    s.OTHER_SOURCE_PRICE = SereServVirtualColumn.VIR_LIMIT_PRICE(s);
                                }
                                else
                                {
                                    s.OTHER_SOURCE_PRICE = SereServVirtualColumn.VIR_PATIENT_PRICE_NO_OTHER_SOURCE(s);
                                }
                            }
                            else
                            {
                                //Nguon se chi tra toan bo so tien BN phai chi tra
                                s.OTHER_SOURCE_PRICE = SereServVirtualColumn.VIR_PATIENT_PRICE_NO_OTHER_SOURCE(s);
                            }
                        }
                    }
                }
			}
		}
	}
}
