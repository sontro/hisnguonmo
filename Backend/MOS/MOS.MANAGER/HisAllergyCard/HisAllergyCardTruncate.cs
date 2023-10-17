using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAllergenic;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAllergyCard
{
    partial class HisAllergyCardTruncate : BusinessBase
    {
        internal HisAllergyCardTruncate()
            : base()
        {

        }

        internal HisAllergyCardTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAllergyCardCheck checker = new HisAllergyCardCheck(param);
                HisAllergenicCheck allergenicChecker = new HisAllergenicCheck(param);
                HIS_ALLERGY_CARD raw = null;
                List<HIS_ALLERGENIC> allergenics = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.AllowUpdateOrDeleteLoginname(raw, ref allergenics);
                valid = valid && allergenicChecker.IsUnLock(allergenics);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    string sqlAllergyCard = String.Format("DELETE HIS_ALLERGY_CARD WHERE ID = {0}", id);
                    string sqlMety = String.Format("DELETE HIS_ALLERGENIC WHERE ALLERGY_CARD_ID = {0}", id);
                    sqls.Add(sqlMety);
                    sqls.Add(sqlAllergyCard);
                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Xoa HIS_ALLERGY_CARD, HIS_ALLERGENIC that bai");
                    }
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

        internal bool TruncateList(List<HIS_ALLERGY_CARD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAllergyCardCheck checker = new HisAllergyCardCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisAllergyCardDAO.TruncateList(listData);
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
    }
}
