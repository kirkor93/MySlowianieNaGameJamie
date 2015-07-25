using UnityEngine;
using System.Collections;

public class FlameParticlesController : MonoBehaviour
{
    #region Variables
    public ParticleSystem[] Particles;

    private int _particleIndex = 0;
    private IDamagable _damagableObject;
    private float _thresholdLevelJump;
    private float _currentThresholdLevel;
    #endregion
    #region Methods

    protected void Awake()
    {
        _damagableObject = GetComponent(typeof (IDamagable)) as IDamagable;
        if (Particles == null
            || Particles.Length == 0
            || _damagableObject == null)
        {
            Debug.LogError("Particles manager couldn't get required components!");
            return;
        }

        foreach (ParticleSystem particle in Particles)
        {
            particle.enableEmission = false;
        }

        _thresholdLevelJump = (float) ((_damagableObject.MaxHitPoints*0.9f)/Particles.Length);
        _currentThresholdLevel = _damagableObject.MaxHitPoints - _thresholdLevelJump;
    }

    protected void Update()
    {
        if (_damagableObject.HitPoints > _currentThresholdLevel
            || _particleIndex >= Particles.Length)
        {
            return;
        }

        Particles[_particleIndex].enableEmission = true;
        _particleIndex++;
        _currentThresholdLevel -= _thresholdLevelJump;
    }

    #endregion
}
