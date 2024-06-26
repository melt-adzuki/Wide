using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ElementAnimator : MonoBehaviour
{
    #region プライベートフィールド
    [Tooltip("移動する座標の量")]
    [SerializeField]
    private Vector3 trip = new Vector3(0f, 0f, 0f);

    [Tooltip("持続秒数")]
    [SerializeField]
    private float duration = 1f;

    [Tooltip("遅延レベル")]
    [SerializeField]
    private float delayLevel = 0f;

    [Tooltip("フェードアニメーションを有効化するか否か")]
    [SerializeField]
    private bool doFadeAnimation = true;
    #endregion

    #region 変数
    private float delay;
    private Vector3 initialPosition;
    private Image image;
    #endregion

    #region Unityのコールバック
    void Start()
    {
        initialPosition = transform.position;
        image = GetComponent<Image>();
        delay = delayLevel * float.Parse(
            PlayerPrefs.HasKey("AnimationDelay")
                ? PlayerPrefs.GetString("AnimationDelay")
                : "0"
        );

        transform.position = initialPosition - trip;

        transform
            .DOMove(transform.position + trip, duration)
            .SetEase(Ease.OutCubic)
            .SetDelay(delay);

        if (doFadeAnimation)
        {
            image.color = new Func<Color>(() =>
            {
                Color color = image.color;
                color.a = 0f;
                return color;
            })();

            image
                .DOFade(1f, duration)
                .SetEase(Ease.OutCubic)
                .SetDelay(delay);
        }
    }
    #endregion
}
