using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Units;

public class Character : MonoBehaviour
{
   
    private IUnitController _unitController;
    [SerializeField] private float speed = 5f;
    [SerializeField] Rigidbody2D _rigidbody;

    private Vector2 movementDirection;
    // Update is called once per frame
    private void Start()
    {
        _unitController = GetComponent<IUnitController>(); //find controller on this character
    }
    void Update()
    {
        movementDirection = _unitController.ReturnMovement();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + movementDirection * speed * Time.fixedDeltaTime);
    }
}
