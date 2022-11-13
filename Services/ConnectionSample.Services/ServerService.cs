using ConnectionSample.Core.Commands;
using ConnectionSample.Core.Consts;
using ConnectionSample.Core.Models;
using ConnectionSample.Services.Interfaces;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConnectionSample.Services
{

    /// <inheritdoc/>
    public class ServerService : IServerService
    {

        #region フィールド変数
        /// <summary>
        /// アプリケーション状態クラス
        /// </summary>
        private readonly IApplicationStatusInfo _applicationStatusInfo;
        /// <summary>
        /// TCPリスナー
        /// </summary>
        private TcpListener _listener;
        /// <summary>
        /// スタンバイフラグ
        /// </summary>
        private bool _isStandBy = true;
        /// <summary>
        /// 受信中フラグ
        /// </summary>
        private bool _isWaiting = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// サーバーサービスクラスを初期化します。
        /// </summary>
        /// <param name="applicationStatusInfo">アプリケーション状態クラス</param>
        public ServerService(IApplicationStatusInfo applicationStatusInfo)
            => this._applicationStatusInfo = applicationStatusInfo;
        #endregion

        #region パブリックメソッド
        /// <inheritdoc/>
        public async void StandByRecieve()
        {
            if (!_isWaiting)
            {
                // 現在待機中でなければポート番号を利用して受信待機状態にする
                this._isWaiting = true;
                this._listener = new TcpListener(IPAddress.Any, ApplicationConst.Port);
                this._listener.Start();
            }
            await Task.Run(async () =>
            {
                while (this._isStandBy)
                {
                    // バックグラウンドで常に受信状態とする
                    using (var client = await this._listener.AcceptTcpClientAsync())
                    using (var stream = client.GetStream())
                    {
                        // 受信したJSONをデシリアライズしApplicationCommandクラスにする
                        var applicationCommand = JsonSerializer.Deserialize<ApplicationCommand>(stream);
                        // アプリケーションコマンド用のReactivePropertyへ受信したコマンドをセット
                        // セットすることで各ビューモデルで購読しているところに変更通知される
                        this._applicationStatusInfo.GetApplicationCommand().Value = applicationCommand;
                    }
                }
            });
        }

        #endregion

    }
}
