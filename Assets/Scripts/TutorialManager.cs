using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class TutorialManager : MonoBehaviour
{
    [Header("Configuration des étapes")]
    public TMP_Text tutorialText;
    public Button nextButton;
    public Button startButton;
    public Button menuButton;

    [Header("Flèches de guidage")]
    public GameObject arrowRight;
    public GameObject arrowLeft;

    [Header("Images des touches")]
    public GameObject spaceKeyImage;

    // Liste des textes à afficher
    private string[] steps = new string[]
    {
        "Bienvenue dans le tutoriel !",
        "Utilise la flèche droite pour avancer.",
        "Utilise la flèche gauche pour reculer.",
        "Appuie sur [Espace] pour sauter.",
        "Prêt à jouer ?"
    };

    // Liste des flèches et images activées à chaque étape
    private GameObject[][] arrowSteps;
    private int currentStep = 0;

    void Start()
    {
        // Vérification des références
        if (!CheckReferences()) return;

        // Initialisation des flèches et images par étape
        arrowSteps = new GameObject[][]
        {
            new GameObject[] {},                   // Étape 0 : Rien à montrer
            new GameObject[] { arrowRight },        // Étape 1 : Flèche droite
            new GameObject[] { arrowLeft },         // Étape 2 : Flèche gauche
            new GameObject[] { spaceKeyImage },     // Étape 3 : Barre d'espace
            new GameObject[] {}                     // Étape 4 : Aucune flèche (écran de fin)
        };

        // Initialisation de l'interface
        UpdateText();
        startButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(true);
        DeactivateAllArrows();

        // Désactiver la navigation clavier sur les boutons
        DisableButtonNavigation();

        // Retirer la sélection UI pour éviter les triggers avec "Espace"
        EventSystem.current.SetSelectedGameObject(null);

        // Supprimer le bouton Précédent s'il est présent
        if (nextButton != null)
        {
            nextButton.interactable = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventSystem.current.SetSelectedGameObject(null); 
        }
    }

    public void NextStep()
    {
        if (currentStep < steps.Length - 1)
        {
            currentStep++;
            UpdateText();
        }

        nextButton.interactable = (currentStep < steps.Length - 1);

        if (currentStep == steps.Length - 1)
        {
            startButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false); // On cache le bouton suivant à la fin
        }
        else
        {
            startButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(true);
        }
    }

    void UpdateText()
    {
        if (tutorialText != null)
        {
            tutorialText.text = steps[currentStep];
        }

        DeactivateAllArrows();

        foreach (GameObject arrow in arrowSteps[currentStep])
        {
            if (arrow != null) arrow.SetActive(true);
        }
    }

    void DeactivateAllArrows()
    {
        arrowRight?.SetActive(false);
        arrowLeft?.SetActive(false);
        spaceKeyImage?.SetActive(false);
    }

    bool CheckReferences()
    {
        if (tutorialText == null || nextButton == null || startButton == null ||
            menuButton == null || arrowRight == null || arrowLeft == null || spaceKeyImage == null)
        {
            Debug.LogError("Une ou plusieurs références ne sont pas assignées dans l'inspecteur !");
            return false;
        }
        return true;
    }

    void DisableButtonNavigation()
    {
        Navigation noNav = new Navigation { mode = Navigation.Mode.None };
        nextButton.navigation = noNav;
        startButton.navigation = noNav;
        menuButton.navigation = noNav;
    }

    public void StartGame()
    {
        Debug.Log("Chargement de la scène Level1...");
        SceneManager.LoadScene("Scenes/Level1");
    }

    public void ReturnToMenu()
    {
        Debug.Log("Retour au menu principal...");
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
