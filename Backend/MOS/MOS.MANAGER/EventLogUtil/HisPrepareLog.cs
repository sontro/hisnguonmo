using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisPrepareLog
    {

        public static void Run(HIS_TREATMENT treatment, HIS_PREPARE preapre, List<HIS_PREPARE_MATY> materials, List<HIS_PREPARE_METY> medicines, EventLog.Enum logEnum)
        {
            try
            {
                string treatmentCode = treatment != null ? treatment.TREATMENT_CODE : null;
                List<string> datas = new List<string>();
                if (materials != null && materials.Count > 0)
                {
                    foreach (var item in materials)
                    {
                        string mateName = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == item.MATERIAL_TYPE_ID).MATERIAL_TYPE_NAME;
                        if (preapre.APPROVAL_TIME.HasValue)
                        {
                            datas.Add(String.Format("{0}({1})", mateName, item.APPROVAL_AMOUNT ?? 0));
                        }
                        else
                        {
                            datas.Add(String.Format("{0}({1})", mateName, item.REQ_AMOUNT));
                        }
                    }
                }
                if (medicines != null && medicines.Count > 0)
                {
                    foreach (var item in medicines)
                    {
                        string mediName = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == item.MEDICINE_TYPE_ID).MEDICINE_TYPE_NAME;
                        if (preapre.APPROVAL_TIME.HasValue)
                        {
                            datas.Add(String.Format("{0}({1})", mediName, item.APPROVAL_AMOUNT ?? 0));
                        }
                        else
                        {
                            datas.Add(String.Format("{0}({1})", mediName, item.REQ_AMOUNT));
                        }
                    }
                }

                new EventLogGenerator(logEnum)
                    .TreatmentCode(treatmentCode)
                    .PrepareCode(preapre.PREPARE_CODE)
                    .PrepareDetailList(datas)
                    .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public static void Run(List<HIS_TREATMENT> listTreatment, List<HIS_PREPARE> listPreapre, EventLog.Enum logEnum)
        {
            try
            {
                List<string> datas = new List<string>();
                if (listPreapre != null && listPreapre.Count > 0)
                {
                    var Groups = listPreapre.GroupBy(g => g.TREATMENT_ID).ToList();
                    foreach (var group in Groups)
                    {
                        HIS_TREATMENT treat = listTreatment.FirstOrDefault(o => o.ID == group.Key);
                        string log = String.Format("{0}: {1}(", SimpleEventKey.TREATMENT_CODE, treat.TREATMENT_CODE);
                        foreach (var item in group.ToList())
                        {
                            if (log.EndsWith("("))
                            {
                                log = String.Format("{0}{1}: {2}", log, SimpleEventKey.PREPARE_CODE, item.PREPARE_CODE);
                            }
                            else
                            {
                                log = String.Format("{0};{1}: {2}", log, SimpleEventKey.PREPARE_CODE, item.PREPARE_CODE);
                            }
                        }
                        log = log + ")";
                        datas.Add(log);
                    }

                    new EventLogGenerator(logEnum)
                    .PrepareDetailList(datas)
                    .Run();
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
