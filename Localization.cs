using System.Collections.Generic;

public class Localization
{
    private static readonly Dictionary<string, string []> local = new Dictionary<string, string []>
    {
        { "info", new string[] { "INFO", "ИНФО"} },
        { "elemental", new string[] { "ELEMENTAL MAGIG", "МАГИЯ ЭЛЕМЕНТАЛЕЙ"} },
        { "nature", new string[] { "NATURE MAGIG", "МАГИЯ ПРИРОДЫ"} },
        { "demonology", new string[] { "DEMONOLOGY", "ДЕМОНОЛОГИЯ"} },
        { "necromancy", new string[] { "NECROMANCY", "НЕКРОМАНТИЯ"} },
        { "defencive", new string[] { "DEFENCIVE MAGIC", "ЗАЩИТНАЯ МАГИЯ"} },

        { "defence", new string[] { "Defence", "Защита"} },
        { "mana", new string[] { "Mana", "Мана"} },
        { "exp", new string[] { "Experience", "Опыт"} },
        { "manaPPS", new string[] { "Mana recovery pps", "Восст. маны оч/сек"} },
        { "manaPR", new string[] { "Mana return %", "Возврат маны %"} },
        { "possibleSpells", new string[] { "Possible spells", "Доступные чары"} },

        { "known", new string[] { "Learned spells", "Изученные чары"} },
        { "elemental_stat", new string[] { "Elementalistics", "Елементалистика"} },
        { "natural_stat", new string[] { "Natural", "Природная"} },
        { "demon_stat", new string[] { "Demonology", "Демонология"} },
        { "necro_stat", new string[] { "Necromancy", "Некромантия"} },
        { "defencive_stat", new string[] { "Defencive", "Защитная"} },


    };

    public static string GetString( string str )
    {
        return local [str] [PlayerStats.GetPlayerLanguage ()];
    }

}
