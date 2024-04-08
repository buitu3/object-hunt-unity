using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Sidebarexample : MonoBehaviour
{
    [TitleGroup("Sidebar", Alignment = TitleAlignments.Centered)]
    [VerticalGroup("Sidebar/Buttons")]
    [Button(ButtonSizes.Large)]
    private void FirstOption()
    {
        Debug.Log("First option clicked");
    }

    [VerticalGroup("Sidebar/Buttons")]
    [Button(ButtonSizes.Large)]
    private void SecondOption()
    {
        Debug.Log("Second option clicked");
    }

    [VerticalGroup("Sidebar/Buttons")]
    [Button(ButtonSizes.Large)]
    private void ThirdOption()
    {
        Debug.Log("Third option clicked");
    }

    // Other properties and methods can be placed outside the sidebar group
    // to appear in the main panel of the inspector
}
