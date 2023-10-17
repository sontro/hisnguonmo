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

namespace MOS.MANAGER.HisExpMest.AggrExam.Remove
{
    partial class HisExpMestAggrExamRemoveCheck : BusinessBase
    {
        internal HisExpMestAggrExamRemoveCheck()
            : base()
        {

        }

        internal HisExpMestAggrExamRemoveCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
            
        }

        internal bool IsAllowed(HisExpMestSDO data, ref HIS_EXP_MEST expMest)
        {
            try
            {
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    return false;
                }

                HIS_EXP_MEST tmp = new HisExpMestGet().GetById(data.ExpMestId);
                if (tmp == null || tmp.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ExpMestId ko hop le hoac loai ko phai la tong hop kham");
                    return false;
                }

                if (!tmp.AGGR_EXP_MEST_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongThuocPhieuLinh);
                    return false;
                }

                if (tmp.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
                {
                    HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmp.EXP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepChinhSua, expMestStt.EXP_MEST_STT_NAME);
                    return false;
                }

                if (workPlace.DepartmentId != tmp.REQ_DEPARTMENT_ID && workPlace.MediStockId != tmp.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoHoacKhoaYeuCau);
                    return false;
                }

                expMest = tmp;
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