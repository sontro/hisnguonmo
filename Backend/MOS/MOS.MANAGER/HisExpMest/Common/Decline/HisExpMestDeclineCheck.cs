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

namespace MOS.MANAGER.HisExpMest.Common.Decline
{
    partial class HisExpMestDeclineCheck : BusinessBase
    {
        internal HisExpMestDeclineCheck()
            : base()
        {

        }

        internal HisExpMestDeclineCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool IsAllowed(HisExpMestSDO data, ref HIS_EXP_MEST expMest)
        {
            try
            {
                var tmp = new HisExpMestGet().GetById(data.ExpMestId);
                if (tmp == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("exp_mest_id ko hop le");
                    return false;
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != tmp.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }

                if (tmp.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
                {
                    HIS_EXP_MEST_STT stt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmp.EXP_MEST_STT_ID).FirstOrDefault();
                    HIS_EXP_MEST_STT sttReq = HisExpMestSttCFG.DATA.Where(o => o.ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDangOTrangThaiKhongChoPhepTuChoiDuyet, stt.EXP_MEST_STT_NAME, sttReq.EXP_MEST_STT_NAME);
                    return false;
                }

                if (tmp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL || tmp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("Loai phieu xuat la phieu tong hop (phieu linh). Khong duoc thuc hien chuc nang nay");
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

        /// <summary>
        /// Kiem tra xem da vuot qua so luong da duyet hay chua
        /// </summary>
        /// <param name="metyReqs"></param>
        /// <param name="matyReqs"></param>
        /// <param name="bltyReqs"></param>
        /// <returns></returns>
        internal bool IsNotApprovalAmountExceed(HisExpMestApproveSDO data, List<HIS_EXP_MEST_METY_REQ> metyReqs, List<HIS_EXP_MEST_MATY_REQ> matyReqs, List<HIS_EXP_MEST_BLTY_REQ> bltyReqs)
        {
            try
            {
                if (data != null)
                {
                    if (IsNotNullOrEmpty(data.Medicines))
                    {
                        foreach (HIS_EXP_MEST_METY_REQ mety in metyReqs)
                        {
                            data.Medicines.Where(o => o.MedicineTypeId == mety.MEDICINE_TYPE_ID).FirstOrDefault();
                        }
                    }
                }
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
