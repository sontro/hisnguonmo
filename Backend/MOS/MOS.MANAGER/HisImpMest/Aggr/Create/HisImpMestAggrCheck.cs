using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using MOS.MANAGER.Config;
using MOS.UTILITY;

namespace MOS.MANAGER.HisImpMest.Aggr
{
    public class HisImpMestAggrCheck : BusinessBase
    {
        internal HisImpMestAggrCheck()
            : base()
        {

        }

        internal HisImpMestAggrCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValid(HisImpMestAggrSDO data, ref List<HIS_IMP_MEST> impMests)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> listImpMest = new List<HIS_IMP_MEST>();
                if (!IsNotNullOrEmpty(data.ImpMestIds))
                {
                    LogSystem.Warn("ListImpMestIds rong");
                    return false;
                }
                HisImpMestCheck impMestChecker = new HisImpMestCheck(param);

                V_HIS_ROOM reqRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.RequestRoomId);
                if (reqRoom == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("RequestRoomId Invalid: " + data.RequestRoomId);
                    return false;
                }

                if (data.OddMediStockId.HasValue)
                {
                    V_HIS_MEDI_STOCK oddMediStock = Config.HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == data.OddMediStockId.Value);
                    if (oddMediStock == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("OddMediStockId invalid: " + data.OddMediStockId.Value);
                        return false;
                    }
                    if (oddMediStock.IS_ODD != MOS.UTILITY.Constant.IS_TRUE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_KhoKhongPhaiLaKhoThuocVatTuLe, oddMediStock.MEDI_STOCK_NAME);
                        return false;
                    }
                }

                foreach (var impMestId in data.ImpMestIds)
                {
                    HIS_IMP_MEST hisImpMest = null;
                    if (!impMestChecker.VerifyId(impMestId, ref hisImpMest))
                    {
                        LogSystem.Warn("ImpMestId Invalid: " + impMestId);
                        return false;
                    }
                    if (hisImpMest.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Phieu nhap khong o trang thai yeu cau. khong cho phep tong hop" + LogUtil.TraceData("ImpMest", hisImpMest));
                        return false;
                    }
                    if (hisImpMest.AGGR_IMP_MEST_ID.HasValue)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Phieu nhap da thuoc phieu tong hop khac. khong cho phep tong hop lai" + LogUtil.TraceData("ImpMest", hisImpMest));
                        return false;
                    }
                    if (hisImpMest.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL
                        && hisImpMest.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Chi cho phep tong hop voi don noi tru tra lai va don tu truc tra lai" + LogUtil.TraceData("ImpMest", hisImpMest));
                        return false;
                    }
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA != null ? HisMediStockCFG.DATA.Where(o => o.ID == hisImpMest.MEDI_STOCK_ID).FirstOrDefault() : null;
                    if (mediStock.IS_CABINET == Constant.IS_TRUE)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Khong cho phep tong hop doi voi phieu tra ve tu truc");
                        return false;
                    }

                    if (!hisImpMest.REQ_DEPARTMENT_ID.HasValue || hisImpMest.REQ_DEPARTMENT_ID.Value != reqRoom.DEPARTMENT_ID)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Ton tai phieu nhap tra lai 'con' khong co Khoa yeu cau hoac Khoa yeu cau khong dung voi khoa nguoi dung dang lam viec " + LogUtil.TraceData("RequestRoom", reqRoom) + LogUtil.TraceData("HisImpMest", hisImpMest));
                        return false;
                    }
                    listImpMest.Add(hisImpMest);
                }
                impMests = listImpMest;
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
