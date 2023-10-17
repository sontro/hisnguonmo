using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Export
{
    class HisExpMestInPresExportCheck : BusinessBase
    {
        internal HisExpMestInPresExportCheck()
            : base()
        {

        }

        internal HisExpMestInPresExportCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
            
        }

        internal bool IsAllowed(HisExpMestSDO data, ref HIS_EXP_MEST expMest)
        {
            try
            {
                HIS_EXP_MEST tmpExp = new HisExpMestGet().GetById(data.ExpMestId);

                if (tmpExp.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Khong phai don dieu tri");
                }

                if (tmpExp.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                {
                    HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmpExp.EXP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepThucXuat, expMestStt.EXP_MEST_STT_NAME);
                    return false;
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != tmpExp.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }
                expMest = tmpExp;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        internal bool IsValidCancellationOfExport(List<HIS_EXP_MEST_MATERIAL> materials)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }
    }
}
