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
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisImpMestMaterial;

namespace MOS.MANAGER.HisExpMest.Manu
{
    partial class HisExpMestManuCheck : BusinessBase
    {
        internal HisExpMestManuCheck()
            : base()
        {

        }

        internal HisExpMestManuCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
            
        }

        internal bool VerifyRequireField(HisExpMestManuSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.Materials) && !IsNotNullOrEmpty(data.Medicines) && !IsNotNullOrEmpty(data.Bloods)) throw new ArgumentNullException("data.Materials, data.Medicines, data.Bloods null");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId null");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
                if (data.ManuImpMestId <= 0) throw new ArgumentNullException("data.ManuImpMestId null");
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

        internal bool VerifyData(HisExpMestManuSDO data, ref HIS_IMP_MEST manuImpMest)
        {
            bool valid = true;
            try
            {
                HIS_IMP_MEST impMest = new HisImpMestGet().GetById(data.ManuImpMestId);
                if (impMest == null || impMest.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("imp_mest null hoac khong phai phieu xuat tra NCC");
                    return false;
                }
                if (impMest.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuNhapChuaThucNhap);
                    return false;
                }
                if (IsNotNullOrEmpty(data.Medicines))
                {
                    List<HIS_IMP_MEST_MEDICINE> impMestMedicines = new HisImpMestMedicineGet().GetByImpMestId(data.ManuImpMestId);

                    List<ExpMedicineSDO> invalidList = data.Medicines
                        .Where(o => impMestMedicines == null || !impMestMedicines.Exists(t => t.MEDICINE_ID == o.MedicineId))
                        .ToList();
                    if (IsNotNullOrEmpty(invalidList))
                    {
                        LogSystem.Warn("Yeu cau tra nha cung cap co chua cac medicine_id khong co trong phieu nhap tuong ung." + LogUtil.TraceData("invalidList", invalidList));
                        return false;
                    }
                }
                if (IsNotNullOrEmpty(data.Materials))
                {
                    List<HIS_IMP_MEST_MATERIAL> impMestMaterials = new HisImpMestMaterialGet().GetByImpMestId(data.ManuImpMestId);

                    List<ExpMaterialSDO> invalidList = data.Materials
                        .Where(o => impMestMaterials == null || !impMestMaterials.Exists(t => t.MATERIAL_ID == o.MaterialId))
                        .ToList();
                    if (IsNotNullOrEmpty(invalidList))
                    {
                        LogSystem.Warn("Yeu cau tra nha cung cap co chua cac Material_id khong co trong phieu nhap tuong ung." + LogUtil.TraceData("invalidList", invalidList));
                        return false;
                    }
                }
                manuImpMest = impMest;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsAllowed(HisExpMestManuSDO data)
        {
            try
            {
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != data.MediStockId)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
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
