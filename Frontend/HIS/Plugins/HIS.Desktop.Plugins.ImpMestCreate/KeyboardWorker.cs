using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate
{
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.ImpMestCreate.UCImpMestCreate", "BtnAdd", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnUpdate", "HIS.Desktop.Plugins.ImpMestCreate.UCImpMestCreate", "BtnUpdate", KeyStroke = XKeys.Control | XKeys.U)]
    [KeyboardAction("BtnCancel", "HIS.Desktop.Plugins.ImpMestCreate.UCImpMestCreate", "BtnCancel", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("BtnPrint", "HIS.Desktop.Plugins.ImpMestCreate.UCImpMestCreate", "BtnPrint", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("BtnSave", "HIS.Desktop.Plugins.ImpMestCreate.UCImpMestCreate", "BtnSave", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("BtnSaveDraft", "HIS.Desktop.Plugins.ImpMestCreate.UCImpMestCreate", "BtnSaveDraft", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("BtnNew", "HIS.Desktop.Plugins.ImpMestCreate.UCImpMestCreate", "BtnNew", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("BtnImportExcel", "HIS.Desktop.Plugins.ImpMestCreate.UCImpMestCreate", "BtnImportExcel", KeyStroke = XKeys.Control | XKeys.I)]
    [KeyboardAction("FocusSearchPanel", "HIS.Desktop.Plugins.ImpMestCreate.UCImpMestCreate", "FocusSearchPanel", KeyStroke = XKeys.F2)]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public sealed class KeyboardWorker : Tool<DesktopToolContext>
    {
        public KeyboardWorker() : base() { }

        public override IActionSet Actions
        {
            get
            {
                return base.Actions;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
