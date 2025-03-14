using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image fillImage;
    public PlayerData dataPlayer;
    public Gradient lifeColorGradient;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float lifeRatio = (float)dataPlayer.currentHealth / (float)dataPlayer.maxHealth;
        fillImage.fillAmount = lifeRatio;
        fillImage.color = lifeColorGradient.Evaluate(lifeRatio);
    }
}