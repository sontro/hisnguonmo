using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCarerCard;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    partial class HisCarerCardBorrowSDOCheck : BusinessBase
    {
        internal HisCarerCardBorrowSDOCheck()
            : base()
        {
        }

        internal HisCarerCardBorrowSDOCheck(CommonParam paramCheck)
            : base(paramCheck)
        {
        }

        internal bool VerifyRequireField(HisCarerCardBorrowSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
                if (!IsNotNullOrEmpty(data.CarerCardInfos)) throw new ArgumentNullException("data.CarerCardInfos");
                if (string.IsNullOrWhiteSpace(data.GivingLoginName)) throw new ArgumentNullException("data.GivingLoginName");
                if (string.IsNullOrWhiteSpace(data.GivingUserName)) throw new ArgumentNullException("data.GivingUserName");
                if (!IsGreaterThanZero(data.RequestRoomId)) throw new ArgumentNullException("data.RequestRoomId");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool IsValidCarerCardInfo(List<HisCarerCardSDOInfo> listCarerCardInfo, List<HIS_CARER_CARD> carerCards)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(listCarerCardInfo))
                {
                    // Check du lieu gui len co bi trung nhau
                    if (listCarerCardInfo.GroupBy(o => o.CarerCardId).Any(g => g.Count() > 1))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCarerCardBorrow_DanhSachThongTinTheMuonCoDuLieuBiTrung);
                        return false;
                    }

                    // Check thoi gian muon the
                    if (listCarerCardInfo.Exists(o => o.BorrowTime > Inventec.Common.DateTime.Get.Now()))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCarerCardBorrow_DanhSachThongTinTheMuonTonTaiThoiGianMuonLonHonThoiGianHienTai);
                        return false;
                    }

                    // Verify thong tin id the gui len
                    List<long> ids = listCarerCardInfo.Select(s => s.CarerCardId).ToList();
                    if (IsNotNullOrEmpty(ids))
                    {
                        HisCarerCardFilterQuery filter = new HisCarerCardFilterQuery();
                        filter.IDs = ids;
                        List<HIS_CARER_CARD> listData = new HisCarerCardGet().Get(filter);
                        if (listData == null || ids.Count != listData.Count)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            Logging("ListId invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => ids), ids), LogType.Error);
                            return false;
                        }
                        else
                        {
                            carerCards.AddRange(listData);
                        }
                    }

                    if (IsNotNullOrEmpty(carerCards))
                    {
                        // Check co ton tai the da duoc muon 
                        var cardBorroweds = carerCards.Where(o => o.IS_BORROWED == Constant.IS_TRUE).ToList();
                        if (IsNotNullOrEmpty(cardBorroweds))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCarerCardBorrow_DanhSachThongTinTheMuonTonTaiTheDaCoNguoiMuon, string.Join(", ", cardBorroweds.Select(o => o.CARER_CARD_NUMBER).ToList()));
                            return false;
                        }

                        // Check co ton tai the da bao mat
                        var cardLosts = carerCards.Where(o => o.IS_LOST == Constant.IS_TRUE).ToList();
                        if (IsNotNullOrEmpty(cardLosts))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCarerCardBorrow_DanhSachThongTinTheMuonTonTaiTheDaBaoMat, string.Join(", ", cardLosts.Select(o => o.CARER_CARD_NUMBER).ToList()));
                            return false;
                        }

                        // Kiem tra dich vu tuong ung voi the co bi khoa hay ko
                        List<V_HIS_SERVICE> services = HisServiceCFG.DATA_VIEW.Where(
                            o => carerCards.Select(s => s.SERVICE_ID).Distinct().ToList().Contains(o.ID)
                             && (!o.IS_ACTIVE.HasValue || o.IS_ACTIVE != Constant.IS_TRUE)
                            ).ToList();
                        if (IsNotNullOrEmpty(services))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCarerCardBorrow_DanhSachThongTinTheMuonTonTaiDichVuTuongUngDaBiKhoa, string.Join(", ", services.Select(o => o.SERVICE_CODE).ToList()));
                            return false;
                        }

                        // Check co ton tai chinh sach gia la vien phi
                        List<V_HIS_SERVICE_PATY> sPatys = HisServicePatyCFG.DATA.Where(
                            o => carerCards.Select(s => s.SERVICE_ID).Distinct().ToList().Contains(o.SERVICE_ID)
                              && o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE
                            ).ToList();
                        if (!IsNotNullOrEmpty(sPatys))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCarerCardBorrow_DanhSachThongTinTheMuonKhongTonTaiChinhSachGiaTuongUngLaVienPhi);
                            return false;
                        }
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
    }
}
