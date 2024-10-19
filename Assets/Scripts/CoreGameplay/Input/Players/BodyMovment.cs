using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovementScript;
using UnityEngine.UIElements;
using Unity.VisualScripting;

namespace BodyMovmentScript
{
    public class BodyMovment : PlayerMovement
    {
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Vector2 direction = facingRight ? Vector2.right : Vector2.left;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction * interactionDistance));
        }

        protected override void Update()
        {
            base.Update();
            base.pushAndPull();
        }
    }
}

