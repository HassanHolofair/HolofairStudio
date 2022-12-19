namespace HolofairStudio
{
    public class RemoteJSONSceneResourceFactory : SceneResourceFactory
    {
        public RemoteJSONSceneResourceFactory(SceneResources resources, string url) : base(resources, url){ }

        public override void StartLoading()
        {
            base.StartLoading();
        }
    }
}
