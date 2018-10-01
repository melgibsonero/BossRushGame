using UnityEngine;

public class UIMenuItem : MonoBehaviour
{
    public bool isDefault;
    public UIMenuItem upItem, downItem, leftItem, rightItem;

    [HideInInspector]
    public bool isHighlighted;

    [Header("Buttons")]
    public bool isPlay;
    public bool isQuit, isDeleteSaveFile;

    [Header("Sliders")]
    public bool isMusicNoice;
    [Range(0f, 1f)]
    public float musicNoice;
    
    public bool isSoundNoice;
    [Range(0f, 1f)]
    public float soundNoice;

    [Space(-10), Header("Sliders only")]
    public Transform mute;
    public Transform noice;
    private float _lerpTime;

    public void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    public void UpdateVolumeSlider(float speed)
    {
        if (isMusicNoice)
            _lerpTime = musicNoice;
        if (isSoundNoice)
            _lerpTime = soundNoice;

        _lerpTime = MathHelp.Clamp(_lerpTime + Time.unscaledDeltaTime * speed, 0f, 1f);

        transform.position = Vector3.Lerp(mute.position, noice.position, _lerpTime);
        transform.rotation = Quaternion.Lerp(mute.rotation, noice.rotation, _lerpTime);

        if (isMusicNoice)
        {
            musicNoice = _lerpTime;
            SaveLoad.Floats[SaveLoad.MUSIC_NOICE] = musicNoice;
        }
        if (isSoundNoice)
        {
            soundNoice = _lerpTime;
            SaveLoad.Floats[SaveLoad.SOUND_NOICE] = soundNoice;
        }

        SaveLoad.Save();
    }

    public bool IsSlider()
    {
        return isSoundNoice || isMusicNoice;
    }
}