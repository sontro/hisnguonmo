using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisHeinApproval;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentCheckPrescription : BusinessBase
    {
        internal HisTreatmentCheckPrescription()
            : base()
        {

        }

        internal HisTreatmentCheckPrescription(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        public bool HasNoPostponePrescriptionByDepartmentTran(long departmentId, long treatmentId)
        {
            try
            {
                //Neu co cau hinh bat buoc linh thuoc truoc khi ra khoi khoa thi kiem tra
                if (this.IsMustExportPresByDepartmentTran(treatmentId, departmentId))
                {
                    HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                    filter.REQ_DEPARTMENT_ID = departmentId;
                    filter.TDL_TREATMENT_ID = treatmentId;
                    filter.EXP_MEST_STT_IDs = new List<long>()
                        {
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT,
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE,
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST
                        };
                    filter.EXP_MEST_TYPE_IDs = new List<long>()
                        {
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT,
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK,
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT,
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM
                        };
                    List<HIS_EXP_MEST> prescriptions = new HisExpMestGet().Get(filter);
                    if (IsNotNullOrEmpty(prescriptions))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_ChuaLinhThuocKhongChoPhepRoiKhoa);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        public bool HasNoPostponePrescriptionByFinish(List<HIS_SERVICE_REQ> serviceReqs, HIS_PATIENT_TYPE_ALTER pta, HIS_DEPARTMENT_TRAN lastDt)
        {
            try
            {
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == lastDt.DEPARTMENT_ID);
                //Neu co cau hinh bat buoc linh thuoc truoc khi ra khoi khoa thi kiem tra
                if (IsNotNullOrEmpty(serviceReqs) && this.IsMustExportPresByFinish(pta, department))
                {
                    List<HIS_SERVICE_REQ> unfinishPres = serviceReqs
                            .Where(o => HisServiceReqTypeCFG.PRESCRIPTION_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID)
                                && (o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL || o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL))
                            .ToList();

                    if (IsNotNullOrEmpty(unfinishPres))
                    {
                        List<HIS_EXP_MEST> expMests = new HisExpMestGet().GetByServiceReqIds(unfinishPres.Select(o => o.ID).ToList());
                        //hien thi ma phieu xuat va y lenh tuong ung
                        //y lenh co thuoc ngoai kho khong doi trang thai khong hien thi vao.
                        if (IsNotNullOrEmpty(expMests))
                        {
                            List<string> expMestCodes = expMests.Select(o => o.EXP_MEST_CODE).ToList();
                            List<string> serviceReqCodes = expMests.Select(o => o.TDL_SERVICE_REQ_CODE).ToList();
                            string expMestStr = string.Join(",", expMestCodes);
                            string serviceReqStr = string.Join(",", serviceReqCodes);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacPhieuXuatChuaDuocLinh, expMestStr, serviceReqStr);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        private bool IsMustExportPresByDepartmentTran(long treatmentId, long departmentId)
        {
            //Neu ko co cau hinh "phai bat buoc xuat truoc khi chuyen khoa" thi tra ve false
            if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_ALL && HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_IN && HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_EX
                && HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_NN)
            {
                return false;
            }

            HIS_PATIENT_TYPE_ALTER pta = new HisPatientTypeAlterGet().GetLastByTreatmentId(treatmentId);
            HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == departmentId);

            if (pta == null || department == null)
            {
                Inventec.Common.Logging.LogSystem.Error("loi du lieu HIS_PATIENT_TYPE_ALTER hoac  HIS_DEPARTMENT null  treatmentId:" + treatmentId + "departmentId:" + departmentId);
                return false;
            }

            if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_ALL)
            {
                return pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU;
            }

            else if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_IN)
            {
                return pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU;
            }

            else if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_EX)
            {
                return (department.IS_EMERGENCY != Constant.IS_TRUE && pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU) || (department.IS_EMERGENCY != Constant.IS_TRUE && pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU);

            }

            else if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_NN)
            {
                return (department.IS_EMERGENCY != Constant.IS_TRUE && pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU);
            }

            return false;
        }

        /// <summary>
        /// Trong truong hop ket thuc dieu tri thi phai check ca 3 key: "chan khi roi khoa" va key "chan khi ket thuc dieu tri".
        /// Vi ket thuc dieu tri cung dong nghia voi viec roi khoi tat ca cac khoa
        /// </summary>
        /// <param name="pta"></param>
        /// <returns></returns>
        private bool IsMustExportPresByFinish(HIS_PATIENT_TYPE_ALTER pta, HIS_DEPARTMENT department)
        {
            //Neu ko co cau hinh "phai bat buoc xuat truoc khi chuyen khoa" va "bat buoc xuat truoc khi ket thuc dieu tri" thi tra ve false
            if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_ALL
                && HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_IN
                && HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_EX
                && HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_NN
                && HisPrescriptionCFG.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_ALL
                && HisPrescriptionCFG.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_IN
                && HisPrescriptionCFG.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_EX
                && HisPrescriptionCFG.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_NN
                && HisPrescriptionCFG.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT != HisPrescriptionCFG.AppliedOption.FOR_NT)
            {
                return false;
            }

            if (pta == null)
            {
                return false;
            }

            if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_ALL)
            {
                //Luu y: chi true thi moi return, false thi check tiep key tiep theo
                if (pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU)
                {
                    return true;
                }
            }
            else if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_IN)
            {
                //Luu y: chi true thi moi return, false thi check tiep key tiep theo
                if (pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                {
                    return true;
                }
            }
            else if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_EX)
            {
                //Luu y: chi true thi moi return, false thi check tiep key tiep theo
                if ((department.IS_EMERGENCY != Constant.IS_TRUE && pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU) || (department.IS_EMERGENCY != Constant.IS_TRUE && pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU))
                {
                    return true;
                }
            }
            else if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_NN)
            {
                //Luu y: chi true thi moi return, false thi check tiep key tiep theo
                if (department.IS_EMERGENCY != Constant.IS_TRUE && pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                {
                    return true;
                }
            }

            if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_ALL)
            {
                return pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU;
            }
            else if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_IN)
            {
                return pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU;
            }
            else if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_EX)
            {
                return pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU || pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY;
            }
            else if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_NN)
            {
                return (department.IS_EMERGENCY != Constant.IS_TRUE && pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU) || (department.IS_EMERGENCY != Constant.IS_TRUE && pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU);
            }
            else if (HisPrescriptionCFG.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT == HisPrescriptionCFG.AppliedOption.FOR_NT)
            {
                return (department.IS_EMERGENCY != Constant.IS_TRUE && pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU);
            }

            return false;
        }

        /// <summary>
        /// Kiem tra xem co cau hinh bat buoc phai duyet phieu thu hoi don thuoc moi cho ket thuc dieu tri khong
        /// </summary>
        /// <param name="treatment"></param>
        /// <returns></returns>
        internal bool IsMustApproveMobaPress(HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (!HisImpMestCFG.MUST_APPROVE_BEFORE_TREATMENT_FINISHED) return true;

                HisImpMestFilterQuery filter = new HisImpMestFilterQuery();
                filter.TDL_TREATMENT_ID = treatment.ID;
                filter.IMP_MEST_TYPE_IDs = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL,
                    IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL
                };
                filter.IMP_MEST_STT_IDs = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST,
                    IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REJECT,
                    IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__DRAFT
                };

                List<HIS_IMP_MEST> impMests = new HisImpMestGet().Get(filter);

                if (IsNotNullOrEmpty(impMests))
                {
                    var Groups = impMests.GroupBy(g => g.REQ_DEPARTMENT_ID ?? 0);
                    List<string> mess = new List<string>();
                    foreach (var g in Groups)
                    {
                        string codes = String.Join(",", g.Select(s => s.IMP_MEST_CODE).ToList());
                        HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == g.Key);
                        string name = "";
                        if (department != null)
                        {
                            name = department.DEPARTMENT_NAME;
                        }
                        mess.Add(String.Format("{0}({1})", name, codes));
                    }
                    string m = String.Join(", ", mess);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_CacPhieuThuHoiSauChuDuocDuyet, m);
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
