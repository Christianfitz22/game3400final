using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class ChooseAnimation : MonoBehaviour
{
    
    // Start is called before the first frame update
    [SerializeField] private string _animation;
    private Animator _anim;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        
    }
    
    int Frame = 0;
    [SerializeField] [Range(1,120)] int randomOffset;
    private void FixedUpdate()
    {
        Frame++;
        if (Frame >= randomOffset)
        {
            _anim.Play(_animation);
        }
    }
}
