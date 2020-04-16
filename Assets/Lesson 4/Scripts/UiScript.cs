using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class UiScript : MonoBehaviour
{
    public PlayerData playerData;
    public Canvas canvas;
    public Volume volume;
    public Animator animator;

    public TextMeshProUGUI killCountText;

    public float dofFocalLength;
    public float vigIntensity;

    DepthOfField dof;
    Vignette vig;

    void Start()
    {

        canvas.enabled = false;

        DepthOfField tempDof;
        if (volume.profile.TryGet<DepthOfField>(out tempDof)) {
            dof = tempDof;
        }

        Vignette tempVig;
        if (volume.profile.TryGet<Vignette>(out tempVig)) {
            vig = tempVig;
        } 
    }

    void Update()
    {
        dof.focalLength.value = dofFocalLength;
        vig.intensity.value = vigIntensity;
    }

    void LateUpdate()
    {
        if (playerData.Dead()) {
            animator.SetTrigger("Dead");
            canvas.enabled = true;

            if (playerData.killCount == 1) {
                killCountText.text = "but took " + playerData.killCount.ToString() + " zombie with you.";
            } else if (playerData.killCount == 0) {
                killCountText.text = "and didn't kill any zombies. You suck.";
            } else {
                killCountText.text = "but took " + playerData.killCount.ToString() + " zombies with you.";
            }
            
        }
    }
}
