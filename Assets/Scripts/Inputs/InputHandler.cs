using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        //Debug.Log(playerInput.playerIndex);
        if(playerInput.playerIndex == 0)
        {
            player = PlayerController.PlayerOne;
        }
        else
        {
            player = PlayerController.PlayerTwo;
        }
        //playerInput.camera = player.GetCam();
    }

    public void OnMove(CallbackContext ctx)
    {
        if (player != null)
        {
            player.Move(ctx.ReadValue<Vector2>());
        }
    }
    public void OnBasic(CallbackContext ctx)
    {
        if (player != null && ctx.started)
        {
            player.Basic();
        }
    }
    public void OnSkill(CallbackContext ctx)
    {
        if (player != null && ctx.started)
        {
            player.Skill();
        }
    }
    public void OnUltimate(CallbackContext ctx)
    {
        if (player != null && ctx.started)
        {
            player.Ultimate();
        }
    }
}
