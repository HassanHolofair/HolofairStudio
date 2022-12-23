using Battlehub.RTCommon;
using Battlehub.RTHandles.Demo;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HolofairStudio
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private LayerMask _modelsLayer;

        private ControlType _controlType = ControlType.none;
        private Transform _selectedModelTransform;

        private SimpleEditor _simpleEditor;
        private IRTE m_editor;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _simpleEditor = FindObjectOfType<SimpleEditor>();  
        }

        private void OnEnable()
        {
            m_editor = IOC.Resolve<IRTE>();
            m_editor.Tools.ToolChanged += OnToolChanged;
        }

        private void OnDisable()
        {
            m_editor.Tools.ToolChanged -= OnToolChanged;
        }

        private void Update()
        {
            if (Mouse.current.leftButton.isPressed)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    // user clicked on a model? select it
                    var view = hit.collider.GetComponentInParent<ItemView>();
                    if (view)
                        _selectedModelTransform = hit.transform.root;

                    else if (_selectedModelTransform && _controlType != ControlType.none)
                        Control(_controlType,hit.point, hit.normal);
                }
                else
                {
                    //_selectedModelTransform = null;
                }
            }
        }

        private void Control(ControlType controlType, Vector3 point, Vector3 normal)
        {
            Vector3 direction = Vector3.zero;

            switch (controlType)
            {
                case ControlType.none:
                    break;
                case ControlType.X:
                    direction = Quaternion.Euler(new Vector3(0, -90, 0)) * normal;
                    break;
                case ControlType.InverseX:
                    direction = Quaternion.Euler(new Vector3(0, 90, 0)) * normal;
                    break;
                case ControlType.Y:
                    direction = Quaternion.Euler(new Vector3(90, 0, 0)) * normal;
                    break;
                case ControlType.InverseY:
                    direction = Quaternion.Euler(new Vector3(-90, 0, 0)) * normal;
                    break;
                case ControlType.Z:
                    direction = normal;
                    break;
                case ControlType.InverseZ:
                    direction = -normal;
                    break;
                default:
                    break;
            }

            _selectedModelTransform.SetPositionAndRotation(point, Quaternion.LookRotation(direction));
        }

        public void SetControl(int controlType)
        {
            _simpleEditor.DisableTools();
            _controlType = (ControlType)controlType;
        }

        private void OnToolChanged()
        {
            Debug.Log("tool changed");
            _controlType = ControlType.none;
        }

    }

    public enum ControlType
    {
        none = -1,
        X,
        InverseX,
        Y,
        InverseY,
        Z,
        InverseZ
    }
}
