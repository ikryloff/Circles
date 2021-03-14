using System.Collections.Generic;

public class Localization
{
    private static readonly Dictionary<string, string []> local = new Dictionary<string, string []>
    {
        { "NONE", new string[] { "ERROR", "������"} },

        { "info", new string[] { "INFO", "����"} },
        { "elemental", new string[] { "ELEMENTAL MAGIG", "����� �����������"} },
        { "nature", new string[] { "NATURE MAGIG", "����� �������"} },
        { "demonology", new string[] { "DEMONOLOGY", "�����������"} },
        { "necromancy", new string[] { "NECROMANCY", "�����������"} },
        { "defencive", new string[] { "DEFENCIVE MAGIC", "�������� �����"} },

        { "defence", new string[] { "Defence", "������"} },
        { "mana", new string[] { "Mana", "����"} },
        { "exp", new string[] { "Experience", "����"} },
        { "manaPPS", new string[] { "Mana recovery pps", "�����. ���� ��/���"} },
        { "manaPR", new string[] { "Mana return %", "������� ���� %"} },
        { "possibleSpells", new string[] { "Possible spells", "��������� ����"} },

        { "known", new string[] { "Learned spells", "��������� ����"} },
        { "elemental_stat", new string[] { "Elementalistics", "���������������"} },
        { "natural_stat", new string[] { "Natural", "���������"} },
        { "demon_stat", new string[] { "Demonology", "�����������"} },
        { "necro_stat", new string[] { "Necromancy", "�����������"} },
        { "defencive_stat", new string[] { "Defencive", "��������"} },

        { "rock_from_the_sky", new string[] { "ROCK FROM THE SKY", "������ � ����"} },
        { "power_impulse", new string[] { "POWERFUL IMPULSE", "������ �������"} },
        { "thunder_sound", new string[] { "THUNDER SOUND", "������ �����"} },
        { "victims_return", new string[] { "VICTIMS RETURN", "������� ������"} },
        { "ice_icicle", new string[] { "ICE ICICLE", "������� ��������"} },
        { "elemental_power", new string[] { "ELEMENTAL POWER", "���� ������"} },

        { "air_elemental", new string[] { "AIR ELEMENTAL", "��������� ����������"} },
        { "archer_cadaver", new string[] { "ARCHER CADAVER", "������-����"} },
        { "bear_rogue", new string[] { "BEAR ROGUE", "������� �����"} },
        { "come_here", new string[] { "COME HERE", "������� �����"} },
        { "go_away", new string[] { "GO AWAY", "���� �����"} },
        { "ghoul", new string[] { "GHOLE", "�����"} },
        { "hungman", new string[] { "HUNGMAN", "�����"} },
        { "ice_spikes", new string[] { "ICE SPIKES", "������� ����"} },
        { "shield", new string[] { "SHIELD", "���"} },
        { "snake_nest", new string[] { "SNAKE NEST", "������� ������"} },
        { "stone_wall", new string[] { "STONE WALL", "�������� �����"} },
        { "thunder_elemental", new string[] { "THUNDER ELEMENTAL", "���������� �����"} },
        { "wild_wolf", new string[] { "WILD WOLF", "����� ����"} },
        { "wood_wall", new string[] { "WOOD WALL", "���������� �����"} },
        { "demons_trap", new string[] { "DEMONS TRAP", "������������ �������"} },



    };

    public static string GetString( string str )
    {
        if ( local.ContainsKey(str) )
            return local [str] [PlayerStats.GetPlayerLanguage ()];
        else
            return local ["NONE"] [PlayerStats.GetPlayerLanguage ()];
    }

}
