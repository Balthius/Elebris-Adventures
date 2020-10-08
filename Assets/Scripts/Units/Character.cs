using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Units;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    //we have the option of changing player scale to -1 when moving left and  1 when moving right in order to have a "player side" sprite transform.localScale
    //rather than a left and right, that can be reused 
    IUnitController _unitController;
    Rigidbody2D _rigidbody;
    Animator _animator;
    [SerializeField] private float speed = 5f;
   

    private Vector2 movementDirection;
    private Vector2 facingDirection;
    // Update is called once per frame
    private void Start()
    {
        _unitController = GetComponent<IUnitController>(); //find controller on this character, receives a normalized value
        _rigidbody = GetComponent<Rigidbody2D>(); //find controller on this character, receives a normalized value
        _animator = GetComponent<Animator>(); //find controller on this character, receives a normalized value
    }
    void Update()
    {

        _animator.SetFloat("Horizontal", facingDirection.x);
        _animator.SetFloat("Vertical", facingDirection.y);
        _animator.SetFloat("Speed", movementDirection.sqrMagnitude); //sqr version is more optimizied, using movement direction to access idle animation but lock facing
    
        movementDirection = _unitController.ReturnMovement();
        if(movementDirection.sqrMagnitude > 0.01)
        {
            facingDirection = movementDirection; //allows you to lock direction facing for skill casts etc
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + movementDirection * speed * Time.fixedDeltaTime);
    }
}
