using System.Collections.Generic;

public class Localization
{
    private static readonly Dictionary<string, string []> local = new Dictionary<string, string []>
    {
        { "NONE", new string[] { "ERROR", "ньхайю"} },

        { "info", new string[] { "INFO", "хмтн"} },
        { "elemental", new string[] { "ELEMENTAL MAGIG", "люцхъ щкелемрюкеи"} },
        { "nature", new string[] { "NATURE MAGIG", "люцхъ опхпндш"} },
        { "demonology", new string[] { "DEMONOLOGY", "делнмнкнцхъ"} },
        { "necromancy", new string[] { "NECROMANCY", "мейпнлюмрхъ"} },
        { "defencive", new string[] { "DEFENCIVE MAGIC", "гюыхрмюъ люцхъ"} },

        { "defence", new string[] { "Defence", "гЮЫХРЮ"} },
        { "mana", new string[] { "Mana", "лЮМЮ"} },
        { "exp", new string[] { "Experience", "нОШР"} },
        { "manaPPS", new string[] { "Mana recovery pps", "бНЯЯР. ЛЮМШ НВ/ЯЕЙ"} },
        { "manaPR", new string[] { "Mana return %", "бНГБПЮР ЛЮМШ %"} },
        { "possibleSpells", new string[] { "Possible spells", "дНЯРСОМШЕ ВЮПШ"} },

        { "known", new string[] { "Learned spells", "хГСВЕММШЕ ВЮПШ"} },
        { "elemental_stat", new string[] { "Elementalistics", "еКЕЛЕМРЮКХЯРХЙЮ"} },
        { "natural_stat", new string[] { "Natural", "оПХПНДМЮЪ"} },
        { "demon_stat", new string[] { "Demonology", "дЕЛНМНКНЦХЪ"} },
        { "necro_stat", new string[] { "Necromancy", "мЕЙПНЛЮМРХЪ"} },
        { "defencive_stat", new string[] { "Defencive", "гЮЫХРМЮЪ"} },

        { "rock_from_the_sky", new string[] { "ROCK FROM THE SKY", "йюлемэ я меаю"} },
        { "power_impulse", new string[] { "POWERFUL IMPULSE", "лнымши хлоскэя"} },
        { "thunder_sound", new string[] { "THUNDER SOUND", "пюяйюр цпнлю"} },
        { "sacrifice", new string[] { "SACRIFICE", "фепрбнопхмньемхе"} },
        { "ice_icicle", new string[] { "ICE ICICLE", "кедъмюъ яняскэйю"} },
        { "elemental_power", new string[] { "ELEMENTAL POWER", "яхкю ярхухх"} },

        { "air_elemental", new string[] { "AIR ELEMENTAL", "бнгдсьмши щкелемрюкэ"} },
        { "archer_cadaver", new string[] { "ARCHER CADAVER", "ксвмхй-рпсо"} },
        { "bear_rogue", new string[] { "BEAR ROGUE", "ледбедэ ьюрсм"} },
        { "come_here", new string[] { "COME HERE", "онднидх акхфе"} },
        { "go_away", new string[] { "GO AWAY", "сидх опнвэ"} },
        { "ghoul", new string[] { "GHOLE", "сошпэ"} },
        { "hungman", new string[] { "HUNGMAN", "оюкюв"} },
        { "ice_spikes", new string[] { "ICE SPIKES", "кедъмше ьхош"} },
        { "shield", new string[] { "SHIELD", "ыхр"} },
        { "snake_nest", new string[] { "SNAKE NEST", "глехмне цмегдн"} },
        { "stone_wall", new string[] { "STONE WALL", "йюлеммюъ яремю"} },
        { "thunder_elemental", new string[] { "THUNDER ELEMENTAL", "щкелемрюкэ цпнлю"} },
        { "wild_wolf", new string[] { "WILD WOLF", "дхйхи бнкй"} },
        { "wolf_pack", new string[] { "WOLF PACK", "ярюъ бнкйнб"} },
        { "wood_wall", new string[] { "WOOD WALL", "депебъммши гюанп"} },
        { "demons_trap", new string[] { "DEMONS TRAP", "делнмхвеяйюъ кнбсьйю"} },
        { "pharaohs_mantrap", new string[] { "PHARAOHS MANTRAP", "йюойюм тюпюнмнб"} },
        { "encouragement", new string[] { "ENCOURAGEMENT", "онныпемхе"} },
        { "suppression", new string[] { "SUPPRESSION", "сялхпемхе"} },
        { "deadmans_skull", new string[] { "DEADMANS SKULL", "вепео лепрбежю"} },
        { "giant_spider", new string[] { "GIANT SPIDER", "цхцюмряйхи оюсй"} },
        { "skeleton", new string[] { "SKELETON", "яйекер"} },




    };

    public static string GetString( string str )
    {
        if ( local.ContainsKey(str) )
            return local [str] [PlayerStats.GetPlayerLanguage ()];
        else
            return local ["NONE"] [PlayerStats.GetPlayerLanguage ()];
    }

}
