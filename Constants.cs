using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants 
{
    // Numbers
    public static float CELL_WIDTH = 1.35f;
    public static float CELL_HEIGHT = 1.125f;
    public static float TRAP_DIST = 0.3f;
    public static float PATH_LENGHT = 17f;
    public static float PATH_START_X = 10.4f;

    public static float TOWER_HEAL_POINTS = 30f;
    //Strings
    public static string ANIM_ENEMY_HIT_FORWARD = "enemy_hit_forward";
    public static string ANIM_ENEMY_HIT_BACK = "enemy_hit_back";
    public static string ANIM_ENEMY_FIGHT_STATE_FORWARD =   "isInFight_Forward";
    public static string ANIM_ENEMY_FIGHT_STATE_BACK =      "isInFight_Back";
    public static string ANIM_ENEMY_ATTACK_BACK =           "enemy_attack_back";
    public static string ANIM_ENEMY_ATTACK_FORWARD =        "enemy_attack_forward";
   

    public static string ENEMY_TYPE_PEASANT =                "peasant";
    public static string ENEMY_TYPE_VIGILANTE =              "vigilante";

    public static string ANIM_TOWER_HIT_BACK =                  "tower_hit_back";
    public static string ANIM_TOWER_HIT_FORWARD =               "tower_hit_forward";
    public static string ANIM_TOWER_ATTACK_FORWARD =            "tower_attack_forward";
    public static string ANIM_TOWER_ATTACK_BACK =               "tower_attack_back";
    public static string ANIM_TOWER_STAND_FORWARD =             "isTargetForward";
    

    public static float ANIM_ATTACK_TIME = 0.15f;
    public static float ANIM_DEATH_TIME = 1f;

    public static string BULLET_BULLET = "bullet";
    public static string BULLET_ARROW = "arrow";
    public static string BULLET_AE_BULLET = "ae-bullet";
    public static string BLOOD_IMPACT = "bloodImpact";
    public static string CREEP_DEATH = "creepDeath";

    // spell targets
    public static string SPELL_TARGET_RANDOM_IN_LINE = "RIL";
    public static string SPELL_TARGET_NEAREST_IN_LINE = "NIL";
    public static string SPELL_TARGET_ALL = "ALL";

    //special spells
    public static string TOWER_CODE_SHIELD = "1212";
    public static string TOWER_CODE_GO_AWAY = "110211020012";
    public static string TOWER_CODE_COME_HERE = "010211020112";
    public static string SPELL_CODE_SACRIFICE = "110210120112";
    public static string SPELL_CODE_ENCOURAGEMENT = "110210020112";
    public static string SPELL_CODE_SUPPRESSION = "102112012";

    //Schools
    public static string NATURE = "NATURE MAGIC";
    public static string ELEMENTAL = "ELEMENTAL MAGIC";
    public static string DEFENSIVE = "DEFENSIVE MAGIC";
    public static string NECROMANCY = "NECROMANCY";
    public static string DEMONOLOGY = "DEMONOLOGY";
    public static string INFO = "INFORMATION";

}
