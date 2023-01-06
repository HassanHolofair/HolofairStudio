using UnityEngine;
using Newtonsoft.Json.Linq;

namespace HolofairStudio
{
    public static class JObjectExtention
    {
        public static JObject ToJObject(this Vector3 vector3)
        {
            var data = new JObject
            {
                { "x", vector3.x },
                { "y", vector3.y },
                { "z", vector3.z }
            };
            return data;
        }

        public static Vector3 ToVector3(this JArray jobject)
        {
            return new Vector3(
                x: jobject[0].ToObject<float>(),
                y: jobject[1].ToObject<float>(),
                z: jobject[2].ToObject<float>());
        }

        public static void ToTransform(this JObject jobject, ItemView model)
        {
            Transform transform = model.transform;
            if (jobject.ContainsKey("position"))
            {
                var o = jobject.GetValue("position").ToObject<JArray>();
                transform.position = o.ToVector3();
            }
        }

        public static bool ContainsKey(this JObject jobject, string value)
        {
            return jobject.TryGetValue(value, out _);
        }
    }
}
