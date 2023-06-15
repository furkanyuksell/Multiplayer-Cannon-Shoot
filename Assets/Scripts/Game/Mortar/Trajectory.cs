using Game.Balls;
using Inputs;
using Mirror;
using UnityEngine;

namespace Game.Mortar
{
    public class Trajectory : NetworkBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer;
        [SerializeField]
        private Transform releasePoint;
    
        [SerializeField]
        [Range(10, 100)]
        private int linePoints = 25;
    
        [SerializeField]
        [Range(0.01f, 0.25f)]
        private float timeBetweenPoints = 0.1f;
    
        [SerializeField]
        private LayerMask trajectoryTargetLayers;

        private float _activeBallForce;
        private float _activeBallMass;
        private void Awake()
        {
            var parent = transform.parent;
            if (lineRenderer == null) lineRenderer = parent.GetComponentInChildren<LineRenderer>();
            if (releasePoint == null) releasePoint = parent.GetComponentInChildren<Shooter>().transform;

        }
    
        private void DrawTrajectory(bool show)
        { 
            if (!isLocalPlayer) return;
            
            if (show)
            {
                lineRenderer.enabled = true;
                lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;
                Vector3 startPosition = releasePoint.position;
                Vector3 startVelocity = (releasePoint.forward * _activeBallForce) / _activeBallMass;
                int i = 0;
                lineRenderer.SetPosition(i, startPosition);
                for (float time = 0; time < linePoints; time += timeBetweenPoints)
                {
                    i++;
                    Vector3 point = startPosition + time * startVelocity;
                    point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

                    lineRenderer.SetPosition(i, point);

                    Vector3 lastPosition = lineRenderer.GetPosition(i - 1);

                    if (Physics.Raycast(lastPosition, 
                            (point - lastPosition).normalized, 
                            out RaycastHit hit,
                            (point - lastPosition).magnitude,
                            trajectoryTargetLayers))
                    {
                        lineRenderer.SetPosition(i, hit.point);
                        lineRenderer.positionCount = i + 1;
                        return;
                    }
                }
            }else
            {
                lineRenderer.enabled = false;
                lineRenderer.positionCount = 0;
            }
        
        }

        public void SetActiveBall(Ball ball)
        {
            _activeBallForce = ball.ballInfo.force;
            _activeBallMass = ball.ballInfo.mass;
        
        }
        private void OnEnable()
        {
            InputBase.ShowTrajectory += DrawTrajectory;
            MortarController.SetBall += SetActiveBall;
        }

        private void OnDisable()
        {
            InputBase.ShowTrajectory += DrawTrajectory;
            MortarController.SetBall -= SetActiveBall;
        }
    }
}
