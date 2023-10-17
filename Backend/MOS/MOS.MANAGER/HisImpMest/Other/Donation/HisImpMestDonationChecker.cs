using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Other.Donation
{
    class HisImpMestDonationChecker : BusinessBase
    {
        internal HisImpMestDonationChecker()
            : base()
        {

        }

        internal HisImpMestDonationChecker(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HisImpMestDonationSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.DonationDetail == null) throw new ArgumentNullException("DonationDetail");
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

        internal bool VerifyBloodGiver(List<DonationDetailSDO> data)
        {
            bool valid = true;
            try
            {
                List<HIS_BLOOD_GIVER> bloodGivers = data.Select(s => s.BloodGiver).ToList();

                //trung ma trong 1 lan tao
                var group = bloodGivers.GroupBy(o => o.GIVE_CODE).ToList();
                if (group.Exists(e => e.Count() > 1))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisEkipTemp_TonTaiDuLieuTrungNhau);
                    return false;
                }

                //trung ma DB
                HisBloodGiver.HisBloodGiverCheck check = new HisBloodGiver.HisBloodGiverCheck(param);
                foreach (var item in bloodGivers)
                {
                    valid = valid && check.ExistsCode(item.GIVE_CODE, item.ID);
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
    }
}
