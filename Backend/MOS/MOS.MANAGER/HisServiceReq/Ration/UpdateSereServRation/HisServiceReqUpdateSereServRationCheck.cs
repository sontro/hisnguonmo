using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceRati;
using MOS.MANAGER.HisRationTime;
using MOS.MANAGER.HisSereServRation;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    class HisServiceReqUpdateSereServRationCheck : BusinessBase
    {
        internal HisServiceReqUpdateSereServRationCheck()
            : base()
        {
        }

        internal HisServiceReqUpdateSereServRationCheck(CommonParam paramCheck)
            : base(paramCheck)
        {
        }

        internal bool VerifyRequireField(HisServiceReqRationUpdateSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.ServiceReqId)) throw new ArgumentNullException("data.ServiceReqId");
                if (!IsGreaterThanZero(data.ExecuteRoomId)) throw new ArgumentNullException("data.ExecuteRoomId");
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

        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisSereServRationDAO.IsUnLock(id))
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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

        internal bool IsTypeRationForCreating(List<RationServiceSDO> insertServices)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(insertServices))
                {
                    List<long> serviceIds = insertServices.Select(o => o.ServiceId).ToList();
                    if (IsNotNullOrEmpty(serviceIds))
                    {
                        var services = HisServiceCFG.DATA_VIEW.Where(o => serviceIds.Contains(o.ID)).ToList();
                        if (IsNotNullOrEmpty(services))
                        {
                            var notTypes = services.Where(o => o.SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__AN).ToList();

                            if (IsNotNullOrEmpty(notTypes))
                            {
                                string codes = String.Join(",", notTypes.Select(s => s.SERVICE_NAME).ToList());
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisService_CacDichVuThemMoiSauKhongPhaiLaLoaiSuatAn, codes);
                                return false;
                            }
                        }

                        //Kiem tra xem thoi gian an cua cac suat an co thoa man ko
                        List<HIS_SERVICE_RATI> serviceRatis = new HisServiceRatiGet().GetByServiceId(serviceIds);

                        foreach (RationServiceSDO sdo in insertServices)
                        {
                            List<long> tmp = sdo.RationTimeIds
                                .Where(t => serviceRatis == null || !serviceRatis.Exists(o => o.SERVICE_ID == sdo.ServiceId && t == o.RATION_TIME_ID)).ToList();
                            if (IsNotNullOrEmpty(tmp))
                            {
                                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == sdo.ServiceId).FirstOrDefault();
                                List<HIS_RATION_TIME> rationTimes = new HisRationTimeGet().GetById(tmp);
                                List<string> rationTimeNames = IsNotNullOrEmpty(rationTimes) ? rationTimes.Select(o => o.RATION_TIME_NAME).ToList() : null;
                                string rationTimeNameStr = rationTimeNames != null ? string.Join(",", rationTimeNames) : "";
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_SuatAnKhongCungCapVaoThoiGian, service.SERVICE_NAME, rationTimeNameStr);
                                return false;
                            }
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

        internal bool IsValidSereServRatition(List<RationServiceSDO> insertServices, List<long> deleteSereServRationIds, List<HIS_SERE_SERV_RATION> updateSereServRations,  List<HIS_SERE_SERV_RATION> deleteSereServRations, HIS_SERVICE_REQ req)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(insertServices))
                {
                    if (insertServices.Exists(o => !o.SereServRationId.HasValue)) throw new ArgumentNullException("Thong tin danh sach cap nhat suat an khong co thong tin SereServRationId");
                    bool isValidUpdate = new HisSereServRationCheck(param).VerifyIds(insertServices.Select(o => o.SereServRationId.Value).ToList(), updateSereServRations);
                }

                if (IsNotNullOrEmpty(deleteSereServRationIds))
                {
                    bool isValidDelete = new HisSereServRationCheck(param).VerifyIds(deleteSereServRationIds, deleteSereServRations);
                }

                if (IsNotNullOrEmpty(updateSereServRations))
                {
                    try
                    {
                        if (updateSereServRations.Exists(o => o.SERVICE_REQ_ID != req.ID)) throw new ArgumentNullException("Thong tin danh sach cap nhat suat an khong thuoc ve y lenh dang xu ly");
                    }
                    catch (ArgumentNullException ex)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn(ex);
                        valid = false;
                    }
                }

                if (IsNotNullOrEmpty(deleteSereServRations) && deleteSereServRations.Exists(o => o.SERVICE_REQ_ID != req.ID))
                {
                    try
                    {
                        throw new ArgumentNullException("Thong tin danh sach xoa suat an khong thuoc ve y lenh dang xu ly");
                    }
                    catch (ArgumentNullException ex)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn(ex);
                        valid = false;
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
