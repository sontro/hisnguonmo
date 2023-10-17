using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMediStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Chms
{
    class HisImpMestChmsCheck : BusinessBase
    {
        internal HisImpMestChmsCheck()
            : base()
        {

        }

        internal HisImpMestChmsCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool VerifyRequireField(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!data.CHMS_EXP_MEST_ID.HasValue || !IsGreaterThanZero(data.CHMS_EXP_MEST_ID.Value)) throw new ArgumentNullException("data.CHMS_EXP_MEST_ID");
                if (!HisImpMestContanst.TYPE_CHMS_IDS.Contains(data.IMP_MEST_TYPE_ID))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Loai nhap IMP_MEST_TYPE_ID Invalid: " + data.IMP_MEST_TYPE_ID);
                }
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyExpMestId(long expMestId, ref HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                expMest = new HisExpMestGet().GetById(expMestId);
                if (expMest == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("CHMS_EXP_MEST_ID Invalid: " + expMestId);
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

        internal bool IsValidExpMestType(HIS_IMP_MEST data, HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (data.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__CK && expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisChmsImpMest_PhieuXuatTuongUngPhaiLaXuatChuyenKho);
                    return false;
                }
                if (data.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCS && expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_PhieuXuatTuongUngPhaiLaXuatBuCoSo);
                    return false;
                }
                if (data.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BL && expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_PhieuXuatTuongUngPhaiLaXuatBuLe);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidExpMestStt(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS && expMest.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuChuaThucXuat);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidImpMediStock(HIS_IMP_MEST data, HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (!expMest.IMP_MEDI_STOCK_ID.HasValue || expMest.IMP_MEDI_STOCK_ID != data.MEDI_STOCK_ID)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Phieu xuat chuyen kho khong co IMP_MEDI_STOCK_ID hoac IMP_MEDI_STOCK_ID != MEDI_STOCK_ID cua phieu nhap: " + expMest.IMP_MEDI_STOCK_ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExistsImpMest(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> impMests = new HisImpMestGet().GetByChmsExpMestId(expMest.ID);
                if (IsNotNullOrEmpty(impMests))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisChmsImpMest_PhieuXuatTuongUngDaCoPhieuNhap);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExistsMoba(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> impMests = new HisImpMestGet().GetByMobaExpMestId(expMest.ID);
                if (IsNotNullOrEmpty(impMests))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisChmsImpMest_DaTonTaiYeuCauThuHoi);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidMediStock(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (data.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCS)
                {
                    HIS_MEDI_STOCK mediStock = new HisMediStockGet().GetById(data.MEDI_STOCK_ID);
                    if (mediStock == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("MEDI_STOCK_ID Invalid: " + data.MEDI_STOCK_ID);
                    }
                    if (!mediStock.IS_CABINET.HasValue || mediStock.IS_CABINET.Value != MOS.UTILITY.Constant.IS_TRUE)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Kho khong phai la tu truc khong cho nhap bu co so" + LogUtil.TraceData("MediStock", mediStock));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool ValidData(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, ref List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, ref List<HIS_EXP_MEST_BLOOD> hisExpMestBloods)
        {
            bool valid = true;
            try
            {
                hisExpMestMedicines = new HisExpMestMedicineGet().GetExportedByExpMestId(expMest.ID);
                hisExpMestMaterials = new HisExpMestMaterialGet().GetExportedByExpMestId(expMest.ID);
                hisExpMestBloods = new HisExpMestBloodGet().GetExportedByExpMestId(expMest.ID);

                hisExpMestMedicines = hisExpMestMedicines != null ? hisExpMestMedicines.Where(o => !o.CK_IMP_MEST_MEDICINE_ID.HasValue).ToList() : null;
                hisExpMestMaterials = hisExpMestMaterials != null ? hisExpMestMaterials.Where(o => !o.CK_IMP_MEST_MATERIAL_ID.HasValue).ToList() : null;

                if (!IsNotNullOrEmpty(hisExpMestMaterials) && !IsNotNullOrEmpty(hisExpMestMedicines) && !IsNotNullOrEmpty(hisExpMestBloods))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_PhieuXuatChuaThucXuatHoacDaTonTaiPhieuNhapTuongUng);
                    return false;
                }
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
