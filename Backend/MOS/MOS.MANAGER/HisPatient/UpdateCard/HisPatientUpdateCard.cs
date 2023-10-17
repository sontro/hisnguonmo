using COS.SDO;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisCard;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatient.UpdateCard
{
    class HisPatientUpdateCard : BusinessBase
    {
        private HisCardUpdate hisCardUpdate;
        private HisCardCreate hisCardCreate;

        internal HisPatientUpdateCard()
            : base()
        {
            this.Init();
        }

        internal HisPatientUpdateCard(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisCardCreate = new HisCardCreate(param);
            this.hisCardUpdate = new HisCardUpdate(param);
        }

        internal bool Run(HisPatientUpdateCardSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_PATIENT raw = null;
                HIS_CARD card = null;
                CardOwnerSDO cardSdo = null;

                HisPatientUpdateCardCheck checker = new HisPatientUpdateCardCheck(param);
                HisPatientCheck commonChecker = new HisPatientCheck(param);
                HisCardCheck cardChecker = new HisCardCheck(param);

                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.PatientId, ref raw);
                valid = valid && commonChecker.IsUnLock(raw);
                valid = valid && checker.VerifyCosCard(data.CardCode, ref cardSdo);
                valid = valid && checker.VerifySameInfo(raw, cardSdo);
                if (valid)
                {
                    card = new HisCardGet().GetByCardCode(data.CardCode);
                    if (card != null)
                    {
                        card.PATIENT_ID = raw.ID;

                        if (!this.hisCardUpdate.Update(card))
                        {
                            throw new Exception("hisCardUpdate. Ket thuc nghiep vu");
                        }
                    }
                    else
                    {
                        card = new HIS_CARD();
                        card.PATIENT_ID = raw.ID;
                        card.SERVICE_CODE = cardSdo.ServiceCode;
                        card.BANK_CARD_CODE = cardSdo.BankCardCode;
                        card.CARD_CODE = cardSdo.CardCode;
                        if (!this.hisCardCreate.Create(card))
                        {
                            throw new Exception("hisCardCreate. Ket thuc nghiep vu");
                        }
                    }

                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisPatient_CapNhatTheKhamChuaBenh, card.CARD_CODE)
                           .PatientCode(raw.PATIENT_CODE)
                           .Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
