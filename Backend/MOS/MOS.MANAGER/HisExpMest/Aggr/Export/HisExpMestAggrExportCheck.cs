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

namespace MOS.MANAGER.HisExpMest.Aggr.Export
{
    partial class HisExpMestAggrExportCheck : BusinessBase
    {
        internal HisExpMestAggrExportCheck()
            : base()
        {

        }

        internal HisExpMestAggrExportCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
            
        }

        internal bool IsAllowed(HisExpMestSDO data, ref HIS_EXP_MEST aggrExpMest)
        {
            try
            {
                HIS_EXP_MEST tmpAggr = new HisExpMestGet().GetById(data.ExpMestId);

                if (tmpAggr.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                {
                    HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmpAggr.EXP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepThucXuat, expMestStt.EXP_MEST_STT_NAME);
                    return false;
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != tmpAggr.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }
                aggrExpMest = tmpAggr;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        internal bool Children(long aggrExpMestId, ref List<HIS_EXP_MEST> children)
        {
            try
            {
                //Chi lay cac phieu dang o trang thai da duyet
                //Muc dich: tranh truong hop bi loi du lieu, dan den trang thai phieu linh la đã hoàn thành nhưng vẫn
                //còn các phiếu con chưa hoàn thành thì chỉ cần vào DB cập nhật lại trạng thái của phiếu lĩnh rồi
                //thực hiện thực xuất lại.
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.AGGR_EXP_MEST_ID = aggrExpMestId;
                filter.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;
                children = new HisExpMestGet().Get(filter);
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
