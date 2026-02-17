using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using UnityEngine;

public class SlotEffectsController : MonoBehaviourExtBind
{
    [Header("Particles")] 
    [SerializeField] private ParticleSystem _reelStoppedParticles; 
    [SerializeField] private ParticleSystem _reelStopingParticles; 
    [SerializeField] private ParticleSystem _startActiveParticles;
    [SerializeField] private ParticleSystem _stopActiveParticles;

    [Header("Settings")] 
    [SerializeField] private float  _reelStoppedParticlesDelay = 0.2f;

    [Bind("StartReelSpin")]
    public void OnStartSpin() => DeactivateParticles(_reelStoppedParticles);

    [Bind("OnReelStoping")]
    public void OnReelStoping() => ActivateParticles(_reelStopingParticles);
    
    [Bind("OnReelStopped")]
    public void OnStopSpin()
    {
        DeactivateParticles(_reelStopingParticles);
        
        Path = new CPath()
            .Wait( _reelStoppedParticlesDelay)
            .Action(() => { ActivateParticles(_reelStoppedParticles); });
    } 

    [Bind("OnCanStopClick")]
    private void OnStopClick() => DeactivateParticles(_stopActiveParticles);
    
    [Bind("OnCanStartClick")]
    private void OnStarClick() => DeactivateParticles(_startActiveParticles);

    [Bind("OnCanStartActive")]
    private void OnStartActive()
    {
        DeactivateParticles(_stopActiveParticles);
        ActivateParticles(_startActiveParticles);
    }

    [Bind("OnCanStopActive")]
    private void OnStopActive()
    {
        DeactivateParticles(_startActiveParticles);
        ActivateParticles(_stopActiveParticles);
    }

    private void ActivateParticles(ParticleSystem particles)
    {
        if (particles != null)
            particles.Play();
    }

    private void DeactivateParticles(ParticleSystem particles)
    {
        if (particles != null)
            particles.Stop();
    }
}