using Battlehub.RTCommon;
using System.Collections.Generic;
using UnityEngine;

namespace HolofairStudio
{
    /// <summary>
    /// Add the instantiated item to game history to enable undo/redo
    /// </summary>
    public class Options : MonoBehaviour
    {
        private IRTE m_editor;

        protected virtual void Start()
        {
            m_editor = IOC.Resolve<IRTE>();
            ItemDownloadHandler.OnItemDownloadFinish += RecordPrefab;
        }

        private void OnDestroy()
        {
            ItemDownloadHandler.OnItemDownloadFinish -= RecordPrefab;
        }

        public void RecordPrefab(GameObject prefab)
        {
            if (prefab == null)
                return;

            ExposeToEditor exposeToEditor = prefab.GetComponent<ExposeToEditor>();
            m_editor.Undo.BeginRecord();
            m_editor.Undo.RegisterCreatedObjects(new[] { exposeToEditor });
            m_editor.Selection.activeObject = prefab;
            m_editor.Undo.EndRecord();
        }
    }
}
