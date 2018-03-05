using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth PlayerHealth;

    private Animator _anim;
    
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (PlayerHealth.CurrentHealth <= 0)
        {
            _anim.SetTrigger("GameOver");
        }
    }
}
