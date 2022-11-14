using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D playerRb;

    [Header("Movement and position variables")]
    [SerializeField] private float playerMaxSpeed; // TODO in the future will be sucked from game cotroller script
    private bool facingRight = true;
    Vector2 directionVector;

    [Header("Player tool related variables")]
    public int selectedTool = 0;

    void Start()
    {
        SelectTool();
    }


    void Update()
    {
        directionVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        SwitchTool();
    }

    private void FixedUpdate()
    {
       MovePlayer(directionVector);
       FlippingCoinditions();
    }

    #region MOVEMENT
    void MovePlayer(Vector2 direction)
    {
        playerRb.velocity = directionVector * playerMaxSpeed * Time.deltaTime ;
    }

    void FlippingCoinditions()
    {
        if(directionVector.x > 0 && !facingRight)
        {
            FlipCharacterSprite();
        }
        else if (directionVector.x <0 && facingRight)
        {
            FlipCharacterSprite();
        }
    }

    void FlipCharacterSprite()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

    }

    #endregion


    #region TOOLSWIYCHING

    void SelectTool()
    {
        int i = 0;
        foreach (Transform tool in transform)
        {
            if(i == selectedTool)
            {
                tool.gameObject.SetActive(true);
            }
            else
            {
                tool.gameObject.SetActive(false);
            }
            i++;
        }
    }

    void SwitchTool()
    {
        int previousSelectedTool = selectedTool;
        if (Input.GetButtonDown("Jump"))
        {
            if(selectedTool >= transform.childCount - 1)
            {
                selectedTool = 0;
            }
            else
            {
                selectedTool++;
            }
        }
        if (previousSelectedTool != selectedTool)
        {
            SelectTool();
        }
    }

    #endregion


}

