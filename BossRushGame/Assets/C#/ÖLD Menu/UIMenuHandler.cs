using UnityEngine;

public class UIMenuHandler : MonoBehaviour
{
    // menu
    public float sliderSpeed;
    public Color activeColor, defaultColor;
    public UIMenu activeMenu;

    private bool _isInTransition;

    private void Awake()
    {
        if (SaveLoad.FindSaveFile())
            SaveLoad.Load();
        else
            SaveLoad.MakeSaveFile();
    }

    private void Start()
    {
        activeMenu.gameObject.SetActive(true);
    }

    private void Update()
    {
        _isInTransition = false;

        #region inputs

        // TODO: better input

        if (Input.GetKeyDown(KeyCode.Return))
            activeMenu.DoSelect();
        if (Input.GetKeyDown(KeyCode.Escape))
            activeMenu.DoBack();

        if (_isInTransition)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            activeMenu.DoUp();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            activeMenu.DoDown();

        // if slider -> key hold
        if (activeMenu.activeItem.IsSlider())
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                activeMenu.DoLeft();
            if (Input.GetKey(KeyCode.RightArrow))
                activeMenu.DoRight();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                activeMenu.DoLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                activeMenu.DoRight();
        }

        #endregion
    }

    public void SetActiveMenu(UIMenu menu)
    {
        if (menu == null)
            return;

        _isInTransition = true;

        // clear old
        if (activeMenu != null)
        {
            activeMenu.gameObject.SetActive(false);
        }

        // swap
        activeMenu = menu;

        // setup new
        activeMenu.gameObject.SetActive(true);
    }
}