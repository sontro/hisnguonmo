using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Update
{
    class HisDispenseHandlerUpdateCheck : BusinessBase
    {
        internal HisDispenseHandlerUpdateCheck()
            : base()
        {

        }

        internal HisDispenseHandlerUpdateCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool CheckValidData(HisDispenseUpdateSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.Id <= 0) throw new ArgumentNullException("data.Id");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
                if (data.DispenseTime <= 0) throw new ArgumentNullException("data.DispenseTime");
                if (data.Amount <= 0) throw new ArgumentNullException("data.Amount");
                if (!IsNotNullOrEmpty(data.MedicinePaties)) throw new ArgumentNullException("data.MedicinePaties");
                if (!IsNotNullOrEmpty(data.MedicineTypes) && !IsNotNullOrEmpty(data.MaterialTypes)) throw new ArgumentNullException("data.MedicineTypes && data.MaterialTypes");
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

        internal bool ValidMedicineType(List<HisDispenseMetySDO> medicineTypes)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(medicineTypes))
                {
                    foreach (HisDispenseMetySDO metySdo in medicineTypes)
                    {
                        if (metySdo == null) throw new ArgumentNullException("matySdo");
                        if (metySdo.MedicineTypeId <= 0) throw new ArgumentNullException("matySdo.MedicineTypeId");
                        if (metySdo.Amount <= 0) throw new ArgumentNullException("matySdo.Amount");
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

        internal bool ValidMaterialType(List<HisDispenseMatySDO> materialTypes)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(materialTypes))
                {
                    foreach (HisDispenseMatySDO matySdo in materialTypes)
                    {
                        if (matySdo == null) throw new ArgumentNullException("matySdo");
                        if (matySdo.MaterialTypeId <= 0) throw new ArgumentNullException("matySdo.MaterialTypeId");
                        if (matySdo.Amount <= 0) throw new ArgumentNullException("matySdo.Amount");
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

        internal bool ValidDispense(HisDispenseUpdateSDO data, ref HIS_DISPENSE dispense)
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

                if (dispense.IS_CONFIRM.HasValue && dispense.IS_CONFIRM.Value == Constant.IS_TRUE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDispense_PhieuBaoCheDaDuocXacNhan);
                    throw new Exception("Phieu bao che da duoc xac nhan");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckWorkPlace(HisDispenseUpdateSDO data, HIS_DISPENSE dispense)
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

        internal bool CheckExpMest(HIS_DISPENSE dispense, ref HIS_EXP_MEST expMest)
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
                if (raw.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
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
                expMest = raw;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckImpMest(HIS_DISPENSE dispense, ref HIS_IMP_MEST imppMest)
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

                if (raw.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST)
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
                imppMest = raw;
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
