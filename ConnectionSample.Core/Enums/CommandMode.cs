using System.ComponentModel;

namespace ConnectionSample.Core.Enums
{
    /// <summary>
    /// コマンドのモードを表す列挙型です。
    /// </summary>
    public enum CommandMode
    {
        /// <summary>
        /// ウィンドウ標準
        /// </summary>
        [Description("ウィンドウ標準")]
        Normal,
        /// <summary>
        /// ウィンドウ最小化
        /// </summary>
        [Description("ウィンドウ最小化")]
        Min,
        /// <summary>
        /// ウィンドウ最大化
        /// </summary>
        [Description("ウィンドウ最大化")]
        Max,
        /// <summary>
        /// 操作コマンド
        /// </summary>
        [Description("シャットダウン")]
        Shutdown,
    }
}
