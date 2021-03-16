using System.Collections.Generic;

public static class EnemyProperties
{
   
    private static readonly Dictionary<string, float []> properties = new Dictionary<string, float []>
    {
       // { "name",     new float[] { HP, AR,  FD, DMG, SPD,  XP, isRanger } };

        { "peasant",        new float[] {  80, 1.1f, 3,  5, 0.11f,  15,  0 } },
        { "vigilante",      new float[] { 100, 1.1f, 2, 10, 0.14f,  25,  0 } },
        { "archer",         new float[] {  85,   4f, 3,  5, 0.11f,  35,  1 } },
        { "spearman",       new float[] { 130,   2f, 4, 10, 0.14f,  50,  1 } },
        { "swordsman",      new float[] { 200, 1.1f, 3, 25, 0.11f,  70,  0 } },
        { "horseman",       new float[] { 250, 1.1f, 4, 30,  0.3f, 110,  0 } },

    };

    private static readonly Dictionary<string, string> rangerBullets = new Dictionary<string, string>
    {
        { "archer",    "arrow" },
        { "spearman",  "spear" },
    };


    public static float GetHP(string creep)
    {
        return properties [creep] [0];
    }

    public static float GetAttackRange( string creep )
    {
        return properties [creep] [1];
    }

    public static float GetFireDelay( string creep )
    {
        return properties [creep] [2];
    }

    public static float GetDamage( string creep )
    {
        return properties [creep] [3];
    }

    public static float GetSpeed( string creep )
    {
        return properties [creep] [4];
    }

    public static float GetXP( string creep )
    {
        return properties [creep] [5];
    }

    public static bool IsRanger( string creep )
    {
        return properties [creep] [6] > 0;
    }

    public static string GetBulletName( string creep )
    {
        return rangerBullets [creep];
    }
}
