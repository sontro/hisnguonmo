using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisServiceReq;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMaterial.IsUsed
{
	partial class HisExpMestMaterialUpdateIsUsed : BusinessBase
	{
		internal HisExpMestMaterialUpdateIsUsed()
			: base()
		{

		}

        internal HisExpMestMaterialUpdateIsUsed(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Used(long expMestMaterialId, ref HIS_EXP_MEST_MATERIAL resultData)
		{
			bool result = false;
			try
			{
				HIS_EXP_MEST_MATERIAL expMestMaterial = null;
                HisExpMestMaterialCheck expMestMaterialCheck = new HisExpMestMaterialCheck(param);
                HisExpMestMaterialUpdateIsUsedCheck checker = new HisExpMestMaterialUpdateIsUsedCheck(param);
                bool valid = true;

                valid = valid && expMestMaterialCheck.VerifyId(expMestMaterialId, ref expMestMaterial);
                valid = valid && checker.IsExported(expMestMaterial);

                if (valid)
				{
                    expMestMaterial.IS_USED = Constant.IS_TRUE;

                    if (DAOWorker.HisExpMestMaterialDAO.Update(expMestMaterial))
					{
						result = true;
                        resultData = expMestMaterial;
					}
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

        internal bool Unused(long expMestMaterialId, ref HIS_EXP_MEST_MATERIAL resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST_MATERIAL expMestMaterial = null;
                HisExpMestMaterialCheck expMestMaterialCheck = new HisExpMestMaterialCheck(param);
                bool valid = true;

                valid = valid && expMestMaterialCheck.VerifyId(expMestMaterialId, ref expMestMaterial);

                if (valid)
                {
                    Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                    HIS_EXP_MEST_MATERIAL old = Mapper.Map<HIS_EXP_MEST_MATERIAL>(expMestMaterial);
                    expMestMaterial.IS_USED = null;

                    if (DAOWorker.HisExpMestMaterialDAO.Update(expMestMaterial))
                    {
                        result = true;
                        resultData = expMestMaterial;
                    }
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
