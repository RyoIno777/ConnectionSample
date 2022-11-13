using ConnectionSample.Core.Enums;
using ConnectionSample.Core.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace ConnectionSample.Modules.ModuleName.ViewModels
{
    /// <summary>
    /// ステータス表示ビューモデルクラスです。
    /// </summary>
    public class ViewStatusViewModel : BindableBase
    {

        #region バインディング
        /// <summary>
        /// ステータステキスト
        /// </summary>
        public ReactivePropertySlim<string> StatusText { get; } = new();
        /// <summary>
        /// メッセージ
        /// </summary>
        public ReactiveProperty<string> Message { get; } = new();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// ステータス表示ビューモデルクラスを初期化します。
        /// </summary>
        /// <param name="applicationStatusInfo">アプリケーション状態クラス</param>
        public ViewStatusViewModel(IApplicationStatusInfo applicationStatusInfo)
        {
            // アプリケーション状態の購読
            applicationStatusInfo.GetApplicationStatus()
                .Where(x => x != ApplicationStatus.None).Subscribe(v => this.StatusText.Value = $"{v.GetDescription()}モード");

            // メッセージの購読
            applicationStatusInfo.GetMessage().Subscribe(message => this.Message.Value = message);
        }
        #endregion

    }
}
