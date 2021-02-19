using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            animator.SetBool("tabClicked", true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            animator.SetBool("tabClicked", false);
        }
    }
}
