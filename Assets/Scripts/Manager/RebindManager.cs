using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RebindManager : MonoBehaviour
{
    private static RebindManager instance;

    public static RebindManager Instance
    {
        get { return instance; }
    }

    public PlayerL playerL;
    public PlayerR playerR;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject arrowBox;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private void Awake()
    {
        if (instance is null)
            instance = this;

        playerL = new PlayerL();
        playerR = new PlayerR();
    }

    public void Initialize()
    {
        canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        arrowBox = GameObject.Find("ArrowBox").gameObject;
        arrowBox.SetActive(false);
    }

    #region Player 1

    public void RebindMoveLeftL() => RebindCompositeKey(playerL.map.Move, "left");
    public void RebindMoveRightL() => RebindCompositeKey(playerL.map.Move, "right");
    public void RebindMoveForwardL() => RebindCompositeKey(playerL.map.Move, "up");
    public void RebindMoveBackL() => RebindCompositeKey(playerL.map.Move, "down");
    public void RebindRunL() => RebindOneKey(playerL.map.Run);
    public void RebindAttackL() => RebindOneKey(playerL.map.Attack);
    public void RebindSkillL() => RebindOneKey(playerL.map.Skill);
    public void RebindInteractL() => RebindOneKey(playerL.map.Interact);
    public void RebindJumpL() => RebindOneKey(playerL.map.Jump);

    #endregion

    #region Player 2

    public void RebindMoveLeftR() => RebindCompositeKey(playerR.map.Move, "left");
    public void RebindMoveRightR() => RebindCompositeKey(playerR.map.Move, "right");
    public void RebindMoveForwardR() => RebindCompositeKey(playerR.map.Move, "up");
    public void RebindMoveBackR() => RebindCompositeKey(playerR.map.Move, "down");
    public void RebindMoveRunR() => RebindOneKey(playerR.map.Run);
    public void RebindRunR() => RebindOneKey(playerR.map.Run);
    public void RebindAttackR() => RebindOneKey(playerR.map.Attack);
    public void RebindSkillR() => RebindOneKey(playerR.map.Skill);
    public void RebindInteractR() => RebindOneKey(playerR.map.Interact);
    public void RebindJumpR() => RebindOneKey(playerR.map.Jump);

    #endregion

    private void RebindCompositeKey(InputAction inputAction, string name)
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
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

    private void RebindOneKey(InputAction inputAction)
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
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
