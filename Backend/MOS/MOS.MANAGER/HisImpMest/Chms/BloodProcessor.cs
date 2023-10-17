using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisImpMestBlood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Chms
{
    class BloodProcessor : BusinessBase
    {
        private HisImpMestBloodCreate hisImpMestBloodCreate;

        internal BloodProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestBloodCreate = new HisImpMestBloodCreate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_EXP_MEST_BLOOD> expMestBloods, ref List<HIS_IMP_MEST_BLOOD> hisImpMestBloods)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMestBloods))
                {
                    List<HIS_IMP_MEST_BLOOD>  createList=new List<HIS_IMP_MEST_BLOOD>();

                    foreach (HIS_EXP_MEST_BLOOD expBlood in expMestBloods)
                    {
                        HIS_IMP_MEST_BLOOD impBlood = new HIS_IMP_MEST_BLOOD();
                        impBlood.BLOOD_ID = expBlood.BLOOD_ID;
                        impBlood.IMP_MEST_ID = impMest.ID;
                        createList.Add(impBlood);
                    }

                    if (!this.hisImpMestBloodCreate.CreateList(createList))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_BLOOD that bai");
                    }
                    hisImpMestBloods = createList;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestBloodCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
