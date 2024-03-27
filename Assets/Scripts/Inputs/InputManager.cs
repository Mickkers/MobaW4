using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public PlayerInputActions.GameplayActions gameplay;
    private CharSelect cSelect;

    private PlayerMovement playerMovement;
    private PlayerAction playerAction;
    private CamManager camManager;

    private MjlSkill mjlSkill;

    // Start is called before the first frame update
    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        camManager = FindObjectOfType(typeof(CamManager)) as CamManager;
        gameplay = playerInputActions.Gameplay;
        playerMovement = GetComponent<PlayerMovement>();
        playerAction = GetComponent<PlayerAction>();
        cSelect = FindObjectOfType(typeof(CharSelect)) as CharSelect;

        gameplay.CameraLock.performed += ctx => camManager.CameraLock();
        //gameplay.BasicAttack.performed += ctx => playerMovement.MoveToEnemy(Mouse.current.position.ReadValue());
        gameplay.Ultimate.performed += ctx => playerAction.Ultimate();
        gameplay.Skill.performed += ctx => playerAction.Skill();
        if(cSelect.pOneSelection == EnumCharType.Ranged)
        {
            mjlSkill = GetComponent<MjlSkill>();
            gameplay.BasicAttack.performed += ctx => mjlSkill.SkillAction();
        }
    }

    private void OnEnable()
    {
        gameplay.Enable();
    }

    private void OnDisable()
    {
        gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameplay.Move.IsPressed())
        {
            playerMovement.Move(gameplay.Move.ReadValue<Vector2>());
        }
    }

    public static Vector2 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }
}
