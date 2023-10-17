using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Update.PayslipInfo
{
    class HisSereServUpdatePayslipInfoCheck : BusinessBase
    {
        internal HisSereServUpdatePayslipInfoCheck()
            : base()
        {
        }

        internal HisSereServUpdatePayslipInfoCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsValidData(HisSereServPayslipSDO data, bool isAllowUpdateNoExecutePres, ref List<HIS_SERE_SERV> allSereServs, ref List<HIS_SERE_SERV> affectList, ref List<HIS_SERVICE_REQ> affectServiceReqs)
        {
            bool valid = true;
            try
            {
                if (data == null || !IsNotNullOrEmpty(data.SereServs) || data.TreatmentId <= 0)
                {
                    LogSystem.Warn("data null hoac thieu SereServs (hoac TreatmentId)");
                    return false;
                }

                allSereServs = new HisSereServGet().GetByTreatmentId(data.TreatmentId);
                affectList = allSereServs != null ? allSereServs.Where(o => data.SereServs.Exists(t => t.ID == o.ID)).ToList() : null;

                if (!IsNotNullOrEmpty(affectList) || affectList.Count != data.SereServs.Count)
                {
                    LogSystem.Warn("Ton tai sere_serv_id ko thuoc treatment hoac co sere_serv_id trung nhau");
                    return false;
                }

                if (!isAllowUpdateNoExecutePres && data.Field == UpdateField.IS_NO_EXECUTE
                    && affectList.Exists(t => t.MEDICINE_ID.HasValue || t.MATERIAL_ID.HasValue || t.BLOOD_ID.HasValue))
                {
                    LogSystem.Warn("Ko cho phep cap nhat truong 'is_no_execute' doi voi thuoc/vat tu/mau");
                    return false;
                }

                if (data.Field == UpdateField.STENT_ORDER)
                {
                    List<string> notStents = data.SereServs
                        .Where(t => t.STENT_ORDER.HasValue
                            && !HisMaterialTypeCFG.IsStentByServiceId(t.SERVICE_ID)).Select(o => o.TDL_SERVICE_NAME).ToList();
                    if (IsNotNullOrEmpty(notStents))
                    {
                        string stentStr = string.Join(",", notStents);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_KhongPhaiLaStent, stentStr);
                        return false;
                    }
                }

                if (data.Field == UpdateField.EQUIPMENT_SET_ORDER__AND__EQUIPMENT_SET_ID)
                {
                    List<HIS_SERE_SERV> invalids = data.SereServs
                        .Where(t => t.EQUIPMENT_SET_ORDER.HasValue && !t.EQUIPMENT_SET_ID.HasValue).ToList();
                    if (IsNotNullOrEmpty(invalids))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Co thong tin EQUIPMENT_SET_ORDER nhung ko co thong tin EQUIPMENT_SET_ID");
                        return false;
                    }
                }

                if (data.Field == UpdateField.EXPEND_TYPE_ID)
                {
                    List<HIS_SERE_SERV> invalids = data.SereServs
                        .Where(t => t.EXPEND_TYPE_ID.HasValue && t.EXPEND_TYPE_ID.Value != 1).ToList();
                    if (IsNotNullOrEmpty(invalids))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Co thong tin EXPEND_TYPE_ID ko duoc khac 1 hoac null");
                        return false;
                    }
                }

                if (data.Field == UpdateField.PATIENT_TYPE_ID)
                {
                    List<HIS_SERE_SERV> raws = affectList;

                    //Neu cau hinh ODD_MEDICINE_MANAGEMENT_OPTION != gia tri 4 thi ko cho phep doi DTTT cua thuoc duoc danh dau "ko phai bs ke"
                    List<HIS_SERE_SERV> invalids = HisMediStockCFG.ODD_MEDICINE_MANAGEMENT_OPTION != HisMediStockCFG.OddManagementOption.MANAGEMENT_PATIENT ? data.SereServs.Where(o => (o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE) && raws.Any(a => a.ID == o.ID && a.IS_NOT_PRES.HasValue && a.IS_NOT_PRES.Value == Constant.IS_TRUE)).ToList() : null;
                    if (IsNotNullOrEmpty(invalids))
                    {
                        HIS_PATIENT_TYPE paty = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE);
                        string patyName = paty != null ? paty.PATIENT_TYPE_NAME : "";
                        string names = String.Join(",", invalids.Select(s => s.TDL_SERVICE_NAME).Distinct().ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_ThuocVatTuSauKhongPhaiBacSyKeKhongChoPhepSuaDTTT, patyName, names);
                        return false;
                    }
                }

                if (data.Field == UpdateField.PACKAGE_PRICE || data.Field == UpdateField.USER_PRICE)
                {
                    //Ko cho phep chi dinh gia voi doi tuong thanh toan (hoac phu thu) la BHYT
                    List<string> invalidPatientTypes = data.SereServs
                        .Where(o => (o.PACKAGE_PRICE.HasValue || o.USER_PRICE.HasValue)
                            && (o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || o.PRIMARY_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                        .Select(o => o.TDL_SERVICE_NAME).ToList();

                    if (IsNotNullOrEmpty(invalidPatientTypes))
                    {
                        string codeStr = string.Join(",", invalidPatientTypes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuChiDinhGiaKhongChoPhepSuDungDoiTuongThanhToanBhyt, codeStr);
                        return false;
                    }

                    if (data.Field == UpdateField.PACKAGE_PRICE)
                    {
                        //Ko cho phep chi dinh gia goi neu ko co thong tin goi
                        List<string> invalidPackages = data.SereServs
                            .Where(o => o.PACKAGE_PRICE.HasValue && !o.PACKAGE_ID.HasValue)
                            .Select(o => o.TDL_SERVICE_NAME).ToList();

                        if (IsNotNullOrEmpty(invalidPackages))
                        {
                            string codeStr = string.Join(",", invalidPackages);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuKhongCoThongTinGoi, codeStr);
                            return false;
                        }
                    }
                    else if (data.Field == UpdateField.USER_PRICE)
                    {
                        //Ko cho phep chi dinh gia voi loai la phau thuat
                        List<string> invalidSurgeries = data.SereServs
                            .Where(o => o.USER_PRICE.HasValue && o.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT)
                            .Select(o => o.TDL_SERVICE_NAME).ToList();

                        if (IsNotNullOrEmpty(invalidSurgeries))
                        {
                            string codeStr = string.Join(",", invalidSurgeries);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuKhongPhaiLaPhauThuat, codeStr);
                            return false;
                        }
                    }
                }

                affectServiceReqs = new HisServiceReqGet().GetByIds(affectList.Select(o => o.SERVICE_REQ_ID.Value).ToList());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool ValidateInCaseOfPackageService(UpdateField field, List<HIS_SERE_SERV> updates, List<HIS_SERE_SERV> olds)
        {
            try
            {
                List<UpdateField> notAllowed = new List<UpdateField>(){UpdateField.IS_EXPEND,
                        UpdateField.IS_NO_EXECUTE,
                        UpdateField.PATIENT_TYPE_ID,
                        UpdateField.IS_EXPEND,
                        UpdateField.PACKAGE_PRICE
                    };

                if (IsNotNullOrEmpty(updates) && notAllowed.Contains(field))
                {
                    List<string> inPackages = new List<string>();
                    foreach (HIS_SERE_SERV s in updates)
                    {
                        HIS_SERE_SERV old = olds != null ? olds.Where(o => o.ID == s.ID).FirstOrDefault() : null;
                        if (old == null
                            || (field == UpdateField.IS_NO_EXECUTE && s.IS_NO_EXECUTE != old.IS_NO_EXECUTE)
                            || (field == UpdateField.PATIENT_TYPE_ID && s.PATIENT_TYPE_ID != old.PATIENT_TYPE_ID)
                            || (field == UpdateField.IS_EXPEND && s.IS_NO_EXECUTE != old.IS_EXPEND)
                            || (field == UpdateField.PACKAGE_PRICE && s.IS_NO_EXECUTE != old.PACKAGE_PRICE)
                            )
                        {
                            if (old.PACKAGE_ID.HasValue && (HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS == null || !HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS.Contains(old.PACKAGE_ID.Value)))
                            {
                                inPackages.Add(old.TDL_SERVICE_NAME);
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(inPackages))
                    {
                        string inPackageNameStr = string.Join(",", inPackages);

                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_CacDichVuTrongGoi, inPackageNameStr);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal bool ValidateInCaseOfUpdateExecute(HisSereServCheck checker, UpdateField field, List<HIS_SERE_SERV> changes, List<HIS_SERE_SERV> olds)
        {
            try
            {
                if (IsNotNullOrEmpty(changes) && field == UpdateField.IS_NO_EXECUTE)
                {
                    List<HIS_SERE_SERV> toNoExecutes = new List<HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> toExecutes = new List<HIS_SERE_SERV>();
                    foreach (HIS_SERE_SERV t in changes)
                    {
                        HIS_SERE_SERV old = olds.Where(o => o.ID == t.ID).FirstOrDefault();
                        if (old != null && t.IS_NO_EXECUTE == Constant.IS_TRUE && old.IS_NO_EXECUTE != Constant.IS_TRUE)
                        {
                            toNoExecutes.Add(old);
                        }
                        else if (old != null && t.IS_NO_EXECUTE != Constant.IS_TRUE && old.IS_NO_EXECUTE == Constant.IS_TRUE)
                        {
                            toExecutes.Add(old);
                        }

                    }

                    //Neu cap nhat trang thai tu "thuc hien" ==> "khong thuc hien" thi can kiem tra, chieu nguoc lai thi ko can kiem tra
                    return checker.IsAllowUpdateToNoExecute(toNoExecutes)

                    //Neu cap nhat trang thai tu "ko thuc hien" ==> "thuc hien" thi can kiem tra, chieu nguoc lai thi ko can kiem tra
                        && this.IsUnNotTakenPres(toExecutes);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal bool ValidInCaseOfUpdateExpend(UpdateField field, List<HIS_SERE_SERV> allExists, HIS_TREATMENT treatment)
        {
            bool valid = true;
            if (field == UpdateField.IS_EXPEND
                && allExists.Exists(e => e.AMOUNT > 0 && e.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK && (!e.IS_EXPEND.HasValue || e.IS_EXPEND.Value != Constant.IS_TRUE)))
            {
                HIS_PATIENT_TYPE_ALTER lastPt = new HisPatientTypeAlterGet().GetLastByTreatmentId(treatment.ID);
                if (lastPt.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                    || lastPt.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_BenNhanDienDieuTriKhongDuocPhepSuaThongTinHaoPhiDonKham);
                    return false;
                }
            }
            return valid;
        }

        internal bool ValidateInCaseOfUpdatePatientType(UpdateField field, List<HIS_SERE_SERV> updates, List<HIS_SERE_SERV> olds)
        {
            try
            {
                if (IsNotNullOrEmpty(updates) && field == UpdateField.PATIENT_TYPE_ID)
                {
                    List<HIS_SERE_SERV> toBhyts = updates.Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();

                    if (IsNotNullOrEmpty(toBhyts) && IsNotNullOrEmpty(olds))
                    {
                        List<HIS_SERE_SERV> fromNotBhyts = olds.Where(o => toBhyts.Exists(t => t.ID == o.ID) && o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();
                    }

                    //if (IsNotNullOrEmpty(inPackages))
                    //{
                    //    string inPackageNameStr = string.Join(",", inPackages);

                    //    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_CacDichVuTrongGoi, inPackageNameStr);
                    //    return false;
                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// Kiem tra xem co phieu xuat nao bi danh dau "ko lay" ko
        /// </summary>
        /// <param name="toExecutes"></param>
        /// <returns></returns>
        private bool IsUnNotTakenPres(List<HIS_SERE_SERV> toExecutes)
        {
            //Chi kiem tra voi cac du lieu don thuoc/vat tu/mau
            List<long> serviceReqIds = toExecutes != null ? toExecutes
                .Where(o => o.MEDICINE_ID.HasValue || o.BLOOD_ID.HasValue || o.MATERIAL_ID.HasValue)
                .Where(o => o.SERVICE_REQ_ID.HasValue && o.IS_DELETE == Constant.IS_FALSE)
                .Select(o => o.SERVICE_REQ_ID.Value).ToList() : null;

            if (IsNotNullOrEmpty(serviceReqIds))
            {
                //Kiem tra xem co phieu xuat nao da bi danh dau 'benh nhan ko lay' ko
                //neu co thi ko cho bo check "ko thuc hien"
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                filter.IS_NOT_TAKEN = true;
                List<HIS_EXP_MEST> expMests = new HisExpMestGet().Get(filter);
                List<string> expMestCodes = expMests != null ? expMests.Select(o => o.EXP_MEST_CODE).ToList() : null;

                if (IsNotNullOrEmpty(expMestCodes))
                {
                    string expMestCodeStr = string.Join(",", expMestCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_PhieuXuatDaDanhDauKhongLay, expMestCodeStr);
                    return false;
                }
            }
            return true;
        }

        internal bool IsAllowed(List<HIS_SERE_SERV> allExists)
        {
            if (HisServiceReqCFG.DO_NOT_ALLOW_TO_EDIT_IF_PAID && IsNotNullOrEmpty(allExists))
            {
                HisSereServCheck checker = new HisSereServCheck(param);
                List<long> ssIds = allExists.Select(o => o.ID).ToList();
                if (!checker.HasNoBill(allExists)
                    || !checker.HasNoInvoice(allExists)
                    || !checker.HasNoDebt(allExists)
                    || !checker.HasNoDeposit(ssIds, true))
                {
                    return false;
                }
            }
            return true;
        }

        internal bool VerifyRequireField(HisSereServPayslipSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
                if (!IsNotNullOrEmpty(data.SereServs)) throw new ArgumentNullException("data.SereServs");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }


        internal bool ValidateNoExpendAllowed(HisSereServPayslipSDO data, List<HIS_SERE_SERV> allExists)
        {
            try
            {
                if (IsNotNullOrEmpty(data.SereServs) && data.Field == UpdateField.IS_EXPEND)
                {
                    List<long> ssIds = data.SereServs.Select(s => s.ID).ToList();
                    List<HIS_SERE_SERV> sereServThVt = allExists.Where(o => ssIds.Contains(o.ID) && (o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC || o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT) && o.IS_EXPEND == Constant.IS_TRUE).ToList();
                    if (IsNotNullOrEmpty(sereServThVt))
                    {
                        List<long> executeRoom = sereServThVt.Select(s => s.TDL_EXECUTE_ROOM_ID).Distinct().ToList();
                        List<V_HIS_MEDI_STOCK> mediStocks = HisMediStockCFG.IS_EXPEND_VIEW.Where(o => executeRoom.Contains(o.ROOM_ID)).ToList();
                        if (IsNotNullOrEmpty(mediStocks))
                        {
                            List<long> roomIds = mediStocks.Select(s => s.ROOM_ID).ToList();
                            List<HIS_SERE_SERV> expendSereServ = sereServThVt.Where(o => roomIds.Contains(o.TDL_EXECUTE_ROOM_ID)).ToList();
                            string serviceNames = string.Join(",", expendSereServ.Select(s => s.TDL_SERVICE_NAME).Distinct());
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.ThuocVatTuDuocKeTaiKhoHaoPhi
, serviceNames);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal bool IsValidBHYTServices(HisSereServPayslipSDO data, List<HIS_SERE_SERV> allExists, List<HIS_SERVICE_REQ> reqs, bool alwaysTrue)
        {
            bool valid = true;
            try
            {
                if (data != null && IsNotNullOrEmpty(data.SereServs) && IsNotNullOrEmpty(allExists))
                {
                    List<HIS_SERE_SERV> validSS = data.SereServs;
                    if (data.Field == UpdateField.IS_NOT_USE_BHYT)
                    {
                        List<V_HIS_SERVICE> services = HisServiceCFG.DATA_VIEW.Where(o => data.SereServs.Select(s => s.SERVICE_ID).Distinct().Contains(o.ID)).ToList();
                        List<string> inValidStr = new List<string>();
                        List<HIS_SERE_SERV> newSS = data.SereServs.Where(o => o.IS_NOT_USE_BHYT == Constant.IS_TRUE).ToList();
                        if (IsNotNullOrEmpty(newSS))
                        {
                            foreach (var ss in newSS)
                            {
                                HIS_SERE_SERV oldSSBHYT = allExists.FirstOrDefault(o => o.ID == ss.ID);
                                if (oldSSBHYT != null && oldSSBHYT.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                {
                                    if (alwaysTrue)
                                        validSS.Remove(ss);
                                    V_HIS_SERVICE sv = services.FirstOrDefault(o => o.ID == oldSSBHYT.SERVICE_ID);
                                    if (sv != null)
                                    {
                                        inValidStr.Add(string.Format("{0} - {1}({2}: {3})", sv.SERVICE_CODE, sv.SERVICE_NAME, MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common_MaYLenh, param.LanguageCode), reqs.FirstOrDefault(o => o.ID == oldSSBHYT.SERVICE_REQ_ID).SERVICE_REQ_CODE));
                                    }
                                }
                            }
                            if (IsNotNullOrEmpty(inValidStr))
                            {
                                string name = string.Join(", ", inValidStr);
                                MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DTTTCuaDichVuDangLaBHYTKhongChoPhepCapNhatKhongHuongBHYT, name);
                                if (!alwaysTrue)
                                    return false;
                            }
                        }
                    }
                    else if (data.Field == UpdateField.PATIENT_TYPE_ID)
                    {
                        List<V_HIS_SERVICE> services = HisServiceCFG.DATA_VIEW.Where(o => data.SereServs.Select(s => s.SERVICE_ID).Distinct().Contains(o.ID)).ToList();
                        List<string> inValidStr = new List<string>();
                        List<HIS_SERE_SERV> newSSBHYT = data.SereServs.Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();
                        if (IsNotNullOrEmpty(newSSBHYT))
                        {
                            foreach (var ss in newSSBHYT)
                            {
                                HIS_SERE_SERV oldSS = allExists.FirstOrDefault(o => o.ID == ss.ID);
                                if (oldSS != null && oldSS.IS_NOT_USE_BHYT == Constant.IS_TRUE)
                                {
                                    if (alwaysTrue)
                                        validSS.Remove(ss);
                                    V_HIS_SERVICE sv = services.FirstOrDefault(o => o.ID == oldSS.SERVICE_ID);
                                    if (sv != null)
                                    {
                                        inValidStr.Add(string.Format("{0} - {1}({2}: {3})", sv.SERVICE_CODE, sv.SERVICE_NAME, MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common_MaYLenh, param.LanguageCode), reqs.FirstOrDefault(o => o.ID == oldSS.SERVICE_REQ_ID).SERVICE_REQ_CODE));
                                    }
                                }
                            }
                            if (IsNotNullOrEmpty(inValidStr))
                            {
                                string name = string.Join(", ", inValidStr);
                                MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepDoiSangDTTTBHYTDoDichVuDaTichKhongHuongBHYT, name);
                                if (!alwaysTrue)
                                    return false;
                            }
                        }
                    }

                    // Neu goi tu dong doi doi tuong thanh toan
                    if (alwaysTrue)
                        data.SereServs = validSS;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

            return valid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Du lieu client truyen len</param>
        /// <param name="affectedExists">Cac du lieu lay tu CSDL tuong ung voi cac ban ghi do client truyen len</param>
        /// <param name="allExists">Tat ca cac du lieu lay tu CSDL</param>
        /// <returns></returns>
        internal bool IsValidServiceConditions(HisSereServPayslipSDO data, List<HIS_SERE_SERV> affectedExists, List<HIS_SERE_SERV> allExists)
        {
            bool valid = true;
            try
            {
                if (data != null && IsNotNullOrEmpty(data.SereServs) && IsNotNullOrEmpty(affectedExists))
                {
                    List<long> serviceIds = affectedExists.Select(o => o.SERVICE_ID).ToList();

                    //Lay ra cac sere_serv_id tuong ung voi cac dich vu co thiet lap "dieu kien"
                    List<long> conditionedSereServIds = HisServiceConditionCFG.DATA != null ? affectedExists.Where(o => HisServiceConditionCFG.DATA.Exists(t => t.SERVICE_ID == o.SERVICE_ID)).Select(o => o.ID).ToList() : null;

                    if (IsNotNullOrEmpty(conditionedSereServIds))
                    {
                        List<long> missingConditionSereServIds = null;

                        //Kiem tra, neu ton tai dv co thiet lap "dieu kien", doi tuong trong CSDL la BHYT, nhung khong truyen len thong tin doi tuong thi bao loi
                        //Trong truong hop y/c update truong doi tuong thanh toan, thi lay doi tuong thanh toan theo truong client gui len
                        if (data.Field == UpdateField.PATIENT_TYPE_ID)
                        {
                            missingConditionSereServIds = data.SereServs.Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && conditionedSereServIds.Contains(o.ID) && !o.SERVICE_CONDITION_ID.HasValue).Select(o => o.ID).ToList();
                        }
                        //Trong truong hop yeu cau update cac truong khac (ko phai truong "doi tuong thanh toan"), thi lay doi tuong thanh toan theo du lieu trong CSDL
                        else
                        {
                            missingConditionSereServIds = data.SereServs
                                .Where(o => !o.SERVICE_CONDITION_ID.HasValue && conditionedSereServIds.Contains(o.ID)
                                    && allExists.Exists(t => t.ID == o.ID && t.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                                .Select(o => o.ID).ToList();
                        }
                        if (IsNotNullOrEmpty(missingConditionSereServIds))
                        {
                            List<string> serviceNames = affectedExists.Where(o => missingConditionSereServIds.Contains(o.ID)).Select(o => o.TDL_SERVICE_NAME).ToList();
                            string nameStr = string.Join(",", serviceNames);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuBatBuocChonDieuKien, nameStr);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

            return valid;
        }


        internal bool IsValidDoNotUseBHYT(List<HIS_SERE_SERV> allExists)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(allExists))
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (!HisEmployeeUtil.IsAdmin(loginName))
                    {
                        var invalidSereServs = allExists.Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();
                        if (IsNotNullOrEmpty(invalidSereServs))
                        {
                            invalidSereServs = invalidSereServs.Where(o => HisServiceCFG.DO_NOT_USE_BHYT_DATA_VIEW.Exists(e => e.ID == o.SERVICE_ID)).ToList();
                            if (IsNotNullOrEmpty(invalidSereServs))
                            {
                                List<string> lstValid = invalidSereServs.Select(o => string.Format("{0} - {1}", o.TDL_SERVICE_CODE, o.TDL_SERVICE_NAME)).ToList();
                                string strValid = string.Join(", ", lstValid);

                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuDuocCauHinhKhongHuongBHYT, strValid);
                                return false;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                valid = false;
                LogSystem.Error(ex);
            }

            return valid;
        }

        internal bool ValidateWhenExpMestFinished(HisSereServPayslipSDO data, List<HIS_SERE_SERV> affectList, List<HIS_SERVICE_REQ> affectServiceReqs)
        {
            bool result = true;
            try
            {
                if (!IsNotNullOrEmpty(affectList) || !IsNotNullOrEmpty(affectServiceReqs)) return result;
                List<HIS_SERE_SERV> sereServs_ToCheck = new List<HIS_SERE_SERV>();
                if (data.Field == UpdateField.PATIENT_TYPE_ID)
                    sereServs_ToCheck = affectList.Where(o => o.PATIENT_TYPE_ID != data.SereServs.First(m => m.ID == o.ID).PATIENT_TYPE_ID).ToList();
                else
                    sereServs_ToCheck = affectList;
                    
                List<string> invalidMessages1 = new List<string>();
                List<string> invalidMessages2 = new List<string>();
                foreach (var serviceReq in affectServiceReqs)
                {
                    if (serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                        continue;
                    var medicines = sereServs_ToCheck.Where(o => o.SERVICE_REQ_ID == serviceReq.ID && o.EXP_MEST_MEDICINE_ID != null).ToList() ?? new List<HIS_SERE_SERV>();
                    var materials = sereServs_ToCheck.Where(o => o.SERVICE_REQ_ID == serviceReq.ID && o.EXP_MEST_MATERIAL_ID != null).ToList() ?? new List<HIS_SERE_SERV>();
                    if (IsNotNullOrEmpty(medicines))
                        invalidMessages1.Add(String.Format(MOS.MANAGER.Base.MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisSereServ_ThuocDaThucXuat, param.LanguageCode), String.Join(", ", medicines.Select(m => m.TDL_SERVICE_NAME)), serviceReq.SERVICE_REQ_CODE));
                    if (IsNotNullOrEmpty(materials))
                        invalidMessages2.Add(String.Format(MOS.MANAGER.Base.MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisSereServ_VatTuDaThucXuat, param.LanguageCode), String.Join(", ", materials.Select(m => m.TDL_SERVICE_NAME)), serviceReq.SERVICE_REQ_CODE));
                }
                invalidMessages1.AddRange(invalidMessages2);
                if (IsNotNullOrEmpty(invalidMessages1))
                {
                    result = false;
                    param.Messages.Add(String.Join(" ", invalidMessages1));
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }

        internal bool FilterValidateWhenExpMestFinished(HisSereServPayslipSDO data, List<HIS_SERE_SERV> affectList, List<HIS_SERVICE_REQ> affectServiceReqs)
        {
            bool result = true;
            try
            {
                if (data.Field == UpdateField.PATIENT_TYPE_ID)
                {
                    List<string> invalidMessages = new List<string>();
                    List<HIS_SERE_SERV> sereServs_ToCheck = affectList.Where(o => o.PATIENT_TYPE_ID != data.SereServs.First(m => m.ID == o.ID).PATIENT_TYPE_ID).ToList();
                    foreach (var serviceReq in affectServiceReqs)
                    {
                        if (serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                            continue;
                        var medicinesOrMaterials = sereServs_ToCheck.Where(o => o.SERVICE_REQ_ID == serviceReq.ID && (o.EXP_MEST_MEDICINE_ID != null || o.EXP_MEST_MATERIAL_ID != null)).ToList() ?? new List<HIS_SERE_SERV>();
                        if (IsNotNullOrEmpty(medicinesOrMaterials))
                        {
                            data.SereServs = data.SereServs.Where(o => !medicinesOrMaterials.Select(m => m.ID).Contains(o.ID)).ToList();
                            invalidMessages.Add(String.Format("Thuoc(hoac VT) {0} đã thực xuất (mã y lệnh {1}).", String.Join(", ", medicinesOrMaterials.Select(m => m.TDL_SERVICE_NAME)), serviceReq.SERVICE_REQ_CODE));
                        }
                    }
                    if (IsNotNullOrEmpty(invalidMessages))
                        Inventec.Common.Logging.LogSystem.Warn("___ValidateWhenExpMestFinished: " + String.Join(" ", invalidMessages));
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}
