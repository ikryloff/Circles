using System.Linq;

public static class PlayerStats
{
    private static int playerLanguage = 1; // 0 - english, 1 - russian
    private static int playerLevel;
    private static int playerMP = 300;
    private static int playerManaBonus = 1;
    private static float playerManaReturn = 0.3f;

    private static int [] playerSpellsIDList = 
    {
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0 
    };

    public static int [] GetPlayerSpellsIDList()
    {
        return playerSpellsIDList;
    }

    public static bool IsSpellInPlayerSpellsIDList( int id)
    {
        return playerSpellsIDList [id] == 1;
    }

    public static void AddSpellToPlayerSpellsIDList (int id)
    {        
        playerSpellsIDList [id] = 1;
    }

    public static int GetPlayerSpellsQuantity()
    {     
        return playerSpellsIDList.Sum ();
    }

    public static int GetPlayerMP()
    {
        return playerMP;
    }

    public static int GetPlayerLevel()
    {
        return playerLevel;
    }

    public static void SetPlayerLevel( int level )
    {
        playerLevel = level;
    }

    public static void SetPlayerLanguage( int lang )
    {
        playerLanguage = lang;
    }

    public static int GetPlayerLanguage( )
    {
        return playerLanguage;
    }

    public static int GetPlayerManaBonus()
    {
        return playerManaBonus;
    }

    public static void SetPlayerManaBonus( int bonus )
    {
        playerManaBonus = bonus;
    }

    public static float GetPlayerManaReturn()
    {
        return playerManaReturn;
    }
}
