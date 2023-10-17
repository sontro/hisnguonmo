using AutoMapper;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;
using MOS.MANAGER.Token;
using MOS.MANAGER.AosAccount;
using AOS.SDO;
using Inventec.Core;

namespace MOS.MANAGER.HisPatient
{
    partial class HisPatientBalance : GetBase
    {
        internal HisPatientBalance()
            : base()
        {

        }

        internal HisPatientBalance(CommonParam param)
            : base(param)
        {

        }

        public enum CardFilterOption
        {
            //Khong xu ly loc
            NONE,
            //Loc thong tin the theo ma mem (service-code)
            SERVICE_CODE,
            //Loc thong tin the theo cac so cuoi cua so the ngan hang (bank-card-code)
            LAST_DIGITS_OF_BANK_CARD_CODE
        }

        /// <summary>
        /// Cap nhat va lay thong tin so du cua the moi nhat tuong ung voi benh nhan
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        internal decimal? GetAndUpdateCardBalance(long patientId, string lastDigitsOfBankCardCode)
        {
            HIS_BRANCH branch = new TokenManager().GetBranch();

            if (branch != null && !string.IsNullOrWhiteSpace(branch.THE_BRANCH_CODE) && patientId > 0)
            {
                List<HIS_CARD> cards = new HisCardGet().GetByPatientId(patientId);
                if (IsNotNullOrEmpty(cards))
                {
                    HIS_CARD card = cards
                        .Where(o => o.IS_ACTIVE == Constant.IS_TRUE && o.SERVICE_CODE != null
                            && (string.IsNullOrWhiteSpace(lastDigitsOfBankCardCode) 
                            || (o.BANK_CARD_CODE != null && o.BANK_CARD_CODE.EndsWith(lastDigitsOfBankCardCode)))).OrderByDescending(o => o.ID).FirstOrDefault();

                    if (card != null && !string.IsNullOrWhiteSpace(card.SERVICE_CODE))
                    {
                        return this.GetAndUpdateCardBalance(card.SERVICE_CODE, branch.THE_BRANCH_CODE);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Cap nhat va lay thong tin so du cua the tuong ung voi "ma mem" (serviceCode) truyen vao
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        internal decimal? GetAndUpdateCardBalance(string cardServiceCode, string theBranchCode)
        {
            if (!string.IsNullOrWhiteSpace(cardServiceCode) != null && !string.IsNullOrWhiteSpace(theBranchCode))
            {
                AosGetBalanceInfoSDO balanceInfo = new AosAccountUpdate().UpdateBankBalance(cardServiceCode, theBranchCode);
                return balanceInfo != null ? balanceInfo.BankBalance : null;
            }
            return null;
        }

        /// <summary>
        /// Lay thong tin so du cua the moi nhat tuong ung voi benh nhan
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="theBranchCode"></param>
        /// <param name="hisCard"></param>
        /// <returns></returns>
        internal decimal? GetCardBalance(long patientId, ref string theBranchCode, ref HIS_CARD hisCard)
        {
            try
            {
                return this.GetCardBalance(patientId, null, CardFilterOption.NONE, ref theBranchCode, ref hisCard);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        /// <summary>
        /// Lay thong tin so du dua vao ma mem the (quet vao dau doc) hoac cac so cuoi cua so the ngan hang
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="usingCardServiceCode"></param>
        /// <param name="theBranchCode"></param>
        /// <param name="hisCard"></param>
        /// <returns></returns>
        internal decimal? GetCardBalance(long patientId, string cardFilterString, CardFilterOption cardFilterOption, ref string theBranchCode, ref HIS_CARD hisCard)
        {
            try
            {
                List<HIS_CARD> cards = new HisCardGet().GetByPatientId(patientId);
                if (IsNotNullOrEmpty(cards))
                {
                    HIS_CARD card = cards.Where(o => o.IS_ACTIVE == Constant.IS_TRUE
                        && o.SERVICE_CODE != null
                        && (cardFilterOption == CardFilterOption.NONE ||
                            cardFilterString == null ||
                            (cardFilterOption == CardFilterOption.SERVICE_CODE && o.SERVICE_CODE == cardFilterString) ||
                            (cardFilterOption == CardFilterOption.LAST_DIGITS_OF_BANK_CARD_CODE && o.BANK_CARD_CODE != null && o.BANK_CARD_CODE.EndsWith(cardFilterString))
                        )).OrderByDescending(o => o.ID).FirstOrDefault();
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    if (card != null && branch != null
                        && !string.IsNullOrWhiteSpace(branch.THE_BRANCH_CODE)
                        && !string.IsNullOrWhiteSpace(card.SERVICE_CODE))
                    {
                        theBranchCode = branch.THE_BRANCH_CODE;
                        hisCard = card;
                        return this.GetCardBalance(card.SERVICE_CODE, theBranchCode);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        /// <summary>
        /// Chi lay thong tin so du cua the
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <param name="theBranchCode"></param>
        /// <returns></returns>
        private decimal? GetCardBalance(string cardServiceCode, string theBranchCode)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(cardServiceCode) && !string.IsNullOrWhiteSpace(theBranchCode))
                {
                    AosGetBalanceInfoSDO balanceInfo = new AosAccountGet().GetBalanceInfo(cardServiceCode, theBranchCode);
                    return balanceInfo != null ? balanceInfo.BankBalance : null;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}