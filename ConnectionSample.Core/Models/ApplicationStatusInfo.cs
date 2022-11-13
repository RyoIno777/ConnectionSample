using ConnectionSample.Core.Commands;
using ConnectionSample.Core.Enums;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSample.Core.Models
{

    /// <inheritdoc/>
    public class ApplicationStatusInfo : IApplicationStatusInfo
    {

        #region フィールド変数
        /// <summary>
        /// アプリケーション状態
        /// </summary>
        private ReactivePropertySlim<ApplicationStatus> _applicationStatus = new();
        /// <summary>
        /// メッセージ
        /// </summary>
        private ReactivePropertySlim<string> _message = new();
        /// <summary>
        /// アプリケーションコマンド
        /// </summary>
        private ReactivePropertySlim<ApplicationCommand> _applicationCommand = new();
        #endregion

        #region パブリックメソッド
        /// <inheritdoc/>
        public ReactivePropertySlim<ApplicationStatus> GetApplicationStatus() => _applicationStatus;
        /// <inheritdoc/>
        public ReactivePropertySlim<string> GetMessage() => _message;
        /// <inheritdoc/>
        public ReactivePropertySlim<ApplicationCommand> GetApplicationCommand() => _applicationCommand;
        #endregion

    }

}
