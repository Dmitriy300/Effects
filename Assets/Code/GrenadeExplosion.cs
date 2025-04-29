using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class GrenadeExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private PostProcessVolume _postProcessVolume;
    [SerializeField] private float _effectDuration = 0.5f;

    private Bloom bloom;
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        // Получаем ссылки на эффекты
        _postProcessVolume.profile.TryGetSettings(out bloom);
        _postProcessVolume.profile.TryGetSettings(out chromaticAberration);
    }

    public void TriggerExplosion()
    {
        // Запускаем частицы
        _explosionParticles.Play();

        // Активируем пост-обработку
        StartCoroutine(ExplosionEffect());
    }

    private IEnumerator ExplosionEffect()
    {
        float elapsed = 0;

        while (elapsed < _effectDuration)
        {
            bloom.intensity.value = Mathf.Lerp(10, 0, elapsed / _effectDuration);
            chromaticAberration.intensity.value = Mathf.Lerp(1, 0, elapsed / _effectDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}