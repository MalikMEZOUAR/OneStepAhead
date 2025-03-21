using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Charge la première scène 
        SceneManager.LoadScene("Level1");
    }

    public void ShowCredits()
    {
        // Charge la scène des crédits 
        SceneManager.LoadScene("CreditScene");
    }

    public void QuitGame()
    {
        // Quitte le jeu (ne fonctionne pas en mode Play dans l'éditeur)
        Application.Quit();
        Debug.Log("Quitter le jeu"); // Visible dans la Console pour vérifier que le bouton fonctionne
    }
}
