using System.Collections;
using System.Collections.Generic;
using Tarahiro.Sound;
using Tarahiro.MasterData;
using UnityEngine;
using UnityEngine.Audio;
using Tarahiro;
using static ConstSound;


/// <summary>
/// サウンド周りを管理するクラス
/// </summary>
public class SoundManagerCommon
{
    //Common

    public static void SetVolumeLevel(MixerExposedParameter mixerExposedParameter, int volumeLevel)
    {
        SoundMgr.Instance.SetVolume(mixerExposedParameter, volumeLevel);
    }

    public static int VolumeLevel(MixerExposedParameter mixerExposedParameter)
    {
        return SoundMgr.Instance.VolumeLevel(mixerExposedParameter);
    }
    public static void CompressOrDecompressVolume(MixerExposedParameter mixerExposedParameter, bool IsCompress, float changeTime = SoundMgr.longChangeTime)
    {
        SoundMgr.Instance.CompressOrDecompressVolume(mixerExposedParameter, IsCompress, changeTime);
    }

    public static void SetOffsetDecibel(MixerExposedParameter mixerExposedParameter, float t_setOffsetDecibel, float changeTime = SoundMgr.longChangeTime)
    {
        SoundMgr.Instance.SetOffsetDecibel(mixerExposedParameter, t_setOffsetDecibel, changeTime);
    }
    public static void Mute(float changeTime, MixerExposedParameter mixerLabelEnum)
    {
        SoundMgr.Instance.Mute(mixerLabelEnum, true, changeTime);
    }

}
