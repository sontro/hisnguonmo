using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmployee;
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

namespace MOS.MANAGER.HisDispense.Packing.Delete
{
    class HisPackingTruncateCheck : BusinessBase
    {
        internal HisPackingTruncateCheck()
            : base()
        {

        }

        internal HisPackingTruncateCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool CheckValidData(HisPackingSDO data)
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

        internal bool ValidDispense(HisPackingSDO data, ref HIS_DISPENSE dispense)
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
                    throw new Exception("Phieu dong goi da duoc xac nhan");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckWorkPlace(HisPackingSDO data, HIS_DISPENSE dispense)
        {
            bool valid = true;
            try
            {
                WorkPlaceSDO sdo = TokenManager.GetWorkPlace(data.RequestRoomId);
                if (sdo == null || !sdo.MediStockId.HasValue || sdo.MediStockId.Value != dispense.MEDI_STOCK_ID)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    throw new Exception("Khong co thong tin phong lam viec. Hoac phong lam viec khong phai la Kho dong goi" + LogUtil.TraceData("WorkPlace", sdo));
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
                    throw new Exception("Phieu xuat dong goi khong o trang trang thai yeu cau. Khong cho phep sua: " + sttname);
                }
                if (raw.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCT)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Phieu xuat khong phai la xuat dong goi. Kiem tra lai du lieu expMestId: " + raw.ID);
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
                    throw new Exception("Phieu nhap dong goi khong o trang trang thai yeu cau. Khong cho phep sua: " + sttname);
                }
                if (raw.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCT)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Phieu nhap khong phai la nhap dong goi. Kiem tra lai du lieu impMestId: " + raw.ID);
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

        internal bool CheckPermission(HIS_DISPENSE dispense)
        {
            bool valid = true;
            try
            {
                string loginName = ResourceTokenManager.GetLoginName();
                if (string.IsNullOrWhiteSpace(loginName) || (!loginName.Equals(dispense.CREATOR) && !HisEmployeeUtil.IsAdmin()))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDoNguoiKhacTaoKhongChoPhepXoa);
                    throw new Exception("Du lieu do nguoi khac tao, ko cho phep xoa" + LogUtil.TraceData("dispense", dispense));
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
    }
}
