using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Configuration des étapes")]
    public TMP_Text tutorialText;         // Le texte qui change
    public Button nextButton;             // Le bouton Suivant
    public Button previousButton;         // Le bouton Précédent
    public Button startButton;            // Le bouton Commencer le jeu
    public Button menuButton;             // Le bouton Retour au menu

    [Header("Flèches de guidage")]
    public GameObject arrowRight;
    public GameObject arrowUp;
    public GameObject arrowDown;

    [Header("Images des touches")]
    public GameObject spaceKeyImage;

    // Liste des textes à afficher
    private string[] steps = new string[]
    {
        "Bienvenue dans le tutoriel !",
        "Utilise les flèches pour te déplacer.",
        "Appuie sur [Espace] pour sauter.",
        "Appuie sur [F] pour attaquer.",
        "Évite les obstacles pour survivre."
    };

    // Liste des flèches et images activées à chaque étape
    private GameObject[][] arrowSteps;

    private int currentStep = 0;

    void Start()
    {
        // Initialisation des flèches et images par étape
        arrowSteps = new GameObject[][]
        {
            new GameObject[] {},                     // Étape 0 : Aucune flèche
            new GameObject[] { arrowRight },         // Étape 1 : Flèche de déplacement
            new GameObject[] { arrowUp, spaceKeyImage }, // Étape 2 : Flèche de saut + image de touche Espace
            new GameObject[] { arrowDown },          // Étape 3 : Flèche d'attaque
            new GameObject[] {}                      // Étape 4 : Aucune flèche
        };

        // On affiche le premier texte
        UpdateText();
        
        // Bouton Précédent désactivé au début
        previousButton.interactable = false;
        startButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(true);
        spaceKeyImage.SetActive(false); // On désactive l'image au début
    }

    public void NextStep()
    {
        if (currentStep < steps.Length - 1)
        {
            currentStep++;
            UpdateText();
        }

        // Gestion des boutons
        previousButton.interactable = (currentStep > 0);
        nextButton.interactable = (currentStep < steps.Length - 1);

        // Si on est à la dernière étape, on montre les boutons finaux
        if (currentStep == steps.Length - 1)
        {
            startButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
        }
    }

    public void PreviousStep()
    {
        if (currentStep > 0)
        {
            currentStep--;
            UpdateText();
        }

        // Gestion des boutons
        previousButton.interactable = (currentStep > 0);
        nextButton.interactable = (currentStep < steps.Length - 1);

        // Si on revient en arrière, on cache les boutons finaux
        if (currentStep < steps.Length - 1)
        {
            startButton.gameObject.SetActive(false);
        }
    }

    void UpdateText()
    {
        tutorialText.text = steps[currentStep];

        // Désactiver toutes les flèches et images
        arrowRight.SetActive(false);
        arrowUp.SetActive(false);
        arrowDown.SetActive(false);
        spaceKeyImage.SetActive(false);

        // Activer les flèches et images nécessaires pour l'étape actuelle
        foreach (GameObject arrow in arrowSteps[currentStep])
        {
            arrow.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1"); // Remplace "MainLevel" par le nom exact de ta scène de jeu
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Remplace "MainMenu" par le nom exact de ta scène de menu
    }
}
