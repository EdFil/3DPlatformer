using UnityEngine;
using System.Diagnostics;

[RequireComponent(typeof(Mover))]
public class PlayerController : MonoBehaviour
{
    private Mover _mover;

    void Awake() {
        _mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    void Update() {
        // Update Input
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");
        bool isJumpPressed = Input.GetAxisRaw("Jump") > 0.0f;

        _mover.Move(horizontalAxis, verticalAxis, isJumpPressed);

    }
}
