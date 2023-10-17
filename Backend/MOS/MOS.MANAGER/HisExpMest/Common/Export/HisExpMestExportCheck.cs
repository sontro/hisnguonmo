using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.UTILITY;
using MOS.MANAGER.HisAntibioticRequest;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean;

namespace MOS.MANAGER.HisExpMest.Common.Export
{
    partial class HisExpMestExportCheck : BusinessBase
    {
        internal HisExpMestExportCheck()
            : base()
        {

        }

        internal HisExpMestExportCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool IsUnlockTreatment(HIS_EXP_MEST expMest, HIS_TREATMENT treatment)
        {
            try
            {
                bool valid = true;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM && expMest.TDL_TREATMENT_ID.HasValue)
                {
                    valid = valid && treatmentChecker.IsUnLock(treatment);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                    valid = valid && treatmentChecker.IsUnpause(treatment);
                    valid = valid && treatmentChecker.IsUnLockHein(treatment);
                }
                return valid;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsAllowed(HisExpMestExportSDO sdo, bool isAuto, ref HIS_EXP_MEST expMest)
        {
            try
            {
                HIS_EXP_MEST exp = new HisExpMestGet().GetById(sdo.ExpMestId);
                if (exp == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Exp_mest_id ko hop le: " + sdo.ExpMestId);
                    return false;
                }
                if (exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Don dieu tri phai goi api khac");
                    return false;
                }

                if (exp.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuChuaDuocDuyetHoacDaThucXuat);
                    return false;
                }

                if (exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("Loai phieu xuat la phieu tong hop (phieu linh). Khong duoc thuc hien chuc nang nay");
                    return false;
                }

                V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == exp.MEDI_STOCK_ID).FirstOrDefault();

                if (mediStock == null || mediStock.IS_ACTIVE != MOS.UTILITY.Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa);
                    return false;
                }

                //Neu ko phai tu dong xuat thi kiem tra xem co dang lam viec tai kho xuat khong
                if (!isAuto)
                {
                    WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(sdo.ReqRoomId);
                    if (workPlace == null || workPlace.MediStockId != exp.MEDI_STOCK_ID)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                        return false;
                    }
                }

                expMest = exp;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal bool IsValidAntibioticRequest(List<HIS_EXP_MEST> expMests)
        {
            if (HisExpMestCFG.DISALLOW_TO_EXPORT_UNAPPROVED_USING_REQUEST && IsNotNullOrEmpty(expMests))
            {
                List<HIS_EXP_MEST> usingRequestApprovedRequireds = expMests.Where(o => o.IS_USING_APPROVAL_REQUIRED == Constant.IS_TRUE && o.SERVICE_REQ_ID.HasValue).ToList();
                List<HIS_EXP_MEST> unRequests = IsNotNullOrEmpty(usingRequestApprovedRequireds) ? usingRequestApprovedRequireds.Where(o => !o.ANTIBIOTIC_REQUEST_ID.HasValue).ToList() : null;

                if (IsNotNullOrEmpty(unRequests))
                {
                    List<string> codes = unRequests.Select(o => o.EXP_MEST_CODE).ToList();
                    string codeStr = string.Join(",", codes);

                    List<string> serviceReqCodes = unRequests.Select(o => o.TDL_SERVICE_REQ_CODE).ToList();
                    string serviceReqCodeStr = string.Join(",", serviceReqCodes);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_ChuaTaoPhieuYeuCauSuDungKhangSinh, codeStr, serviceReqCodeStr);
                    return false;
                }

                List<long> antibioticRequestIds = IsNotNullOrEmpty(usingRequestApprovedRequireds) ? usingRequestApprovedRequireds.Where(o => o.ANTIBIOTIC_REQUEST_ID.HasValue).Select(o => o.ANTIBIOTIC_REQUEST_ID.Value).ToList() : null;

                if (IsNotNullOrEmpty(antibioticRequestIds))
                {
                    HisAntibioticRequestFilterQuery filter = new HisAntibioticRequestFilterQuery();
                    filter.IDs = antibioticRequestIds;
                    List<HIS_ANTIBIOTIC_REQUEST> antibioticRequests = new HisAntibioticRequestGet().Get(filter);

                    List<long> unapprovalIds = IsNotNullOrEmpty(antibioticRequests) ? antibioticRequests.Where(o => o.ANTIBIOTIC_REQUEST_STT != IMSys.DbConfig.HIS_RS.HIS_ANTIBIOTIC_REQUEST_STT.APPROVED).Select(o => o.ID).ToList() : null;

                    List<HIS_EXP_MEST> unapprovals = IsNotNullOrEmpty(unapprovalIds) ? usingRequestApprovedRequireds.Where(o => o.ANTIBIOTIC_REQUEST_ID.HasValue && unapprovalIds.Contains(o.ANTIBIOTIC_REQUEST_ID.Value)).ToList() : null;

                    if (IsNotNullOrEmpty(unapprovals))
                    {
                        List<string> codes = unapprovals.Select(o => o.EXP_MEST_CODE).ToList();
                        string codeStr = string.Join(",", codes);

                        List<string> serviceReqCodes = unapprovals.Select(o => o.TDL_SERVICE_REQ_CODE).ToList();
                        string serviceReqCodeStr = string.Join(",", serviceReqCodes);

                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_ChuaDuyetPhieuYeuCauSuDungKhangSinh, codeStr, serviceReqCodeStr);
                        return false;
                    }
                    
                }
            }
            return true;
        }

        internal bool CheckUnpaidOutPatientPrescription(HIS_TREATMENT treatment, HIS_EXP_MEST raw, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            try
            {
                List<long> expMestTypeIds = new List<long>
                {
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK
                };

                //Chi check voi cac loai la don thuoc
                if (!expMestTypeIds.Contains(raw.EXP_MEST_TYPE_ID))
                {
                    return true;
                }

                //Neu la don phong kham thi kiem tra xem con no tien vien phi ko
                if ((HisExpMestCFG.CHECK_UNPAID_PRES_OPTION == HisExpMestCFG.CheckUnpaidPresOption.BY_PRES_TYPE && raw != null && raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK)
                    || (HisExpMestCFG.CHECK_UNPAID_PRES_OPTION == HisExpMestCFG.CheckUnpaidPresOption.BY_TREATMENT_TYPE && treatment != null && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM))
                {
                    List<long> notCheckPatientTypeIds = HisPatientTypeCFG
                        .DATA.Where(o => o.IS_NOT_CHECK_FEE_WHEN_EXP_PRES == Constant.IS_TRUE)
                        .Select(o => o.ID).ToList();

                    //Lay ra xem co bao nhieu thuoc/vat tu thuoc phieu xuat ko phai la hao phi, 
                    //va co doi tuong thanh toan ko phai la doi tuong co check "ko kiem tra vien phi" khi xuat thuoc

                    int countNotAllow = !IsNotNullOrEmpty(expMestMedicines) ? 0 : expMestMedicines.Where(o => o.IS_EXPEND != MOS.UTILITY.Constant.IS_TRUE && (notCheckPatientTypeIds == null || !o.PATIENT_TYPE_ID.HasValue || !notCheckPatientTypeIds.Contains(o.PATIENT_TYPE_ID.Value))).Count();
                    countNotAllow = !IsNotNullOrEmpty(expMestMaterials) ? countNotAllow + 0 : countNotAllow + expMestMaterials.Where(o => o.IS_EXPEND != MOS.UTILITY.Constant.IS_TRUE && (notCheckPatientTypeIds == null || !o.PATIENT_TYPE_ID.HasValue || !notCheckPatientTypeIds.Contains(o.PATIENT_TYPE_ID.Value))).Count();

                    //Neu co thi thuc hien kiem tra
                    if (countNotAllow > 0)
                    {
                        V_HIS_TREATMENT_FEE_1 treatmentFee = new HisTreatmentGet().GetFeeView1ById(raw.TDL_TREATMENT_ID.Value);

                        if (treatmentFee.IS_ACTIVE == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_HoSoDieuTriChuaDuocDuyetKhoaTaiChinhKhongChoThucXuat, treatmentFee.TREATMENT_CODE);
                            return false;
                        }

                        decimal? unpaid = new HisTreatmentGet().GetUnpaid(treatmentFee);
                        if (!unpaid.HasValue)
                        {
                            LogSystem.Warn("Loi du lieu");
                            return false;
                        }

                        //tranh truong hop lam tron den 4 so sau phan thap phan dan den nguoi dung ko the thanh toan het toan bo chi phi
                        if (unpaid.Value > Constant.PRICE_DIFFERENCE)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_HoSoDieuTriChuaDongDuTienKhongChoThucXuat, treatmentFee.TREATMENT_CODE);
                            LogSystem.Warn("Ho so dieu tri chua dong du tien, khong cho thuc xuat" + LogUtil.TraceData("treatmentFee", treatmentFee));
                            return false;
                        }

                        List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqId(raw.SERVICE_REQ_ID.Value);
                        hisSereServs = hisSereServs != null ? hisSereServs.Where(o => o.AMOUNT > 0 && o.VIR_TOTAL_PATIENT_PRICE > 0).ToList() : null;
                        if (IsNotNullOrEmpty(hisSereServs))
                        {
                            HisSereServCheck ssChecker = new HisSereServCheck(param);
                            if (!ssChecker.HasBillOrDepositAndNoRepay(hisSereServs))
                            {
                                LogSystem.Warn("Don chua duoc thanh toan hoac chua tam ung hoac da tam ung ma da hoan ung");
                                return false;
                            }
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

        internal bool HasUnexport(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_EXP_MEST_BLOOD> bloods)
        {
            try
            {
                if (!IsNotNullOrEmpty(materials) && !IsNotNullOrEmpty(medicines))
                {
                    medicines = new HisExpMestMedicineGet().GetUnexportByExpMestId(expMest.ID);
                    materials = new HisExpMestMaterialGet().GetUnexportByExpMestId(expMest.ID);
                }
                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN)
                    bloods = new HisExpMestBloodGet().GetUnexportByExpMestId(expMest.ID);

                if (!IsNotNullOrEmpty(medicines) && !IsNotNullOrEmpty(materials) && !IsNotNullOrEmpty(bloods))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongCoThongTinDuyet);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool CheckBillForSale(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN)
                {
                    return true;
                }
                if (!HisExpMestCFG.EXPORT_SALE_MUST_BILL)
                {
                    return true;
                }

                if (!expMest.BILL_ID.HasValue && !expMest.DEBT_ID.HasValue)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatBanChuaThanhToan, expMest.EXP_MEST_CODE);
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

        internal bool CheckValidData(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials)
        {
            bool valid = true;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> listMedicineErr = new List<HIS_EXP_MEST_MEDICINE>();
                List<HIS_EXP_MEST_MATERIAL> listMaterialErr = new List<HIS_EXP_MEST_MATERIAL>();

                List<long> expMestMedicineIds = IsNotNullOrEmpty(medicines) ? medicines.Select(o => o.ID).ToList() : new List<long>();
                List<long> expMestMaterialIds = IsNotNullOrEmpty(materials) ? materials.Select(o => o.ID).ToList() : new List<long>();
                List<HIS_MEDICINE_BEAN> medicineBeans = (new HisMedicineBeanGet().GetByExpMestMedicineIds(expMestMedicineIds)) ?? new List<HIS_MEDICINE_BEAN>();
                List<HIS_MATERIAL_BEAN> materialBeans = (new HisMaterialBeanGet().GetByExpMestMaterialIds(expMestMaterialIds)) ?? new List<HIS_MATERIAL_BEAN>();
                if (IsNotNullOrEmpty(medicines))
                {
                    foreach (var medicine in medicines)
                    {
                        if (medicine == null)
                            continue;
                        if (medicine.AMOUNT != medicineBeans.Where(o => o.EXP_MEST_MEDICINE_ID == medicine.ID).Sum(o => o.AMOUNT))
                            listMedicineErr.Add(medicine);
                    }
                }
                if (IsNotNullOrEmpty(materials))
                {
                    foreach (var material in materials)
                    {
                        if (material == null)
                            continue;
                        if (material.AMOUNT != materialBeans.Where(o => o.EXP_MEST_MATERIAL_ID == material.ID).Sum(o => o.AMOUNT))
                            listMaterialErr.Add(material);
                    }
                }
                if (listMedicineErr.Count > 0 || listMaterialErr.Count > 0)
                {
                    valid = false;
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.LoiDuLieuKhongHopLe);
                    Inventec.Common.Logging.LogSystem.Warn("Loi du lieu,Khong cho phep xuat phieu " + expMest.EXP_MEST_CODE);
                    if (listMedicineErr.Count > 0)
                    {
                        Inventec.Common.Logging.LogSystem.Warn("__Thuoc loi: " + Inventec.Common.Logging.LogUtil.TraceData("listMedicineErr", listMedicineErr));
                    }
                    if (listMaterialErr.Count > 0)
                    {
                        Inventec.Common.Logging.LogSystem.Warn("__Vat tu loi: " + Inventec.Common.Logging.LogUtil.TraceData("listMaterialErr", listMaterialErr));
                    }
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
