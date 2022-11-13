using ConnectionSample.Core.Commands;
using ConnectionSample.Core.Enums;
using ConnectionSample.Core.Models;
using ConnectionSample.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Timers;

namespace ConnectionSample.Modules.ModuleName.ViewModels
{
    /// <summary>
    /// メインビューモデルクラスです。
    /// </summary>
    public class ViewMainViewModel : BindableBase
    {

        #region バインディング
        /// <summary>
        /// サーバー起動ボタン
        /// </summary>
        public ReactiveCommand StartupServerCommand { get; } = new();
        /// <summary>
        /// クライアント起動ボタン
        /// </summary>
        public ReactiveCommand StartupClientCommand { get; } = new();
        /// <summary>
        /// 停止ボタン
        /// </summary>
        public ReactiveCommand StopCommand { get; } = new();

        public ReactiveCommand<CommandMode> SendCommand { get; } = new();

        /// <summary>
        /// アプリケーションの状態
        /// </summary>
        public ReactivePropertySlim<ApplicationStatus> AppStatus { get; } = new();
        #endregion

        #region フィールド変数
        /// <summary>
        /// アプリケーション状態クラス
        /// </summary>
        private readonly IApplicationStatusInfo _applicationStatusInfo;
        #endregion

        /// <summary>
        /// メインビューモデルクラスを初期化します。
        /// </summary>
        /// <param name="applicationStatusInfo">アプリケーション状態クラス</param>
        /// <param name="serverService">サーバーサービスクラス</param>
        public ViewMainViewModel(
            IApplicationStatusInfo applicationStatusInfo,
            IServerService serverService,
            IClientService clientService)
        {

            this._applicationStatusInfo = applicationStatusInfo;

            #region 各種ボタン
            // サーバー起動ボタン
            StartupServerCommand.Subscribe(() =>
            {
                Debug.WriteLine("サーバーモードで起動");
                applicationStatusInfo.GetApplicationStatus().Value = ApplicationStatus.Server;
                serverService.StandByRecieve();
            });
            // クライアント起動ボタン
            StartupClientCommand.Subscribe(() =>
            {
                Debug.WriteLine("クライアントモードで起動");
                applicationStatusInfo.GetApplicationStatus().Value = ApplicationStatus.Clinent;
            });
            // 動作停止ボタン
            StopCommand.Subscribe(() =>
            {
                applicationStatusInfo.GetApplicationStatus().Value= ApplicationStatus.None;
            });
            // クライアント用のボタン
            SendCommand.Subscribe(commandMode =>
            {
                Debug.WriteLine($"{commandMode.GetDescription()}");
                // 送信先を指定して押されたボタンのコマンドを送信
                var command = new ApplicationCommand("127.0.0.1", commandMode);
                clientService.SendCommandAsync(command: command);
            });
            #endregion

            #region 各種購読
            // アプリケーションの状態を購読
            applicationStatusInfo.GetApplicationStatus()
                .Subscribe(applicationStatus => AppStatus.Value = applicationStatus);
            // コマンド受信時の購読
            applicationStatusInfo.GetApplicationCommand().Skip(1)
                .Subscribe(applicationCommand =>
            {
                var message = string.Empty;
                switch (applicationCommand.CommandMode)
                {
                    case CommandMode.Normal:
                        message = "ウィンドウを標準にするコマンドを受信しました。";
                        this.NormalCommand();
                        break;
                    case CommandMode.Min:
                        message = "ウィンドウを最小化するコマンドを受信しました。";
                        this.MinCommand();
                        break;
                    case CommandMode.Max:
                        message = "ウィンドウを最大化するコマンドを受信しました。";
                        this.MaxCommand();
                        break;
                    case CommandMode.Shutdown:
                        message = "シャットダウンするコマンドを受信しました。2秒後にアプリを終了します。";
                        this.ShutdownCommand();
                        break;
                }
                applicationStatusInfo.GetMessage().Value = message;
            });
            #endregion
        }

        #region プライベートメソッド
        /// <summary>
        /// ウィンドウを標準サイズに変更します。
        /// </summary>
        private void NormalCommand()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }), DispatcherPriority.Normal);
        }

        /// <summary>
        /// ウィンドウを最小化します。
        /// </summary>
        private void MinCommand()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }), DispatcherPriority.Normal);
        }

        /// <summary>
        /// ウィンドウを最大化します。
        /// </summary>
        private void MaxCommand()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }), DispatcherPriority.Normal);
        }

        /// <summary>
        /// アプリケーションを終了します。
        /// </summary>
        private void ShutdownCommand()
        {
            var timer = new System.Timers.Timer(2000);
            timer.Start();
            timer.Elapsed += (sender, args) =>
            {
                timer.Stop();
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Application.Current.Shutdown();
                }), DispatcherPriority.Normal);
            };
        }
        #endregion
    }
}
