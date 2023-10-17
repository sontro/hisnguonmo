using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAnticipateBlty;
using MOS.MANAGER.HisAnticipateMaty;
using MOS.MANAGER.HisAnticipateMety;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAnticipate
{
    partial class HisAnticipateUpdate : BusinessBase
    {
        private List<HIS_ANTICIPATE> beforeUpdates = new List<HIS_ANTICIPATE>();

        internal HisAnticipateUpdate()
            : base()
        {

        }

        internal HisAnticipateUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ANTICIPATE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateCheck checker = new HisAnticipateCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ANTICIPATE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.VerifyValidData(data);
                if (valid)
                {
                    this.beforeUpdates.Add(raw);
                    List<HIS_ANTICIPATE_MATY> hisAnticipateMaties = data.HIS_ANTICIPATE_MATY != null ? data.HIS_ANTICIPATE_MATY.ToList() : null;
                    List<HIS_ANTICIPATE_METY> hisAnticipateMeties = data.HIS_ANTICIPATE_METY != null ? data.HIS_ANTICIPATE_METY.ToList() : null;
                    List<HIS_ANTICIPATE_BLTY> hisAnticipateBlties = data.HIS_ANTICIPATE_BLTY != null ? data.HIS_ANTICIPATE_BLTY.ToList() : null;


                    data.HIS_ANTICIPATE_MATY = null;//set null truoc khi goi update de tranh loi tang DAO
                    data.HIS_ANTICIPATE_METY = null;//set null truoc khi goi update de tranh loi tang DAO
                    data.HIS_ANTICIPATE_BLTY = null;//set null truoc khi goi update de tranh loi tang DAO
                    result = DAOWorker.HisAnticipateDAO.Update(data);
                    this.ProcessAnticipateMaty(data.ID, hisAnticipateMaties);
                    this.ProcessAnticipateMety(data.ID, hisAnticipateMeties);
                    this.ProcessAnticipateBlty(data.ID, hisAnticipateBlties);

                    data.HIS_ANTICIPATE_MATY = hisAnticipateMaties;
                    data.HIS_ANTICIPATE_METY = hisAnticipateMeties;
                    data.HIS_ANTICIPATE_BLTY = hisAnticipateBlties;

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessAnticipateMaty(long anticipateId, List<HIS_ANTICIPATE_MATY> hisAnticipateMaties)
        {
            List<HIS_ANTICIPATE_MATY> oldAnticipateMaties = new HisAnticipateMatyGet().GetByAnticipateId(anticipateId);
            if (IsNotNullOrEmpty(oldAnticipateMaties))
            {
                if (!new HisAnticipateMatyTruncate(param).TruncateList(oldAnticipateMaties))
                {
                    throw new Exception("Truncate du lieu HIS_ANTICIPATE_MATY that bai. Ket thuc nghiep vu.");
                }
            }

            if (IsNotNullOrEmpty(hisAnticipateMaties))
            {
                hisAnticipateMaties.ForEach(o => o.ANTICIPATE_ID = anticipateId);
                if (!new HisAnticipateMatyCreate(param).CreateList(hisAnticipateMaties))
                {
                    throw new Exception("Tao du lieu HIS_ANTICIPATE_MATY that bai. Ket thuc nghiep vu. ");
                }
            }
        }

        private void ProcessAnticipateMety(long anticipateId, List<HIS_ANTICIPATE_METY> hisAnticipateMeties)
        {
            List<HIS_ANTICIPATE_METY> oldAnticipateMeties = new HisAnticipateMetyGet().GetByAnticipateId(anticipateId);
            if (IsNotNullOrEmpty(oldAnticipateMeties))
            {
                if (!new HisAnticipateMetyTruncate(param).TruncateList(oldAnticipateMeties))
                {
                    throw new Exception("Truncate du lieu HIS_ANTICIPATE_METY that bai. Ket thuc nghiep vu.");
                }
            }

            if (IsNotNullOrEmpty(hisAnticipateMeties))
            {
                hisAnticipateMeties.ForEach(o => o.ANTICIPATE_ID = anticipateId);
                if (!new HisAnticipateMetyCreate(param).CreateList(hisAnticipateMeties))
                {
                    throw new Exception("Tao du lieu HIS_ANTICIPATE_METY that bai. Ket thuc nghiep vu. ");
                }
            }
        }

        private void ProcessAnticipateBlty(long anticipateId, List<HIS_ANTICIPATE_BLTY> hisAnticipateBlties)
        {
            List<HIS_ANTICIPATE_BLTY> oldAnticipateBlties = new HisAnticipateBltyGet().GetByAnticipateId(anticipateId);
            if (IsNotNullOrEmpty(oldAnticipateBlties))
            {
                if (!new HisAnticipateBltyTruncate(param).TruncateList(oldAnticipateBlties))
                {
                    throw new Exception("Truncate du lieu HIS_ANTICIPATE_BLTY that bai. Ket thuc nghiep vu.");
                }
            }

            if (IsNotNullOrEmpty(hisAnticipateBlties))
            {
                hisAnticipateBlties.ForEach(o => o.ANTICIPATE_ID = anticipateId);
                if (!new HisAnticipateBltyCreate(param).CreateList(hisAnticipateBlties))
                {
                    throw new Exception("Tao du lieu HIS_ANTICIPATE_BLTY that bai. Ket thuc nghiep vu. ");
                }
            }
        }

        internal bool UpdateList(List<HIS_ANTICIPATE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateCheck checker = new HisAnticipateCheck(param);
                List<HIS_ANTICIPATE> listRaw = new List<HIS_ANTICIPATE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdates.AddRange(listRaw);
                    result = DAOWorker.HisAnticipateDAO.UpdateList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdates))
            {
                if (!new HisAnticipateUpdate(param).UpdateList(this.beforeUpdates))
                {
                    LogSystem.Warn("Rollback du lieu HisAnticipate that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdates", this.beforeUpdates));
                }
            }
        }
    }
}
