using Battlehub.RTCommon;
using Battlehub.RTHandles;

using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using HolofairStudio.SceneItems;

namespace HolofairStudio.UI
{
    public class UIToolsController : MonoBehaviour, IRTEState
    {
        [Header("Editor Tools")]
        [SerializeField] private Toggle m_viewToggle = null;
        [SerializeField] private Toggle m_positionToggle = null;
        [SerializeField] private Toggle m_rotationToggle = null;
        [SerializeField] private Toggle m_scaleToggle = null;
        [SerializeField] private Toggle m_rectToggle = null;
        [SerializeField] private Toggle m_pivotModeToggle = null;
        [SerializeField] private Toggle m_pivotRotationToggle = null;

        [Header("Snap tools")]
        [SerializeField] private Toggle m_snapXToggle = null;
        [SerializeField] private Toggle m_snapYToggle = null;
        [SerializeField] private Toggle m_snapZToggle = null;
        [SerializeField] private Button m_invertButton = null;

        [Header("Undo")]
        [SerializeField] private Button m_undoButton = null;
        [SerializeField] private Button m_redoButton = null;

        [SerializeField] private Button m_focusButton = null;

        [SerializeField] private Button m_deleteButton = null;
        [SerializeField] private Button m_saveButton = null;
        [SerializeField] private Button m_loadButton = null;

        private SceneJSON _sceneJSON;

        public bool IsCreated
        {
            get { return true; }
        }
        protected IRTE Editor { get; private set; }

        private void Start()
        {
            Editor = IOC.Resolve<IRTE>();

            Editor.Tools.ToolChanged += OnToolChanged;
            Editor.Tools.PivotModeChanged += OnPivotModeChanged;
            Editor.Tools.PivotRotationChanged += OnPivotRotationChanged;

            Editor.Undo.UndoCompleted += UpdateUndoRedoButtons;
            Editor.Undo.RedoCompleted += UpdateUndoRedoButtons;
            Editor.Undo.StateChanged += UpdateUndoRedoButtons;

            SubscribeUIEvents();

            UpdateUndoRedoButtons();
            Editor.IsOpened = true;
            Editor.IsPlaying = false;

            Editor.Selection.SelectionChanged += OnSelectionChanged;

            _sceneJSON = FindObjectOfType<SceneJSON>();
        }

        private void OnDestroy()
        {
            if (Editor != null)
            {
                Editor.Tools.ToolChanged -= OnToolChanged;
                Editor.Tools.PivotModeChanged -= OnPivotModeChanged;
                Editor.Tools.PivotRotationChanged -= OnPivotRotationChanged;

                Editor.Undo.UndoCompleted -= UpdateUndoRedoButtons;
                Editor.Undo.RedoCompleted -= UpdateUndoRedoButtons;
                Editor.Undo.StateChanged -= UpdateUndoRedoButtons;
            }

            UnsubscribeUIEvents();
            if (Editor != null)
            {
                Editor.Selection.SelectionChanged -= OnSelectionChanged;
            }
        }

        private void Update()
        {
            if (Editor.Input.GetKeyDown(KeyCode.Delete))
            {
                DeleteSelected();
            }
        }
        private void SubscribeUIEvents()
        {
            if (m_viewToggle) m_viewToggle.onValueChanged.AddListener(OnViewToggle);
            if (m_positionToggle) m_positionToggle.onValueChanged.AddListener(OnPositionToggle);
            if (m_rotationToggle) m_rotationToggle.onValueChanged.AddListener(OnRotationToggle);
            if (m_scaleToggle) m_scaleToggle.onValueChanged.AddListener(OnScaleToogle);
            if (m_rectToggle) m_rectToggle.onValueChanged.AddListener(OnRectToggle);

            if (m_snapXToggle) m_snapXToggle.onValueChanged.AddListener(OnSnapXToggle);
            if (m_snapYToggle) m_snapYToggle.onValueChanged.AddListener(OnSnapYToggle);
            if (m_snapZToggle) m_snapZToggle.onValueChanged.AddListener(OnSnapZToggle);

            if (m_pivotModeToggle) m_pivotModeToggle.onValueChanged.AddListener(OnPivotModeToggle);
            if (m_pivotRotationToggle) m_pivotRotationToggle.onValueChanged.AddListener(OnPivotRotationToggle);

            if (m_undoButton) m_undoButton.onClick.AddListener(OnUndoClick);
            if (m_redoButton) m_redoButton.onClick.AddListener(OnRedoClick);
            if (m_invertButton) m_invertButton.onClick.AddListener(OnInvertButton);

            if (m_focusButton != null)
            {
                m_focusButton.onClick.AddListener(OnFocusClick);
            }
            if (m_deleteButton != null)
            {
                m_deleteButton.onClick.AddListener(OnDeleteClick);
            }

            if (m_saveButton != null)
            {
                m_saveButton.onClick.AddListener(OnSaveClick);
            }

            if (m_loadButton != null)
            {
                m_loadButton.onClick.AddListener(OnLoadClick);
            }
        }

        private void UnsubscribeUIEvents()
        {
            if (m_viewToggle) m_viewToggle.onValueChanged.RemoveListener(OnViewToggle);
            if (m_positionToggle) m_positionToggle.onValueChanged.RemoveListener(OnPositionToggle);
            if (m_rotationToggle) m_rotationToggle.onValueChanged.RemoveListener(OnRotationToggle);
            if (m_scaleToggle) m_scaleToggle.onValueChanged.RemoveListener(OnScaleToogle);
            if (m_rectToggle) m_rectToggle.onValueChanged.RemoveListener(OnRectToggle);

            if (m_snapXToggle) m_snapXToggle.onValueChanged.RemoveListener(OnSnapXToggle);
            if (m_snapYToggle) m_snapYToggle.onValueChanged.RemoveListener(OnSnapYToggle);
            if (m_snapZToggle) m_snapZToggle.onValueChanged.RemoveListener(OnSnapZToggle);

            if (m_pivotModeToggle) m_pivotModeToggle.onValueChanged.RemoveListener(OnPivotModeToggle);
            if (m_pivotRotationToggle) m_pivotRotationToggle.onValueChanged.RemoveListener(OnPivotRotationToggle);

            if (m_undoButton) m_undoButton.onClick.RemoveListener(OnUndoClick);
            if (m_redoButton) m_redoButton.onClick.RemoveListener(OnRedoClick);
            if (m_invertButton) m_invertButton.onClick.RemoveListener(OnInvertButton);

            if (m_focusButton != null)
            {
                m_focusButton.onClick.RemoveListener(OnFocusClick);
            }
            if (m_deleteButton != null)
            {
                m_deleteButton.onClick.RemoveListener(OnDeleteClick);
            }

            if (m_saveButton != null)
            {
                m_saveButton.onClick.RemoveListener(OnSaveClick);
            }

            if (m_loadButton != null)
            {
                m_loadButton.onClick.RemoveListener(OnLoadClick);
            }
        }

        private void OnSelectionChanged(Object[] unselectedObjects)
        {
            if (m_focusButton != null)
                m_focusButton.interactable = Editor.Selection.Length > 0;

            if (m_deleteButton != null)
                m_deleteButton.interactable = Editor.Selection.Length > 0;
        }

        private void OnFocusClick()
        {
            IScenePivot scenePivot = Editor.GetWindow(RuntimeWindowType.Scene).IOCContainer.Resolve<IScenePivot>();
            scenePivot.Focus(FocusMode.Default);
        }

        private void OnSaveClick() => _sceneJSON.Save();

        private void OnLoadClick() => _sceneJSON.Load();

        private void OnDeleteClick() => DeleteSelected();
        private void DeleteSelected()
        {
            if (Editor.Selection.Length > 0)
            {
                ExposeToEditor[] exposed = Editor.Selection.gameObjects
                    .Where(o => o != null)
                    .Select(o => o.GetComponent<ExposeToEditor>())
                    .Where(o => o != null)
                    .ToArray();

                Editor.Undo.BeginRecord();
                Editor.Selection.objects = null;
                Editor.Undo.DestroyObjects(exposed);
                Editor.Undo.EndRecord();
            }
        }

        private void OnToolChanged()
        {
            UnsubscribeUIEvents();

            RuntimeTool tool = Editor.Tools.Current;
            switch (tool)
            {
                case RuntimeTool.View:
                    if (m_viewToggle != null) m_viewToggle.isOn = true;
                    break;
                case RuntimeTool.Move:
                    if (m_positionToggle != null) m_positionToggle.isOn = true;
                    break;
                case RuntimeTool.Rotate:
                    if (m_rotationToggle != null) m_rotationToggle.isOn = true;
                    break;
                case RuntimeTool.Scale:
                    if (m_scaleToggle != null) m_scaleToggle.isOn = true;
                    break;
                case RuntimeTool.Rect:
                    if (m_rectToggle != null) m_rectToggle.isOn = true;
                    break;
                case RuntimeTool.SnapX:
                    if (m_snapXToggle != null) m_snapXToggle.isOn = true;
                    break;
                case RuntimeTool.SnapY:
                    if (m_snapYToggle != null) m_snapYToggle.isOn = true;
                    break;
                case RuntimeTool.SnapZ:
                    if (m_snapZToggle != null) m_snapZToggle.isOn = true;
                    break;
                case RuntimeTool.None:
                    if (m_viewToggle != null) m_viewToggle.isOn = false;
                    if (m_positionToggle != null) m_positionToggle.isOn = false;
                    if (m_rotationToggle != null) m_rotationToggle.isOn = false;
                    if (m_scaleToggle != null) m_scaleToggle.isOn = false;
                    if (m_rectToggle != null) m_rectToggle.isOn = false;
                    if (m_snapXToggle != null) m_snapXToggle.isOn = false;
                    if (m_snapYToggle != null) m_snapYToggle.isOn = false;
                    if (m_snapZToggle != null) m_snapZToggle.isOn = false;
                    break;

            }

            SubscribeUIEvents();
        }

        private void OnPivotModeChanged()
        {
            UnsubscribeUIEvents();

            m_pivotModeToggle.isOn = Editor.Tools.PivotMode == RuntimePivotMode.Center;

            Text text = m_pivotModeToggle.GetComponent<Text>();
            if (text != null)
            {
                text.text = Editor.Tools.PivotMode.ToString() + " (Z)";
            }

            SubscribeUIEvents();
        }

        private void OnPivotRotationChanged()
        {
            UnsubscribeUIEvents();

            m_pivotRotationToggle.isOn = Editor.Tools.PivotRotation == RuntimePivotRotation.Global;

            Text text = m_pivotRotationToggle.GetComponent<Text>();
            if (text != null)
            {
                text.text = Editor.Tools.PivotRotation.ToString() + " (X)";
            }

            SubscribeUIEvents();
        }

        private void UpdateUndoRedoButtons()
        {
            if (m_undoButton) m_undoButton.interactable = Editor.Undo.CanUndo;
            if (m_redoButton) m_redoButton.interactable = Editor.Undo.CanRedo;
        }

        private void OnViewToggle(bool value)
        {
            Editor.Tools.Current = RuntimeTool.View;
        }

        private void OnPositionToggle(bool value)
        {
            Editor.Tools.Current = RuntimeTool.Move;
        }

        private void OnRotationToggle(bool value)
        {
            Editor.Tools.Current = RuntimeTool.Rotate;
        }

        private void OnScaleToogle(bool value)
        {
            Editor.Tools.Current = RuntimeTool.Scale;
        }

        private void OnRectToggle(bool value)
        {
            Editor.Tools.Current = RuntimeTool.Rect;
        }

        private void OnPivotModeToggle(bool value)
        {
            Editor.Tools.PivotMode = value ? RuntimePivotMode.Center : RuntimePivotMode.Pivot;
        }

        private void OnPivotRotationToggle(bool value)
        {
            Editor.Tools.PivotRotation = value ? RuntimePivotRotation.Global : RuntimePivotRotation.Local;
        }

        private void OnUndoClick()
        {
            Editor.Undo.Undo();
        }

        private void OnRedoClick()
        {
            Editor.Undo.Redo();
        }

        private void OnSnapXToggle(bool value)
        {
            Editor.Tools.Current = RuntimeTool.SnapX;
        }

        private void OnSnapYToggle(bool value)
        {
            Editor.Tools.Current = RuntimeTool.SnapY;
        }

        private void OnSnapZToggle(bool value)
        {
            Editor.Tools.Current = RuntimeTool.SnapZ;
        }

        private void OnInvertButton()
        {
            Editor.Tools.InvertSnapping = !Editor.Tools.InvertSnapping;
        }

        #region IRTEState implementation
        public event System.Action<object> Created;
        public event System.Action<object> Destroyed;
        private void Use()
        {
            Created(null);
            Destroyed(null);
        }
        #endregion
    }
}