using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public UIMenu nextMenu;
    public UIMenu backMenu;

    private UIMenuHandler _menuHandler;
    private UIMenuItem[] _menuItems;
    
    public UIMenuItem activeItem;

    private void Start()
    {
        // menu handler
        _menuHandler = FindObjectOfType<UIMenuHandler>();

        #region menuItems: find active and setup noice sliders

        _menuItems = GetComponentsInChildren<UIMenuItem>(includeInactive: true);
        foreach (UIMenuItem item in _menuItems)
        {
            if (item.isMusicNoice)
            {
                item.musicNoice = SaveLoad.Floats[SaveLoad.MUSIC_NOICE];
                item.UpdateVolumeSlider(0f);
            }

            if (item.isSoundNoice)
            {
                item.soundNoice = SaveLoad.Floats[SaveLoad.SOUND_NOICE];
                item.UpdateVolumeSlider(0f);
            }

            if (item.isDefault)
                SetActiveItem(item);
        }

        #endregion
    }

    public void DoSelect()
    {
        if (activeItem.isPlay)
            _menuHandler.SetActiveMenu(nextMenu);

        if (activeItem.isQuit)
            Application.Quit();

        if (activeItem.isDeleteSaveFile)
            SaveLoad.Delete();
    }

    public void DoBack()
    {
        _menuHandler.SetActiveMenu(backMenu);
    }

    public void DoUp()
    {
        SetActiveItem(activeItem.upItem);
    }

    public void DoDown()
    {
        SetActiveItem(activeItem.downItem);
    }

    public void DoLeft()
    {
        #region slider handling

        if (activeItem.isMusicNoice)
        {
            activeItem.UpdateVolumeSlider(-_menuHandler.sliderSpeed);
            return;
        }

        if (activeItem.isSoundNoice)
        {
            activeItem.UpdateVolumeSlider(-_menuHandler.sliderSpeed);
            return;
        }

        #endregion

        SetActiveItem(activeItem.leftItem);
    }

    public void DoRight()
    {
        #region slider handling

        if (activeItem.isMusicNoice)
        {
            activeItem.UpdateVolumeSlider(_menuHandler.sliderSpeed);
            return;
        }

        if (activeItem.isSoundNoice)
        {
            activeItem.UpdateVolumeSlider(_menuHandler.sliderSpeed);
            return;
        }

        #endregion

        SetActiveItem(activeItem.rightItem);
    }

    private void SetActiveItem(UIMenuItem item)
    {
        if (item == null)
            return;

        // clear old item
        if (activeItem != null)
        {
            activeItem.isHighlighted = false;
            activeItem.SetColor(_menuHandler.defaultColor);
        }

        // swap
        activeItem = item;

        // setup new item
        activeItem.isHighlighted = true;
        activeItem.SetColor(_menuHandler.activeColor);
    }
}
