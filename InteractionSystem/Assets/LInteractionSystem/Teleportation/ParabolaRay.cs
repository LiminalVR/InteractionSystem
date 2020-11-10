using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// Add as a child object to your Grabber, Hand or Controller.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class ParabolaRay : MonoBehaviour
    {
        public LineRenderer LineRenderer;

        public float Velocity = 5;

        [Range(-90, 90)]
        public float Angle = 45;

        [Range(2, 100)]
        public int Resolution;

        private float _timeToReachFloor;
        private float _maxDistance;
        private float _height = 2;

        private float _gravity;
        private float _radianAngle;

        private void Awake()
        {
            LineRenderer = GetComponent<LineRenderer>();
            _gravity = Mathf.Abs(Physics2D.gravity.y); // 9.81
        }

        private void Start()
        {
            RenderArc();
        }

        private void Update()
        {
            _height = transform.position.y;
            Angle = -transform.eulerAngles.x;

            RenderArc();
        }

        private void RenderArc()
        {
            LineRenderer.SetVertexCount(Resolution + 1);
            LineRenderer.SetPositions(CalculateArcArray());
        }

        private Vector3[] CalculateArcArray()
        {
            var arcArray = new Vector3[Resolution + 1];
            _radianAngle = Mathf.Deg2Rad * Angle;
            float a, b, c, discriminant;

            /*
                Formula used here is Y = Y0 + V0y * t + 1/2 * g * t^2, where
                Y = 0 at the moment projectile lands
                Y0 = our relative height
                V0y = vertical velocity of projectile
                g = gravity
                t = what we are trying to find!
            */

            a = -_gravity / 2;
            b = Velocity * Mathf.Sin(_radianAngle);
            c = _height;
            discriminant = b * b - 4 * a * c;
            _timeToReachFloor = (-b - Mathf.Sqrt(discriminant)) / (2 * a); // Here we find time, and now we can find any of the variables above

            _maxDistance = Velocity * Mathf.Cos(_radianAngle) * _timeToReachFloor; // Maximum distance at X-axis travelled by projectile

            for (var i = 0; i <= Resolution; i++)
            {
                var t = (float)i * _timeToReachFloor / (float)Resolution; // We add time here so our line t matches our equation time
                arcArray[i] = CalculateArcPoint(t, _maxDistance);
            }

            return arcArray;
        }

        private Vector3 CalculateArcPoint(float t, float maxDistance)
        {
            var x = t * maxDistance;
            var y = _height + Velocity * Mathf.Sin(_radianAngle) * t - (_gravity / 2) * t * t; // Changed this to our formula used above

            var orign = transform;
            var finalPosition = orign.position + orign.forward * x;
            finalPosition.y = y;

            return finalPosition;
        }

        public float AngleInPlane(Transform from, Vector3 to, Vector3 planeNormal)
        {
            Vector3 dir = to - from.position;

            Vector3 p1 = Project(dir, planeNormal);
            Vector3 p2 = Project(from.forward, planeNormal);

            return Vector3.Angle(p1, p2);
        }

        public Vector3 Project(Vector3 v, Vector3 onto)
        {
            return v - (Vector3.Dot(v, onto) / Vector3.Dot(onto, onto)) * onto;
        }
    }
}