using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.UnConfirm
{
    class HisDispenseHandlerUnConfirmCheck : BusinessBase
    {
        internal HisDispenseHandlerUnConfirmCheck()
            : base()
        {

        }

        internal HisDispenseHandlerUnConfirmCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool CheckValidData(HisDispenseConfirmSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.Id <= 0) throw new ArgumentNullException("data.Id");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
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
            }
            return valid;
        }

        internal bool ValidDispense(HisDispenseConfirmSDO data, ref HIS_DISPENSE dispense)
        {
            bool valid = true;
            try
            {
                dispense = new HisDispenseGet().GetById(data.Id);
                if (dispense == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Khong lay duoc HIS_DISPENSE theo Id: " + data.Id);
                }

                if (!dispense.IS_CONFIRM.HasValue && dispense.IS_CONFIRM.Value != Constant.IS_TRUE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDispense_PhieuBaoCheChuaDuocXacNhan);
                    throw new Exception(" Phieu bao chec chua duoc xac nhan");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckWorkPlace(HisDispenseConfirmSDO data, HIS_DISPENSE dispense)
        {
            bool valid = true;
            try
            {
                WorkPlaceSDO sdo = TokenManager.GetWorkPlace(data.RequestRoomId);
                if (sdo == null || !sdo.MediStockId.HasValue || sdo.MediStockId.Value != dispense.MEDI_STOCK_ID)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    throw new Exception("Khong co thong tin phong lam viec. Hoac phong lam viec khong phai la Kho bao che" + LogUtil.TraceData("WorkPlace", sdo));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckExpMest(HIS_DISPENSE dispense, ref HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_EXP_MEST_MEDICINE> medicines)
        {
            bool valid = true;
            try
            {
                List<HIS_EXP_MEST> hisExpMests = new HisExpMestGet().GetByDispenseId(dispense.ID);
                if (hisExpMests == null || hisExpMests.Count != 1)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong lay duoc HIS_EXP_MEST theo DispenseId: " + dispense.ID);
                }
                HIS_EXP_MEST raw = hisExpMests[0];
                if (raw.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    string sttname = Config.HisExpMestSttCFG.DATA.FirstOrDefault(o => o.ID == raw.EXP_MEST_STT_ID).EXP_MEST_STT_NAME;

                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDangOTrangThai, sttname);
                    throw new Exception("Phieu xuat bao che khong o trang trang thai yeu cau. Khong cho phep sua: " + sttname);
                }
                if (raw.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCT)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Phieu xuat khong phai la xuat bao che. Kiem tra lai du lieu expMestId: " + raw.ID);
                }

                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new HisExpMestMaterialGet().GetByExpMestId(raw.ID);
                expMestMaterials = expMestMaterials != null ? expMestMaterials.Where(o => o.IS_EXPORT == Constant.IS_TRUE).ToList() : null;

                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new HisExpMestMedicineGet().GetByExpMestId(raw.ID);
                expMestMedicines = expMestMedicines != null ? expMestMedicines.Where(o => o.IS_EXPORT == Constant.IS_TRUE).ToList() : null;

                if (expMestMaterials != null && expMestMaterials.Exists(e => e.MEDI_STOCK_PERIOD_ID.HasValue))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DaDuocChotKyKhongChoPhepCapNhatHoacXoa);
                    throw new Exception("Phieu xuat da duoc chot ky");
                }
                if (expMestMedicines != null && expMestMedicines.Exists(e => e.MEDI_STOCK_PERIOD_ID.HasValue))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DaDuocChotKyKhongChoPhepCapNhatHoacXoa);
                    throw new Exception("Phieu xuat da duoc chot ky");
                }

                expMest = raw;
                materials = expMestMaterials;
                medicines = expMestMedicines;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckImpMest(HIS_DISPENSE dispense, ref HIS_IMP_MEST imppMest, ref HIS_IMP_MEST_MEDICINE medicine)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> hisImppMests = new HisImpMestGet().GetByDispenseId(dispense.ID);
                if (hisImppMests == null || hisImppMests.Count != 1)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong lay duoc HIS_IMP_MEST theo DispenseId: " + dispense.ID);
                }

                HIS_IMP_MEST raw = hisImppMests[0];

                if (raw.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT)
                {
                    string sttname = Config.HisImpMestSttCFG.DATA.FirstOrDefault(o => o.ID == raw.IMP_MEST_STT_ID).IMP_MEST_STT_NAME;

                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_PhieuNhapDangOTrangThai, sttname);
                    throw new Exception("Phieu nhap bao che khong o trang trang thai yeu cau. Khong cho phep sua: " + sttname);
                }
                if (raw.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCT)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Phieu nhap khong phai la nhap bao che. Kiem tra lai du lieu impMestId: " + raw.ID);
                }

                if (raw.MEDI_STOCK_PERIOD_ID.HasValue)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_DaDuocChotKyKhongChoPhepCapNhatHoacXoa);
                    throw new Exception("Phieu nhap da duoc chot ky");
                }

                List<HIS_IMP_MEST_MEDICINE> impMestMedicines = new HisImpMestMedicineGet().GetByImpMestId(raw.ID);
                if (impMestMedicines == null || impMestMedicines.Count != 1)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("So luong HIS_IMP_MEST_MEDICINE (thuoc bao che) khac 1");
                }

                imppMest = raw;
                medicine = impMestMedicines[0];
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
