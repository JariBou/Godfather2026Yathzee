using _project.Scripts.Die;
using UnityEngine;

namespace _project.Scripts
{
    public class BouncyInvisiWalls : MonoBehaviour
    {
        [SerializeField] private float _bounceForce = 0.8f;


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<DiceBase>() is { } dice)
            {
                // collision.linearVelocity;
                collision.rigidbody.AddForce(collision.linearVelocity*-_bounceForce, ForceMode.VelocityChange);
            }
        }
    }
}