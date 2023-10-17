using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using AutoMapper;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisEmployee;

namespace MOS.MANAGER.HisSereServ
{
    class HisSereServCheck : BusinessBase
    {
        internal HisSereServCheck()
            : base()
        {

        }

        internal HisSereServCheck(Inventec.Core.CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_SERE_SERV data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.PATIENT_TYPE_ID)) throw new ArgumentNullException("data.PATIENT_TYPE_ID");
                if (!IsGreaterThanZero(data.SERVICE_ID)) throw new ArgumentNullException("data.SERVICE_ID");
                if (!data.SERVICE_REQ_ID.HasValue && data.IS_DELETE != Constant.IS_TRUE) throw new ArgumentNullException("data.SERVICE_REQ_ID");
                if (!data.TDL_PATIENT_ID.HasValue) throw new ArgumentNullException("data.TDL_PATIENT_ID");
                if (!data.TDL_TREATMENT_ID.HasValue) throw new ArgumentNullException("data.TDL_TREATMENT_ID");
                if (data.AMOUNT < 0) throw new ArgumentNullException("data.AMOUNT");
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

        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisSereServDAO.IsUnLock(id))
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_SERE_SERV data)
        {
            bool valid = true;
            try
            {
                data = new HisSereServGet().GetById(id);
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id), LogType.Error);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra su ton tai cua danh sach cac id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId, List<HIS_SERE_SERV> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisSereServFilterQuery filter = new HisSereServFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SERE_SERV> listData = new HisSereServGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listId), listId), LogType.Error);
                        valid = false;
                    }
                    else
                    {
                        listObject.AddRange(listData);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung doi tuong)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(HIS_SERE_SERV raw)
        {
            return IsUnLock(new List<HIS_SERE_SERV>() { raw });
        }

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<HIS_SERE_SERV> listRaw)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV> locks = IsNotNullOrEmpty(listRaw) ? listRaw.Where(o => IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != o.IS_ACTIVE).ToList() : null;
                if (IsNotNullOrEmpty(locks))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }


        internal bool HasNoHeinApproval(List<HIS_SERE_SERV> data)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV> approvals = IsNotNullOrEmpty(data) ? data.Where(o => o.HEIN_APPROVAL_ID.HasValue).ToList() : null;
                if (IsNotNullOrEmpty(approvals))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuDaDuocGiamDinhBhyt);
                    throw new Exception("His_sere_serv khong thuc hien,khong cho phep thanh toan" + LogUtil.TraceData("data", data));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasNoHeinApproval(HIS_SERE_SERV data)
        {
            return this.HasNoHeinApproval(new List<HIS_SERE_SERV>() { data });
        }


        internal bool HasExecute(HIS_SERE_SERV raw)
        {
            return this.HasExecute(new List<HIS_SERE_SERV>() { raw });
        }

        internal bool HasExecute(V_HIS_SERE_SERV_1 raw)
        {
            Mapper.CreateMap<V_HIS_SERE_SERV_1, HIS_SERE_SERV>();
            HIS_SERE_SERV ss = Mapper.Map<HIS_SERE_SERV>(raw);
            return this.HasExecute(ss);
        }

        internal bool HasExecute(List<HIS_SERE_SERV> data)
        {
            bool valid = true;
            try
            {
                List<string> noExecutes = IsNotNullOrEmpty(data) ? data.Where(o => o.IS_NO_EXECUTE == MOS.UTILITY.Constant.IS_TRUE).Select(o => o.TDL_SERVICE_NAME).ToList() : null;
                if (IsNotNullOrEmpty(noExecutes))
                {
                    string noExecuteStr = string.Join(",", noExecutes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuKhongThucHien, noExecuteStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }


        internal bool HasNoChild(List<long> sereServIds)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(sereServIds))
                {
                    List<HIS_SERE_SERV> chidls = new HisSereServGet().GetByParentIds(sereServIds);
                    if (IsNotNullOrEmpty(chidls) && !chidls.Exists(e => sereServIds.Contains(e.ID)))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_TonTaiDichVuCon);
                        throw new Exception("Ton tai dich vu con. khong cho phep xoa");
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

        internal bool HasNoBill(HIS_SERE_SERV sereServ)
        {
            return this.HasNoBill(new List<HIS_SERE_SERV>() { sereServ });
        }

        internal bool HasNoBill(List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(sereServs))
                {
                    List<long> sereServIds = sereServs.Select(o => o.ID).ToList();
                    List<HIS_SERE_SERV_BILL> hasBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                    if (IsNotNullOrEmpty(hasBills))
                    {
                        List<string> serviceNames = sereServs.Where(o => hasBills.Exists(t => t.SERE_SERV_ID == o.ID)).Select(o => o.TDL_SERVICE_NAME).ToList();
                        string serviceNameStr = string.Join(",", serviceNames);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DaTonTaiHoaDon, serviceNameStr);
                        return false;
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


        internal bool HasBill(List<HIS_SERE_SERV> listRaw)
        {
            bool valid = true;
            try
            {
                List<long> sereServIds = IsNotNullOrEmpty(listRaw) ? listRaw.Select(o => o.ID).ToList() : null;
                List<HIS_SERE_SERV_BILL> sereServBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);

                List<string> noBills = IsNotNullOrEmpty(listRaw) ? listRaw.Where(o => sereServBills == null || !sereServBills.Exists(t => t.SERE_SERV_ID == o.ID)).Select(o => o.TDL_SERVICE_NAME).ToList() : null;

                if (IsNotNullOrEmpty(noBills))
                {
                    string noBillStr = string.Join(",", noBills);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_ChuaCoHoaDonThanhToan, noBillStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasBill(HIS_SERE_SERV raw)
        {
            return this.HasBill(new List<HIS_SERE_SERV>() { raw });
        }


        internal bool HasNoDeposit(long sereServId, bool checkRepay)
        {
            return this.HasNoDeposit(new List<long>() { sereServId }, checkRepay);
        }

        internal bool HasNoDeposit(List<long> sereServIds, bool andNoRepay)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV_DEPOSIT> deposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                if (IsNotNullOrEmpty(deposits) && andNoRepay)
                {
                    List<HIS_SESE_DEPO_REPAY> hasRepays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(deposits.Select(s => s.ID).ToList());
                    if (IsNotNullOrEmpty(hasRepays))
                    {
                        deposits = deposits.Where(o => !hasRepays.Exists(e => e.SERE_SERV_DEPOSIT_ID == o.ID)).ToList();
                    }
                }
                if (IsNotNullOrEmpty(deposits))
                {
                    List<string> serviceNames = deposits.Select(o => o.TDL_SERVICE_NAME).ToList();
                    string serviceNameStr = string.Join(",", serviceNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_CacDichVuDaDuocTamUng, serviceNameStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }


        internal bool HasDepositAndNoRepay(List<HIS_SERE_SERV> listRaw)
        {
            bool valid = false;
            try
            {
                List<long> sereServIds = IsNotNullOrEmpty(listRaw) ? listRaw.Select(o => o.ID).ToList() : null;
                List<HIS_SERE_SERV_DEPOSIT> deposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                if (IsNotNullOrEmpty(deposits))
                {
                    List<HIS_SESE_DEPO_REPAY> hasRepays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(deposits.Select(s => s.ID).ToList());
                    if (IsNotNullOrEmpty(hasRepays))
                    {
                        string s = string.Join(",", hasRepays);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaDuocHoanUng, s);
                    }
                    else
                    {
                        valid = true;
                    }
                }
                else
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_HoSoKhongCoGiaoDichTamUngDichVu);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasBillOrDepositAndNoRepay(List<HIS_SERE_SERV> listRaw)
        {
            bool valid = true;
            try
            {
                List<long> sereServIds = IsNotNullOrEmpty(listRaw) ? listRaw.Select(o => o.ID).ToList() : null;
                List<HIS_SERE_SERV_DEPOSIT> deposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                List<HIS_SESE_DEPO_REPAY> hasRepays = IsNotNullOrEmpty(deposits) ? new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(deposits.Select(s => s.ID).ToList()) : null;

                List<HIS_SERE_SERV_BILL> sereServBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                List<string> noBills = IsNotNullOrEmpty(listRaw) ? listRaw.Where(o => sereServBills == null || !sereServBills.Exists(t => t.SERE_SERV_ID == o.ID)).Select(o => o.TDL_SERVICE_NAME).ToList() : null;

                if (!IsNotNullOrEmpty(noBills) || (IsNotNullOrEmpty(deposits) && !IsNotNullOrEmpty(hasRepays)))
                {
                    return true;
                }

                if (!IsNotNullOrEmpty(deposits))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_PhieuXuatChuaDuocTamUngDichVu);
                    return false;
                }

                if (IsNotNullOrEmpty(hasRepays))
                {
                    string s = string.Join(",", hasRepays);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaDuocHoanUng, s);
                    return false;
                }
                if (IsNotNullOrEmpty(noBills))
                {
                    string noBillStr = string.Join(",", noBills);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_ChuaCoHoaDonThanhToan, noBillStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }

            return valid;
        }

        internal bool HasNoDebt(List<long> sereServIds)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV_DEBT> debts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);

                if (IsNotNullOrEmpty(debts))
                {
                    List<string> serviceNames = debts.Select(o => o.TDL_SERVICE_NAME).ToList();
                    string serviceNameStr = string.Join(",", serviceNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_CacDichVuDaDuocChotNo, serviceNameStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasNoDebt(List<HIS_SERE_SERV> sereServs)
        {
            List<long> sereServIds = sereServs != null ? sereServs.Select(o => o.ID).ToList() : null;
            return this.HasNoDebt(sereServIds);
        }

        internal bool HasNoInvoice(List<long> sereServIds)
        {
            if (IsNotNullOrEmpty(sereServIds))
            {
                HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                ssFilter.IDs = sereServIds;
                ssFilter.HAS_INVOICE = true;
                List<HIS_SERE_SERV> listRaw = new HisSereServGet().Get(ssFilter);
                return this.HasNoInvoice(listRaw);
            }
            return true;
        }

        internal bool HasNoInvoice(List<HIS_SERE_SERV> listRaw)
        {
            bool valid = true;
            try
            {
                List<string> hasInvoices = IsNotNullOrEmpty(listRaw) ? listRaw.Where(o => o.INVOICE_ID.HasValue).Select(o => o.TDL_SERVICE_NAME).ToList() : null;
                if (IsNotNullOrEmpty(hasInvoices))
                {
                    string hasInvoiceStr = string.Join(",", hasInvoices);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DaTonTaiHoaDonDo, hasInvoiceStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasNoInvoice(HIS_SERE_SERV data)
        {
            bool valid = true;
            try
            {
                if (data != null && data.INVOICE_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DaTonTaiHoaDonDo);
                    throw new Exception("His_sere_serv da ton tai hoa don do" + LogUtil.TraceData("data", data));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasNoInvoice(V_HIS_SERE_SERV_1 data)
        {
            bool valid = true;
            try
            {
                if (data != null && data.INVOICE_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DaTonTaiHoaDonDo);
                    throw new Exception("His_sere_serv da ton tai hoa don do" + LogUtil.TraceData("data", data));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }


        internal bool IsAllowUsingPatientType(List<HIS_KSK_CONTRACT> kskContracts, HIS_PATIENT_TYPE_ALTER appliedPta, long serviceId, long sereServPatientTypeId, long instructionTime)
        {
            bool valid = true;
            try
            {
                if (sereServPatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK)
                {
                    HIS_KSK_CONTRACT contract = kskContracts != null && appliedPta.KSK_CONTRACT_ID.HasValue ?
                        kskContracts.Where(o => o.ID == appliedPta.KSK_CONTRACT_ID.Value).FirstOrDefault() : null;

                    if (appliedPta.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__KSK || contract == null)
                    {
                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == serviceId).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_KhongTonTaiThongTinDienDoiTuongKskNenKhongChoPhepThanhToanSuDungKsk, service.SERVICE_NAME);
                        return false;
                    }

                    long? effectDate = contract.EFFECT_DATE.HasValue ? Inventec.Common.DateTime.Get.StartDay(contract.EFFECT_DATE.Value) : null;
                    long? expiryDate = contract.EXPIRY_DATE.HasValue ? Inventec.Common.DateTime.Get.StartDay(contract.EXPIRY_DATE.Value) : null;

                    long? instructionDate = Inventec.Common.DateTime.Get.StartDay(instructionTime);

                    if ((effectDate.HasValue && effectDate > instructionDate) || (expiryDate.HasValue && expiryDate < instructionDate))
                    {
                        string fromDate = expiryDate.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToDateString(expiryDate.Value) : "";
                        string toDate = expiryDate.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToDateString(expiryDate.Value) : "";
                        string instructionTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToDateString(instructionTime);
                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == serviceId).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuCoThoiGianYLenhNamNgoaiKhoangThoiGianHieuLucCuaHopDong, service.SERVICE_NAME, instructionTimeStr, fromDate, toDate);
                        return false;
                    }
                }

                if (sereServPatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    if (appliedPta.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                    {
                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == serviceId).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_KhongTonTaiThongTinDienDoiTuongBhytNenKhongChoPhepThanhToanSuDungBhyt, service.SERVICE_NAME);
                        return false;
                    }

                    if (!appliedPta.IS_NO_CHECK_EXPIRE.HasValue || appliedPta.IS_NO_CHECK_EXPIRE != MOS.UTILITY.Constant.IS_TRUE)
                    {
                        if (!appliedPta.HEIN_CARD_FROM_TIME.HasValue || !appliedPta.HEIN_CARD_FROM_TIME.HasValue)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_TheBhytThieuThongTinThoiGian);
                            return false;
                        }

                        //Cong them "so ngay cho phep vuot qua" truoc khi check han the (chi ap dung cho BN dieu tri noi tru)
                        int exceedDayAllow = appliedPta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || appliedPta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY ? HisHeinBhytCFG.EXCEED_DAY_ALLOW_FOR_IN_PATIENT : 0;

                        DateTime toTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(appliedPta.HEIN_CARD_TO_TIME.Value).Value.AddDays(exceedDayAllow);
                        long toTimeNum = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(toTime).Value;

                        long? heinCardFromDate = Inventec.Common.DateTime.Get.StartDay(appliedPta.HEIN_CARD_FROM_TIME.Value);
                        long? heinCardToDate = Inventec.Common.DateTime.Get.StartDay(toTimeNum);
                        long? instructionDate = Inventec.Common.DateTime.Get.StartDay(instructionTime);

                        if (heinCardFromDate > instructionDate || heinCardToDate < instructionDate)
                        {
                            string fromDateStr = Inventec.Common.DateTime.Convert.TimeNumberToDateString(heinCardFromDate.Value);
                            string toDateStr = Inventec.Common.DateTime.Convert.TimeNumberToDateString(heinCardToDate.Value);
                            string instructionTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToDateString(instructionTime);
                            V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == serviceId).FirstOrDefault();
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuCoThoiGianYLenhNamNgoaiKhoangThoiGianHieuLucCuaTheBaoHiem, service.SERVICE_NAME, instructionTimeStr, fromDateStr, toDateStr);
                            return false;
                        }
                    }
                }

                //Neu doi tuong thanh toan giong doi tuong BN thi ko can check
                if (appliedPta.PATIENT_TYPE_ID != sereServPatientTypeId)
                {
                    if (!IsNotNullOrEmpty(HisPatientTypeAllowCFG.DATA))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAllow_KhongTonTaiDuLieu);
                        throw new Exception();
                    }

                    List<HIS_PATIENT_TYPE_ALLOW> allows = HisPatientTypeAllowCFG.DATA
                        .Where(o => o.PATIENT_TYPE_ID == appliedPta.PATIENT_TYPE_ID
                           && o.PATIENT_TYPE_ALLOW_ID == sereServPatientTypeId
                           && o.IS_ACTIVE == Constant.IS_TRUE)
                        .ToList();

                    //Neu trong truong hop khong cho phep chuyen doi tuong thanh toan thi add message
                    if (!IsNotNullOrEmpty(allows))
                    {
                        string patientTypeName = HisPatientTypeCFG.DATA
                            .Where(o => o.ID == appliedPta.PATIENT_TYPE_ID)
                            .Select(o => o.PATIENT_TYPE_NAME)
                            .SingleOrDefault();
                        string unAllowPatientTypeName = HisPatientTypeCFG.DATA
                            .Where(o => o.ID == sereServPatientTypeId)
                            .Select(o => o.PATIENT_TYPE_NAME)
                            .SingleOrDefault();
                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == serviceId).FirstOrDefault();
                        MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DoiTuongThanhToanKhongDuocPhepChuyenDoi, patientTypeName, unAllowPatientTypeName, service.SERVICE_NAME);
                        return false;
                    }
                }

                if (appliedPta.PATIENT_TYPE_ID != sereServPatientTypeId
                    && !HisPatientTypeCFG.NO_CO_PAYMENT.Exists(t => t.ID == sereServPatientTypeId))
                {
                    HIS_PATIENT_TYPE p = HisPatientTypeCFG.DATA.Where(o => o.ID == sereServPatientTypeId).FirstOrDefault();

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_KhongChoPhepThanhToanDoKhongTonTaiThongTinDoiTuongThanhToanTuongUng, p.PATIENT_TYPE_NAME);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Lay du lieu doi tuong thanh toan tuong ung voi instruction_time
        /// </summary>
        /// <param name="instructionTime"></param>
        /// <returns></returns>
        internal bool HasAppliedPatientTypeAlter(long instructionTime, List<HIS_PATIENT_TYPE_ALTER> ptas, ref HIS_PATIENT_TYPE_ALTER usingPta)
        {
            try
            {
                HIS_PATIENT_TYPE_ALTER result = null;
                if (IsNotNullOrEmpty(ptas))
                {
                    result = ptas
                        .Where(o => o.LOG_TIME <= instructionTime)
                        .OrderByDescending(o => o.LOG_TIME)
                        .ThenByDescending(o => o.ID)
                        .FirstOrDefault();
                }

                if (result == null)
                {
                    string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(instructionTime);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_KhongTonTaiDuLieuTruocNgayYLenh, time);
                    return false;
                }

                usingPta = result;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Kiem tra xem co cho phep cap nhat sang ko thuc hien hay ko
        /// </summary>
        /// <param name="toNoExecutes"></param>
        /// <returns></returns>
        internal bool IsAllowUpdateToNoExecute(List<HIS_SERE_SERV> toNoExecutes)
        {
            try
            {
                if (IsNotNullOrEmpty(toNoExecutes))
                {
                    if (HisSereServCFG.MUST_BE_ACCEPTED_BEFORE_SETING_NO_EXECUTE)
                    {
                        //Chi kiem tra neu dv khong phai la "thuoc/vat tu/mau" va duoc chi dinh boi phong kham
                        List<HIS_SERE_SERV> ss = toNoExecutes.Where(o => o.IS_ACCEPTING_NO_EXECUTE != Constant.IS_TRUE
                            && o.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT
                            && o.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                            && o.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU
                            && HisRoomCFG.DATA.Exists(t => t.ID == o.TDL_REQUEST_ROOM_ID && t.IS_EXAM == Constant.IS_TRUE)).ToList();

                        if (IsNotNullOrEmpty(ss))
                        {
                            string str = "";
                            foreach (HIS_SERE_SERV s in ss)
                            {
                                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == s.TDL_REQUEST_ROOM_ID).FirstOrDefault();
                                if (room != null)
                                {
                                    str += string.Format("{0} ({1}-{2}); ", s.TDL_SERVICE_NAME, s.TDL_SERVICE_REQ_CODE, room.ROOM_NAME);
                                }
                            }
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_BacSyChuaXacNhanDongYKhongThucHien, str);
                            return false;
                        }
                    }

                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    List<long> toCheckDepositIds = null;

                    //Neu cau hinh cho phep nguoi dung la quan tri hoac la nguoi chi dinh thi cho phep tick "ko thuc hien" trong truong hop da tam ung
                    if (HisSereServCFG.ALLOW_NO_EXECUTE_FOR_PAID_SERVICE_OPTION == HisSereServCFG.AllowNoExecuteForPaidServiceOption.ALLOW_FOR_DIPOSIT)
                    {
                        if (HisEmployeeUtil.IsAdmin(loginName))
                        {
                            toCheckDepositIds = null;
                        }
                        else
                        {
                            toCheckDepositIds = toNoExecutes.Where(o => o.TDL_REQUEST_LOGINNAME != loginName).Select(o => o.ID).ToList();
                        }
                    }
                    else
                    {
                        toCheckDepositIds = toNoExecutes.Select(o => o.ID).ToList();
                    }

                    if (IsNotNullOrEmpty(toCheckDepositIds) && !this.HasNoDeposit(toCheckDepositIds, true))
                    {
                        return false;
                    }

                    List<HIS_SERE_SERV> ssTests = toNoExecutes.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN).ToList();
                    List<long> ssTestNeedCheck = null;
                    if (IsNotNullOrEmpty(ssTests))
                    {
                        if (HisSereServCFG.TEST__IS_ALLOW_CHECK_IS_NO_EXECUTE)
                        {
                            HisSereServTeinFilterQuery ssTeinFilter = new HisSereServTeinFilterQuery();
                            ssTeinFilter.SERE_SERV_IDs = ssTests.Select(s => s.ID).ToList();
                            ssTeinFilter.HAS_VALUE = true;
                            List<HIS_SERE_SERV_TEIN> ssTeins = new HisSereServTeinGet().Get(ssTeinFilter);
                            if (IsNotNullOrEmpty(ssTeins))
                            {
                                ssTestNeedCheck = ssTeins.Where(o => !String.IsNullOrWhiteSpace(o.VALUE)).Select(s => s.SERE_SERV_ID).Distinct().ToList();
                            }
                        }
                        else
                        {
                            ssTestNeedCheck = ssTests.Select(s => s.ID).ToList();
                        }
                    }

                    List<long> serviceReqIds = toNoExecutes.Where(o => o.SERVICE_REQ_ID.HasValue
                        && ((o.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN)
                        || (o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN
                            && ssTestNeedCheck != null && ssTestNeedCheck.Contains(o.ID))))
                        .Select(o => o.SERVICE_REQ_ID.Value).ToList();

                    List<long> serviceReqConfirmNoExecuteIds = toNoExecutes.Where(o => o.IS_CONFIRM_NO_EXCUTE != 1).Select(o => o.SERVICE_REQ_ID.Value).ToList();

                    if (IsNotNullOrEmpty(serviceReqIds))
                    {
                        List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByIds(serviceReqIds);
                        List<string> starteds = serviceReqs != null ? serviceReqs
                            .Where(o => (o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL && serviceReqConfirmNoExecuteIds != null && serviceReqConfirmNoExecuteIds.Contains(o.ID)) || o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                            .Select(o => o.SERVICE_REQ_CODE).ToList() : null;

                        if (IsNotNullOrEmpty(starteds))
                        {
                            string codeStr = string.Join(",", starteds);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuChiDinhDaThucHien, codeStr);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }

        internal bool AllowUpdate(List<HIS_SERE_SERV> changes, List<HIS_SERE_SERV> olds)
        {
            if (IsNotNullOrEmpty(changes))
            {
                List<HIS_SERE_SERV> unallowWhenHavingInvoices = HisSereServUtil.GetUnallowUpdateWhenHavingInvoice(changes, olds);
                if (IsNotNullOrEmpty(unallowWhenHavingInvoices))
                {
                    List<HIS_SERE_SERV_BILL> hasBills = new HisSereServBillGet().GetNoCancelBySereServIds(unallowWhenHavingInvoices.Select(o => o.ID).ToList());

                    if (IsNotNullOrEmpty(hasBills))
                    {
                        List<string> hasBillNames = unallowWhenHavingInvoices.Where(o => hasBills.Exists(t => t.SERE_SERV_ID == o.ID)).Select(o => o.TDL_SERVICE_NAME).ToList();

                        string serviceNameStr = string.Join(",", hasBillNames);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DaTonTaiHoaDonKhongThucHienCapNhat, serviceNameStr);
                        return false;
                    }
                }
            }
            return true;
        }

        internal bool IsGServiceType(HIS_SERE_SERV sereServ)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(sereServ) && sereServ.TDL_SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__G)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_LoaiDichVuDangXuLyKhongPhaiLaGiuong);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
