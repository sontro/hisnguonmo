using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood
{
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.ImportBlood.UCImportBloodPlus", "BtnAdd", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnSave", "HIS.Desktop.Plugins.ImportBlood.UCImportBloodPlus", "BtnSave", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("BtnSaveDraft", "HIS.Desktop.Plugins.ImportBlood.UCImportBloodPlus", "BtnSaveDraft", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("BtnNew", "HIS.Desktop.Plugins.ImportBlood.UCImportBloodPlus", "BtnNew", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("BtnPrint", "HIS.Desktop.Plugins.ImportBlood.UCImportBloodPlus", "BtnPrint", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("BtnUpdate", "HIS.Desktop.Plugins.ImportBlood.UCImportBloodPlus", "BtnUpdate", KeyStroke = XKeys.Control | XKeys.U)]
    [KeyboardAction("BtnCancel", "HIS.Desktop.Plugins.ImportBlood.UCImportBloodPlus", "BtnCancel", KeyStroke = XKeys.Control | XKeys.Q)]
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
