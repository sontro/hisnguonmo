using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.ADO;

namespace EMR.Desktop.Plugins.SignedDocument.SignedDocument
{
    class SignedDocumentBehavior : Tool<IDesktopToolContext>, ISignedDocument
    {
        object[] entity;

        internal SignedDocumentBehavior()
            : base()
        { }

        internal SignedDocumentBehavior(Inventec.Core.CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ISignedDocument.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                EmrDocumentInfoADO ado = null;
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
                            if (entity[i] is EmrDocumentInfoADO)
                            {
                                ado = (EmrDocumentInfoADO)entity[i];
                            }
                        }
                    }
                }

                return new frmSignedDocument(moduleData,ado);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
