using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class RayRenderer : MonoBehaviour
    {
        public RayBase Ray;
        public RayPhysicsCaster RayPhysicsCaster;
        public bool RenderThrough;

        public LineRenderer LineRenderer;

        private void Update()
        {
            Render();
        }

        private void Render()
        {
            var positions = RenderThrough ? Ray.Positions : RayPhysicsCaster.GetPositions();
            LineRenderer.positionCount = positions.Length;
            LineRenderer.SetPositions(positions);
        }
    }
}