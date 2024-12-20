using UnityEngine;
using UnityEngine.UI;

public class RebindButtonsL : MonoBehaviour
{
    Button MoveForward, MoveLeft, MoveBack, MoveRight, Attack, Skill, Interact, Jump, Run;

    private void Awake()
    {
        MoveForward = transform.parent.Find("Move Forward").GetComponent<Button>();
        MoveLeft = transform.parent.Find("Move Left").GetComponent<Button>();
        MoveBack = transform.parent.Find("Move Back").GetComponent<Button>();
        MoveRight = transform.parent.Find("Move Right").GetComponent<Button>();
        Attack = transform.parent.Find("Attack").GetComponent<Button>();
        Skill = transform.parent.Find("Skill").GetComponent<Button>();
        Interact = transform.parent.Find("Interact").GetComponent<Button>();
        Jump = transform.parent.Find("Jump").GetComponent<Button>();
        Run = transform.parent.Find("Run").GetComponent<Button>();
    }

    private void Start()
    {
        MoveForward.onClick.AddListener(() => RebindManager.Instance.RebindMoveForwardL(MoveForward.gameObject));
        MoveLeft.onClick.AddListener(() => RebindManager.Instance.RebindMoveLeftL(MoveLeft.gameObject));
        MoveBack.onClick.AddListener(() => RebindManager.Instance.RebindMoveBackL(MoveBack.gameObject));
        MoveRight.onClick.AddListener(() => RebindManager.Instance.RebindMoveRightL(MoveRight.gameObject));
        Attack.onClick.AddListener(() => RebindManager.Instance.RebindAttackL(Attack.gameObject));
        Skill.onClick.AddListener(() => RebindManager.Instance.RebindSkillL(Skill.gameObject));
        Interact.onClick.AddListener(() => RebindManager.Instance.RebindInteractL(Interact.gameObject));
        Jump.onClick.AddListener(() => RebindManager.Instance.RebindJumpL(Jump.gameObject));
        Run.onClick.AddListener(() => RebindManager.Instance.RebindRunL(Run.gameObject));
    }
}
