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
    void Start()
    {
        _anim.Play(_animation);
    }
}
