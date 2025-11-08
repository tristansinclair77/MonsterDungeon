namespace MonsterDungeon.Domain.Entities
{
    /// <summary>
    /// Represents a tile on the game grid
    /// </summary>
    public class Tile
    {
   public int X { get; set; }
        public int Y { get; set; }
   
public bool IsOccupied { get; set; }
        public bool IsWalkable { get; set; }
      
  public TileContent Content { get; set; }

    public Tile()
 {
  IsWalkable = true;
   IsOccupied = false;
    Content = TileContent.Empty;
      }
    }

    public enum TileContent
    {
        Empty,
        Player,
    Enemy,
        Item,
        Block,
        Trap
    }
}
