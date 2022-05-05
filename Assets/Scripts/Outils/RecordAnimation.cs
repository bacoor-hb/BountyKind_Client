using UnityEngine;
using UnityEditor.Animations;

public class RecordAnimation : MonoBehaviour
{
    public AnimationClip clip;

    private GameObjectRecorder gameObjectRecorder;

    void Start()
    {
        gameObjectRecorder = new GameObjectRecorder(gameObject);
        gameObjectRecorder.BindComponentsOfType<Transform>(gameObject, true);
    }

    void LateUpdate()
    {
        if (clip == null)
            return;
        gameObjectRecorder.TakeSnapshot(Time.deltaTime);
    }

    void OnDisable()
    {
        if (clip == null)
            return;

        if (gameObjectRecorder.isRecording)
        {
            gameObjectRecorder.SaveToClip(clip);
        }
    }
}