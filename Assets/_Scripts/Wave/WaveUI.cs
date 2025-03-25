using UnityEngine;

public class WaveUI : MonoBehaviour
{
    void StopAnimation()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isNextWave", false);
    }
}
