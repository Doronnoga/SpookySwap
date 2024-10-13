using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovingPlatform
{
    public class MovingPlatforms : MonoBehaviour
    {
        [Header("All moving points")]
        [SerializeField]
        protected Transform start;
        [SerializeField]
        protected Transform end;
        [SerializeField]
        protected Transform platform;

        [Header("Speed and curve")]
        [SerializeField]
        protected AnimationCurve speedCurve; //for nice smooth move
        [SerializeField]
        protected float duration = 2f;  // of one full movement 

        protected float elapsedTime = 0f;
        protected bool movingToEnd = true;

        private void OnDrawGizmos()
        {
            if (start != null && end != null && platform != null)
            {
                Gizmos.DrawLine(platform.position, start.position);
                Gizmos.DrawLine(platform.position, end.position);
            }
        }

        protected virtual void movePlatform()
        {
            float normalizedTime = elapsedTime / duration;
            float curveValue = speedCurve.Evaluate(normalizedTime);

            if (movingToEnd)
            {
                platform.position = Vector2.Lerp(start.position, end.position, curveValue);
            }
            else
            {
                platform.position = Vector2.Lerp(end.position, start.position, curveValue);
            }

            elapsedTime += Time.deltaTime;

            // Check if movement complete and reverse
            if (elapsedTime >= duration)
            {
                elapsedTime = 0f;  // Reset time
                movingToEnd = !movingToEnd;  // Switch direction
            }
        }

        void Update()
        {
            movePlatform();
        }
    }
 
}

