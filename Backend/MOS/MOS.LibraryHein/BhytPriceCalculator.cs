using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt.HeinRatio;
using MOS.LibraryHein.Bhyt.HeinTreatmentType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LibraryHein.Bhyt
{
    public class BhytPriceCalculator
    {
        /// <summary>
        /// Lay gia dich vu doi voi benh nhan ngoai tru theo ti le huong mac dinh cua the
        /// </summary>
        /// <param name="sereServ"></param>
        /// <param name="branch"></param>
        /// <returns></returns>
        public static decimal? DefaultPatientPriceForOutBhyt(V_HIS_SERE_SERV sereServ)
        {
            decimal? result = null;
            if (sereServ != null)
            {
                if (!string.IsNullOrWhiteSpace(sereServ.JSON_PATIENT_TYPE_ALTER))
                {
                    BhytServiceRequestData bhytService = new BhytServiceRequestData(sereServ.JSON_PATIENT_TYPE_ALTER, HeinRatioTypeCode.NORMAL);

                    if (bhytService != null && bhytService.PatientTypeData != null)
                    {
                        BhytHeinProcessor processor = new BhytHeinProcessor();

                        decimal? ratio = processor.GetDefaultHeinRatio(HeinTreatmentTypeCode.EXAM, sereServ.HEIN_CARD_NUMBER, bhytService.PatientTypeData.LEVEL_CODE, bhytService.PatientTypeData.RIGHT_ROUTE_CODE);
                        if (sereServ.VIR_PRICE.HasValue && ratio.HasValue)
                        {
                            decimal chenhLech = (sereServ.VIR_PRICE.Value - (sereServ.HEIN_LIMIT_PRICE ?? sereServ.VIR_PRICE.Value)) * sereServ.AMOUNT;
                            decimal bnDongChiTra = (sereServ.HEIN_LIMIT_PRICE ?? sereServ.VIR_PRICE.Value) * sereServ.AMOUNT * (1 - ratio.Value);
                            result = chenhLech + bnDongChiTra;
                        }
                    }
                }
                else
                {
                    result = sereServ.VIR_PRICE.Value;
                }
            }
            return result;
        }
    }
}
