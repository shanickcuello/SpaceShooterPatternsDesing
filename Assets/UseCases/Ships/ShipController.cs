using System.Runtime.CompilerServices;
using UnityEngine;

namespace UseCases.Ships
{
    public class ShipController : IController
    {
        private Camera _camera;
        private ShipModel _model;
        private ShipView _view;
        private IFormulaMovement _linealFormula = new LinealFormula();
        private IFormulaMovement _sinusoidalFormula = new SinusoidalFormula();
        
        public ShipController(Camera cam, ShipModel mod, ShipView view)
        {
            _camera = cam;
            _model = mod;
            _view = view;
        }
        
        public void OnUpdate()
        {
            Vector3 lookAtPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _model.LookAt(lookAtPos);

            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            _model.Move(x, y);

            if (Input.GetMouseButtonDown(0))
            {
                _model.Shoot(_linealFormula);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _model.Shoot(_sinusoidalFormula);
            }
        }

    }
}