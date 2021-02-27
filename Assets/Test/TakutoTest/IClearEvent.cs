
/// <summary> 花に当たった"瞬間"に実行したい内容にのみ継承してね </summary>
public interface IClearEvent
{
    /// <summary> レベルをクリアした時の処理 </summary>
    void Clear();
}
