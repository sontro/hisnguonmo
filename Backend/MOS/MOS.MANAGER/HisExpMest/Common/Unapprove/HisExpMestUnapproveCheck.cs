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

namespace MOS.MANAGER.HisExpMest.Common.Unapprove
{
    partial class HisExpMestUnapproveCheck : BusinessBase
    {
        internal HisExpMestUnapproveCheck()
            : base()
        {

        }

        internal HisExpMestUnapproveCheck(CommonParam paramCreate)
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

                if (tmp.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                {
                    HIS_EXP_MEST_STT stt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmp.EXP_MEST_STT_ID).FirstOrDefault();
                    HIS_EXP_MEST_STT sttExec = HisExpMestSttCFG.DATA.Where(o => o.ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDangOTrangThaiKhongChoPhepTuChoiDuyet, stt.EXP_MEST_STT_NAME, sttExec.EXP_MEST_STT_NAME);
                    return false;
                }

                if (tmp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL || tmp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("Loai phieu xuat la phieu tong hop (phieu linh). Khong duoc thuc hien chuc nang nay");
                    return false;
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != tmp.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
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
        /// Kiem tra xem phieu xuat co ton tai du lieu da duyet nhung chua thuc xuat hay khong
        /// </summary>
        /// <param name="expMestMedicines"></param>
        /// <param name="expMestMaterials"></param>
        /// <param name="expMestBloods"></param>
        /// <returns></returns>
        internal bool IsExists(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_EXP_MEST_BLOOD> bloods, ref List<HIS_EXP_MEST_MEDICINE> approveMedicines, ref List<HIS_EXP_MEST_MATERIAL> approveMaterials, ref List<HIS_EXP_MEST_BLOOD> approveBloods)
        {
            bool result = true;
            try
            {
                materials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                medicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                bloods = new HisExpMestBloodGet().GetByExpMestId(expMest.ID);

                approveMedicines = IsNotNullOrEmpty(medicines) ? medicines.Where(o => o.IS_EXPORT != MOS.UTILITY.Constant.IS_TRUE).ToList() : null;
                approveMaterials = IsNotNullOrEmpty(materials) ? materials.Where(o => o.IS_EXPORT != MOS.UTILITY.Constant.IS_TRUE).ToList() : null;
                approveBloods = IsNotNullOrEmpty(bloods) ? bloods.Where(o => o.IS_EXPORT != MOS.UTILITY.Constant.IS_TRUE).ToList() : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
