using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    private float knockBackForce = 8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 parentPos = transform.parent.position;

            Vector2 direction = collision.gameObject.transform.position - parentPos;
            direction.Normalize();

            Vector2 force = direction * knockBackForce;

            AudioManager.Instance.PlaySoundEffect(SoundEffectType.SwordSlide);
            collision.gameObject.GetComponent<EmenyComtroller>().TakeDame(2f, force);
        }
    }
}
