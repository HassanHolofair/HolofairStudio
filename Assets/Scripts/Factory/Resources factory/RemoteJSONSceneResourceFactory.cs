using System.Collections;
using Newtonsoft.Json.Linq;

using UnityEngine;
using UnityEngine.Networking;

namespace HolofairStudio
{
    public class RemoteJSONSceneResourceFactory : SceneResourceFactory
    {
        private const string GLTF_KEY = "gltf";
        private const string IMAGE_KEY = "image";

        public RemoteJSONSceneResourceFactory(SceneResources resources, string url) : base(resources, url) { }

        public override void StartLoading()
        {
            base.StartLoading();

            CoroutineRunner.Instance.StartCoroutine(DownloadJSON());
        }

        private IEnumerator DownloadJSON()
        {
            UnityWebRequest request = UnityWebRequest.Get(_url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
                yield break;
            }

            string json = request.downloadHandler.text;
            JArray array = JArray.Parse(json);

            for (int i = 0; i < array.Count; i++)
            {
                JObject jobject = (JObject)array[i];

                string imageURL = (string)jobject.GetValue(IMAGE_KEY);
                string prefabURL = (string)jobject.GetValue(GLTF_KEY);
                SceneResources.AddResource(modelUrl: prefabURL, imageUrl: imageURL);
            }

        }
    }
}
