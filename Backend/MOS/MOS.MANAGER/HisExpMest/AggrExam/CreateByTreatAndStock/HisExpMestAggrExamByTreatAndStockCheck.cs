using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.UTILITY;

namespace MOS.MANAGER.HisExpMest.AggrExam.ByTreatAndStock
{
    class HisExpMestAggrExamByTreatAndStockCheck : BusinessBase
    {
        internal HisExpMestAggrExamByTreatAndStockCheck()
            : base()
        {
        }

        internal HisExpMestAggrExamByTreatAndStockCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool VerifyRequireField(AggrExamByTreatAndStockSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (string.IsNullOrWhiteSpace(data.TreatmentCode)) throw new ArgumentNullException("data.TreatmentCode");
                if (!IsGreaterThanZero(data.MediStockId)) throw new ArgumentNullException("data.MediStockId");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Error(ex);
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

        internal bool IsValidExpMest(HIS_TREATMENT treatment, long mediStockId, ref List<HIS_EXP_MEST> aggrs)
        {
            bool valid = true;
            try
            {
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.TDL_TREATMENT_ID = treatment.ID;
                filter.MEDI_STOCK_ID = mediStockId;
                filter.HAS_IS_NOT_TAKEN = false;
                if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                {
                    filter.EXP_MEST_TYPE_IDs = new List<long> { 
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL};
                }
                else
                {
                    filter.EXP_MEST_TYPE_IDs = new List<long> { 
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT};
                }

                aggrs = new HisExpMestGet().Get(filter);
                if (!IsNotNullOrEmpty(aggrs))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_KhongCoDonThuoc);
                    return false;
                }
                else
                {
                    if (aggrs.All(o => o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE))
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DonDaDuocPhat);
                        return false;
                    }
                    else if (aggrs.All(o => o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT))
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DonDaTuChoiPhat);
                        return false;
                    }
                    else if (aggrs.All(o => (o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT
                                        || o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)))
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DonDaDuocPhatHoacTuChoiPhat);
                        return false;
                    }

                    List<string> notTaken = aggrs.Where(o => o.IS_NOT_TAKEN == Constant.IS_TRUE).Select(s => s.EXP_MEST_CODE).ToList();
                    if (IsNotNullOrEmpty(notTaken))
                    {
                        string notTakenStr = string.Join(", ", notTaken);
                        MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDaDuocDanhDauKhongLay, notTakenStr);
                        return false;
                    }

                    List<HIS_EXP_MEST> expMestInTreat = aggrs.Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT).ToList();
                    if (IsNotNullOrEmpty(expMestInTreat))
                    {
                        List<long> aggrExpMestIds = expMestInTreat.Where(o => o.AGGR_EXP_MEST_ID.HasValue).Select(s => s.AGGR_EXP_MEST_ID.Value).Distinct().ToList();

                        if (IsNotNullOrEmpty(aggrExpMestIds))
                        {
                            List<long> ids = aggrs.Select(s => s.ID).ToList();
                            List<long> notExists = aggrExpMestIds.Where(o => !ids.Contains(o)).ToList();

                            //Phiếu lĩnh đã tổng hợp cùng hồ sơ khác thì bỏ qua
                            //bỏ chi tiết thuộc phiếu lĩnh khác
                            if (IsNotNullOrEmpty(notExists))
                            {
                                aggrs = aggrs.Where(o => !notExists.Contains(o.AGGR_EXP_MEST_ID ?? 0)).ToList();
                            }

                            //kiểm tra bỏ phiếu lĩnh và phiếu con trạng thái phiếu hoàn thành
                            if (IsNotNullOrEmpty(aggrs))
                            {
                                List<HIS_EXP_MEST> aggrHt = aggrs.Where(o => o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE && aggrExpMestIds.Contains(o.ID)).ToList();
                                if (IsNotNullOrEmpty(aggrHt))
                                {
                                    List<long> htIds = aggrHt.Select(s => s.ID).ToList();
                                    aggrs = aggrs.Where(o => !htIds.Contains(o.ID)).ToList();
                                    aggrs = aggrs.Where(o => !htIds.Contains(o.AGGR_EXP_MEST_ID ?? 0)).ToList();
                                }
                            }
                        }
                        else
                        {
                            //chỉ tạo phiếu tổng hợp cho 1 loại đơn
                            aggrs = expMestInTreat;
                        }

                        if (!IsNotNullOrEmpty(aggrs))
                        {
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_KhongCoDonThuoc);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidTreatmentCode(string treatmentCode, ref HIS_TREATMENT raw)
        {
            bool valid = true;
            try
            {
                if (!String.IsNullOrWhiteSpace(treatmentCode))
                {
                    raw = new HisTreatmentGet().GetByCode(treatmentCode);
                    if (raw == null)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_KhongTimThayThongTinHoSo);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsPause(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (!data.IS_PAUSE.HasValue || data.IS_PAUSE.Value != MOS.UTILITY.Constant.IS_TRUE)
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.TREATMENT_ID = data.ID;
                    filter.IS_MAIN_EXAM = true;
                    var mainReqs = new HisServiceReqGet().Get(filter);

                    V_HIS_EXECUTE_ROOM exeRoom = mainReqs != null ? HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == mainReqs.FirstOrDefault().EXECUTE_ROOM_ID) : null;
                    string exeRoomName = exeRoom != null ? exeRoom.EXECUTE_ROOM_NAME : "";
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_BenhNhanChuaKetThucDieuTriTaiPhongKham, exeRoomName);
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
