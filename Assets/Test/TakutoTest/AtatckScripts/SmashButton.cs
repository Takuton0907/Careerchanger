using System.Collections;
using UnityEngine.UI;

public class SmashButton : AttackButton
{
    protected override void Start()
    {
        base.Start();
        m_myAttackMode = AttackMode.Smash;
        m_button = GetComponent<Button>();
    }

    public override void Able()
    {
        base.Able();
    }

    public override void Unable()
    {
        base.Unable();
    }

    protected override IEnumerator Recast()
    {
        yield return null;
    }
}