namespace ConnectionSample.Services.Interfaces
{
    /// <summary>
    /// サーバー用のサービスクラスです。
    /// </summary>
    public interface IServerService
    {
        /// <summary>
        /// 受信待機します。
        /// </summary>
        void StandByRecieve();
    }
}
