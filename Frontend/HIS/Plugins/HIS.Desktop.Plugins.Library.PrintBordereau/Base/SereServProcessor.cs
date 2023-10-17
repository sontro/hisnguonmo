using HIS.Desktop.Plugins.Library.PrintBordereau.Config;
using MOS.EFMODEL.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintBordereau.Base
{
    public class SereServProcessor
    {
        public static Dictionary<string, List<HIS_SERE_SERV>> GroupSereServByPatyAlterBhyt(List<HIS_SERE_SERV> listSereServ)
        {
            Dictionary<string, List<HIS_SERE_SERV>> dicSereServ = new Dictionary<string, List<HIS_SERE_SERV>>();
            try
            {
                listSereServ = listSereServ.Where(o => o.IS_NO_EXECUTE != 1).ToList();
                if (listSereServ == null || listSereServ.Count == 0)
                {
                    Inventec.Common.Logging.LogSystem.Debug("Khong tim thay dich vu nao duoc thuc hien");
                    return null;
                }

                foreach (HIS_SERE_SERV s in listSereServ)
                {
                    if (s.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && s.JSON_PATIENT_TYPE_ALTER != null)
                    {
                        HIS_PATIENT_TYPE_ALTER patyAlter = JsonConvert.DeserializeObject<HIS_PATIENT_TYPE_ALTER>(s.JSON_PATIENT_TYPE_ALTER);
                        if (patyAlter != null)
                        {
                            string key = ToString(patyAlter);
                            List<HIS_SERE_SERV> list;
                            if (dicSereServ.ContainsKey(key))
                            {
                                list = dicSereServ[key];
                            }
                            else
                            {
                                list = new List<HIS_SERE_SERV>();
                                dicSereServ.Add(key, list);
                            }
                            list.Add(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dicSereServ = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return dicSereServ;
        }

        private static string ToString(HIS_PATIENT_TYPE_ALTER patyAlter)
        {
            if (patyAlter != null)
            {
                return NVL(patyAlter.HEIN_CARD_NUMBER) + "|"
                    + NVL(patyAlter.HEIN_MEDI_ORG_CODE) + "|"
                    + NVL(patyAlter.LEVEL_CODE) + "|"
                    + NVL(patyAlter.RIGHT_ROUTE_CODE) + "|"
                    + NVL(patyAlter.RIGHT_ROUTE_TYPE_CODE) + "|"
                    + NVL(patyAlter.JOIN_5_YEAR) + "|"
                    + NVL(patyAlter.PAID_6_MONTH) + "|"
                    + NVL(patyAlter.LIVE_AREA_CODE) + "|"
                    + NVL(patyAlter.HNCODE)
                    + NVL((patyAlter.HEIN_CARD_FROM_TIME ?? 0).ToString())
                    + NVL((patyAlter.HEIN_CARD_TO_TIME ?? 0).ToString()); ;
            }
            return null;
        }

        private static string NVL(string s)
        {
            return !string.IsNullOrWhiteSpace(s) ? s : "";
        }

        public static string GetDefaultHeinRatioForView(string heinCardNumber, string treatmentTypeCode, string levelCode, string rightRouteCode)
        {
            string result = "";
            try
            {
                result = ((new MOS.LibraryHein.Bhyt.BhytHeinProcessor().GetDefaultHeinRatio(treatmentTypeCode, heinCardNumber, levelCode, rightRouteCode) ?? 0) * 100) + "%";
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
    }
}
