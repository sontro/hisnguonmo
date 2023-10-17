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

namespace MOS.MANAGER.HisExpMest.Aggr.Unapprove
{
    partial class HisExpMestAggrUnapproveCheck : BusinessBase
    {
        internal HisExpMestAggrUnapproveCheck()
            : base()
        {

        }

        internal HisExpMestAggrUnapproveCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool IsAllowed(HisExpMestSDO data, ref HIS_EXP_MEST expMest)
        {
            try
            {
                var tmp = new HisExpMestGet().GetById(data.ExpMestId);
                if (tmp == null || tmp.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("exp_mest_id ko hop le hoac loai ko phai la phieu linh");
                    return false;
                }

                if (tmp.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                {
                    HIS_EXP_MEST_STT stt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmp.EXP_MEST_STT_ID).FirstOrDefault();
                    HIS_EXP_MEST_STT sttExec = HisExpMestSttCFG.DATA.Where(o => o.ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDangOTrangThaiKhongChoPhepTuChoiDuyet, stt.EXP_MEST_STT_NAME, sttExec.EXP_MEST_STT_NAME);
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
        internal bool IsExists(long expMestId, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<HIS_EXP_MEST_MATERIAL> materials)
        {
            bool result = true;
            try
            {
                materials = new HisExpMestMaterialGet().GetByAggrExpMestIdOrExpMestId(expMestId);
                medicines = new HisExpMestMedicineGet().GetByAggrExpMestIdOrExpMestId(expMestId);

                if (!IsNotNullOrEmpty(materials) && !IsNotNullOrEmpty(medicines))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongCoDuLieuDuyet);
                    result = false;
                }

                if (materials.Exists(o => o.IS_EXPORT == MOS.UTILITY.Constant.IS_TRUE) || medicines.Exists(o => o.IS_EXPORT == MOS.UTILITY.Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DaCoDuLieuThucXuat);
                    result = false;
                }
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
