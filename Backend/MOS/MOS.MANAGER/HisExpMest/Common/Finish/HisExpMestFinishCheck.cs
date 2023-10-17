using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Finish
{
    class HisExpMestFinishCheck : BusinessBase
    {
        internal HisExpMestFinishCheck()
            : base()
        {

        }

        internal HisExpMestFinishCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsAllowFinish(HisExpMestSDO sdo, ref HIS_EXP_MEST raw, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            bool valid = true;
            try
            {
                HIS_EXP_MEST expMest = new HisExpMestGet().GetById(sdo.ExpMestId);

                if (expMest == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("ExpMestId Invalid");
                }

                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai la phieu xuat bu co so");
                }

                if (expMest.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE
                    || !expMest.IS_EXPORT_EQUAL_APPROVE.HasValue || expMest.IS_EXPORT_EQUAL_APPROVE.Value != Constant.IS_TRUE)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("trang thai phieu xuat khong chinh xac EXP_MEST_STT_ID Or IS_EXPORT_EQUAL_APPROVE");
                }

                V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == expMest.MEDI_STOCK_ID).FirstOrDefault();

                if (mediStock == null || mediStock.IS_ACTIVE != MOS.UTILITY.Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa);
                    return false;
                }

                List<HIS_EXP_MEST_MATERIAL> currentMaterials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                List<HIS_EXP_MEST_MEDICINE> currentMedicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);


                List<HIS_EXP_MEST_MATERIAL> materials = currentMaterials != null ? currentMaterials.Where(o => o.IS_EXPORT != Constant.IS_TRUE).ToList() : null;
                List<HIS_EXP_MEST_MEDICINE> medicines = currentMedicines != null ? currentMedicines.Where(o => o.IS_EXPORT != Constant.IS_TRUE).ToList() : null;

                if (IsNotNullOrEmpty(materials) || IsNotNullOrEmpty(medicines))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TonTaiDuLieuDuyetChuaDuocXuat);
                    throw new Exception("Ton tai du lieu duyet chua duoc xuat");
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(sdo.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != expMest.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }

                expMestMaterials = currentMaterials;
                expMestMedicines = currentMedicines;
                raw = expMest;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
