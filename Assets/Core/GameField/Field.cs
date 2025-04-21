using UnityEngine;

namespace Core.GameField
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private Transform _field;
        
        private float _xMax;
        private float _xMin;
        private float _yMax;
        private float _yMin;

        public void Initialize()
        {
            SetupBorder();
        }

        public Vector3 ClampedMoveInFieldZone(Vector3 originPosition, Vector3 offset = default(Vector3))
        {
            if (_field == null)
            {
                return originPosition;
            }
            
            var xValue = Mathf.Clamp(originPosition.x, _xMin + offset.x / 2f, _xMax - offset.x / 2f);
            var yValue = Mathf.Clamp(originPosition.y, _yMin + offset.y / 2f, _yMax - offset.y / 2f);
            
            return new Vector3(xValue, yValue, originPosition.z);
        }

        private void SetupBorder()
        {
            var xHalf = _field.localScale.x / 2f;
        
            _xMax = _field.position.x + xHalf;
            _xMin = _field.position.x - xHalf;
        
            var yHalf = _field.localScale.y / 2f;

            _yMax = _field.position.y + yHalf;
            _yMin = _field.position.y - yHalf;
        }
    }
}
