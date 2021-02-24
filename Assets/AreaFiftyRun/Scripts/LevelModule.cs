using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModule : MonoBehaviour
{
    [SerializeField, Tooltip("The modules that can be Instantiated above this one")]
    private List<LevelModule> aboveModules;
    [SerializeField, Tooltip("The modules that can be instantiated to the right of this one")]
    private List<LevelModule> rightModules;
    [SerializeField, Tooltip("after what number of columns this module is able to be instantiated again")]
    private int columnsUntilRepeatAvailable;
    [SerializeField, Tooltip("if true, columnsUntilRepeatAvailable only counts for modules being instantiated on that row")]
    private bool repeatableOnDifferentRows;
    [SerializeField, Tooltip("This module cannot be placed above this height")]
    private int maxHeight;
    [SerializeField, Tooltip("This module cannot be place below this height")]
    private int minHeight;
}
