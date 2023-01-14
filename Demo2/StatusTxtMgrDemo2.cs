using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;

namespace Demo
{
    [ApiVersion(2, 1)]
    public class StatusTxtMgrDemo2 : TerrariaPlugin
    {
        #region Plugin Infos
        public override string Name => "Status Text Manager Demo2";
        public override Version Version => new Version("1.0.0");
        public override string Author => "LaoSparrow (Team CNS)";
        public override string Description => "Status Text Manager Demo2";
        #endregion

        #region Initialize / Dispose
        public StatusTxtMgrDemo2(Main game) : base(game)
        {
            Order = 1;
        }

        public override void Initialize()
        {
            StatusTxtMgr.StatusTxtMgr.Hooks.StatusTextUpdate.Register(OnStatusTextUpdate, 30);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StatusTxtMgr.StatusTxtMgr.Hooks.StatusTextUpdate.Deregister(OnStatusTextUpdate);
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Hooks
        private ulong callCount = 0;
        private void OnStatusTextUpdate(StatusTxtMgr.StatusTextUpdateEventArgs args)
        {
            var sb = args.statusTextBuilder;
            sb.AppendLine("Hello World2");
            sb.AppendLine("Hello Player " + args.tsplayer.Name);
            sb.AppendLine("Call Count: " + ++callCount);
        }
        #endregion
    }
}
