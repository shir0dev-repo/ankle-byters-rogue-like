using System;
using System.Collections.Generic;
using System.Linq;

public class RoomArgs : EventArgs
{
  public Room Room;
  public RoomInteractionType InteractionType;
}

[Flags]
public enum RoomInteractionType { Entered, Cleared }

public static class RoomInteractionUtils
{
  public static bool Contains(this RoomInteractionType flag, RoomInteractionType interactionType)
  {
    return flag.HasFlag(interactionType);
  }

}