using System;
using System.Collections;
using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject dashCooldown;
        [SerializeField] private GameObject dashCooldownText;
        
        // TODO refactor all this sh*t
        private void OnEnable()
        {
            PlayerEventConfig.OnDashCooldown += HandleOnDashCooldown;
        }
        
        private void OnDisable()
        {
            PlayerEventConfig.OnDashCooldown -= HandleOnDashCooldown;
        }

        private void HandleOnDashCooldown(Guid guid, float duration)
        {
            Image cooldownImage = dashCooldown.GetComponent<Image>();
            cooldownImage.fillAmount = 1.0f;

            TMP_Text cooldownText = dashCooldownText.GetComponent<TMP_Text>();
            cooldownText.text = duration.ToString("N0");
            dashCooldownText.SetActive(true);
            
            StartCoroutine(IncreaseFillAmountOverTime(cooldownImage, cooldownText, duration));
        }
        
        private IEnumerator IncreaseFillAmountOverTime(Image image, TMP_Text text, float duration)
        {
            float elapsedTime = 0.0f;
            
            while (elapsedTime < duration)
            {
                image.fillAmount = Mathf.Lerp(1.0f, 0.0f, elapsedTime / duration);
                text.text = Mathf.Ceil(duration - elapsedTime).ToString("N0");
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            image.fillAmount = 0.0f;
            dashCooldownText.SetActive(false);
        }
    }
}