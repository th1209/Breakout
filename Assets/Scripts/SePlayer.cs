using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 効果音再生用クラス。
/// </summary>
/// <remarks>
/// AudioSourceには、このスクリプトをアタッチしたGameObjectを経由してアクセス。
/// 以下の点で明らかに不便なクラスなので、そのうち改善版を作ってみること。
/// * Scene毎にこのスクリプトを保持するGameObjectを持つ必要がある。
/// * インスペクタから、わざわざAudioClipとそのSE名を指定してやる必要がある。
/// * 効果音の同時再生数管理や、複数効果音を再生した時の減衰が考慮できていない。
/// </remarks>
public class SePlayer : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public List<string> audioClipKeys = new List<string>();

    public void Play(string seName)
    {
        DontDestroyOnLoad(this.gameObject);

        int index = this.audioClipKeys.FindIndex((string clipName) => { return clipName == seName; });
        if (index < 0) {
            Debug.Assert(false);
        }
        AudioClip clip = this.audioClips[index];
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
