using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ImpMestViewDetail.ImpMestViewDetail;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.ImpMestViewDetail.ImpMestViewDetail
{
    class ImpMestViewDetailBehavior : BusinessBase, IImpMestViewDetail
    {
        object[] entity;

        internal ImpMestViewDetailBehavior(CommonParam param, object[] filter)
            : base()
        {
            entity = filter;
        }

        object IImpMestViewDetail.Run()
        {
            try
            {
                ImpMestViewDetailADO impMestViewDetailADO = null;
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                DelegateSelectData delegateSelectData = null;


                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            else if (entity[i] is ImpMestViewDetailADO)
                            {
                                impMestViewDetailADO = ((ImpMestViewDetailADO)entity[i]);
                            }
                            else if (entity[i] is DelegateSelectData)
                            {
                                delegateSelectData = ((DelegateSelectData)entity[i]);
                            }
                        }
                    }
                }

                return new frmImpMestViewDetail(impMestViewDetailADO.ImpMestId, impMestViewDetailADO.IMP_MEST_TYPE_ID, impMestViewDetailADO.ImpMestSttId, moduleData, delegateSelectData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
