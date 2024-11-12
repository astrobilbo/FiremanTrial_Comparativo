using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.UI
{
    public class CellphoneBackgroundChanger : MonoBehaviour
    {
        [SerializeField]private List<CellphoneBackgroundImages> cellphoneBackgrounds = new List<CellphoneBackgroundImages>();
        [SerializeField] private Image backgroundImageShower;
        [SerializeField]private int backgroundActiveImageIndex = 0;
        [SerializeField]private Cellphone cellphone;
        [SerializeField] private Button nextImageToShow;
        [SerializeField] private Button previousImageToShow;
        [SerializeField] private Button selectImage;
        private const string BackgroundImageKey = "BackgroundImage";
        private int _backgroundNextImageIndex = 0;
        private void Start()
        {
            backgroundActiveImageIndex=PlayerPrefs.GetInt(BackgroundImageKey, backgroundActiveImageIndex);
            _backgroundNextImageIndex=backgroundActiveImageIndex;
            UpdateImageDisplay();
            SetPhoneBackground();

            nextImageToShow.onClick.AddListener(NextImage);
            previousImageToShow.onClick.AddListener(PreviousImage);
            selectImage.onClick.AddListener(SelectImage);
        }

        private void SetPhoneBackground()
        {
            var sprite = GetSpriteFromList(backgroundActiveImageIndex);
            var color = GetColorFromList(backgroundActiveImageIndex);
            cellphone.ChangeBackgroundImage(sprite,color);
        }
        
        private Sprite GetSpriteFromList(int imageIndex)
        {
            return cellphoneBackgrounds[imageIndex].backgroundSprite;
        }
        
        private Color GetColorFromList(int imageIndex)
        {
            return cellphoneBackgrounds[imageIndex].backgroundColor;
        }

        private void NextImage()
        {
            _backgroundNextImageIndex++;
            if (_backgroundNextImageIndex >= cellphoneBackgrounds.Count)
            {
                _backgroundNextImageIndex = 0;
            }
            UpdateImageDisplay();
        }

        private void PreviousImage()
        {
            _backgroundNextImageIndex--;
            if (_backgroundNextImageIndex < 0)
            {
                _backgroundNextImageIndex = cellphoneBackgrounds.Count - 1; // Loop to the last image
            }
            UpdateImageDisplay();
        }

        private void UpdateImageDisplay()
        {
            backgroundImageShower.sprite = GetSpriteFromList(_backgroundNextImageIndex);
            backgroundImageShower.color = GetColorFromList(_backgroundNextImageIndex);
        }
        private void SelectImage()
        {
            backgroundActiveImageIndex=_backgroundNextImageIndex;
            PlayerPrefs.SetInt(BackgroundImageKey, backgroundActiveImageIndex);
            PlayerPrefs.Save();
            SetPhoneBackground();
        }
        
    }
}
