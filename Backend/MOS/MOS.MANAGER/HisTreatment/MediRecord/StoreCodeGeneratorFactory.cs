using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.MANAGER.CodeGenerator.HisMediRecord;
using MOS.MANAGER.HisPatientTypeAlter;
using Inventec.Common.Logging;

namespace MOS.MANAGER.HisTreatment.MediRecord
{
    public class StoreCodeGeneratorFactory : BusinessBase
    {
        internal StoreCodeGeneratorFactory()
            : base()
        {

        }

        internal StoreCodeGeneratorFactory(CommonParam param)
            : base(param)
        {

        }

        internal StoreCodeGeneratorFactory(CommonParam param, List<string> keepedStoreCodes)
            : base(param)
        {
            MediRecordStoreCodeGenerator.AddKeepedStoreCode(keepedStoreCodes);
        }

        public string GenerateStoreCode(long storeTime, long? dataStoreId, HIS_TREATMENT treatment, ref long? seedTime, ref List<string> storeCodes)
        {
            string result = null;
            if (!String.IsNullOrWhiteSpace(treatment.STORE_CODE))
            {
                result = treatment.STORE_CODE;
            }
            else
            {
                seedTime = storeTime;
                
                if (HisTreatmentCFG.STORE_CODE_SEED_TIME_OPTION == (int)HisTreatmentCFG.StoreCodeSeedTimeOption.BY_OUT_TIME)
                {
                    if (!treatment.OUT_TIME.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoChuaKetThucDieuTri);
                        throw new Exception("StoreCode Generate by out_time but treatment is not finish");
                    }
                    seedTime = treatment.OUT_TIME.Value;
                }
                else if (HisTreatmentCFG.STORE_CODE_SEED_TIME_OPTION == (int)HisTreatmentCFG.StoreCodeSeedTimeOption.BY_IN_TIME)
                {
                    if (!IsGreaterThanZero(treatment.IN_TIME))
                    {
                        throw new Exception("StoreCode Generate by in_time <= 0");
                    }
                    seedTime = treatment.IN_TIME;
                }

                if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION1)
                {
                    result = MediRecordStoreCodeGenerator.GetNextOption1(seedTime.Value);
                }
                else if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION2)
                {
                    HIS_PATIENT_TYPE_ALTER lastPatientTypeAlter = new HisPatientTypeAlterGet().GetLastByTreatmentId(treatment.ID);

                    if (lastPatientTypeAlter != null)
                    {
                        HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.ID == lastPatientTypeAlter.PATIENT_TYPE_ID).FirstOrDefault();
                        result = MediRecordStoreCodeGenerator.GetNextOption2(patientType.PATIENT_TYPE_CODE, seedTime.Value);
                    }
                    else
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongCoThongTinDienDoiTuong);
                        throw new Exception("Generate StoreCode by PatientTypeCode but not exits PatientTypeAlter");
                    }
                }
                else if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION3)
                {
                    if (!treatment.END_DEPARTMENT_ID.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongCoThongTinKhoaKetThucDieuTri);
                        throw new Exception("Generate StoreCode by EndDepartmentId but EndDepartmentId is null");
                    }
                    HIS_DEPARTMENT endDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == treatment.END_DEPARTMENT_ID.Value).FirstOrDefault();
                    if (endDepartment == null)
                    {
                        throw new Exception("Ko tim thay thong tin khoa tuong ung voi id (co the do khoa bi khoa): " + treatment.END_DEPARTMENT_ID.Value);
                    }

                    result = MediRecordStoreCodeGenerator.GetNextOption3(endDepartment.DEPARTMENT_CODE, seedTime.Value);
                    storeCodes.Add(result);
                }
                else if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION4)
                {
                    if (!dataStoreId.HasValue)
                    {
                        throw new Exception("Chua co thong tin tu luu tru (data_store_id)");
                    }
                    result = MediRecordStoreCodeGenerator.GetNextOption4(dataStoreId.Value, seedTime.Value);
                    storeCodes.Add(result);
                }
                else if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION5)
                {
                    if (!dataStoreId.HasValue)
                    {
                        throw new Exception("Chua co thong tin tu luu tru (data_store_id)");
                    }
                    result = MediRecordStoreCodeGenerator.GetNextOption5(dataStoreId.Value, seedTime.Value);
                    storeCodes.Add(result);
                }
                else if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION6
                    || HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION1)
                {
                    //ko xu ly j vi sinh theo trigger trong DB
                    return result;
                }
            }

            return result;
        }

        public void FinishUpdateDB(List<string> storeCodes)
        {
            if (!MediRecordStoreCodeGenerator.FinishUpdateDB(storeCodes))
            {
                LogSystem.Warn("MediRecordStoreCodeGenerator.FinishUpdateDB that bai");
            }
        }
    }
}
