using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisAntigenMety;
using MOS.MANAGER.HisPatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDT.EFMODEL.DataModels;

namespace MOS.MANAGER.HisVaccination
{
    public class HisVaccinationUploadYdt : BusinessBase
    {
        private static bool IS_SENDING = false;

        public HisVaccinationUploadYdt()
            : base()
        {

        }

        public HisVaccinationUploadYdt(CommonParam param)
            : base(param)
        {

        }

        public static void Run()
        {
            try
            {
                if (!HisBranchCFG.IS_SYNC_HID)
                {
                    LogSystem.Info("He thong khong cau hinh dong bo HID");
                    return;
                }
                if (IS_SENDING)
                {
                    LogSystem.Warn("Tien trinh dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }
                IS_SENDING = true;

                DateTime dt = DateTime.Now.AddMinutes(-YdtCFG.YDT_VACCINATION__EXECUTE_TIME_DIFF);
                long time = Convert.ToInt64(dt.ToString("yyyyMMddHHmmss"));

                string sqlQuery = String.Format("SELECT * FROM V_HIS_VACCINATION V JOIN HIS_PATIENT P ON V.PATIENT_ID = P.ID WHERE V.IS_YDT_UPLOAD IS NULL AND V.EXECUTE_TIME IS NOT NULL AND EXECUTE_TIME <= {0} AND P.PERSON_CODE IS NOT NULL ORDER BY EXECUTE_TIME ASC FETCH FIRST 20 ROWS ONLY", time);

                List<V_HIS_VACCINATION> hisVaccinations = DAOWorker.SqlDAO.GetSql<V_HIS_VACCINATION>(sqlQuery);
                if (hisVaccinations != null && hisVaccinations.Count > 0)
                {
                    List<HIS_PATIENT> patients = new HisPatientGet().GetByIds(hisVaccinations.Select(s => s.PATIENT_ID).Distinct().ToList());
                    HisVaccinationMetyFilterQuery vacMetyFilter = new HisVaccinationMetyFilterQuery();
                    vacMetyFilter.VACCINATION_IDs = hisVaccinations.Select(o => o.ID).ToList();
                    List<HIS_VACCINATION_METY> vaccinationMety = new HisVaccinationMetyGet().Get(vacMetyFilter);
                    var medicineTypeIds = vaccinationMety.Select(s => s.MEDICINE_TYPE_ID).Distinct().ToList();

                    List<HIS_ANTIGEN_METY> listAntigenMety = new List<HIS_ANTIGEN_METY>();
                    if (medicineTypeIds != null && medicineTypeIds.Count > 0)
                    {
                        var skip = 0;
                        while (medicineTypeIds.Count - skip > 0)
                        {
                            var tmIds = medicineTypeIds.Skip(skip).Take(100).ToList();
                            skip += 100;
                            HisAntigenMetyFilterQuery antiMetyFilter = new HisAntigenMetyFilterQuery();
                            antiMetyFilter.MEDICINE_TYPE_IDs = tmIds;
                            var antiMety = new HisAntigenMetyGet().Get(antiMetyFilter);
                            if (antiMety != null && antiMety.Count > 0)
                            {
                                listAntigenMety.AddRange(antiMety);
                            }
                        }
                    }

                    foreach (var vaccination in hisVaccinations)
                    {
                        if (!vaccination.EXECUTE_TIME.HasValue) continue;

                        HIS_PATIENT patient = patients.FirstOrDefault(o => o.ID == vaccination.PATIENT_ID);
                        HIS_BRANCH branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == vaccination.BRANCH_ID);
                        HIS_VACCINATION_METY vaccinmationMety = vaccinationMety.FirstOrDefault(o => o.VACCINATION_ID == vaccination.ID);
                        HIS_MEDICINE_TYPE medicineType = vaccinmationMety != null ? HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == vaccinmationMety.MEDICINE_TYPE_ID) : null;

                        if (patient == null || branch == null || vaccinmationMety == null || medicineType == null)
                        {
                            continue;
                        }

                        System.Threading.Thread.Sleep(1000);

                        YDT_VACCINATION createData = new YDT_VACCINATION();
                        createData.AMOUNT = vaccinmationMety.AMOUNT;
                        createData.BRANCH_CODE = branch.HEIN_MEDI_ORG_CODE;
                        createData.BRANCH_NAME = branch.BRANCH_NAME;

                        createData.DEATH_TIME = vaccination.DEATH_TIME;
                        createData.DOB = vaccination.TDL_PATIENT_DOB;
                        createData.EXECUTE_DEPARTMENT_NAME = vaccination.EXECUTE_DEPARTMENT_NAME;
                        createData.EXECUTE_LOGINNAME = vaccination.EXECUTE_LOGINNAME;
                        createData.EXECUTE_ROOM_NAME = vaccination.EXECUTE_ROOM_NAME;
                        createData.EXECUTE_TIME = vaccination.EXECUTE_TIME.Value;
                        createData.EXECUTE_USERNAME = vaccination.EXECUTE_USERNAME;
                        createData.FIRST_NAME = vaccination.TDL_PATIENT_FIRST_NAME;
                        createData.FOLLOW_LOGINNAME = vaccination.FOLLOW_LOGINNAME;
                        createData.FOLLOW_USERNAME = vaccination.FOLLOW_USERNAME;
                        createData.GENDER_NAME = vaccination.TDL_PATIENT_GENDER_NAME;
                        createData.IS_HAS_NOT_DAY_DOB = vaccination.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                        createData.LAST_NAME = vaccination.TDL_PATIENT_LAST_NAME;
                        createData.PATHOLOGICAL_HISTORY = vaccination.PATHOLOGICAL_HISTORY;
                        createData.REACT_NAME = vaccination.VACCINATION_REACT_NAME;
                        createData.REACT_TIME = vaccination.REACT_TIME;

                        createData.PERSON_CODE = patient.PERSON_CODE;

                        createData.IS_TCMR = medicineType.IS_TCMR;
                        createData.MEDICINE_TYPE_NAME = medicineType.MEDICINE_TYPE_NAME;

                        var lstAntiMety = listAntigenMety.Where(o => o.MEDICINE_TYPE_ID == vaccinmationMety.MEDICINE_TYPE_ID).ToList();
                        if (lstAntiMety != null && lstAntiMety.Count > 0)
                        {
                            var antigens = Config.HisAntigenCFG.DATA.Where(o => lstAntiMety.Select(s => s.ANTIGEN_ID).Contains(o.ID)).ToList();
                            if (antigens != null && antigens.Count > 0)
                            {
                                createData.ANTIGEN_NAMES = string.Join(",", antigens.Select(s => s.ANTIGEN_NAME).ToList());
                            }
                        }

                        //createData.IS_UV

                        var rs = ApiConsumerManager.ApiConsumerStore.YdtConsumer.Post<ApiResultObject<bool>>("api/HisVaccination/Create", null, createData);
                        if (rs != null && rs.Success && rs.Data)
                        {
                            HIS_VACCINATION update = new HIS_VACCINATION();
                            AutoMapper.Mapper.CreateMap<HIS_VACCINATION, V_HIS_VACCINATION>();
                            AutoMapper.Mapper.Map<HIS_VACCINATION, V_HIS_VACCINATION>(update, vaccination);
                            update.IS_YDT_UPLOAD = 1;

                            if (!new HisVaccinationUpdate().Update(update))
                            {
                                LogSystem.Warn("Update IS_YDT_UPLOAD cho HIS_VITAMIN_A That bai");
                            }
                        }
                        else
                        {
                            LogSystem.Warn("Goi Ydt tao YdtVitaminA that bai");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            finally
            {
                IS_SENDING = false;
            }
        }
    }
}
