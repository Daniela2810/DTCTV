using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPageManager : MonoBehaviour
{
    [SerializeField] private List<SkillPage> skillPages; // Lista de páginas de habilidades
    [SerializeField] private List<Sprite> bookCovers; // Portadas de libros
    [SerializeField] private Image bookCoverImage; // Imagen de la portada actual
    [SerializeField] private Image iconImage; // Imagen del icono de habilidad (faltaba esta definición)
    [SerializeField] private GameObject bookUI; // UI que muestra el contenido del libro
    [SerializeField] private TMP_Text narrativeText; // Texto narrativo de la habilidad
    [SerializeField] private TMP_Text skillCostText; // Costo de la habilidad
    [SerializeField] private Button nextButton; // Botón para la siguiente portada
    [SerializeField] private Button previousButton; // Botón para la portada anterior
    [SerializeField] private Button purchaseButton; // Botón para comprar habilidad

    private int currentBookIndex = 0; // Índice del libro actual
    private int[] skillLevels; // Niveles actuales por cada libro

    private void Start()
    {
        skillLevels = new int[skillPages.Count];
        UpdateBookCover();
    }

    public void NextBook()
    {
        if (currentBookIndex < bookCovers.Count - 1)
        {
            currentBookIndex++;
            UpdateBookCover();
        }
    }

    public void PreviousBook()
    {
        if (currentBookIndex > 0)
        {
            currentBookIndex--;
            UpdateBookCover();
        }
    }

    private void UpdateBookCover()
    {
        if (currentBookIndex >= 0 && currentBookIndex < bookCovers.Count)
        {
            bookCoverImage.sprite = bookCovers[currentBookIndex];
            UpdateSkillPageUI();
        }
        nextButton.interactable = currentBookIndex < bookCovers.Count - 1;
        previousButton.interactable = currentBookIndex > 0;
    }

    private void UpdateSkillPageUI()
    {
        SkillPage currentSkillPage = skillPages[currentBookIndex];
        int currentLevel = skillLevels[currentBookIndex];
        if (currentLevel < currentSkillPage.icons.Count)
        {
            narrativeText.text = currentSkillPage.narrativeTexts[currentLevel];
            skillCostText.text = "Costo: " + currentSkillPage.GetSkillCost(currentLevel);
            iconImage.sprite = currentSkillPage.icons[currentLevel];
            purchaseButton.interactable = SkillManager.Instance.SkillPoints >= currentSkillPage.GetSkillCost(currentLevel);
        }
    }

    public void OpenBook()
    {
        bookUI.SetActive(true);
    }

    public void PurchaseSkill()
    {
        int currentLevel = skillLevels[currentBookIndex];
        SkillPage currentSkillPage = skillPages[currentBookIndex];
        int cost = currentSkillPage.GetSkillCost(currentLevel);

        if (SkillManager.Instance.SkillPoints >= cost && currentLevel < currentSkillPage.icons.Count)
        {
            SkillManager.Instance.SkillPointDecrease(cost);
            skillLevels[currentBookIndex]++;
            UpdateSkillPageUI();
        }
    }
}
