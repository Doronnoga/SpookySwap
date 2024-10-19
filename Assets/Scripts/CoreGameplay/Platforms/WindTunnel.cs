using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ButtonClass;

namespace WindTunnelScript
{
    public class WindTunnel : MonoBehaviour
    {
        [SerializeField]
        private Vector2 forceAmount; 

        [Header("Target")]
        [SerializeField]
        protected string targetTag = "Ghost";

        [Header("Button Controls")]
        [SerializeField]
        private Button button;

        [SerializeField]
        private bool onePressButton = true;

        private Collider2D windCollider;
        private ParticleSystem windParticles;

        private bool isActivated = true;

        void Start()
        {
            windCollider = GetComponent<Collider2D>();
            windParticles = GetComponent<ParticleSystem>();

            if (button != null)
            {
                button.OnButtonPressed += deactivateWind;
                if (onePressButton)
                {
                    button.OnButtonReleased += activateWind;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isActivated && collision.CompareTag(targetTag))
            {
                ApplyWindForce(collision);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (isActivated && collision.CompareTag(targetTag))
            {
                ApplyWindForce(collision);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (isActivated && collision.CompareTag(targetTag))
            {
            }
        }

        private void ApplyWindForce(Collider2D collision)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (rb.transform.position - transform.position).normalized;
                Vector2 force = direction * forceAmount;
                rb.AddForce(force);
            }
        }

        private void activateWind()
        {
            isActivated = true;
            if (windParticles != null)
            {
                windParticles.Play();
            }
        }

        private void deactivateWind()
        {
            isActivated = false;
            if (windParticles != null)
            {
                windParticles.Stop(); 
            }
        }
    }
}
