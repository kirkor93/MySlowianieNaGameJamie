using UnityEngine;
using System.Collections;

public class FlameParticlesController : MonoBehaviour
{
    #region Variables
    public ParticleSystem[] Particles;

    private int _particleIndex = 0;
    private IDamagable _damagableObject;
    private float _thresholdLevelJump;
    private float _thresholdMin;
    private float _thresholdMax;
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
        _thresholdMin = _damagableObject.MaxHitPoints - _thresholdLevelJump;
        _thresholdMax = _damagableObject.MaxHitPoints + 0.1f; //just in case
    }

    protected void Update()
    {
        if (_damagableObject.HitPoints < _thresholdMin
            && _particleIndex < Particles.Length)
        {
            Debug.Log(string.Format("Enabled particle: {0}", _particleIndex));
            Particles[_particleIndex].enableEmission = true;
            _particleIndex++;
            _thresholdMin -= _thresholdLevelJump;
            _thresholdMax -= _thresholdLevelJump;
        }
        else
        {
            while (_damagableObject.HitPoints > _thresholdMax
            && _particleIndex > 0)
            {
                _particleIndex--;
                Debug.Log(string.Format("Disabled particle: {0}", _particleIndex));
                Particles[_particleIndex].enableEmission = false;
                _thresholdMin += _thresholdLevelJump;
                _thresholdMax += _thresholdLevelJump;
            }
        }
    }

    #endregion
}
