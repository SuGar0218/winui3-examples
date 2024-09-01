using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MaterialExample;
/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(titleBar);
        Closed += delegate { ViewModel.Dispose(); };
        
        ViewModel = new MainViewModel(this);
    }

    private MainViewModel ViewModel { get; }

    public ComboBox BackdropComboBox { get => backdropComboBox; }
    public ComboBox PresenterKindsComboBox { get => presenterKindsComboBox; }

    public void MakeWholeWindowDraggable(bool draggable) {
        if (draggable) {
            SetTitleBar(container);
        } else {
            SetTitleBar(titleBar);
        }
    }
}

internal class MainViewModel : ObservableObject, IDisposable {
    public MainViewModel(MainWindow window) {
        this.window = window;
        backdropTarget = window.As<ICompositionSupportsSystemBackdrop>();
        // Note: Be sure to have "using WinRT;" to support the window.As<...>() call.

        currentBackdropController = newBackdropControllers[0].Invoke();
        this.window.Activated += delegate (object _, WindowActivatedEventArgs args) {
            if (fallback) {
                currentBackdropController?.SetSystemBackdropConfiguration(new() {
                    IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated
                });
            }
        };

        backdropName = backdropNames[backdropIndex];
    }

    private readonly MainWindow window;
    private ICompositionSupportsSystemBackdrop backdropTarget;

    public static readonly SystemBackdropConfiguration configuration = new() { IsInputActive = true };

    public static readonly Func<ISystemBackdropControllerWithTargets>[] newBackdropControllers = [
        delegate { return new MicaController { Kind = MicaKind.Base }; },
        delegate { return new MicaController { Kind = MicaKind.BaseAlt }; },
        delegate { return new DesktopAcrylicController { Kind = DesktopAcrylicKind.Default }; },
        delegate { return new DesktopAcrylicController { Kind = DesktopAcrylicKind.Base }; },
        delegate { return new DesktopAcrylicController { Kind = DesktopAcrylicKind.Thin }; },
    ];

    public ISystemBackdropControllerWithTargets currentBackdropController;

    public readonly string[] backdropNames = ["Mica", "MicaAlt", "Acrylic", "AcrylicBase", "AcrylicThin"];


    private string backdropName;
    public string BackdropName {
        get => backdropName;
        set => SetProperty(ref backdropName, value, nameof(BackdropName));
    }


    private bool fallback = true;
    public bool Fallback {
        get => fallback;
        set => SetProperty(ref fallback, value, nameof(Fallback));
    }


    private int backdropIndex = 0;
    public int BackdropIndex {
        get => backdropIndex;
        set => SetProperty(ref backdropIndex, value, nameof(BackdropIndex));
    }

    public void ChangeBackdrop() {
        currentBackdropController.RemoveAllSystemBackdropTargets();
        currentBackdropController.Dispose();
        backdropIndex = window.BackdropComboBox.SelectedIndex;
        currentBackdropController = newBackdropControllers[backdropIndex].Invoke();
        currentBackdropController.AddSystemBackdropTarget(backdropTarget);
        currentBackdropController.SetSystemBackdropConfiguration(configuration);

        GetLuminosityOpacity();
        GetTintOpacity();
        GetTintColor();
    }


    private int luminosityOpacity = 60;
    public int LuminosityOpacity {
        get => luminosityOpacity;
        set => SetProperty(ref luminosityOpacity, value, nameof(LuminosityOpacity));
    }

    public void ChangeLuminosityOpacity() {
        if (backdropName.StartsWith("Mica")) {
            (currentBackdropController as MicaController).LuminosityOpacity = (float) luminosityOpacity / 100.0f;
        } else {
            (currentBackdropController as DesktopAcrylicController).LuminosityOpacity = (float)luminosityOpacity / 100.0f;
        }
    }

    public void GetLuminosityOpacity() {
        if (backdropName.StartsWith("Mica")) {
            LuminosityOpacity = (int) ((currentBackdropController as MicaController).LuminosityOpacity * 100.0f);
        } else {
            LuminosityOpacity = (int) ((currentBackdropController as DesktopAcrylicController).LuminosityOpacity * 100.0f);
        }
    }


    private int tintOpacity = 60;
    public int TintOpacity {
        get => tintOpacity;
        set => SetProperty(ref tintOpacity, value, nameof(TintOpacity));
    }

    public void ChangeTintOpacity() {
        if (backdropName.StartsWith("Mica")) {
            (currentBackdropController as MicaController).TintOpacity = (float)tintOpacity / 100.0f;
        } else {
            (currentBackdropController as DesktopAcrylicController).TintOpacity = (float)tintOpacity / 100.0f;
        }
    }

    public void GetTintOpacity() {
        if (backdropName.StartsWith("Mica")) {
            TintOpacity = (int)((currentBackdropController as MicaController).TintOpacity * 100.0f);
        } else {
            TintOpacity = (int)((currentBackdropController as DesktopAcrylicController).TintOpacity * 100.0f);
        }
    }


    private Color tintColor;
    public Color TintColor {
        get => tintColor;
        set => SetProperty(ref tintColor, value, nameof(TintColor));
    }

    public void ChangeTintColor() {
        if (backdropName.StartsWith("Mica")) {
            (currentBackdropController as MicaController).TintColor = tintColor;
        } else {
            (currentBackdropController as DesktopAcrylicController).TintColor = tintColor;
        }
    }

    public void GetTintColor() {
        if (backdropName.StartsWith("Mica")) {
            TintColor = (currentBackdropController as MicaController).TintColor;
        } else {
            TintColor = (currentBackdropController as DesktopAcrylicController).TintColor;
        }
    }


    private bool settingsVisible = true;
    public Visibility SettingsVisiblility {
        get => settingsVisible ? Visibility.Visible : Visibility.Collapsed;
        set => SetProperty(ref settingsVisible, value == Visibility.Visible, nameof(SettingsVisiblility));
    }

    public void ChangeSettingsVisibility() {
        if (settingsVisible) {  // “˛≤ÿ
            SettingsVisiblility = Visibility.Collapsed;
            window.AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;
            window.MakeWholeWindowDraggable(true);
        } else {  // œ‘ æ
            SettingsVisiblility = Visibility.Visible;
            window.AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Standard;
            window.MakeWholeWindowDraggable(false);
        }
    }


    public readonly AppWindowPresenterKind[] presenterKinds = [
        AppWindowPresenterKind.Default,  // 0
        AppWindowPresenterKind.CompactOverlay,  // 1
        AppWindowPresenterKind.FullScreen,  // 2
        AppWindowPresenterKind.Overlapped,  // 3
    ];

    private int presenterKindIndex = 0;
    public int PresenterKindIndex {
        get => presenterKindIndex;
        set => SetProperty(ref presenterKindIndex, value, nameof(PresenterKindIndex));
    }

    public void ChangePresenterKind() {
        PresenterKindIndex = window.PresenterKindsComboBox.SelectedIndex;  // ”√ Û±Íπˆ¬÷«–ªª ±
        window.AppWindow.SetPresenter((AppWindowPresenterKind) PresenterKindIndex /*window.PresenterKindsComboBox.SelectedIndex*/);
    }


    public void Dispose() {
        currentBackdropController.Dispose();
        currentBackdropController = null;
    }
}
