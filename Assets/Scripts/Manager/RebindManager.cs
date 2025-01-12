using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindManager : MonoBehaviour
{
    private static RebindManager instance;

    public static RebindManager Instance
    {
        get { return instance; }
    }

    public PlayerL playerL;
    public PlayerR playerR;
    public CanvasGroup canvasGroup;
    public GameObject arrowBox;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private void Awake()
    {
        if (instance is null)
            instance = this;

        playerL = new PlayerL();
        playerR = new PlayerR();
    }

    #region Player 1

    public void RebindMoveForwardL(GameObject button) => RebindCompositeKey(button, playerL.map.Move, "up");
    public void RebindMoveLeftL(GameObject button) => RebindCompositeKey(button, playerL.map.Move, "left");
    public void RebindMoveBackL(GameObject button) => RebindCompositeKey(button, playerL.map.Move, "down");
    public void RebindMoveRightL(GameObject button) => RebindCompositeKey(button, playerL.map.Move, "right");
    public void RebindAttackL(GameObject button) => RebindOneKey(button, playerL.map.Attack);
    public void RebindSkillL(GameObject button) => RebindOneKey(button, playerL.map.Skill);
    public void RebindInteractL(GameObject button) => RebindOneKey(button, playerL.map.Interact);
    public void RebindJumpL(GameObject button) => RebindOneKey(button, playerL.map.Jump);
    public void RebindRunL(GameObject button) => RebindOneKey(button, playerL.map.Run);

    #endregion

    #region Player 2

    public void RebindMoveLeftR(GameObject button) => RebindCompositeKey(button, playerR.map.Move, "left");
    public void RebindMoveRightR(GameObject button) => RebindCompositeKey(button, playerR.map.Move, "right");
    public void RebindMoveForwardR(GameObject button) => RebindCompositeKey(button, playerR.map.Move, "up");
    public void RebindMoveBackR(GameObject button) => RebindCompositeKey(button, playerR.map.Move, "down");
    public void RebindMoveRunR(GameObject button) => RebindOneKey(button, playerR.map.Run);
    public void RebindRunR(GameObject button) => RebindOneKey(button, playerR.map.Run);
    public void RebindAttackR(GameObject button) => RebindOneKey(button, playerR.map.Attack);
    public void RebindSkillR(GameObject button) => RebindOneKey(button, playerR.map.Skill);
    public void RebindInteractR(GameObject button) => RebindOneKey(button, playerR.map.Interact);
    public void RebindJumpR(GameObject button) => RebindOneKey(button, playerR.map.Jump);

    #endregion

    private void RebindCompositeKey(GameObject button, InputAction inputAction, string name)
    {
        TMP_Text text = button.transform.GetChild(1).GetComponent<TMP_Text>();

        arrowBox.transform.SetParent(button.transform);
        arrowBox.transform.localPosition = new Vector3(300f, 7f, 0f);
        arrowBox.SetActive(true);

        for (int i = 0; i < inputAction.bindings.Count; i++)
        {
            if (inputAction.bindings[i].isComposite && inputAction.bindings[i].name == "WASD")
            {
                Debug.Log($"Found 2DVector composite at index {i}");

                for (int j = i + 1; j < inputAction.bindings.Count && inputAction.bindings[j].isPartOfComposite; j++)
                {
                    if (inputAction.bindings[j].name == name)
                    {
                        playerL.map.Disable();
                        playerR.map.Disable();

                        rebindingOperation = inputAction.PerformInteractiveRebinding(j)
                        .WithExpectedControlType("Button")
                        .OnPotentialMatch(
                            operation =>
                            {
                                var control = operation.selectedControl;
                                if (
                                    control == null 
                                    || control.path == "/Keyboard/escape"
                                    || control.path == "/Keyboard/backspace"
                                )
                                    operation.Cancel();
                            }
                        )
                        .OnMatchWaitForAnother(0.1f)
                        .OnComplete(operation => RebindComplete(text, button))
                        .OnCancel(operation => ResetBind(button))
                        .Start();
                    }
                }
            }
        }
    }

    private void RebindOneKey(GameObject button, InputAction inputAction)
    {
        TMP_Text text = button.transform.GetChild(1).GetComponent<TMP_Text>();

        arrowBox.transform.SetParent(button.transform);
        arrowBox.transform.localPosition = new Vector3(300f, 7f, 0f);
        arrowBox.SetActive(true);

        playerL.map.Disable();
        playerR.map.Disable();

        rebindingOperation = inputAction.PerformInteractiveRebinding()
            .WithExpectedControlType("Button")
            .OnPotentialMatch(
                operation =>
                {
                    var control = operation.selectedControl;
                    if (
                        control == null
                        || control.path == "/Keyboard/escape"
                        || control.path == "/Keyboard/backspace"
                    )
                        operation.Cancel();
                }
            )
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(text, button))
            .OnCancel(operation => ResetBind(button))
            .Start();
    }

    private void RebindComplete(TMP_Text text, GameObject button)
    {
        string newBindingPath = rebindingOperation.selectedControl.path;

        Debug.Log($"Path: {rebindingOperation.selectedControl.path}");

        string keyName = InputControlPath.ToHumanReadableString(newBindingPath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        Debug.Log($"Key: {keyName}");
        text.text = keyName.Length == 1 ? keyName.ToUpper() : StringHandler.ToSentence(keyName);

        rebindingOperation.Dispose();
        rebindingOperation = null;

        playerL.map.Enable();
        playerR.map.Enable();

        ResetBind(button);
    }

    private void ResetBind(GameObject button)
    {
        button.GetComponent<Button>().interactable = true;
        arrowBox.SetActive(false);
        canvasGroup.blocksRaycasts = true;
    }
}
