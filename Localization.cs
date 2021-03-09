using System.Collections.Generic;

public class Localization
{
    private static readonly Dictionary<string, string []> local = new Dictionary<string, string []>
    {
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


    };

    public static string GetString( string str )
    {
        return local [str] [PlayerStats.GetPlayerLanguage ()];
    }

}
