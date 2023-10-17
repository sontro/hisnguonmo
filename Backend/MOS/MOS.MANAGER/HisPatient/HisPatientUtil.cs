using HID.EFMODEL.DataModels;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatient
{
    class HisPatientUtil
    {
        internal static void SetInfoHid(HID_PERSON person, HIS_PATIENT patient)
        {
            if (person != null && patient != null)
            {
                person.PERSON_CODE = patient.PERSON_CODE;
                person.ADDRESS = patient.ADDRESS;
                person.BHYT_NUMBER = patient.TDL_HEIN_CARD_NUMBER;
                person.BLOOD_ABO_CODE = patient.BLOOD_ABO_CODE;
                person.BLOOD_RH_CODE = patient.BLOOD_RH_CODE;
                person.BORN_PROVINCE_CODE = patient.BORN_PROVINCE_CODE;
                person.BORN_PROVINCE_NAME = patient.BORN_PROVINCE_NAME;
                person.CAREER_NAME = patient.CAREER_NAME;
                person.CCCD_DATE = patient.CCCD_DATE;
                person.CCCD_NUMBER = patient.CCCD_NUMBER;
                person.CCCD_PLACE = patient.CCCD_PLACE;
                person.CMND_DATE = patient.CMND_DATE;
                person.CMND_NUMBER = patient.CMND_NUMBER;
                person.CMND_PLACE = patient.CMND_PLACE;
                person.COMMUNE_NAME = patient.COMMUNE_NAME;
                person.DISTRICT_NAME = patient.DISTRICT_NAME;
                person.DOB = patient.DOB;
                person.EMAIL = patient.EMAIL;
                person.ETHNIC_NAME = patient.ETHNIC_NAME;
                person.FIRST_NAME = patient.FIRST_NAME;
                person.GENDER_ID = patient.GENDER_ID;
                person.HOUSEHOLD_CODE = patient.HOUSEHOLD_CODE;
                person.HOUSEHOLD_RELATION_NAME = patient.HOUSEHOLD_RELATION_NAME;
                person.HT_ADDRESS = patient.HT_ADDRESS;
                person.HT_COMMUNE_NAME = patient.HT_COMMUNE_NAME;
                person.HT_DISTRICT_NAME = patient.HT_DISTRICT_NAME;
                person.HT_PROVINCE_NAME = patient.HT_PROVINCE_NAME;
                person.IS_HAS_NOT_DAY_DOB = patient.IS_HAS_NOT_DAY_DOB;
                person.LAST_NAME = patient.LAST_NAME;
                person.MOBILE = patient.MOBILE;
                person.MOTHER_NAME = patient.MOTHER_NAME;
                person.FATHER_NAME = patient.FATHER_NAME;
                person.NATIONAL_NAME = patient.NATIONAL_NAME;
                person.PHONE = patient.PHONE;
                person.PROVINCE_NAME = patient.PROVINCE_NAME;
                person.RELATIVE_ADDRESS = patient.RELATIVE_ADDRESS;
                person.RELATIVE_CMND_NUMBER = patient.RELATIVE_CMND_NUMBER;
                person.RELATIVE_MOBILE = patient.RELATIVE_MOBILE;
                person.RELATIVE_NAME = patient.RELATIVE_NAME;
                person.RELATIVE_PHONE = patient.RELATIVE_PHONE;
                person.RELATIVE_TYPE = patient.RELATIVE_TYPE;
                person.RELIGION_NAME = patient.RELIGION_NAME;

                HIS_BRANCH branch = null;
                if (patient.BRANCH_ID.HasValue)
                {
                    branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == patient.BRANCH_ID.Value);
                }
                if (branch == null)
                {
                    branch = HisBranchCFG.DATA.OrderBy(o => o.ID).FirstOrDefault();
                }
                if (branch != null)
                {
                    person.BRANCH_CODE = branch.HEIN_MEDI_ORG_CODE;
                    person.BRANCH_NAME = branch.BRANCH_NAME;

                }

            }
        }

        internal static bool CheckIsDiffForLis(HIS_PATIENT newPatient, HIS_PATIENT oldPatient)
        {
            if (newPatient != null && oldPatient != null)
            {
                return (
                    (newPatient.FIRST_NAME ?? "") != (oldPatient.FIRST_NAME ?? "")
                    || (newPatient.LAST_NAME ?? "") != (oldPatient.LAST_NAME ?? "")
                    || (newPatient.GENDER_ID != oldPatient.GENDER_ID)
                    || (newPatient.DOB != oldPatient.DOB)
                    );
            }
            else
            {
                return false;
            }
        }

        internal static void SetTdl(HIS_PATIENT patient, List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            if (patient != null && ptas != null && ptas.Count > 0)
            {
                HIS_PATIENT_TYPE_ALTER lastPta = ptas
                    .OrderByDescending(o => o.LOG_TIME)
                    .ThenByDescending(o => o.ID)
                    .FirstOrDefault();

                patient.TDL_HEIN_CARD_NUMBER = lastPta.HEIN_CARD_NUMBER;
            }
        }

        internal static void SetOwnBranhIds(HIS_PATIENT patient, long newBranchId)
        {
            try
            {
                List<long> listBranchId = new List<long>();
                if (!String.IsNullOrWhiteSpace(patient.OWN_BRANCH_IDS))
                {
                    if (IsExistsOwnBranchIds(patient.OWN_BRANCH_IDS, newBranchId))
                        return;
                    string[] arr = patient.OWN_BRANCH_IDS.Split(',');
                    if (arr != null)
                    {
                        foreach (var item in arr)
                        {
                            long o = 0;
                            if (long.TryParse(item, out o) && o > 0)
                            {
                                listBranchId.Add(o);
                            }
                        }
                    }
                }
                listBranchId.Add(newBranchId);
                patient.OWN_BRANCH_IDS = String.Join(",", listBranchId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal static void RemoveOwnBranhIds(HIS_PATIENT patient, long oldBranchId)
        {
            try
            {
                List<long> listBranchId = new List<long>();
                if (!String.IsNullOrWhiteSpace(patient.OWN_BRANCH_IDS))
                {
                    string[] arr = patient.OWN_BRANCH_IDS.Split(',');
                    if (arr != null)
                    {
                        foreach (var item in arr)
                        {
                            long o = 0;
                            if (long.TryParse(item, out o) && o > 0)
                            {
                                if (o != oldBranchId) listBranchId.Add(o);
                            }
                        }
                    }
                    if (listBranchId.Count > 0)
                        patient.OWN_BRANCH_IDS = String.Join(",", listBranchId);
                    else
                        patient.OWN_BRANCH_IDS = null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal static bool IsExistsOwnBranchIds(string ownBranchIds, long newBranchId)
        {
            bool valid = true;
            try
            {
                if (String.IsNullOrWhiteSpace(ownBranchIds))
                {
                    return false;
                }
                string ids = "," + ownBranchIds + ",";
                string id = "," + newBranchId + ",";
                if (!ids.Contains(id))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
