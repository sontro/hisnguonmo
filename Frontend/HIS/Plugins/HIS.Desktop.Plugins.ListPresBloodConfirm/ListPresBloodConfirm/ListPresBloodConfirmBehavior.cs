﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.ListPresBloodConfirm.ListPresBloodConfirm
{
    class ListPresBloodConfirmBehavior : Tool<IDesktopToolContext>, IListPresBloodConfirm
    {
        object[] entity;
        internal ListPresBloodConfirmBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IListPresBloodConfirm.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long impMestType = 0;
                MobaImpMestListADO mobaImpMestListADO = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        if (entity[i] is long)
                        {
                            impMestType = (long)entity[i];
                        }
                        if (entity[i] is MobaImpMestListADO)
                        {
                            mobaImpMestListADO = (MobaImpMestListADO)entity[i];
                        }
                    }
                }
                if (moduleData != null && mobaImpMestListADO == null)
                {
                    return new UCListPresBloodConfirm(moduleData);
                }
                else if (moduleData != null && mobaImpMestListADO != null)
                {
                    return new frmHisImportMestMedicine(moduleData, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__TH, mobaImpMestListADO);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}