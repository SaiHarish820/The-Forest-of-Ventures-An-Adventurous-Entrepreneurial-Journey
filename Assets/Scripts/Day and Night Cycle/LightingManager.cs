using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


[ExecuteInEditMode]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;


    //Variables
    [SerializeField, Range(0, 100)] private float TimeOfDay;

    private void Update()
    {
        if(Preset == null) return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 100; //Clamp between 0-24
            UpdateLighting(TimeOfDay / 100f);
        }
    }

    private void UpdateLighting(float timePercent)
    {

        UnityEngine.RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        UnityEngine.RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if(UnityEngine.RenderSettings.sun != null)
        {
            DirectionalLight = UnityEngine.RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights) {
            if(light.type == UnityEngine.LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

}
