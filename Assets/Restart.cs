using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] InputActionMap map;

    private void OnEnable()
    {
        map.actions[0].performed += ctx => RestartScene(ctx);
    }

    private void RestartScene(InputAction.CallbackContext ctx)
    {
        print(ctx.phase);
    }
}
