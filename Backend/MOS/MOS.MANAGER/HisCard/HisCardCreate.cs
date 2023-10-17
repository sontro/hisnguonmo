using HID.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPatient;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MOS.MANAGER.HisCard
{
    partial class HisCardCreate : BusinessBase
    {
        private List<HIS_CARD> recentHisCards = new List<HIS_CARD>();

        internal HisCardCreate()
            : base()
        {

        }

        internal HisCardCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CARD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCardCheck checker = new HisCardCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisCardDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCard_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCard that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCards.Add(data);
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

        internal bool CreateOrUpdate(HIS_CARD data)
        {
            bool result = false;
            try
            {
                //Kiem tra xem da co du lieu the tren he thong chua
                HIS_CARD card = new HisCardGet().GetByCardCode(data.CARD_CODE);
                if (card != null)
                {
                    card.SERVICE_CODE = data.SERVICE_CODE;
                    card.PATIENT_ID = data.PATIENT_ID;
                    result = new HisCardUpdate().Update(card);
                }
                else
                {
                    result = this.Create(data);
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

        internal bool CreateList(List<HIS_CARD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCardCheck checker = new HisCardCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCardDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCard_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCard that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCards.AddRange(listData);
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisCards))
            {
                if (!DAOWorker.HisCardDAO.TruncateList(this.recentHisCards))
                {
                    LogSystem.Warn("Rollback du lieu HisCard that bai, can kiem tra lai." + LogUtil.TraceData("HisCards", this.recentHisCards));
                }
            }
        }
    }
}
