using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ
{
	public class HivFundPriceCalculator
	{
		public HivFundPriceCalculator()
		{
		}

		/// <summary>
		/// Neu 1 thuoc/dich vu duoc danh dau la quy HIV toan cau chi tra thi quy HIV 
        /// se tra toan bo phan BN dong chi tra
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
                    //Trong truong hop duoc quy khac chi tra --> quy phong chong HIV toan cau chi tra
                    if (s.IS_OTHER_SOURCE_PAID == Constant.IS_TRUE)
                    {
                        //Neu cau hinh chi ap dung voi BHYT
                        if (HisSereServCFG.OTHER_SOURCE_PAID_SERVICE_OPTION == HisSereServCFG.OtherSourcePaidServiceOption.BHYT && s.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                        {
                            //Quy toan cau se chi tra toan bo tien con lai
                            s.OTHER_SOURCE_PRICE = SereServVirtualColumn.VIR_PATIENT_PRICE_BHYT_NO_OTHER_SOURCE(s);
                        }
                        //Neu ap dung voi tat ca cac doi tuong
                        else if (HisSereServCFG.OTHER_SOURCE_PAID_SERVICE_OPTION == HisSereServCFG.OtherSourcePaidServiceOption.ALL)
                        {
                            //Nguon se chi tra toan bo so tien BN phai chi tra
                            s.OTHER_SOURCE_PRICE = SereServVirtualColumn.VIR_PATIENT_PRICE_NO_OTHER_SOURCE(s);
                        }
                        //Neu ap dung tat ca cac doi tuong va luon tra 100%
                        else if (HisSereServCFG.OTHER_SOURCE_PAID_SERVICE_OPTION == HisSereServCFG.OtherSourcePaidServiceOption.PAID_ALL)
                        {
                            //Nguon se chi tra toan bo so tien
                            s.OTHER_SOURCE_PRICE = SereServVirtualColumn.VIR_PRICE(s);
                            s.HEIN_RATIO = null;
                            s.HEIN_PRICE = null;
                            s.HEIN_LIMIT_PRICE = 0;
                            s.PATIENT_PRICE_BHYT = null;
                        }
                        else
                        {
                            s.OTHER_SOURCE_PRICE = null;
                        }
                    }
                    else
                    {
                        s.OTHER_SOURCE_PRICE = null;
                    }
                }
			}
		}
	}
}
