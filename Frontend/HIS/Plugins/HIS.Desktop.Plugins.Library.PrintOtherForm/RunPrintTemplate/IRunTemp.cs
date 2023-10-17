using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintOtherForm.RunPrintTemplate
{
    public interface IRunTemp
    {
        bool Run(SAR.EFMODEL.DataModels.SAR_PRINT_TYPE printTemplate, Dictionary<string, object> dicParamPlus, Dictionary<string, System.Drawing.Image> dicImagePlus, Inventec.Common.RichEditor.RichEditorStore richEditorMain, Inventec.Common.SignLibrary.ADO.InputADO emrInputADO);
    }
}
