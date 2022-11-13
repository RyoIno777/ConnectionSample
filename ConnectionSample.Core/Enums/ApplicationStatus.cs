using System.ComponentModel;

namespace ConnectionSample.Core.Enums
{
    /// <summary>
    /// アプリケーションの状態を表す列挙型です。
    /// </summary>
    public enum ApplicationStatus
    {
        /// <summary>
        /// 起動直後
        /// </summary>
        [Description("起動直後")]
        None = 0,
        /// <summary>
        /// サーバー
        /// </summary>
        [Description("サーバー")]
        Server = 1,
        /// <summary>
        /// クライアント
        /// </summary>
        [Description("クライアント")]
        Clinent = 2,
    }
}
