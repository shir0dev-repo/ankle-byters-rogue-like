using System;
using System.Collections.Generic;
using System.Linq;

public class RoomArgs : EventArgs
{
  public Room Room;
  public RoomInteractionType InteractionType;
  public Door EnteredDoor = null;
}

[Flags]
public enum RoomInteractionType
{
  None = 0,
  Entered = 1,
  Cleared = 2
}

public static class RoomInteractionUtils
{
  public static bool Contains(this RoomInteractionType flag, RoomInteractionType interactionType)
  {
    return flag.HasFlag(interactionType);
  }

}