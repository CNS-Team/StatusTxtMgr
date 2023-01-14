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
    public class StatusTxtMgrDemo : TerrariaPlugin
    {
        #region Plugin Infos
        public override string Name => "Status Text Manager Demo";
        public override Version Version => new Version("1.0.0");
        public override string Author => "LaoSparrow (Team CNS)";
        public override string Description => "Status Text Manager Demo";
        #endregion

        #region Initialize / Dispose
        public StatusTxtMgrDemo(Main game) : base(game)
        {
            Order = 1;
        }

        public override void Initialize()
        {
            /*
             * 参数 <updateInterval> 为 <handler> 被调用的间隔. 
             * <handler> 会在以下几种情况被调用:
             *     AccumulateTickCount % UpdateInterval == 0
             * "AccumulateTickCount" 每秒平均60次
             * 
             * 当 <updateInterval> == 5
             * 
             *     0 1 2 3 4 5 6 7 ...
             *     * - - - - * - - ...
             *     ^         ^
             * 
             * <handler> 会在第 0, 5 ... Tick 被调用
             * 
             * 
             * 例子:
             *     
             *     ``` StatusTxtMgr.Hooks.StatusTextUpdate.Register(OnStatusTextUpdate, --> 15 <--); ```
             *     <handler> 每 0.25秒 调用一次 ( 15 / 60 = 0.25 )
             *     
             *     ``` StatusTxtMgr.Hooks.StatusTextUpdate.Register(OnStatusTextUpdate, --> 180 <--); ```
             *     <handler> 每 3秒 调用一次 ( 180 / 60 = 3 )
             */

            StatusTxtMgr.StatusTxtMgr.Hooks.StatusTextUpdate.Register(OnStatusTextUpdate, 120); // update every 2s
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
            // 示例
            var sb = args.statusTextBuilder;
            sb.AppendLine("Hello World");
            sb.AppendLine("Hello Player " + args.tsplayer.Name);
            sb.AppendLine("Call Count: " + ++callCount);
        }
        #endregion
    }
}
