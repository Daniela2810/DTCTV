using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationAxis
{
    X,
    Y,
    Z
}

public enum Difficulty
{
    VeryEasy,
    Easy,
    Medium,
    Hard,
    VeryHard,
    Impossible
}

public enum MovementDirection
{
    Up,
    Down,
    Left,
    Right,
    Forward,
    Backward,
    DiagonalUpLeft,
    DiagonalUpRight,
    DiagonalDownLeft,
    DiagonalDownRight
}

public enum InventoryObject
{
    None,
    Lockpick,  // Ganzúa
    BedroomKey,  // Llave del dormitorio
    BusKey  // Llave del bus
}

public enum colorValue
{
    Black,
    White,
    Green,
    Red,
    Blue,
    Yellow
}

public enum Mode
{
    OutlineAll,
    OutlineVisible,
    OutlineHidden,
    OutlineAndSilhouette,
    SilhouetteOnly
}

public enum DetectiveAbility
{
    None,
    Charisma,
    Investigation,
    Tools
}
