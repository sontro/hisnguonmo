using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatmentBorrow;
using MOS.MANAGER.CodeGenerator.HisTreatment;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentUpdate : BusinessBase
    {
        internal bool UpdateDataStoreId(HIS_TREATMENT data, ref HIS_TREATMENT resultData)
        {
            List<HIS_TREATMENT> rs = null;
            if (this.UpdateListDataStoreId(new List<HIS_TREATMENT>() { data }, ref rs))
            {
                resultData = rs != null ? rs[0] : null;
                return resultData != null;
            }
            return false;
        }

        internal bool UpdateListDataStoreId(List<HIS_TREATMENT> data, ref List<HIS_TREATMENT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                HisTreatmentBorrowCheck borrowChecker = new HisTreatmentBorrowCheck(param);
                List<long> listId = data.Select(o => o.ID).ToList();
                List<HIS_TREATMENT> listRaw = new List<HIS_TREATMENT>();
                valid = valid && checker.VerifyIds(listId, listRaw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    this.beforeUpdateHisTreatments.AddRange(Mapper.Map<List<HIS_TREATMENT>>(listRaw));

                    List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters = null;
                    if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION2)
                    {
                        patientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentIds(listId);
                    }

                    List<string> storeCodes = new List<string>();

                    foreach (HIS_TREATMENT raw in listRaw)
                    {
                        HIS_TREATMENT d = data.Where(o => o.ID == raw.ID).FirstOrDefault();
                        raw.DATA_STORE_ID = d.DATA_STORE_ID;

                        if (d.DATA_STORE_ID.HasValue)
                        {
                            long storeTime = Inventec.Common.DateTime.Get.Now().Value;
                            long seedTime = Inventec.Common.DateTime.Get.Now().Value;

                            if (HisTreatmentCFG.STORE_CODE_SEED_TIME_OPTION == (int)HisTreatmentCFG.StoreCodeSeedTimeOption.BY_STORE_TIME)
                            {
                                seedTime = storeTime;
                            }
                            else if (HisTreatmentCFG.STORE_CODE_SEED_TIME_OPTION == (int)HisTreatmentCFG.StoreCodeSeedTimeOption.BY_OUT_TIME)
                            {
                                if (!raw.OUT_TIME.HasValue)
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoChuaKetThucDieuTri);
                                    return false;
                                }
                                seedTime = raw.OUT_TIME.Value;
                            }
                            else if (HisTreatmentCFG.STORE_CODE_SEED_TIME_OPTION == (int)HisTreatmentCFG.StoreCodeSeedTimeOption.BY_IN_TIME)
                            {
                                if (!IsGreaterThanZero(raw.IN_TIME))
                                {
                                    LogSystem.Error("Thoi gian vao vien cua ho so dieu tri nho hon <= 0");
                                    return false;
                                }
                                seedTime = raw.IN_TIME;
                            }


                            if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION1)
                            {
                                //Neu ko co thong tin "ma luu tru" thi moi thuc hien cap lai "ma luu tru"
                                if (!IsNotNullOrEmpty(raw.STORE_CODE))
                                {
                                    raw.STORE_CODE = StoreCodeGenerator.GetNextOption1(seedTime);
                                }
                                raw.STORE_TIME = storeTime;
                            }
                            else if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION2)
                            {
                                HIS_PATIENT_TYPE_ALTER lastPatientTypeAlter = IsNotNullOrEmpty(patientTypeAlters) ? patientTypeAlters.Where(o => o.TREATMENT_ID == raw.ID).OrderByDescending(o => o.LOG_TIME).ThenByDescending(o => o.ID).FirstOrDefault() : null;
                                if (lastPatientTypeAlter != null)
                                {
                                    HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.ID == lastPatientTypeAlter.PATIENT_TYPE_ID).FirstOrDefault();
                                    //Neu ko co thong tin "ma luu tru" thi moi thuc hien cap lai "ma luu tru"
                                    if (!IsNotNullOrEmpty(raw.STORE_CODE))
                                    {
                                        raw.STORE_CODE = StoreCodeGenerator.GetNextOption2(patientType.PATIENT_TYPE_CODE, seedTime);
                                    }
                                    raw.STORE_TIME = storeTime;
                                }
                                else
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongCoThongTinDienDoiTuong);
                                    return false;
                                }
                            }
                            else if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION3)
                            {
                                if (!raw.END_DEPARTMENT_ID.HasValue)
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongCoThongTinKhoaKetThucDieuTri);
                                    return false;
                                }
                                HIS_DEPARTMENT endDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == raw.END_DEPARTMENT_ID.Value).FirstOrDefault();

                                if (endDepartment == null)
                                {
                                    throw new Exception("Ko tim thay thong tin khoa tuong ung voi id (co the do khoa bi khoa): " + raw.END_DEPARTMENT_ID.Value);
                                }

                                //Neu ko co thong tin "ma luu tru" thi moi thuc hien cap lai "ma luu tru"
                                if (!IsNotNullOrEmpty(raw.STORE_CODE))
                                {
                                    //can truyen vao storeCodes (chua cac ma luu tru duoc sinh ra nhung chua luu vao DB) de tranh sinh ra cac ma trung nhau
                                    raw.STORE_CODE = StoreCodeGenerator.GetNextOption3_4(endDepartment.DEPARTMENT_CODE, seedTime);
                                }
                                raw.STORE_TIME = storeTime;
                                storeCodes.Add(raw.STORE_CODE);
                            }
                            else if (HisTreatmentCFG.STORE_CODE_OPTION == (int)HisTreatmentCFG.StoreCodeOption.OPTION4)
                            {
                                V_HIS_DATA_STORE dataStore = HisDataStoreCFG.DATA.Where(o => o.ID == d.DATA_STORE_ID).FirstOrDefault();

                                if (dataStore == null)
                                {
                                    throw new Exception("Ko tim thay thong tin kho luu tru tuong ung voi ID (co the kho bi khoa) " + d.DATA_STORE_ID.Value);
                                }

                                //Neu ko co thong tin "ma luu tru" thi moi thuc hien cap lai "ma luu tru"
                                if (!IsNotNullOrEmpty(raw.STORE_CODE))
                                {
                                    //can truyen vao storeCodes (chua cac ma luu tru duoc sinh ra nhung chua luu vao DB) 
                                    //de tranh sinh ra cac ma trung nhau
                                    raw.STORE_CODE = StoreCodeGenerator.GetNextOption3_4(dataStore.DATA_STORE_CODE, seedTime);
                                }
                                raw.STORE_TIME = storeTime;
                                storeCodes.Add(raw.STORE_CODE);
                            }
                        }
                        else
                        {
                            if (!borrowChecker.CheckHasBorrow(raw.ID))
                            {
                                return false;
                            }

                            //Neu bat cau hinh "giữ 'mã lưu trữ'" thì không clear thông tin này
                            if (!HisTreatmentCFG.IS_KEEPING_STORE_CODE)
                            {
                                raw.STORE_CODE = null;
                            }
                            raw.STORE_TIME = null;
                        }
                    }

                    if (!DAOWorker.HisTreatmentDAO.UpdateList(listRaw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                    }

                    //Thong bao da cap nhat vao DB de clear khoi du lieu trong RAM
                    StoreCodeGenerator.FinishUpdateDB(storeCodes);

                    resultData = listRaw;
                    result = true;

                    new HisTreatment.Util.HisTreatmentUploadEmr().Run(resultData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
