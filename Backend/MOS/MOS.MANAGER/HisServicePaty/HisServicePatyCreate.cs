using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisService;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.LibraryMessage;

namespace MOS.MANAGER.HisServicePaty
{
    class HisServicePatyCreate : BusinessBase
    {
        internal HisServicePatyCreate()
            : base()
        {

        }

        internal HisServicePatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_PATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServicePatyCheck checker = new HisServicePatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    V_HIS_SERVICE hisService = new HisServiceGet().GetViewById(data.SERVICE_ID);
                    if (!hisService.IS_LEAF.HasValue || hisService.IS_LEAF.Value != MOS.UTILITY.Constant.IS_TRUE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_ChiChoThietLapGiaChoDichVuLa);
                        throw new Exception("Chi cho thiet lap gia cho dich vu la" + LogUtil.TraceData("hisService", hisService));
                    }

                    if (!checker.CheckHeinPrice(data, hisService))
                    {
                        return false;
                    }
                    //neu thiet lap chinh sach gia cho BHYT thi can kiem tra xem dich vu da cau hinh thong tin BHYT hay chua, neu chua thi ko duoc phep them du lieu
                    if (data.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                    {
                        if ((hisService.SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC && string.IsNullOrWhiteSpace(hisService.HEIN_SERVICE_BHYT_CODE)) || string.IsNullOrWhiteSpace(hisService.HEIN_SERVICE_BHYT_NAME))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_DichVuChuaCauHinhThongTinBhytKhongChoPhepTaoChinhSachGiaBhyt);
                            throw new Exception("Dich vu chua cau hinh thong tin BHYT nen khong cho phep tao chinh sach gia cho BHYT" + LogUtil.TraceData("data", data));
                        }

                        if (hisService.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                            && (String.IsNullOrWhiteSpace(hisService.ACTIVE_INGR_BHYT_CODE)
                            || String.IsNullOrWhiteSpace(hisService.ACTIVE_INGR_BHYT_NAME)))
                        {
                            MessageUtil.SetMessage(param, Message.Enum.HisServicePaty_ThuocChuaCoDayDuThongTinHoatChatKhongChoPhepTaoChinhSachGiaBhyt);
                            return false;
                        }
                    }
                    result = DAOWorker.HisServicePatyDAO.Create(data);
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

        internal bool CreateList(List<HIS_SERVICE_PATY> data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServicePatyCheck checker = new HisServicePatyCheck(param);
                valid = valid && IsNotNullOrEmpty(data);

                if (valid)
                {
                    bool isReload = true;
                    List<string> notInfoBhyts = new List<string>();
                    List<string> medicineNotInfoActiveIngrs = new List<string>();
                    foreach (HIS_SERVICE_PATY t in data)
                    {
                        V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == t.SERVICE_ID).FirstOrDefault();

                        if (isReload && hisService == null)
                        {
                            isReload = false;
                            HisServiceCFG.Reload();
                            hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == t.SERVICE_ID).FirstOrDefault();
                        }

                        if (hisService == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ServiceId invalid: " + t.SERVICE_ID);
                        }

                        if (!hisService.IS_LEAF.HasValue || hisService.IS_LEAF.Value != MOS.UTILITY.Constant.IS_TRUE)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_ChiChoThietLapGiaChoDichVuLa);
                            throw new Exception("Chi cho thiet lap gia cho dich vu la" + LogUtil.TraceData("hisService", hisService));
                        }

                        //neu thiet lap chinh sach gia cho BHYT thi can kiem tra xem dich vu da cau hinh thong tin BHYT hay chua, neu chua thi ko duoc phep them du lieu
                        if (t.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                        {
                            if ((hisService.SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC && string.IsNullOrWhiteSpace(hisService.HEIN_SERVICE_BHYT_CODE)) || string.IsNullOrWhiteSpace(hisService.HEIN_SERVICE_BHYT_NAME))
                            {
                                notInfoBhyts.Add(hisService.SERVICE_CODE);
                                //MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_DichVuChuaCauHinhThongTinBhytKhongChoPhepTaoChinhSachGiaBhyt);
                                //throw new Exception("Dich vu chua cau hinh thong tin BHYT nen khong cho phep tao chinh sach gia cho BHYT" + LogUtil.TraceData("data", data));
                            }

                            if (hisService.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                            && (String.IsNullOrWhiteSpace(hisService.ACTIVE_INGR_BHYT_CODE)
                            || String.IsNullOrWhiteSpace(hisService.ACTIVE_INGR_BHYT_NAME)))
                            {
                                medicineNotInfoActiveIngrs.Add(hisService.SERVICE_CODE);
                                //MessageUtil.SetMessage(param, Message.Enum.HisServicePaty_ThuocChuaCoDayDuThongTinHoatChatKhongChoPhepTaoChinhSachGiaBhyt);
                                //return false;
                            }
                        }

                        if (!checker.CheckHeinPrice(t, hisService))
                        {
                            return false;
                        }
                    }

                    if (IsNotNullOrEmpty(notInfoBhyts))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_CacDichVuChuaCauHinhThongTinBhytKhongChoPhepTaoChinhSachGiaBhyt, String.Join(",", notInfoBhyts));
                        LogSystem.Warn("Dich vu chua cau hinh thong tin BHYT nen khong cho phep tao chinh sach gia cho BHYT" + LogUtil.TraceData("notInfoBhyts", notInfoBhyts));
                        return false;
                    }
                    if (IsNotNullOrEmpty(medicineNotInfoActiveIngrs))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_CacThuocChuaCoDayDuThongTinHoatChatKhongChoPhepTaoChinhSachGiaBhyt, String.Join(",", medicineNotInfoActiveIngrs));
                        return false;
                    }

                    result = DAOWorker.HisServicePatyDAO.CreateList(data);
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
