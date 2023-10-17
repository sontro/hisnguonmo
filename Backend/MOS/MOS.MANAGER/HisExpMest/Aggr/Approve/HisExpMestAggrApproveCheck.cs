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
using MOS.MANAGER.HisExpMestReason;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisAntibioticRequest;

namespace MOS.MANAGER.HisExpMest.Aggr.Approve
{
    partial class HisExpMestAggrApproveCheck : BusinessBase
    {
        internal HisExpMestAggrApproveCheck()
            : base()
        {

        }

        internal HisExpMestAggrApproveCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool IsAllowed(HisExpMestSDO data, ref HIS_EXP_MEST aggrExpMest, ref List<HIS_EXP_MEST> children, ref List<HIS_EXP_MEST_REASON> reasons)
        {
            try
            {
                HIS_EXP_MEST tmpAggr = new HisExpMestGet().GetById(data.ExpMestId);

                if (tmpAggr.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
                {
                    HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmpAggr.EXP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepDuyet, expMestStt.EXP_MEST_STT_NAME);
                    return false;
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != tmpAggr.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }

                List<HIS_EXP_MEST> tmpChildren = new HisExpMestGet().GetByAggrExpMestId(tmpAggr.ID);
                if (!IsNotNullOrEmpty(tmpChildren))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongCoDuLieuDuyet);
                    return false;
                }

                if (HisExpMestCFG.IS_REASON_REQUIRED)
                {
                    if (HisMediStockCFG.ODD_MEDICINE_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.NO_MANAGEMENT
                        || HisMediStockCFG.ODD_MEDICINE_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT)
                    {
                        HisExpMestReasonFilterQuery reasonFilter = new HisExpMestReasonFilterQuery();
                        reasonFilter.IS_ODD = true;
                        reasonFilter.IS_ACTIVE = Constant.IS_TRUE;
                        reasons = new HisExpMestReasonGet().Get(reasonFilter);
                        if (!IsNotNullOrEmpty(reasons))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_ChuaKhaiBaoLyDoXuatBuLe);
                            return false;
                        }
                    }

                    List<string> errorCodes = tmpChildren != null ? tmpChildren.Where(o => !o.EXP_MEST_REASON_ID.HasValue).Select(o => o.EXP_MEST_CODE).ToList() : null;
                    if (IsNotNullOrEmpty(errorCodes))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_MaPhieuXuatThieuThongTinLyDoXuat, string.Join(", ", errorCodes));
                        return false;
                    }
                }
                aggrExpMest = tmpAggr;
                children = tmpChildren;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }
    }
}
