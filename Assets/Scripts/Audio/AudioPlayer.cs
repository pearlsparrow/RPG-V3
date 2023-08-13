using FMOD;
using FMODUnity;
using FMOD.Studio;
using UnityEngine;

public static class AudioPlayer
{
    public static void PlayOneShotWithParameters(EventReference fmodEvent, Vector3 position,
        params (string name, float value)[] parameters)
    {
        EventInstance instance = RuntimeManager.CreateInstance(fmodEvent);

        foreach (var (name, value) in parameters)
        {
            instance.setParameterByName(name, value);
        }

        instance.set3DAttributes(position.To3DAttributes());
        instance.start();
        instance.release();
    }

    public static void PlayOneShotWithParameters(EventReference fmodEvent, Vector3 position,
        string parameterWithLabelName, string parameterWithLabelValue, params (string name, float value)[] parameters)
    {
        EventInstance instance = RuntimeManager.CreateInstance(fmodEvent);

        instance.setParameterByNameWithLabel(parameterWithLabelName, parameterWithLabelValue);

        foreach (var (name, value) in parameters)
        {
            instance.setParameterByName(name, value);
        }


        instance.set3DAttributes(position.To3DAttributes());
        instance.start();
        instance.release();
    }

    public static void PlayOneShotWithParameters(EventReference fmodEvent, Transform transform,
        params (string name, float value)[] parameters)
    {
        EventInstance instance = RuntimeManager.CreateInstance(fmodEvent);

        foreach (var (name, value) in parameters)
        {
            instance.setParameterByName(name, value);
        }

        RuntimeManager.AttachInstanceToGameObject(instance, transform, true);
        instance.start();
        instance.release();
    }
}