
/// <summary> 死んだ"瞬間"に実行したい内容にのみ継承してね </summary>
public interface IGameoverEvent
{
    /// <summary> プレイヤーが死んだ時の処理 </summary>
    void GameOver();
}
