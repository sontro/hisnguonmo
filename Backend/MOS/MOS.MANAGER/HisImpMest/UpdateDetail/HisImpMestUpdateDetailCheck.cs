using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UpdateDetail
{
    class HisImpMestUpdateDetailCheck : BusinessBase
    {
        internal HisImpMestUpdateDetailCheck()
            : base()
        {

        }

        internal HisImpMestUpdateDetailCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HisImpMestUpdateDetailSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.ImpMestId)) throw new ArgumentNullException("data.IMP_MEST_STT_ID");
                if (!IsGreaterThanZero(data.ReqestRoomId)) throw new ArgumentNullException("data.IMP_MEST_TYPE_ID");
                //if (!IsNotNullOrEmpty(data.ImpMestBloods) && !IsNotNullOrEmpty(data.ImpMestMaterials) && !IsNotNullOrEmpty(data.ImpMestMedicines)) throw new ArgumentNullException("data.ImpMestBloods && data.ImpMestMaterials && data.ImpMestMedicines");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool VerifyImpMestType(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (!HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(data.IMP_MEST_TYPE_ID))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Loai nhap cua phieu khong cho phep sua chi tiet");
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
