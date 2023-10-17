using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.LibraryHein.Bhyt.HeinRightRoute;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;
using MOS.LogManager;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisVaccinationLog
    {
        internal static void Run(List<V_HIS_VACCINATION> vaccinations, List<HIS_EXP_MEST> expMests, List<V_HIS_EXP_MEST_MEDICINE> medicines, EventLog.Enum logEnum)
        {
            try
            {
                List<VaccinationData> vaccinationList = new List<VaccinationData>();

                foreach (V_HIS_VACCINATION vc in vaccinations)
                {
                    List<string> reqData = new List<string>();
                    List<string> expMestCodes = expMests != null ? expMests
                        .Where(o => o.VACCINATION_ID == vc.ID)
                        .Select(o => o.EXP_MEST_CODE).ToList() : null;

                    if (expMestCodes != null)
                    {
                        expMestCodes[0] = string.Format("{0}: {1}", SimpleEventKey.EXP_MEST_CODE, expMestCodes[0]);
                        reqData.AddRange(expMestCodes);
                    }

                    List<string> items = new List<string>();

                    if (medicines != null && medicines.Count > 0)
                    {
                        List<long> medicineTypeIds = medicines.Where(o => o.TDL_VACCINATION_ID == vc.ID).Select(o => o.TDL_MEDICINE_TYPE_ID.Value).ToList();
                        if (medicineTypeIds != null && medicineTypeIds.Count > 0)
                        {
                            List<string> tmp = HisMedicineTypeCFG.DATA.Where(o => medicineTypeIds.Contains(o.ID)).Select(o => o.MEDICINE_TYPE_NAME).ToList();
                            items.AddRange(tmp);
                        }
                    }

                    V_HIS_ROOM requestRoom = HisRoomCFG.DATA.Where(o => o.ID == vc.REQUEST_ROOM_ID).FirstOrDefault();
                    V_HIS_ROOM executeRoom = HisRoomCFG.DATA.Where(o => o.ID == vc.EXECUTE_ROOM_ID).FirstOrDefault();

                    vaccinationList.Add(new VaccinationData(vc.VACCINATION_CODE, items, requestRoom.ROOM_NAME, executeRoom.ROOM_NAME));
                }

                new EventLogGenerator(logEnum)
                    .VaccinationList(vaccinationList)
                    .Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
