using ConnectionSample.Core.Enums;
using System.Text;

namespace ConnectionSample.Core.Commands
{
    /// <summary>
    /// コマンドクラスが継承するベースとなるクラスです。
    /// コマンドクラスを作成する場合はこのコマンドクラスを継承して作成してください。
    /// </summary>
    public class ApplicationCommand
    {

        #region プロパティ
        /// <summary>
        /// IPアドレス
        /// </summary>
        public string IPAddress { get; }
        /// <summary>
        /// コマンドモード
        /// </summary>
        public CommandMode CommandMode { get; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コマンドクラスを初期化します。
        /// </summary>
        /// <param name="ipAddress">IPアドレス</param>
        /// <param name="commandMode">コマンドモード</param>
        public ApplicationCommand(string ipAddress, CommandMode commandMode)
            => (IPAddress, CommandMode) = (ipAddress, commandMode);

        /// <summary>
        /// ログ出力用の文字列を返します。
        /// </summary>
        /// <returns>フォーマットされた文字列</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("送信先: ");
            sb.Append(IPAddress);
            sb.Append(", モード: ");
            sb.Append(CommandMode.GetDescription());
            return sb.ToString();
        }
        #endregion

    }
}
