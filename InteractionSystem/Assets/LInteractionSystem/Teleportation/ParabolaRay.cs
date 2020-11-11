using System.Collections;
using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public abstract class RayBase : MonoBehaviour
    {
        public Vector3[] Positions { get; set; }
    }

    /// <summary>
    /// Add as a child object to your Grabber, Hand or Controller.
    /// </summary>
    public class ParabolaRay : RayBase
    {
        public float Velocity = 5;

        [Range(2, 100)]
        public int Resolution;

        private float _timeToReachFloor;
        private float _maxDistance;
        private float _height = 2;

        private float _radianAngle;
        private float _angle = 45;

        private const float _gravity = 10;

        private void Start()
        {
            Positions = new Vector3[Resolution + 1];
        }

        private void Update()
        {
            _height = transform.position.y;
            _angle = -transform.eulerAngles.x;

            Positions = GetPoints();
        }

        private Vector3[] GetPoints()
        {
            _radianAngle = Mathf.Deg2Rad * _angle;
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
                Positions[i] = CalculateArcPoint(t, _maxDistance);
            }

            return Positions;
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
    }
    
    // I want to keep the ray separated from the Teleport System in the case we can use it for other things. 
    // This means we should have a button that just turns the ray on and off else where.

    // State Controller -> Turns on Ray
    // Teleport System -> Reads from Ray
}